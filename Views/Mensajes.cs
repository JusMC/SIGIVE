using SIGIVE.Utilidades;

namespace SIGIVE.Views
{
    /// <summary>
    ///     Mensajes que usan todos los formularios, para no repetir el mismo código.
    /// </summary>
    public static class Mensajes
    {
        /// <summary>
        ///     Mensaje de éxito.
        /// </summary>
        public static void Info(string texto)
        {
            MessageBox.Show(texto, "SIGIVE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        ///     Mensaje cuando el usuario escribió algo mal (no es un error del programa).
        /// </summary>
        public static void Advertencia(string texto)
        {
            MessageBox.Show(texto, "Revise los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        ///     Error inesperado: lo guarda en errores.log y muestra un mensaje amigable
        ///     (sin detalles técnicos para el usuario).
        /// </summary>
        public static void Error(Exception ex)
        {
            Logger.RegistrarError(ex);
            MessageBox.Show("Ocurrió un error inesperado. Revise la conexión con la base de datos.\n" +
                            "El detalle quedó guardado en errores.log.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        ///     Pregunta Sí/No. Devuelve true si el usuario dijo que sí.
        /// </summary>
        public static bool Confirmar(string texto)
        {
            DialogResult respuesta = MessageBox.Show(texto, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return respuesta == DialogResult.Yes;
        }
    }
}
