using SIGIVE.DAL;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.BLL
{
    /// <summary>
    ///     Capa de negocio de categorías.
    /// </summary>
    public class CategoriaBLL
    {
        private CategoriaDAO categoriaDAO;

        public CategoriaBLL()
        {
            categoriaDAO = new CategoriaDAO();
        }

        /// <summary>
        ///     Devuelve todas las categorías.
        /// </summary>
        public List<CategoriaDTO> ObtenerTodas()
        {
            return categoriaDAO.ObtenerTodas();
        }

        /// <summary>
        ///     Valida y guarda una categoría nueva.
        /// </summary>
        public void Agregar(CategoriaDTO categoria)
        {
            categoria.Nombre = categoria.Nombre.Trim();

            if (categoria.Nombre == "")
            {
                throw new ValidacionException("El nombre de la categoría es obligatorio.");
            }

            // LINQ: no permitir dos categorías con el mismo nombre
            bool repetida = categoriaDAO.ObtenerTodas().Any(c => c.Nombre.ToLower() == categoria.Nombre.ToLower());
            if (repetida)
            {
                throw new ValidacionException("Ya existe una categoría con ese nombre.");
            }

            categoriaDAO.Insertar(categoria);
        }

        // PENDIENTE: Actualizar y Eliminar (cuando se haga el formulario de categorías)
    }
}
