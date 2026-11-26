namespace SIGIVE.DTO
{
    /// <summary>
    ///     Fila del reporte de clientes: datos del cliente y cuánto ha comprado.
    /// </summary>
    public class ClienteReporteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Correo { get; set; } = "";

        // Cantidad de ventas hechas al cliente
        public int CantidadCompras { get; set; }

        // Suma de los totales de sus ventas
        public decimal TotalComprado { get; set; }

        // Constructor vacío
        public ClienteReporteDTO()
        {
        }
    }
}
