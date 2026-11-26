using SIGIVE.BLL;
using SIGIVE.DTO;
using SIGIVE.Excepciones;

namespace SIGIVE.Views
{
    /// <summary>
    ///     Formulario para administrar productos (agregar, actualizar, eliminar, buscar y listar).
    /// </summary>
    public partial class FormProductos : Form
    {
        // Atributos
        private ProductoBLL productoBLL;
        private CategoriaBLL categoriaBLL;

        // Código del producto seleccionado ("" = ninguno)
        private string codigoSeleccionado;

        // Constructor
        public FormProductos()
        {
            InitializeComponent();
            productoBLL = new ProductoBLL();
            categoriaBLL = new CategoriaBLL();
            codigoSeleccionado = "";

            CargarCategorias();
            RecargarProductos();
        }

        /// <summary>
        ///     Llena el ComboBox de categorías. La primera opción es "(Sin categoría)" con Id 0.
        /// </summary>
        private void CargarCategorias()
        {
            try
            {
                List<CategoriaDTO> categorias = categoriaBLL.ObtenerTodas();
                categorias.Insert(0, new CategoriaDTO(0, "(Sin categoría)", ""));

                cboCategoria.DataSource = categorias;
                cboCategoria.DisplayMember = "Nombre"; // lo que se ve
                cboCategoria.ValueMember = "Id";       // el valor que se guarda
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        /// <summary>
        ///     Muestra la lista de productos en la tabla.
        /// </summary>
        private void ActualizarTabla(List<ProductoDTO> productos)
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productos;

            // El Id de la categoría no le sirve al usuario; se muestra el nombre
            if (dgvProductos.Columns["CategoriaId"] != null)
            {
                dgvProductos.Columns["CategoriaId"].Visible = false;
            }
        }

        /// <summary>
        ///     Recarga todos los productos desde la base de datos.
        /// </summary>
        private void RecargarProductos()
        {
            try
            {
                ActualizarTabla(productoBLL.ObtenerTodos());
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        /// <summary>
        ///     Limpia el formulario y deja todo listo para agregar uno nuevo.
        /// </summary>
        private void Limpiar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            nudPrecio.Value = 0;
            nudStock.Value = 0;
            if (cboCategoria.Items.Count > 0)
            {
                cboCategoria.SelectedIndex = 0; // "(Sin categoría)"
            }

            codigoSeleccionado = "";
            txtCodigo.ReadOnly = false; // el código solo se puede escribir en productos nuevos
            dgvProductos.ClearSelection();
        }

        /// <summary>
        ///     Crea un ProductoDTO con lo que está escrito en el formulario.
        /// </summary>
        private ProductoDTO LeerFormulario()
        {
            ProductoDTO producto = new ProductoDTO();
            producto.Codigo = txtCodigo.Text;
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Precio = nudPrecio.Value;
            producto.Stock = (int)nudStock.Value;

            if (cboCategoria.SelectedValue != null)
            {
                producto.CategoriaId = (int)cboCategoria.SelectedValue;
            }

            return producto;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                productoBLL.Agregar(LeerFormulario());

                Mensajes.Info("Producto agregado.");
                RecargarProductos();
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (codigoSeleccionado == "")
            {
                Mensajes.Advertencia("Seleccione un producto de la tabla.");
                return;
            }

            try
            {
                productoBLL.Actualizar(LeerFormulario());

                Mensajes.Info("Producto actualizado.");
                RecargarProductos();
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
            if (codigoSeleccionado == "")
            {
                Mensajes.Advertencia("Seleccione un producto de la tabla.");
                return;
            }

            if (Mensajes.Confirmar("¿Desea eliminar este producto?") == false)
            {
                return;
            }

            try
            {
                productoBLL.Eliminar(codigoSeleccionado);
                RecargarProductos();
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizarTabla(productoBLL.Buscar(txtBuscar.Text));
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        // Al hacer clic en una fila, se pasan sus datos al formulario
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            ProductoDTO producto = (ProductoDTO)dgvProductos.Rows[e.RowIndex].DataBoundItem;

            codigoSeleccionado = producto.Codigo;
            txtCodigo.Text = producto.Codigo;
            txtCodigo.ReadOnly = true; // el código no se puede cambiar al actualizar
            txtNombre.Text = producto.Nombre;
            txtDescripcion.Text = producto.Descripcion;
            nudPrecio.Value = producto.Precio;
            nudStock.Value = producto.Stock;
            cboCategoria.SelectedValue = producto.CategoriaId;
        }
    }
}
