using MySql.Data.MySqlClient;
using SIGIVE.Utilidades;

namespace SIGIVE.DAL
{
    /// <summary>
    ///     Maneja la conexión con MySQL. Todos los DAO usan esta misma cadena de conexión.
    /// </summary>
    public static class ConexionBD
    {
        // IMPORTANTE: cada uno cambie Uid y Pwd según su MySQL
        public static readonly string CadenaConexion = "Server=localhost;Database=sistema_ventas;Uid=root;Pwd=;";

        /// <summary>
        ///     Crea una conexión nueva (se abre con conn.Open() dentro de un using).
        /// </summary>
        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(CadenaConexion);
        }

        /// <summary>
        ///     Intenta conectarse. Devuelve true si funcionó y false si no.
        /// </summary>
        public static bool ProbarConexion()
        {
            try
            {
                using (MySqlConnection conn = ObtenerConexion())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.RegistrarError(ex);
                return false;
            }
        }

        /// <summary>
        ///     Si el texto está vacío devuelve NULL para la base de datos.
        ///     Así los campos opcionales (teléfono, correo, descripción) quedan en NULL y no en "".
        /// </summary>
        public static object ValorONulo(string texto)
        {
            if (texto == "")
            {
                return DBNull.Value;
            }
            return texto;
        }
    }
}
