namespace SIGIVE.Utilidades
{
    /// <summary>
    ///     Guarda los errores en el archivo "errores.log" con fecha y hora,
    ///     para poder revisar después qué falló.
    /// </summary>
    public static class Logger
    {
        public static void RegistrarError(Exception ex)
        {
            // Fecha, tipo de error y mensaje
            string mensaje = $"[{DateTime.Now}] {ex.GetType().Name}: {ex.Message}\n";

            try
            {
                // AppendAllText agrega al final del archivo (y lo crea si no existe)
                File.AppendAllText("errores.log", mensaje);
            }
            catch (IOException)
            {
                // Si no se puede escribir el log, no hacemos nada para no cerrar el programa
            }
        }
    }
}
