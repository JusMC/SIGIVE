namespace SIGIVE.Excepciones
{
    /// <summary>
    ///     Excepción personalizada: se lanza cuando se quiere vender más unidades de las que hay en stock.
    /// </summary>
    public class StockInsuficienteException : Exception
    {
        // Recibe el mensaje y se lo pasa a la clase base Exception
        public StockInsuficienteException(string mensaje)
            : base(mensaje)
        {
        }
    }
}
