using SIGIVE.DTO;

namespace SIGIVE.DAL.Interfaces
{
    /// <summary>
    ///     Repositorio de clientes: define las operaciones de datos de clientes.
    ///     ClienteDAO lo implementa con MySQL.
    /// </summary>
    public interface IClienteRepositorio
    {
        /// <summary>
        ///     Guarda un cliente nuevo y le asigna su Id.
        /// </summary>
        void Insertar(ClienteDTO cliente);

        /// <summary>
        ///     Devuelve todos los clientes.
        /// </summary>
        List<ClienteDTO> ObtenerTodos();

        /// <summary>
        ///     Busca un cliente por Id. Devuelve null si no existe.
        /// </summary>
        ClienteDTO? ObtenerPorId(int id);

        /// <summary>
        ///     Actualiza un cliente. Devuelve true si lo encontró.
        /// </summary>
        bool Actualizar(ClienteDTO cliente);

        /// <summary>
        ///     Elimina un cliente. Devuelve true si lo encontró.
        /// </summary>
        bool Eliminar(int id);
    }
}
