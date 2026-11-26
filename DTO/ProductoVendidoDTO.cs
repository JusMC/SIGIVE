namespace SIGIVE.DTO
{
    /// <summary>
    ///     Fila del reporte "productos más vendidos": cuánto se vendió de cada producto.
    /// </summary>
    public class ProductoVendidoDTO
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";

        // Suma de las cantidades vendidas en todas las ventas
        public int CantidadVendida { get; set; }

        // Suma de los subtotales (dinero que generó el producto)
        public decimal TotalVendido { get; set; }

        // Constructor vacío
        public ProductoVendidoDTO()
        {
        }
    }
}
