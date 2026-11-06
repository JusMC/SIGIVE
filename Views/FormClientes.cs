using SIGIVE.Controllers;
using SIGIVE.Models;

namespace SIGIVE.Views
{
    /// <summary>
    ///     Formulario para administrar clientes (agregar, editar, eliminar y listar).
    /// </summary>
    public partial class FormClientes : Form
    {
        // Atributos
        private ClienteController controller;
        private int idSeleccionado;

        // Constructor: recibe el controlador desde el formulario principal
        public FormClientes(ClienteController controller)
        {
            InitializeComponent();
            this.controller = controller;
            idSeleccionado = -1;
            ActualizarTabla();
        }

        /// <summary>
        ///     Vuelve a cargar la lista de clientes en la tabla.
        /// </summary>
        private void ActualizarTabla()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = controller.ObtenerClientes();
        }

        /// <summary>
        ///     Limpia las cajas de texto y la selección.
        /// </summary>
        private void Limpiar()
        {
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            idSeleccionado = -1;
            dgvClientes.ClearSelection();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool agregado = controller.Agregar(txtNombre.Text, txtTelefono.Text, txtCorreo.Text);

            if (agregado)
            {
                MessageBox.Show("Cliente agregado.");
                ActualizarTabla();
                Limpiar();
            }
            else
            {
                MessageBox.Show("El nombre es obligatorio.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un cliente de la tabla.");
                return;
            }

            bool editado = controller.Editar(idSeleccionado, txtNombre.Text, txtTelefono.Text, txtCorreo.Text);

            if (editado)
            {
                MessageBox.Show("Cliente actualizado.");
                ActualizarTabla();
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar. Revise que el nombre no esté vacío.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un cliente de la tabla.");
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Desea eliminar este cliente?", "Confirmar", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                controller.Eliminar(idSeleccionado);
                ActualizarTabla();
                Limpiar();
            }
        }

        private void btnVerInfo_Click(object sender, EventArgs e)
        {
            Cliente cliente = controller.BuscarPorId(idSeleccionado);

            if (cliente == null)
            {
                MessageBox.Show("Seleccione un cliente de la tabla.");
                return;
            }

            // Usa el método que Cliente implementa desde la clase abstracta Persona
            MessageBox.Show(cliente.ObtenerInformacion(), "Información del cliente");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        // Al hacer clic en una fila, se pasan sus datos a las cajas de texto
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Cliente cliente = (Cliente)dgvClientes.Rows[e.RowIndex].DataBoundItem;

            idSeleccionado = cliente.Id;
            txtNombre.Text = cliente.Nombre;
            txtTelefono.Text = cliente.Telefono;
            txtCorreo.Text = cliente.Correo;
        }
    }
}
