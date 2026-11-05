namespace SIGIVE.models

/// <summary>
///     Representa a un cliente que realiza las compras en el sistema.
/// </summary>

{
    public class Cliente : Persona
    {
        // Constructor
        public Cliente(int id, string nombre, string telefono, string correo)
            : base(id, nombre, telefono, correo)
        {
        }

        
        public override string ToString()
        {
            return Nombre;
        }
    }
}