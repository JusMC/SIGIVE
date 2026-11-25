using SIGIVE.Models;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace SIGIVE.Controllers
{
    /// <summary>
    ///     Controlador de categorías: maneja el CRUD de categorías en memoria y los guarda en un archivo JSON.
    /// </summary>
    public class CategoriaController
    {
        // Atributos
        private List<Categoria> categorias;
        private int siguienteId;

        // Constructor
        public CategoriaController()
        {
            categorias = new List<Categoria>();
            siguienteId = 1;
            CargarDatosJson();
        }

        /// <summary>
        ///     Devuelve la lista de categorías.
        /// </summary>
        public List<Categoria> ObtenerCategorias()
        {
            return categorias;
        }

        /// <summary>
        ///     Busca una categoría por su Id. Devuelve null si no existe.
        /// </summary>
        public Categoria BuscarPorId(int id)
        {
            foreach (Categoria categoria in categorias)
            {
                if (categoria.Id == id)
                {
                    return categoria;
                }
            }
            return null;
        }

        /// <summary>
        ///     Agrega una categoría y guarda el archivo. Devuelve false si falta el nombre.
        /// </summary>
        public bool Agregar(string nombre, string descripcion)
        {
            if (nombre.Trim() == "")
            {
                return false;
            }

            Categoria categoria = new Categoria(siguienteId, nombre, descripcion);
            categorias.Add(categoria);
            siguienteId++;

            GuardarDatosJson();
            return true;
        }

        /// <summary>
        ///     Cambia los datos de una categoría y guarda el archivo. Devuelve false si no existe o falta el nombre.
        /// </summary>
        public bool Editar(int id, string nombre, string descripcion)
        {
            Categoria categoria = BuscarPorId(id);

            if (categoria == null || nombre.Trim() == "")
            {
                return false;
            }

            categoria.Nombre = nombre;
            categoria.Descripcion = descripcion;

            GuardarDatosJson();
            return true;
        }

        /// <summary>
        ///     Elimina una categoría y guarda el archivo. Devuelve false si no existe.
        /// </summary>
        public bool Eliminar(int id)
        {
            Categoria categoria = BuscarPorId(id);

            if (categoria == null)
            {
                return false;
            }

            categorias.Remove(categoria);
            GuardarDatosJson();
            return true;
        }

        // Métodos para guardar y cargar en JSON
        public bool GuardarDatosJson()
        {
            try
            {
                string json = JsonSerializer.Serialize(categorias);
                File.WriteAllText("categorias_backup.json", json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CargarDatosJson()
        {
            if (File.Exists("categorias_backup.json"))
            {
                string json = File.ReadAllText("categorias_backup.json");
                categorias = JsonSerializer.Deserialize<List<Categoria>>(json);

                if (categorias.Count > 0)
                {
                    siguienteId = categorias.Max(c => c.Id) + 1;
                }
            }
        }
    }
}