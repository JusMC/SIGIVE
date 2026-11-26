using MySql.Data.MySqlClient;
using SIGIVE.DTO;

namespace SIGIVE.DAL
{
    /// <summary>
    ///     DAO de categorías: consultas SQL de la tabla categorias.
    /// </summary>
    public class CategoriaDAO
    {
        /// <summary>
        ///     SELECT: devuelve todas las categorías ordenadas por nombre.
        /// </summary>
        public List<CategoriaDTO> ObtenerTodas()
        {
            List<CategoriaDTO> lista = new List<CategoriaDTO>();

            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT id, nombre, descripcion FROM categorias ORDER BY nombre";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CategoriaDTO categoria = new CategoriaDTO(
                            Convert.ToInt32(dr["id"]),
                            dr["nombre"].ToString() ?? "",
                            dr["descripcion"].ToString() ?? "");

                        lista.Add(categoria);
                    }
                }
            }

            return lista;
        }

        /// <summary>
        ///     INSERT: guarda una categoría nueva.
        /// </summary>
        public void Insertar(CategoriaDTO categoria)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "INSERT INTO categorias (nombre, descripcion) VALUES (@nombre, @descripcion)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", ConexionBD.ValorONulo(categoria.Descripcion));
                    cmd.ExecuteNonQuery();

                    categoria.Id = (int)cmd.LastInsertedId;
                }
            }
        }

        // PENDIENTE: Actualizar y Eliminar categorías (cuando se haga el formulario de categorías)
    }
}
