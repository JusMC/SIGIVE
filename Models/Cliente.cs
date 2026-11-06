namespace SIGIVE.Models
{
    /// <summary>
    ///     Representa a un cliente que realiza las compras en el sistema.
    /// </summary>
    public class Cliente : Persona
    {
        // Constructor
        public Cliente(int id, string nombre, string telefono, string correo)
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
        ///     Texto que se muestra cuando el cliente aparece en un ComboBox o lista.
        /// </summary>
        public override string ToString()
        {
            return Nombre;
        }
    }
}
