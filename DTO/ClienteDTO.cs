namespace SIGIVE.DTO
{
    /// <summary>
    ///     DTO de cliente: transporta los datos de un cliente entre las capas.
    ///     Hereda de Persona (Id, Nombre, Telefono, Correo).
    /// </summary>
    public class ClienteDTO : Persona
    {
        // Constructor vacío
        public ClienteDTO()
        {
        }

        // Constructor con datos: se los pasa a la clase base Persona
        public ClienteDTO(int id, string nombre, string telefono, string correo)
            : base(id, nombre, telefono, correo)
        {
        }

        /// <summary>
        ///     Devuelve la información completa del cliente.
        /// </summary>
        public override string ObtenerInformacion()
        {
            return "Cliente #" + Id + "\nNombre: " + Nombre + "\nTeléfono: " + Telefono + "\nCorreo: " + Correo;
        }

        /// <summary>
        ///     Texto que se muestra cuando el cliente aparece en un ComboBox.
        /// </summary>
        public override string ToString()
        {
            return Nombre;
        }
    }
}
