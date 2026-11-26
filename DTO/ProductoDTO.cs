namespace SIGIVE.DTO
{
    /// <summary>
    ///     DTO de producto: transporta los datos de un producto entre las capas.
    /// </summary>
    public class ProductoDTO
    {
        // El código es texto (ej: "P001"), igual que en la base de datos
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // 0 significa "sin categoría"
        public int CategoriaId { get; set; }

        // Solo se usa para mostrar el nombre de la categoría en la tabla
        public string CategoriaNombre { get; set; } = "";

        // Constructor vacío
        public ProductoDTO()
        {
        }
    }
}
