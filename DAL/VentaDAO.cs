using MySql.Data.MySqlClient;
using SIGIVE.DAL.Interfaces;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.DAL
{
    /// <summary>
    ///     DAO de ventas: guarda la venta (cabecera + detalles) y descuenta el stock.
    /// </summary>
    public class VentaDAO : IVentaRepositorio
    {
        // Consulta base de los detalles: trae también el nombre del producto
        private const string SelectDetalle =
            "SELECT d.id, d.venta_id, d.producto_codigo, p.nombre AS producto_nombre, " +
            "d.cantidad, d.precio_unitario, d.subtotal " +
            "FROM detalle_venta d INNER JOIN productos p ON p.codigo = d.producto_codigo ";

        /// <summary>
        ///     Guarda la venta completa. Usa una transacción: o se guarda TODO
        ///     (venta, detalles y stock) o no se guarda NADA si algo falla.
        /// </summary>
        public void Registrar(VentaDTO venta)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                MySqlTransaction transaccion = conn.BeginTransaction();

                try
                {
                    // 1. Guardar la cabecera de la venta
                    string queryVenta = "INSERT INTO ventas (fecha, cliente_id, total) VALUES (@fecha, @cliente, @total)";
                    using (MySqlCommand cmd = new MySqlCommand(queryVenta, conn, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@fecha", venta.Fecha);
                        cmd.Parameters.AddWithValue("@cliente", venta.ClienteId);
                        cmd.Parameters.AddWithValue("@total", venta.Total);
                        cmd.ExecuteNonQuery();
                        venta.Id = (int)cmd.LastInsertedId;
                    }

                    // 2. Por cada producto: descontar stock y guardar el detalle
                    foreach (DetalleVentaDTO detalle in venta.Detalles)
                    {
                        // Solo descuenta si todavía hay stock suficiente
                        string queryStock = "UPDATE productos SET stock = stock - @cantidad WHERE codigo = @codigo AND stock >= @cantidad";
                        using (MySqlCommand cmd = new MySqlCommand(queryStock, conn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                            cmd.Parameters.AddWithValue("@codigo", detalle.ProductoCodigo);

                            int filasAfectadas = cmd.ExecuteNonQuery();
                            if (filasAfectadas == 0)
                            {
                                throw new StockInsuficienteException("No hay stock suficiente de " + detalle.ProductoNombre + ".");
                            }
                        }

                        string queryDetalle = "INSERT INTO detalle_venta (venta_id, producto_codigo, cantidad, precio_unitario) " +
                                              "VALUES (@venta, @codigo, @cantidad, @precio)";
                        using (MySqlCommand cmd = new MySqlCommand(queryDetalle, conn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@venta", venta.Id);
                            cmd.Parameters.AddWithValue("@codigo", detalle.ProductoCodigo);
                            cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                            cmd.Parameters.AddWithValue("@precio", detalle.PrecioUnitario);
                            cmd.ExecuteNonQuery();
                            detalle.VentaId = venta.Id;
                        }
                    }

                    // 3. Todo salió bien: confirmar los cambios
                    transaccion.Commit();
                }
                catch
                {
                    // Algo falló: deshacer todo y avisar a la capa de negocio
                    transaccion.Rollback();
                    venta.Id = 0;
                    throw;
                }
            }
        }

        /// <summary>
        ///     SELECT: devuelve todas las ventas (sin detalles), de la más nueva a la más vieja.
        /// </summary>
        public List<VentaDTO> ObtenerTodas()
        {
            List<VentaDTO> lista = new List<VentaDTO>();

            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT v.id, v.fecha, v.cliente_id, c.nombre AS cliente_nombre, v.total " +
                               "FROM ventas v INNER JOIN clientes c ON c.id = v.cliente_id " +
                               "ORDER BY v.fecha DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VentaDTO venta = new VentaDTO();
                        venta.Id = Convert.ToInt32(dr["id"]);
                        venta.Fecha = Convert.ToDateTime(dr["fecha"]);
                        venta.ClienteId = Convert.ToInt32(dr["cliente_id"]);
                        venta.ClienteNombre = dr["cliente_nombre"].ToString() ?? "";
                        venta.Total = Convert.ToDecimal(dr["total"]);
                        lista.Add(venta);
                    }
                }
            }

            return lista;
        }

        /// <summary>
        ///     SELECT: devuelve los productos vendidos en una venta (para ver el detalle o hacer reportes).
        /// </summary>
        public List<DetalleVentaDTO> ObtenerDetalles(int ventaId)
        {
            List<DetalleVentaDTO> lista = new List<DetalleVentaDTO>();

            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = SelectDetalle + "WHERE d.venta_id = @venta";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@venta", ventaId);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(LeerDetalle(dr));
                        }
                    }
                }
            }

            return lista;
        }

        /// <summary>
        ///     SELECT: devuelve los detalles de TODAS las ventas en una sola consulta
        ///     (para los reportes; así no se hace una consulta por cada venta).
        /// </summary>
        public List<DetalleVentaDTO> ObtenerTodosLosDetalles()
        {
            List<DetalleVentaDTO> lista = new List<DetalleVentaDTO>();

            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = SelectDetalle + "ORDER BY d.venta_id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(LeerDetalle(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        ///     Convierte la fila actual del reader en un DetalleVentaDTO.
        /// </summary>
        private DetalleVentaDTO LeerDetalle(MySqlDataReader dr)
        {
            DetalleVentaDTO detalle = new DetalleVentaDTO();
            detalle.Id = Convert.ToInt32(dr["id"]);
            detalle.VentaId = Convert.ToInt32(dr["venta_id"]);
            detalle.ProductoCodigo = dr["producto_codigo"].ToString() ?? "";
            detalle.ProductoNombre = dr["producto_nombre"].ToString() ?? "";
            detalle.Cantidad = Convert.ToInt32(dr["cantidad"]);
            detalle.PrecioUnitario = Convert.ToDecimal(dr["precio_unitario"]);
            detalle.Subtotal = Convert.ToDecimal(dr["subtotal"]);
            return detalle;
        }
    }
}
