using SIGIVE.DTO;

namespace SIGIVE.DAL.Interfaces
{
    /// <summary>
    ///     Repositorio de categorías. CategoriaDAO lo implementa con MySQL.
    /// </summary>
    public interface ICategoriaRepositorio
    {
        /// <summary>
        ///     Devuelve todas las categorías.
        /// </summary>
        List<CategoriaDTO> ObtenerTodas();

        /// <summary>
        ///     Guarda una categoría nueva y le asigna su Id.
        /// </summary>
        void Insertar(CategoriaDTO categoria);

        // PENDIENTE: Actualizar y Eliminar (cuando se haga el formulario de categorías)
    }
}
