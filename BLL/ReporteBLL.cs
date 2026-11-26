using SIGIVE.DAL;
using SIGIVE.DAL.Interfaces;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.BLL
{
    /// <summary>
    ///     Capa de negocio de reportes: filtra, agrupa y resume los datos con LINQ
    ///     y diccionarios. No guarda nada, solo consulta.
    /// </summary>
    public class ReporteBLL
    {
        private IVentaRepositorio ventaRepositorio;
        private IProductoRepositorio productoRepositorio;
        private IClienteRepositorio clienteRepositorio;

        /// <summary>
        ///     Constructor normal: pide los repositorios a la fábrica (patrón Factory).
        /// </summary>
        public ReporteBLL() : this(FabricaRepositorios.CrearVentaRepositorio(),
                                   FabricaRepositorios.CrearProductoRepositorio(),
                                   FabricaRepositorios.CrearClienteRepositorio())
        {
        }

        /// <summary>
        ///     Constructor que recibe los repositorios ya creados (para pruebas unitarias).
        /// </summary>
        public ReporteBLL(IVentaRepositorio ventaRepositorio,
                          IProductoRepositorio productoRepositorio,
                          IClienteRepositorio clienteRepositorio)
        {
            this.ventaRepositorio = ventaRepositorio;
            this.productoRepositorio = productoRepositorio;
            this.clienteRepositorio = clienteRepositorio;
        }

        /// <summary>
        ///     LINQ: ventas entre dos fechas (incluidas). Si clienteId es 0 trae las de todos los clientes.
        /// </summary>
        public List<VentaDTO> ObtenerVentas(DateTime desde, DateTime hasta, int clienteId = 0)
        {
            ValidarRango(desde, hasta);

            return ventaRepositorio.ObtenerTodas()
                .Where(v => v.Fecha.Date >= desde.Date && v.Fecha.Date <= hasta.Date)
                .Where(v => clienteId == 0 || v.ClienteId == clienteId)
                .OrderByDescending(v => v.Fecha)
                .ToList();
        }

        /// <summary>
        ///     Devuelve los productos de una venta (para ver el detalle en el reporte).
        /// </summary>
        public List<DetalleVentaDTO> ObtenerDetalleVenta(int ventaId)
        {
            return ventaRepositorio.ObtenerDetalles(ventaId);
        }

        /// <summary>
        ///     LINQ: inventario de productos. Si soloBajoStock es true, solo trae
        ///     los que tienen stock menor o igual al límite (los que hay que reponer).
        /// </summary>
        public List<ProductoDTO> ObtenerProductos(bool soloBajoStock, int limiteStock)
        {
            if (limiteStock < 0)
            {
                throw new ValidacionException("El límite de stock no puede ser negativo.");
            }

            List<ProductoDTO> productos = productoRepositorio.ObtenerTodos();

            if (soloBajoStock)
            {
                return productos
                    .Where(p => p.Stock <= limiteStock)
                    .OrderBy(p => p.Stock)
                    .ThenBy(p => p.Nombre)
                    .ToList();
            }

            return productos.OrderBy(p => p.Nombre).ToList();
        }

        /// <summary>
        ///     Productos más vendidos en el rango de fechas, de mayor a menor cantidad.
        ///     Usa un Dictionary (código → acumulado) para sumar lo vendido de cada producto.
        /// </summary>
        public List<ProductoVendidoDTO> ObtenerProductosMasVendidos(DateTime desde, DateTime hasta)
        {
            // HashSet: los Id de las ventas del rango (buscar en un HashSet es muy rápido)
            HashSet<int> idsVentas = ObtenerVentas(desde, hasta)
                .Select(v => v.Id)
                .ToHashSet();

            List<DetalleVentaDTO> detalles = ventaRepositorio.ObtenerTodosLosDetalles()
                .Where(d => idsVentas.Contains(d.VentaId))
                .ToList();

            // Diccionario: la llave es el código del producto y el valor es lo que lleva acumulado
            Dictionary<string, ProductoVendidoDTO> acumulado = new Dictionary<string, ProductoVendidoDTO>();

            foreach (DetalleVentaDTO detalle in detalles)
            {
                if (!acumulado.ContainsKey(detalle.ProductoCodigo))
                {
                    ProductoVendidoDTO nuevo = new ProductoVendidoDTO();
                    nuevo.Codigo = detalle.ProductoCodigo;
                    nuevo.Nombre = detalle.ProductoNombre;
                    acumulado.Add(detalle.ProductoCodigo, nuevo);
                }

                ProductoVendidoDTO item = acumulado[detalle.ProductoCodigo];
                item.CantidadVendida += detalle.Cantidad;
                item.TotalVendido += detalle.Subtotal;
            }

            return acumulado.Values
                .OrderByDescending(p => p.CantidadVendida)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        /// <summary>
        ///     LINQ: todos los clientes con cuántas compras hicieron y cuánto gastaron en el rango.
        ///     Incluye a los clientes que no compraron (con 0).
        /// </summary>
        public List<ClienteReporteDTO> ObtenerResumenClientes(DateTime desde, DateTime hasta)
        {
            List<VentaDTO> ventas = ObtenerVentas(desde, hasta);

            return clienteRepositorio.ObtenerTodos()
                .Select(c => new ClienteReporteDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Telefono = c.Telefono,
                    Correo = c.Correo,
                    CantidadCompras = ventas.Count(v => v.ClienteId == c.Id),
                    TotalComprado = ventas.Where(v => v.ClienteId == c.Id).Sum(v => v.Total)
                })
                .OrderByDescending(c => c.TotalComprado)
                .ThenBy(c => c.Nombre)
                .ToList();
        }

        /// <summary>
        ///     LINQ + diccionario: total vendido por día (para el gráfico del dashboard).
        ///     SortedDictionary mantiene las fechas ordenadas de la más vieja a la más nueva.
        /// </summary>
        public SortedDictionary<DateTime, decimal> ObtenerVentasPorDia(DateTime desde, DateTime hasta)
        {
            SortedDictionary<DateTime, decimal> ventasPorDia = new SortedDictionary<DateTime, decimal>();

            IEnumerable<IGrouping<DateTime, VentaDTO>> grupos = ObtenerVentas(desde, hasta)
                .GroupBy(v => v.Fecha.Date);

            foreach (IGrouping<DateTime, VentaDTO> grupo in grupos)
            {
                ventasPorDia.Add(grupo.Key, grupo.Sum(v => v.Total));
            }

            return ventasPorDia;
        }

        /// <summary>
        ///     Números generales del dashboard.
        /// </summary>
        public ResumenVentasDTO ObtenerResumen(DateTime desde, DateTime hasta, int limiteStock)
        {
            List<VentaDTO> ventas = ObtenerVentas(desde, hasta);

            ResumenVentasDTO resumen = new ResumenVentasDTO();
            resumen.CantidadVentas = ventas.Count;
            resumen.TotalVendido = ventas.Sum(v => v.Total);

            // Evitar dividir entre 0 si no hubo ventas
            if (resumen.CantidadVentas > 0)
            {
                resumen.TicketPromedio = resumen.TotalVendido / resumen.CantidadVentas;
            }

            resumen.ProductosBajoStock = ObtenerProductos(true, limiteStock).Count;
            return resumen;
        }

        /// <summary>
        ///     La fecha inicial no puede ser mayor que la final.
        /// </summary>
        private void ValidarRango(DateTime desde, DateTime hasta)
        {
            if (desde.Date > hasta.Date)
            {
                throw new ValidacionException("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.");
            }
        }
    }
}
