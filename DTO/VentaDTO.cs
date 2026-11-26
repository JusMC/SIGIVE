namespace SIGIVE.DTO
{
    /// <summary>
    ///     DTO de una venta (cabecera) con su lista de detalles.
    /// </summary>
    public class VentaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }

        // Solo para mostrar el nombre del cliente en la tabla
        public string ClienteNombre { get; set; } = "";

        public decimal Total { get; set; }

        // Productos vendidos en esta venta
        public List<DetalleVentaDTO> Detalles { get; set; } = new List<DetalleVentaDTO>();

        // Constructor vacío
        public VentaDTO()
        {
        }
    }
}
