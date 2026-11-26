using MySql.Data.MySqlClient;
using SIGIVE.DTO;

namespace SIGIVE.DAL
{
    /// <summary>
    ///     DAO de productos: consultas SQL de la tabla productos.
    /// </summary>
    public class ProductoDAO
    {
        // Consulta base: trae el producto y el nombre de su categoría (LEFT JOIN porque puede no tener)
        private const string SelectBase =
            "SELECT p.codigo, p.nombre, p.descripcion, p.precio, p.stock, p.categoria_id, c.nombre AS categoria_nombre " +
            "FROM productos p LEFT JOIN categorias c ON c.id = p.categoria_id ";

        /// <summary>
        ///     INSERT: guarda un producto nuevo.
        /// </summary>
        public void Insertar(ProductoDTO producto)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "INSERT INTO productos (codigo, nombre, descripcion, precio, stock, categoria_id) " +
                               "VALUES (@codigo, @nombre, @descripcion, @precio, @stock, @categoria)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    AgregarParametros(cmd, producto);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        ///     SELECT: devuelve todos los productos ordenados por nombre.
        /// </summary>
        public List<ProductoDTO> ObtenerTodos()
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = SelectBase + "ORDER BY p.nombre";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(LeerProducto(dr));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        ///     SELECT por código. Devuelve null si no existe.
        /// </summary>
        public ProductoDTO? ObtenerPorCodigo(string codigo)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = SelectBase + "WHERE p.codigo = @codigo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return LeerProducto(dr);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     UPDATE: cambia los datos del producto (el código no se cambia). Devuelve true si lo encontró.
        /// </summary>
        public bool Actualizar(ProductoDTO producto)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "UPDATE productos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, " +
                               "stock = @stock, categoria_id = @categoria WHERE codigo = @codigo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    AgregarParametros(cmd, producto);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        /// <summary>
        ///     DELETE: borra el producto. Devuelve true si lo encontró.
        /// </summary>
        public bool Eliminar(string codigo)
        {
            using (MySqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "DELETE FROM productos WHERE codigo = @codigo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        /// <summary>
        ///     Pone los parámetros que usan el INSERT y el UPDATE (así no se repite el código).
        /// </summary>
        private void AgregarParametros(MySqlCommand cmd, ProductoDTO producto)
        {
            cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
            cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@descripcion", ConexionBD.ValorONulo(producto.Descripcion));
            cmd.Parameters.AddWithValue("@precio", producto.Precio);
            cmd.Parameters.AddWithValue("@stock", producto.Stock);

            // Si CategoriaId es 0, el producto queda sin categoría (NULL)
            if (producto.CategoriaId == 0)
            {
                cmd.Parameters.AddWithValue("@categoria", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@categoria", producto.CategoriaId);
            }
        }

        /// <summary>
        ///     Convierte la fila actual del reader en un ProductoDTO.
        /// </summary>
        private ProductoDTO LeerProducto(MySqlDataReader dr)
        {
            ProductoDTO producto = new ProductoDTO();
            producto.Codigo = dr["codigo"].ToString() ?? "";
            producto.Nombre = dr["nombre"].ToString() ?? "";
            producto.Descripcion = dr["descripcion"].ToString() ?? "";
            producto.Precio = Convert.ToDecimal(dr["precio"]);
            producto.Stock = Convert.ToInt32(dr["stock"]);

            // categoria_id puede venir NULL
            if (dr["categoria_id"] == DBNull.Value)
            {
                producto.CategoriaId = 0;
            }
            else
            {
                producto.CategoriaId = Convert.ToInt32(dr["categoria_id"]);
            }

            producto.CategoriaNombre = dr["categoria_nombre"].ToString() ?? "";
            return producto;
        }
    }
}
