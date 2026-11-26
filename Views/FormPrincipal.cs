using SIGIVE.DAL;

namespace SIGIVE.Views
{
    /// <summary>
    ///     Formulario principal: menú que abre cada módulo del sistema.
    /// </summary>
    public partial class FormPrincipal : Form
    {
        // Constructor
        public FormPrincipal()
        {
            InitializeComponent();
        }

        // Al abrir el programa se revisa si hay conexión con MySQL
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            if (ConexionBD.ProbarConexion() == false)
            {
                Mensajes.Advertencia("No se pudo conectar a la base de datos.\n" +
                                     "Revise que MySQL esté encendido y la cadena de conexión en DAL/ConexionBD.cs.");
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            // using: libera el formulario cuando se cierra
            using (FormClientes form = new FormClientes())
            {
                form.ShowDialog();
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            using (FormProductos form = new FormProductos())
            {
                form.ShowDialog();
            }
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            using (FormVentas form = new FormVentas())
            {
                form.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
