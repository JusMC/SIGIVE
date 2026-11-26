namespace SIGIVE.DTO
{
    /// <summary>
    ///     DTO de una línea de la venta: qué producto, cuántos y a qué precio.
    /// </summary>
    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public string ProductoCodigo { get; set; } = "";
        public string ProductoNombre { get; set; } = "";
        public int Cantidad { get; set; }

        // Se guarda el precio del momento de la venta, por si el precio del producto cambia después
        public decimal PrecioUnitario { get; set; }

        // Cantidad * PrecioUnitario (lo calcula la capa de negocio)
        public decimal Subtotal { get; set; }

        // Constructor vacío
        public DetalleVentaDTO()
        {
        }
    }
}
