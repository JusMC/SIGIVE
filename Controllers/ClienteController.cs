using SIGIVE.Models;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace SIGIVE.Controllers
{
    /// <summary>
    ///     Controlador de clientes: maneja el CRUD de clientes en memoria y los guarda en un archivo JSON.
    /// </summary>
    public class ClienteController
    {
        // Atributos
        private List<Cliente> clientes;
        private int siguienteId;

        // Constructor
        public ClienteController()
        {
            clientes = new List<Cliente>();
            siguienteId = 1;
            CargarDatosJson();
        }

        /// <summary>
        ///     Devuelve la lista de clientes.
        /// </summary>
        public List<Cliente> ObtenerClientes()
        {
            return clientes;
        }

        /// <summary>
        ///     Busca un cliente por su Id. Devuelve null si no existe.
        /// </summary>
        public Cliente BuscarPorId(int id)
        {
            foreach (Cliente cliente in clientes)
            {
                if (cliente.Id == id)
                {
                    return cliente;
                }
            }
            return null;
        }

        /// <summary>
        ///     Agrega un cliente y guarda el archivo. Devuelve false si falta el nombre.
        /// </summary>
        public bool Agregar(string nombre, string telefono, string correo)
        {
            if (nombre.Trim() == "")
            {
                return false;
            }

            Cliente cliente = new Cliente(siguienteId, nombre, telefono, correo);
            clientes.Add(cliente);
            siguienteId++;

            GuardarDatosJson();
            return true;
        }

        /// <summary>
        ///     Cambia los datos de un cliente y guarda el archivo. Devuelve false si no existe o falta el nombre.
        /// </summary>
        public bool Editar(int id, string nombre, string telefono, string correo)
        {
            Cliente cliente = BuscarPorId(id);

            if (cliente == null || nombre.Trim() == "")
            {
                return false;
            }

            cliente.Nombre = nombre;
            cliente.Telefono = telefono;
            cliente.Correo = correo;

            GuardarDatosJson();
            return true;
        }

        /// <summary>
        ///     Elimina un cliente y guarda el archivo. Devuelve false si no existe.
        /// </summary>
        public bool Eliminar(int id)
        {
            Cliente cliente = BuscarPorId(id);

            if (cliente == null)
            {
                return false;
            }

            clientes.Remove(cliente);
            GuardarDatosJson();
            return true;
        }

        // 
        // MÉTODOS PARA GUARDAR EN ARCHIVOS JSON
        // 

        public bool GuardarDatosJson()
        {
            try
            {
                string json = JsonSerializer.Serialize(clientes);
                File.WriteAllText("clientes_backup.json", json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CargarDatosJson()
        {
            if (File.Exists("clientes_backup.json"))
            {
                string json = File.ReadAllText("clientes_backup.json");
                clientes = JsonSerializer.Deserialize<List<Cliente>>(json);

                if (clientes.Count > 0)
                {
                    siguienteId = clientes.Max(c => c.Id) + 1;
                }
            }
        }
    }
}
