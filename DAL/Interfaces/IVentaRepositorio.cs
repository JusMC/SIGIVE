using SIGIVE.DTO;

namespace SIGIVE.DAL.Interfaces
{
    /// <summary>
    ///     Repositorio de ventas. VentaDAO lo implementa con MySQL.
    /// </summary>
    public interface IVentaRepositorio
    {
        /// <summary>
        ///     Guarda la venta completa (cabecera + detalles) y descuenta el stock.
        ///     Si algo falla no se guarda nada.
        /// </summary>
        void Registrar(VentaDTO venta);

        /// <summary>
        ///     Devuelve todas las ventas (sin detalles).
        /// </summary>
        List<VentaDTO> ObtenerTodas();

        /// <summary>
        ///     Devuelve los productos vendidos en una venta.
        /// </summary>
        List<DetalleVentaDTO> ObtenerDetalles(int ventaId);

        /// <summary>
        ///     Devuelve los detalles de todas las ventas (para reportes).
        /// </summary>
        List<DetalleVentaDTO> ObtenerTodosLosDetalles();
    }
}
