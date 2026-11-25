using SIGIVE.Models;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;

namespace SIGIVE.Controllers
{
    /// <summary>
    ///     Controlador de productos: maneja el CRUD del inventario usando listas, JSON y LINQ.
    /// </summary>
    public class ProductoController
    {
        private List<Producto> productos;

        public ProductoController()
        {
            productos = new List<Producto>();
            CargarDatosJson();
        }

        /// <summary>
        ///     Devuelve todos los productos.
        /// </summary>
        public List<Producto> ObtenerProductos()
        {
            return productos;
        }

        /// <summary>
        /// Busca un producto rápido por código.
        /// </summary>
        public Producto BuscarPorCodigo(int codigo)
        {
            return productos.FirstOrDefault(p => p.Codigo == codigo);
        }

        /// <summary>
        ///     Uso de LINQ: Filtra productos que contengan un texto en su nombre para una barra de búsqueda.
        /// </summary>
        public List<Producto> BuscarPorNombre(string texto)
        {
            return productos.Where(p => p.Nombre.ToLower().Contains(texto.ToLower())).ToList();
        }

        public bool Agregar(int codigo, string nombre, string descripcion, decimal precio, int stock, Categoria categoria)
        {
            if (nombre.Trim() == "" || precio < 0 || stock < 0)
            {
                return false;
            }

            // Validar que el código no exista ya usando LINQ
            if (productos.Any(p => p.Codigo == codigo))
            {
                return false;
            }

            Producto nuevoProducto = new Producto(codigo, nombre, descripcion, precio, stock, categoria);
            productos.Add(nuevoProducto);
            
            GuardarDatosJson();
            return true;
        }

        public bool Editar(int codigo, string nombre, string descripcion, decimal precio, int stock, Categoria categoria)
        {
            Producto producto = BuscarPorCodigo(codigo);

            if (producto == null || nombre.Trim() == "" || precio < 0 || stock < 0)
            {
                return false;
            }

            producto.Nombre = nombre;
            producto.Descripcion = descripcion;
            producto.Precio = precio;
            producto.Stock = stock;
            producto.Categoria = categoria;

            GuardarDatosJson();
            return true;
        }

        public bool Eliminar(int codigo)
        {
            Producto producto = BuscarPorCodigo(codigo);
            if (producto != null)
            {
                productos.Remove(producto);
                GuardarDatosJson();
                return true;
            }
            return false;
        }


        public bool GuardarDatosJson()
        {
            try
            {
                string json = JsonSerializer.Serialize(productos);
                File.WriteAllText("productos_backup.json", json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CargarDatosJson()
        {
            if (File.Exists("productos_backup.json"))
            {
                string json = File.ReadAllText("productos_backup.json");
                productos = JsonSerializer.Deserialize<List<Producto>>(json);
            }
        }
    }
}