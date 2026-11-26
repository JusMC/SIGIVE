using SIGIVE.DAL.Interfaces;

namespace SIGIVE.DAL
{
    /// <summary>
    ///     Patrón Factory: es el ÚNICO lugar donde se decide qué clase concreta
    ///     se usa para cada repositorio. Las capas de negocio piden un repositorio
    ///     aquí y no saben si por dentro es MySQL, un archivo o una lista en memoria.
    ///     Si mañana se cambia de base de datos, solo se modifica esta clase.
    /// </summary>
    public static class FabricaRepositorios
    {
        /// <summary>
        ///     Crea el repositorio de productos.
        /// </summary>
        public static IProductoRepositorio CrearProductoRepositorio()
        {
            return new ProductoDAO();
        }

        /// <summary>
        ///     Crea el repositorio de clientes.
        /// </summary>
        public static IClienteRepositorio CrearClienteRepositorio()
        {
            return new ClienteDAO();
        }

        /// <summary>
        ///     Crea el repositorio de categorías.
        /// </summary>
        public static ICategoriaRepositorio CrearCategoriaRepositorio()
        {
            return new CategoriaDAO();
        }

        /// <summary>
        ///     Crea el repositorio de ventas.
        /// </summary>
        public static IVentaRepositorio CrearVentaRepositorio()
        {
            return new VentaDAO();
        }
    }
}
