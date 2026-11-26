using SIGIVE.BLL;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.Views
{
    /// <summary>
    ///     Formulario para registrar ventas: se elige el cliente, se agregan productos
    ///     y al final se registra la venta (ahí se descuenta el stock).
    /// </summary>
    public partial class FormVentas : Form
    {
        // Atributos
        private VentaBLL ventaBLL;
        private ClienteBLL clienteBLL;
        private ProductoBLL productoBLL;

        // La venta que se está armando en pantalla (todavía no está guardada)
        private VentaDTO ventaActual;

        // Constructor
        public FormVentas()
        {
            InitializeComponent();
            ventaBLL = new VentaBLL();
            clienteBLL = new ClienteBLL();
            productoBLL = new ProductoBLL();
            ventaActual = new VentaDTO();

            CargarClientes();
            CargarProductos();
            MostrarDetalle();
            CargarVentasRegistradas();
        }

        /// <summary>
        ///     Llena el ComboBox de clientes.
        /// </summary>
        private void CargarClientes()
        {
            try
            {
                cboCliente.DataSource = clienteBLL.ObtenerTodos();
                cboCliente.DisplayMember = "Nombre";
                cboCliente.ValueMember = "Id";
                cboCliente.SelectedIndex = -1; // que no quede ninguno elegido al inicio
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        /// <summary>
        ///     Llena el ComboBox de productos (solo los que tienen stock).
        /// </summary>
        private void CargarProductos()
        {
            try
            {
                cboProducto.DataSource = productoBLL.ObtenerConStock();
                cboProducto.DisplayMember = "Nombre";
                cboProducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        /// <summary>
        ///     Muestra en la tabla los productos de la venta actual y el total.
        /// </summary>
        private void MostrarDetalle()
        {
            dgvDetalle.DataSource = null;
            dgvDetalle.DataSource = ventaActual.Detalles;

            // Columnas que el usuario no necesita ver
            OcultarColumna(dgvDetalle, "Id");
            OcultarColumna(dgvDetalle, "VentaId");

            lblTotal.Text = "Total: " + ventaActual.Total.ToString("N2");
        }

        /// <summary>
        ///     Muestra el historial de ventas guardadas.
        /// </summary>
        private void CargarVentasRegistradas()
        {
            try
            {
                dgvVentas.DataSource = null;
                dgvVentas.DataSource = ventaBLL.ObtenerTodas();
                OcultarColumna(dgvVentas, "ClienteId");
                OcultarColumna(dgvVentas, "Detalles");
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        /// <summary>
        ///     Oculta una columna de la tabla si existe.
        /// </summary>
        private void OcultarColumna(DataGridView tabla, string nombreColumna)
        {
            if (tabla.Columns[nombreColumna] != null)
            {
                tabla.Columns[nombreColumna].Visible = false;
            }
        }

        /// <summary>
        ///     Deja el formulario listo para una venta nueva.
        /// </summary>
        private void NuevaVenta()
        {
            ventaActual = new VentaDTO();
            cboCliente.SelectedIndex = -1;
            cboProducto.SelectedIndex = -1;
            nudCantidad.Value = 1;
            MostrarDetalle();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                // "as" devuelve null si no hay producto elegido
                ProductoDTO? producto = cboProducto.SelectedItem as ProductoDTO;
                ventaBLL.AgregarProducto(ventaActual, producto, (int)nudCantidad.Value);

                MostrarDetalle();
                nudCantidad.Value = 1;
            }
            catch (ValidacionException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
            catch (StockInsuficienteException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow == null)
            {
                Mensajes.Advertencia("Seleccione un producto del detalle.");
                return;
            }

            try
            {
                DetalleVentaDTO detalle = (DetalleVentaDTO)dgvDetalle.CurrentRow.DataBoundItem;
                ventaBLL.QuitarProducto(ventaActual, detalle.ProductoCodigo);
                MostrarDetalle();
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

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                // Pasar el cliente elegido a la venta
                ClienteDTO? cliente = cboCliente.SelectedItem as ClienteDTO;
                if (cliente != null)
                {
                    ventaActual.ClienteId = cliente.Id;
                    ventaActual.ClienteNombre = cliente.Nombre;
                }
                else
                {
                    ventaActual.ClienteId = 0;
                }

                ventaBLL.Registrar(ventaActual);

                Mensajes.Info("Venta #" + ventaActual.Id + " registrada.\nTotal: " + ventaActual.Total.ToString("N2"));

                // El stock cambió, así que se recargan los productos
                CargarProductos();
                CargarVentasRegistradas();
                NuevaVenta();
            }
            catch (ValidacionException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
            catch (StockInsuficienteException ex)
            {
                // Pasa si otra persona vendió el producto mientras tanto
                Mensajes.Advertencia(ex.Message);
                CargarProductos();
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            if (ventaActual.Detalles.Count > 0 &&
                Mensajes.Confirmar("¿Desea descartar la venta actual?") == false)
            {
                return;
            }

            NuevaVenta();
        }

        // PENDIENTE: al hacer doble clic en una venta registrada, mostrar sus productos
        // (ya existe ventaBLL.ObtenerDetalles(ventaId) para eso)
    }
}
