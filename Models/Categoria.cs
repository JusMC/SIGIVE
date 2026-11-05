namespace proyectoFinal.models
{
    /// <summary>
    ///     Representa una categoría para agrupar productos.
    /// </summary>

    public class Categoria
    {
        
        // Atributos
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Constructor
        public Categoria(int id, string nombre, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}