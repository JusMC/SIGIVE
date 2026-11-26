using MySql.Data.MySqlClient;
using SIGIVE.DAL;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.BLL
{
    /// <summary>
    ///     Capa de negocio de clientes: valida los datos y luego llama al DAO.
    /// </summary>
    public class ClienteBLL
    {
        private ClienteDAO clienteDAO;

        public ClienteBLL()
        {
            clienteDAO = new ClienteDAO();
        }

        /// <summary>
        ///     Devuelve todos los clientes.
        /// </summary>
        public List<ClienteDTO> ObtenerTodos()
        {
            return clienteDAO.ObtenerTodos();
        }

        /// <summary>
        ///     Busca un cliente por Id. Devuelve null si no existe.
        /// </summary>
        public ClienteDTO? BuscarPorId(int id)
        {
            return clienteDAO.ObtenerPorId(id);
        }

        /// <summary>
        ///     Valida y guarda un cliente nuevo.
        /// </summary>
        public void Agregar(ClienteDTO cliente)
        {
            LimpiarEspacios(cliente);
            Validar(cliente);
            clienteDAO.Insertar(cliente);
        }

        /// <summary>
        ///     Valida y actualiza un cliente existente.
        /// </summary>
        public void Actualizar(ClienteDTO cliente)
        {
            if (cliente.Id <= 0)
            {
                throw new ValidacionException("Seleccione un cliente de la tabla.");
            }

            LimpiarEspacios(cliente);
            Validar(cliente);

            if (clienteDAO.Actualizar(cliente) == false)
            {
                throw new ValidacionException("El cliente ya no existe.");
            }
        }

        /// <summary>
        ///     Elimina un cliente. No deja eliminarlo si tiene ventas.
        /// </summary>
        public void Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ValidacionException("Seleccione un cliente de la tabla.");
            }

            try
            {
                clienteDAO.Eliminar(id);
            }
            catch (MySqlException ex)
            {
                // Error 1451 = el cliente está siendo usado en otra tabla (tiene ventas)
                if (ex.Number == 1451)
                {
                    throw new ValidacionException("No se puede eliminar el cliente porque tiene ventas registradas.");
                }
                throw;
            }
        }

        /// <summary>
        ///     Quita los espacios del inicio y del final de los textos.
        /// </summary>
        private void LimpiarEspacios(ClienteDTO cliente)
        {
            cliente.Nombre = cliente.Nombre.Trim();
            cliente.Telefono = cliente.Telefono.Trim();
            cliente.Correo = cliente.Correo.Trim();
        }

        /// <summary>
        ///     Reglas del cliente. Si algo está mal lanza ValidacionException con el mensaje para el usuario.
        /// </summary>
        private void Validar(ClienteDTO cliente)
        {
            if (cliente.Nombre == "")
            {
                throw new ValidacionException("El nombre es obligatorio.");
            }

            // El correo es opcional, pero si lo escriben debe tener @ y punto
            if (cliente.Correo != "")
            {
                if (!cliente.Correo.Contains("@") || !cliente.Correo.Contains("."))
                {
                    throw new ValidacionException("El correo no tiene un formato válido.");
                }

                // LINQ: revisar que ningún OTRO cliente tenga el mismo correo
                bool correoRepetido = clienteDAO.ObtenerTodos()
                    .Any(c => c.Correo.ToLower() == cliente.Correo.ToLower() && c.Id != cliente.Id);

                if (correoRepetido)
                {
                    throw new ValidacionException("Ya existe un cliente con ese correo.");
                }
            }

            // PENDIENTE: validar formato del teléfono (por ejemplo, solo números)
        }
    }
}
