using SIGIVE.Models;

namespace SIGIVE.Controllers
{
    /// <summary>
    ///     Controlador de clientes: maneja el CRUD de clientes en memoria.
    ///     No muestra nada en pantalla; recibe los datos desde el formulario y devuelve el resultado.
    /// </summary>
    public class ClienteController
    {
        // Atributos
        private List<Cliente> clientes;
        private int siguienteId;

        // Constructor
        public ClienteController()
        {
            clientes = new List<Cliente>();
            siguienteId = 1;
        }

        /// <summary>
        ///     Devuelve la lista de clientes.
        /// </summary>
        public List<Cliente> ObtenerClientes()
        {
            return clientes;
        }

        /// <summary>
        ///     Busca un cliente por su Id. Devuelve null si no existe.
        /// </summary>
        public Cliente BuscarPorId(int id)
        {
            foreach (Cliente cliente in clientes)
            {
                if (cliente.Id == id)
                {
                    return cliente;
                }
            }
            return null;
        }

        /// <summary>
        ///     Agrega un cliente. Devuelve false si falta el nombre.
        /// </summary>
        public bool Agregar(string nombre, string telefono, string correo)
        {
            if (nombre.Trim() == "")
            {
                return false;
            }

            Cliente cliente = new Cliente(siguienteId, nombre, telefono, correo);
            clientes.Add(cliente);
            siguienteId++;
            return true;
        }

        /// <summary>
        ///     Cambia los datos de un cliente. Devuelve false si no existe o falta el nombre.
        /// </summary>
        public bool Editar(int id, string nombre, string telefono, string correo)
        {
            Cliente cliente = BuscarPorId(id);

            if (cliente == null || nombre.Trim() == "")
            {
                return false;
            }

            cliente.Nombre = nombre;
            cliente.Telefono = telefono;
            cliente.Correo = correo;
            return true;
        }

        /// <summary>
        ///     Elimina un cliente. Devuelve false si no existe.
        /// </summary>
        public bool Eliminar(int id)
        {
            Cliente cliente = BuscarPorId(id);

            if (cliente == null)
            {
                return false;
            }

            clientes.Remove(cliente);
            return true;
        }
    }
}
