using SIGIVE.DTO;

namespace SIGIVE.DAL.Interfaces
{
    /// <summary>
    ///     Repositorio de productos: define QUÉ operaciones de datos existen para productos,
    ///     sin decir CÓMO se hacen. ProductoDAO lo implementa con MySQL.
    /// </summary>
    public interface IProductoRepositorio
    {
        /// <summary>
        ///     Guarda un producto nuevo.
        /// </summary>
        void Insertar(ProductoDTO producto);

        /// <summary>
        ///     Devuelve todos los productos.
        /// </summary>
        List<ProductoDTO> ObtenerTodos();

        /// <summary>
        ///     Busca un producto por código. Devuelve null si no existe.
        /// </summary>
        ProductoDTO? ObtenerPorCodigo(string codigo);

        /// <summary>
        ///     Actualiza un producto. Devuelve true si lo encontró.
        /// </summary>
        bool Actualizar(ProductoDTO producto);

        /// <summary>
        ///     Elimina un producto. Devuelve true si lo encontró.
        /// </summary>
        bool Eliminar(string codigo);
    }
}
