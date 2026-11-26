using SIGIVE.DAL;
using SIGIVE.DAL.Interfaces;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.BLL
{
    /// <summary>
    ///     Capa de negocio de categorías.
    /// </summary>
    public class CategoriaBLL
    {
        // Se guarda la INTERFAZ, no la clase concreta: así la BLL no depende de MySQL
        private ICategoriaRepositorio categoriaRepositorio;

        /// <summary>
        ///     Constructor normal: pide el repositorio a la fábrica (patrón Factory).
        /// </summary>
        public CategoriaBLL() : this(FabricaRepositorios.CrearCategoriaRepositorio())
        {
        }

        /// <summary>
        ///     Constructor que recibe el repositorio de categorías ya creado.
        ///     Sirve para las pruebas unitarias: se le pasa un repositorio en memoria en vez de MySQL.
        /// </summary>
        public CategoriaBLL(ICategoriaRepositorio categoriaRepositorio)
        {
            this.categoriaRepositorio = categoriaRepositorio;
        }

        /// <summary>
        ///     Devuelve todas las categorías.
        /// </summary>
        public List<CategoriaDTO> ObtenerTodas()
        {
            return categoriaRepositorio.ObtenerTodas();
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
            bool repetida = categoriaRepositorio.ObtenerTodas().Any(c => c.Nombre.ToLower() == categoria.Nombre.ToLower());
            if (repetida)
            {
                throw new ValidacionException("Ya existe una categoría con ese nombre.");
            }

            categoriaRepositorio.Insertar(categoria);
        }

        // PENDIENTE: Actualizar y Eliminar (cuando se haga el formulario de categorías)
    }
}
