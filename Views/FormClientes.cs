using SIGIVE.BLL;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.Views
{
    /// <summary>
    ///     Formulario para administrar clientes (agregar, actualizar, eliminar y listar).
    ///     Solo pide datos y muestra mensajes; las validaciones las hace ClienteBLL.
    /// </summary>
    public partial class FormClientes : Form
    {
        // Atributos
        private ClienteBLL clienteBLL;
        private int idSeleccionado;

        // Constructor
        public FormClientes()
        {
            InitializeComponent();
            clienteBLL = new ClienteBLL();
            idSeleccionado = -1;
            ActualizarTabla();
        }

        /// <summary>
        ///     Vuelve a cargar la lista de clientes desde la base de datos.
        /// </summary>
        private void ActualizarTabla()
        {
            try
            {
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clienteBLL.ObtenerTodos();
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
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

        /// <summary>
        ///     Crea un ClienteDTO con lo que está escrito en el formulario.
        /// </summary>
        private ClienteDTO LeerFormulario()
        {
            ClienteDTO cliente = new ClienteDTO();
            cliente.Id = idSeleccionado;
            cliente.Nombre = txtNombre.Text;
            cliente.Telefono = txtTelefono.Text;
            cliente.Correo = txtCorreo.Text;
            return cliente;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteDTO cliente = LeerFormulario();
                cliente.Id = 0; // es nuevo, MySQL le pone el Id
                clienteBLL.Agregar(cliente);

                Mensajes.Info("Cliente agregado.");
                ActualizarTabla();
                Limpiar();
            }
            catch (ValidacionException ex)
            {
                // Primero la excepción específica...
                Mensajes.Advertencia(ex.Message);
            }
            catch (Exception ex)
            {
                // ...y al final la general
                Mensajes.Error(ex);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                clienteBLL.Actualizar(LeerFormulario());

                Mensajes.Info("Cliente actualizado.");
                ActualizarTabla();
                Limpiar();
            }
            catch (ValidacionException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                Mensajes.Advertencia("Seleccione un cliente de la tabla.");
                return;
            }

            if (Mensajes.Confirmar("¿Desea eliminar este cliente?") == false)
            {
                return;
            }

            try
            {
                clienteBLL.Eliminar(idSeleccionado);
                ActualizarTabla();
                Limpiar();
            }
            catch (ValidacionException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        private void btnVerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteDTO? cliente = clienteBLL.BuscarPorId(idSeleccionado);

                if (cliente == null)
                {
                    Mensajes.Advertencia("Seleccione un cliente de la tabla.");
                    return;
                }

                // Usa el método que ClienteDTO implementa desde la clase abstracta Persona
                MessageBox.Show(cliente.ObtenerInformacion(), "Información del cliente");
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
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

            ClienteDTO cliente = (ClienteDTO)dgvClientes.Rows[e.RowIndex].DataBoundItem;

            idSeleccionado = cliente.Id;
            txtNombre.Text = cliente.Nombre;
            txtTelefono.Text = cliente.Telefono;
            txtCorreo.Text = cliente.Correo;
        }
    }
}
