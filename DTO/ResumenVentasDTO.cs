namespace SIGIVE.DTO
{
    /// <summary>
    ///     Números generales que se muestran en el dashboard.
    /// </summary>
    public class ResumenVentasDTO
    {
        // Cuántas ventas hubo en el rango de fechas
        public int CantidadVentas { get; set; }

        // Suma de los totales de esas ventas
        public decimal TotalVendido { get; set; }

        // Promedio por venta (TotalVendido / CantidadVentas)
        public decimal TicketPromedio { get; set; }

        // Cuántos productos tienen stock bajo
        public int ProductosBajoStock { get; set; }

        // Constructor vacío
        public ResumenVentasDTO()
        {
        }
    }
}
