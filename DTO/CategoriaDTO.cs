namespace SIGIVE.DTO
{
    /// <summary>
    ///     DTO de categoría: sirve para agrupar productos.
    /// </summary>
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";

        // Constructor vacío
        public CategoriaDTO()
        {
        }

        // Constructor con datos
        public CategoriaDTO(int id, string nombre, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}
