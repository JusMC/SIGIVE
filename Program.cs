using SIGIVE.Views;

namespace SIGIVE
{
    internal static class Program
    {
        /// <summary>
        ///     Punto de entrada de la aplicación: abre el menú principal.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormPrincipal());
        }
    }
}
