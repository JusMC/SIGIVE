using SIGIVE.Models;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System;

namespace SIGIVE.Controllers
{
    /// <summary>
    ///     Controlador de ventas: gestiona el registro de ventas y reportes básicos.
    /// </summary>
    public class VentaController
    {
        private List<Venta> ventas;
        private int siguienteId;

        public VentaController()
        {
            ventas = new List<Venta>();
            siguienteId = 1;
            CargarDatosJson();
        }

        public List<Venta> ObtenerVentas()
        {
            return ventas;
        }

        /// <summary>
        ///     Registra la venta finalizada en la lista y guarda los cambios.
        /// </summary>
        public bool RegistrarVenta(Venta nuevaVenta)
        {
            if (nuevaVenta.Detalles.Count == 0 || nuevaVenta.Cliente == null)
            {
                return false;
            }

            nuevaVenta.Id = siguienteId;
            ventas.Add(nuevaVenta);
            siguienteId++;

            GuardarDatosJson();
            return true;
        }

  
        // REPORTES USANDO LINQ 
   

        /// <summary>
        ///     Suma el total de todas las ventas registradas.
        /// </summary>
        public decimal CalcularTotalHistoricoVentas()
        {
            return ventas.Sum(v => v.CalcularTotal());
        }

        /// <summary>
        ///     Obtiene las ventas de un cliente específico.
        /// </summary>
        public List<Venta> ObtenerVentasPorCliente(int idCliente)
        {
            return ventas.Where(v => v.Cliente.Id == idCliente).ToList();
        }

       
        // PERSISTENCIA EN ARCHIVOS
     
        public bool GuardarDatosJson()
        {
            try
            {
                string json = JsonSerializer.Serialize(ventas);
                File.WriteAllText("ventas_backup.json", json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CargarDatosJson()
        {
            if (File.Exists("ventas_backup.json"))
            {
                string json = File.ReadAllText("ventas_backup.json");
                ventas = JsonSerializer.Deserialize<List<Venta>>(json);
                
                if (ventas.Count > 0)
                {
                    siguienteId = ventas.Max(v => v.Id) + 1;
                }
            }
        }
    }
}