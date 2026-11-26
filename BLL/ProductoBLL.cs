using MySql.Data.MySqlClient;
using SIGIVE.DAL;
using SIGIVE.DAL.Interfaces;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.BLL
{
    /// <summary>
    ///     Capa de negocio de productos: valida los datos y luego llama al repositorio.
    /// </summary>
    public class ProductoBLL
    {
        // Se guarda la INTERFAZ, no la clase concreta: así la BLL no depende de MySQL
        private IProductoRepositorio productoRepositorio;

        /// <summary>
        ///     Constructor normal: pide el repositorio a la fábrica (patrón Factory).
        /// </summary>
        public ProductoBLL() : this(FabricaRepositorios.CrearProductoRepositorio())
        {
        }

        /// <summary>
        ///     Constructor que recibe el repositorio de productos ya creado.
        ///     Sirve para las pruebas unitarias: se le pasa un repositorio en memoria en vez de MySQL.
        /// </summary>
        public ProductoBLL(IProductoRepositorio productoRepositorio)
        {
            this.productoRepositorio = productoRepositorio;
        }

        /// <summary>
        ///     Devuelve todos los productos.
        /// </summary>
        public List<ProductoDTO> ObtenerTodos()
        {
            return productoRepositorio.ObtenerTodos();
        }

        /// <summary>
        ///     LINQ: devuelve los productos cuyo nombre o código contiene el texto buscado.
        /// </summary>
        public List<ProductoDTO> Buscar(string texto)
        {
            texto = texto.Trim().ToLower();

            if (texto == "")
            {
                return ObtenerTodos();
            }

            return productoRepositorio.ObtenerTodos()
                .Where(p => p.Nombre.ToLower().Contains(texto) || p.Codigo.ToLower().Contains(texto))
                .ToList();
        }

        /// <summary>
        ///     LINQ: devuelve solo los productos que tienen stock (para vender).
        /// </summary>
        public List<ProductoDTO> ObtenerConStock()
        {
            return productoRepositorio.ObtenerTodos()
                .Where(p => p.Stock > 0)
                .ToList();
        }

        /// <summary>
        ///     Valida y guarda un producto nuevo.
        /// </summary>
        public void Agregar(ProductoDTO producto)
        {
            LimpiarEspacios(producto);
            Validar(producto);

            if (productoRepositorio.ObtenerPorCodigo(producto.Codigo) != null)
            {
                throw new ValidacionException("Ya existe un producto con el código " + producto.Codigo + ".");
            }

            productoRepositorio.Insertar(producto);
        }

        /// <summary>
        ///     Valida y actualiza un producto existente.
        /// </summary>
        public void Actualizar(ProductoDTO producto)
        {
            LimpiarEspacios(producto);
            Validar(producto);

            if (productoRepositorio.Actualizar(producto) == false)
            {
                throw new ValidacionException("El producto ya no existe.");
            }
        }

        /// <summary>
        ///     Elimina un producto. No deja eliminarlo si ya se vendió.
        /// </summary>
        public void Eliminar(string codigo)
        {
            if (codigo == "")
            {
                throw new ValidacionException("Seleccione un producto de la tabla.");
            }

            try
            {
                productoRepositorio.Eliminar(codigo);
            }
            catch (MySqlException ex)
            {
                // Error 1451 = el producto aparece en alguna venta
                if (ex.Number == 1451)
                {
                    throw new ValidacionException("No se puede eliminar el producto porque ya tiene ventas registradas.");
                }
                throw;
            }
        }

        /// <summary>
        ///     Quita los espacios del inicio y del final de los textos.
        /// </summary>
        private void LimpiarEspacios(ProductoDTO producto)
        {
            producto.Codigo = producto.Codigo.Trim().ToUpper();
            producto.Nombre = producto.Nombre.Trim();
            producto.Descripcion = producto.Descripcion.Trim();
        }

        /// <summary>
        ///     Reglas del producto. Si algo está mal lanza ValidacionException.
        /// </summary>
        private void Validar(ProductoDTO producto)
        {
            if (producto.Codigo == "")
            {
                throw new ValidacionException("El código es obligatorio.");
            }

            if (producto.Codigo.Length > 20)
            {
                throw new ValidacionException("El código no puede tener más de 20 caracteres.");
            }

            if (producto.Nombre == "")
            {
                throw new ValidacionException("El nombre es obligatorio.");
            }

            if (producto.Precio < 0)
            {
                throw new ValidacionException("El precio no puede ser negativo.");
            }

            if (producto.Stock < 0)
            {
                throw new ValidacionException("El stock no puede ser negativo.");
            }
        }
    }
}
