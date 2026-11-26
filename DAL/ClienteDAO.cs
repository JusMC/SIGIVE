using MySql.Data.MySqlClient;
using SIGIVE.DAL.Interfaces;
using SIGIVE.DTO;

namespace SIGIVE.DAL
{
    /// <summary>
    ///     DAO de clientes: aquí van SOLO las consultas SQL de la tabla clientes.
    ///     No valida nada (eso lo hace la capa de negocio).
    /// </summary>
    public class ClienteDAO : IClienteRepositorio
    {
        /// <summary>
        ///     INSERT: guarda un cliente nuevo y le pone el Id que generó MySQL.
        /// </summary>
        public void Insertar(ClienteDTO cliente)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "INSERT INTO clientes (nombre, telefono, correo) VALUES (@nombre, @telefono, @correo)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", ConexionBD.ValorONulo(cliente.Telefono));
                    cmd.Parameters.AddWithValue("@correo", ConexionBD.ValorONulo(cliente.Correo));
                    cmd.ExecuteNonQuery();

                    // LastInsertedId es el id AUTO_INCREMENT que se acaba de crear
                    cliente.Id = (int)cmd.LastInsertedId;
                }
            }
        }

        /// <summary>
        ///     SELECT: devuelve todos los clientes ordenados por nombre.
        /// </summary>
        public List<ClienteDTO> ObtenerTodos()
        {
            List<ClienteDTO> lista = new List<ClienteDTO>();

            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT id, nombre, telefono, correo FROM clientes ORDER BY nombre";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(LeerCliente(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        ///     SELECT por id. Devuelve null si no existe.
        /// </summary>
        public ClienteDTO? ObtenerPorId(int id)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT id, nombre, telefono, correo FROM clientes WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return LeerCliente(dr);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     UPDATE: cambia los datos del cliente. Devuelve true si encontró el cliente.
        /// </summary>
        public bool Actualizar(ClienteDTO cliente)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "UPDATE clientes SET nombre = @nombre, telefono = @telefono, correo = @correo WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", ConexionBD.ValorONulo(cliente.Telefono));
                    cmd.Parameters.AddWithValue("@correo", ConexionBD.ValorONulo(cliente.Correo));
                    cmd.Parameters.AddWithValue("@id", cliente.Id);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        /// <summary>
        ///     DELETE: borra el cliente. Devuelve true si lo encontró.
        /// </summary>
        public bool Eliminar(int id)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "DELETE FROM clientes WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        /// <summary>
        ///     Convierte la fila actual del reader en un ClienteDTO.
        ///     ToString() convierte los NULL de la base de datos en texto vacío.
        /// </summary>
        private ClienteDTO LeerCliente(MySqlDataReader dr)
        {
            return new ClienteDTO(
                Convert.ToInt32(dr["id"]),
                dr["nombre"].ToString() ?? "",
                dr["telefono"].ToString() ?? "",
                dr["correo"].ToString() ?? "");
        }
    }
}
