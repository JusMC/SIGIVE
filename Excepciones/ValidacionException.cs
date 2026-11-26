namespace SIGIVE.Excepciones
{
    /// <summary>
    ///     Excepción personalizada: se lanza cuando un dato que escribió el usuario no es válido
    ///     (nombre vacío, correo sin @, precio negativo, etc.).
    /// </summary>
    public class ValidacionException : Exception
    {
        // Recibe el mensaje y se lo pasa a la clase base Exception
        public ValidacionException(string mensaje)
            : base(mensaje)
        {
        }
    }
}
