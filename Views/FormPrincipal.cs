using SIGIVE.Controllers;

namespace SIGIVE.Views
{
    /// <summary>
    ///     Formulario principal: menú que abre cada módulo del sistema.
    /// </summary>
    public partial class FormPrincipal : Form
    {
        // Los controladores se crean una sola vez aquí,
        // para que todos los formularios compartan los mismos datos
        private ClienteController clienteController;

        // Constructor
        public FormPrincipal()
        {
            InitializeComponent();
            clienteController = new ClienteController();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FormClientes form = new FormClientes(clienteController);
            form.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de productos en desarrollo.");
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de ventas en desarrollo.");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
