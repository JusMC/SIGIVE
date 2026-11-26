using SIGIVE.DAL;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.BLL
{
    /// <summary>
    ///     Capa de negocio de ventas: arma la venta (agregar/quitar productos),
    ///     calcula el total y la manda a guardar.
    /// </summary>
    public class VentaBLL
    {
        private VentaDAO ventaDAO;

        public VentaBLL()
        {
            ventaDAO = new VentaDAO();
        }

        /// <summary>
        ///     Agrega un producto a la venta que se está armando.
        ///     Si el producto ya estaba, le suma la cantidad.
        ///     OJO: aquí NO se descuenta el stock; eso pasa al registrar la venta.
        /// </summary>
        public void AgregarProducto(VentaDTO venta, ProductoDTO? producto, int cantidad)
        {
            if (producto == null)
            {
                throw new ValidacionException("Seleccione un producto.");
            }

            if (cantidad <= 0)
            {
                throw new ValidacionException("La cantidad debe ser mayor a 0.");
            }

            // LINQ: buscar si el producto ya está en la venta
            DetalleVentaDTO? existente = venta.Detalles.FirstOrDefault(d => d.ProductoCodigo == producto.Codigo);

            int cantidadTotal = cantidad;
            if (existente != null)
            {
                cantidadTotal = existente.Cantidad + cantidad;
            }

            if (cantidadTotal > producto.Stock)
            {
                throw new StockInsuficienteException("Solo hay " + producto.Stock + " unidades de " + producto.Nombre + ".");
            }

            if (existente != null)
            {
                // Ya estaba: solo se actualiza la cantidad
                existente.Cantidad = cantidadTotal;
                existente.Subtotal = existente.Cantidad * existente.PrecioUnitario;
            }
            else
            {
                // Es nuevo: se crea la línea de detalle
                DetalleVentaDTO detalle = new DetalleVentaDTO();
                detalle.ProductoCodigo = producto.Codigo;
                detalle.ProductoNombre = producto.Nombre;
                detalle.Cantidad = cantidad;
                detalle.PrecioUnitario = producto.Precio;
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                venta.Detalles.Add(detalle);
            }

            venta.Total = CalcularTotal(venta);
        }

        /// <summary>
        ///     Quita un producto de la venta que se está armando.
        /// </summary>
        public void QuitarProducto(VentaDTO venta, string codigoProducto)
        {
            DetalleVentaDTO? detalle = venta.Detalles.FirstOrDefault(d => d.ProductoCodigo == codigoProducto);

            if (detalle == null)
            {
                throw new ValidacionException("Seleccione un producto del detalle.");
            }

            venta.Detalles.Remove(detalle);
            venta.Total = CalcularTotal(venta);
        }

        /// <summary>
        ///     LINQ: suma los subtotales de todas las líneas.
        /// </summary>
        public decimal CalcularTotal(VentaDTO venta)
        {
            return venta.Detalles.Sum(d => d.Subtotal);
        }

        /// <summary>
        ///     Valida y guarda la venta. El DAO descuenta el stock en la base de datos.
        /// </summary>
        public void Registrar(VentaDTO venta)
        {
            if (venta.ClienteId <= 0)
            {
                throw new ValidacionException("Seleccione un cliente.");
            }

            if (venta.Detalles.Count == 0)
            {
                throw new ValidacionException("Agregue al menos un producto a la venta.");
            }

            venta.Fecha = DateTime.Now;
            venta.Total = CalcularTotal(venta);

            ventaDAO.Registrar(venta);
        }

        /// <summary>
        ///     Devuelve todas las ventas registradas.
        /// </summary>
        public List<VentaDTO> ObtenerTodas()
        {
            return ventaDAO.ObtenerTodas();
        }

        /// <summary>
        ///     Devuelve los productos de una venta.
        /// </summary>
        public List<DetalleVentaDTO> ObtenerDetalles(int ventaId)
        {
            return ventaDAO.ObtenerDetalles(ventaId);
        }

        /// <summary>
        ///     LINQ: suma el total de todas las ventas (útil para los reportes).
        /// </summary>
        public decimal CalcularTotalVendido()
        {
            return ventaDAO.ObtenerTodas().Sum(v => v.Total);
        }

        /// <summary>
        ///     LINQ: devuelve las ventas de un cliente (útil para los reportes).
        /// </summary>
        public List<VentaDTO> ObtenerPorCliente(int clienteId)
        {
            return ventaDAO.ObtenerTodas()
                .Where(v => v.ClienteId == clienteId)
                .ToList();
        }
    }
}
