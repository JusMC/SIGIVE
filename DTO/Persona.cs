namespace SIGIVE.DTO
{
    /// <summary>
    ///     Clase base abstracta para las personas del sistema (por ahora solo clientes).
    ///     No se puede crear un objeto Persona directamente, solo de sus clases hijas.
    /// </summary>
    public abstract class Persona
    {
        // Propiedades comunes de toda persona
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Correo { get; set; } = "";

        // Constructor vacío
        protected Persona()
        {
        }

        // Constructor con datos
        protected Persona(int id, string nombre, string telefono, string correo)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
            Correo = correo;
        }

        /// <summary>
        ///     Devuelve la información de la persona. Cada clase hija lo hace a su manera (polimorfismo).
        /// </summary>
        public abstract string ObtenerInformacion();
    }
}
