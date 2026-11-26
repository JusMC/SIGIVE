using System.Diagnostics;
using SIGIVE.BLL;
using SIGIVE.DTO;
using SIGIVE.Excepciones;
using SIGIVE.Utilidades;

namespace SIGIVE.Views
{
    /// <summary>
    ///     Formulario de reportes: listados de ventas, productos, más vendidos y clientes,
    ///     un dashboard con gráficos y exportación a Excel y PDF.
    /// </summary>
    public partial class FormReportes : Form
    {
        // Atributos
        private ReporteBLL reporteBLL;
        private ClienteBLL clienteBLL;

        // Evita que los eventos (CheckedChanged, ValueChanged...) se ejecuten mientras se arma el formulario
        private bool inicializado = false;

        // Constructor
        public FormReportes()
        {
            InitializeComponent();
            reporteBLL = new ReporteBLL();
            clienteBLL = new ClienteBLL();

            // Por defecto se muestran los últimos 30 días
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;

            inicializado = true;

            CargarClientesFiltro();
            CargarTodo();
        }

        // =====================================================
        // Carga de datos
        // =====================================================

        /// <summary>
        ///     Recarga todas las pestañas con el rango de fechas elegido.
        /// </summary>
        private void CargarTodo()
        {
            try
            {
                CargarVentas();
                CargarProductos();
                CargarMasVendidos();
                CargarClientes();
                CargarDashboard();
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

        /// <summary>
        ///     Llena el ComboBox de clientes con una opción "(Todos los clientes)" al inicio.
        /// </summary>
        private void CargarClientesFiltro()
        {
            try
            {
                List<ClienteDTO> clientes = clienteBLL.ObtenerTodos();

                // Id 0 significa "todos" (así lo entiende ReporteBLL.ObtenerVentas)
                clientes.Insert(0, new ClienteDTO(0, "(Todos los clientes)", "", ""));

                cboClienteFiltro.DataSource = clientes;
                cboClienteFiltro.DisplayMember = "Nombre";
                cboClienteFiltro.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        /// <summary>
        ///     Pestaña Ventas: ventas del rango (y del cliente elegido) con el total.
        /// </summary>
        private void CargarVentas()
        {
            int clienteId = ClienteSeleccionadoId();
            List<VentaDTO> ventas = reporteBLL.ObtenerVentas(dtpDesde.Value, dtpHasta.Value, clienteId);

            dgvVentas.DataSource = null;
            dgvVentas.DataSource = ventas;

            // LINQ: total de las ventas que se están mostrando
            lblTotalVentas.Text = "Ventas: " + ventas.Count + "    Total vendido: " + ventas.Sum(v => v.Total).ToString("N2");

            CargarDetalleVentaSeleccionada();
        }

        /// <summary>
        ///     Muestra los productos de la venta seleccionada en la tabla de la derecha.
        /// </summary>
        private void CargarDetalleVentaSeleccionada()
        {
            VentaDTO? venta = dgvVentas.CurrentRow?.DataBoundItem as VentaDTO;

            dgvDetalleVenta.DataSource = null;
            if (venta == null)
            {
                return;
            }

            dgvDetalleVenta.DataSource = reporteBLL.ObtenerDetalleVenta(venta.Id);
        }

        /// <summary>
        ///     Pestaña Productos: inventario, con opción de ver solo los de stock bajo.
        /// </summary>
        private void CargarProductos()
        {
            int limite = (int)nudLimiteStock.Value;
            List<ProductoDTO> productos = reporteBLL.ObtenerProductos(chkBajoStock.Checked, limite);

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productos;

            // LINQ: valor del inventario = suma de (precio * stock)
            decimal valorInventario = productos.Sum(p => p.Precio * p.Stock);
            lblTotalProductos.Text = "Productos: " + productos.Count + "    Valor del inventario: " + valorInventario.ToString("N2");
        }

        /// <summary>
        ///     Pestaña Más vendidos.
        /// </summary>
        private void CargarMasVendidos()
        {
            dgvMasVendidos.DataSource = null;
            dgvMasVendidos.DataSource = reporteBLL.ObtenerProductosMasVendidos(dtpDesde.Value, dtpHasta.Value);
        }

        /// <summary>
        ///     Pestaña Clientes: cuánto ha comprado cada cliente en el rango.
        /// </summary>
        private void CargarClientes()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = reporteBLL.ObtenerResumenClientes(dtpDesde.Value, dtpHasta.Value);
        }

        /// <summary>
        ///     Pestaña Dashboard: tarjetas con los números generales y dos gráficos.
        /// </summary>
        private void CargarDashboard()
        {
            int limite = (int)nudLimiteStock.Value;
            ResumenVentasDTO resumen = reporteBLL.ObtenerResumen(dtpDesde.Value, dtpHasta.Value, limite);

            lblCantidadVentas.Text = "Ventas\n" + resumen.CantidadVentas;
            lblTotalVendido.Text = "Total vendido\n" + resumen.TotalVendido.ToString("N2");
            lblTicketPromedio.Text = "Promedio por venta\n" + resumen.TicketPromedio.ToString("N2");
            lblBajoStock.Text = "Productos con stock ≤ " + limite + "\n" + resumen.ProductosBajoStock;

            // Gráfico 1: total vendido por día (las llaves del diccionario son las fechas)
            SortedDictionary<DateTime, decimal> ventasPorDia = reporteBLL.ObtenerVentasPorDia(dtpDesde.Value, dtpHasta.Value);
            graficoVentasDia.MostrarDatos(
                ventasPorDia.Keys.Select(f => f.ToString("dd/MM")).ToList(),
                ventasPorDia.Values.ToList());

            // Gráfico 2: los 5 productos más vendidos (LINQ Take)
            List<ProductoVendidoDTO> top = reporteBLL.ObtenerProductosMasVendidos(dtpDesde.Value, dtpHasta.Value)
                .Take(5)
                .ToList();
            graficoTopProductos.MostrarDatos(
                top.Select(p => p.Nombre).ToList(),
                top.Select(p => (decimal)p.CantidadVendida).ToList());
        }

        // =====================================================
        // Exportar
        // =====================================================

        /// <summary>
        ///     Exporta la tabla de la pestaña actual a Excel o PDF.
        /// </summary>
        private void ExportarPestanaActual(bool esPdf)
        {
            DataGridView? tabla = null;
            string titulo = "";
            string pie = "";
            string rango = "del " + dtpDesde.Value.ToString("dd/MM/yyyy") + " al " + dtpHasta.Value.ToString("dd/MM/yyyy");

            if (tabReportes.SelectedTab == tabVentas)
            {
                tabla = dgvVentas;
                titulo = "Reporte de ventas " + rango;
                if (ClienteSeleccionadoId() != 0)
                {
                    titulo += " - Cliente: " + cboClienteFiltro.Text;
                }
                pie = lblTotalVentas.Text;
            }
            else if (tabReportes.SelectedTab == tabProductos)
            {
                tabla = dgvProductos;
                titulo = chkBajoStock.Checked
                    ? "Productos con stock bajo (≤ " + nudLimiteStock.Value + ")"
                    : "Reporte de productos (inventario)";
                pie = lblTotalProductos.Text;
            }
            else if (tabReportes.SelectedTab == tabMasVendidos)
            {
                tabla = dgvMasVendidos;
                titulo = "Productos más vendidos " + rango;
            }
            else if (tabReportes.SelectedTab == tabClientes)
            {
                tabla = dgvClientes;
                titulo = "Reporte de clientes " + rango;
            }

            if (tabla == null)
            {
                Mensajes.Advertencia("Seleccione una pestaña con una tabla para exportar.");
                return;
            }

            if (tabla.Rows.Count == 0)
            {
                Mensajes.Advertencia("No hay datos para exportar.");
                return;
            }

            string ruta = PedirRutaArchivo(tabReportes.SelectedTab!.Text, esPdf);
            if (ruta == "")
            {
                return; // el usuario canceló
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                if (esPdf)
                {
                    ExportadorPdf.Exportar(tabla, titulo, ruta, pie);
                }
                else
                {
                    ExportadorExcel.Exportar(tabla, titulo, ruta, pie);
                }

                Cursor = Cursors.Default;

                if (Mensajes.Confirmar("Reporte guardado en:\n" + ruta + "\n\n¿Desea abrirlo?"))
                {
                    // UseShellExecute abre el archivo con el programa predeterminado (Excel, lector de PDF...)
                    Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
                }
            }
            catch (IOException)
            {
                Mensajes.Advertencia("No se pudo guardar el archivo. Revise que no esté abierto en otro programa.");
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        ///     Abre el diálogo "Guardar como". Devuelve la ruta elegida o "" si se canceló.
        /// </summary>
        private string PedirRutaArchivo(string nombreReporte, bool esPdf)
        {
            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                string nombreArchivo = "Reporte_" + nombreReporte.Replace(" ", "_") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm");

                if (esPdf)
                {
                    dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    dialogo.FileName = nombreArchivo + ".pdf";
                }
                else
                {
                    dialogo.Filter = "Libro de Excel (*.xlsx)|*.xlsx";
                    dialogo.FileName = nombreArchivo + ".xlsx";
                }

                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    return dialogo.FileName;
                }
                return "";
            }
        }

        // =====================================================
        // Ayudantes de las tablas
        // =====================================================

        /// <summary>
        ///     Pone encabezados, formatos y columnas ocultas de cada tabla.
        ///     Se llama desde DataBindingComplete porque WinForms vuelve a crear las columnas
        ///     cuando una tabla de otra pestaña se muestra por primera vez (y se perderían los cambios).
        /// </summary>
        private void ConfigurarColumnas(DataGridView tabla)
        {
            if (tabla == dgvVentas)
            {
                OcultarColumna(tabla, "ClienteId");
                OcultarColumna(tabla, "Detalles");
                ConfigurarColumna(tabla, "Id", "N.º venta", "");
                ConfigurarColumna(tabla, "Fecha", "Fecha", "dd/MM/yyyy HH:mm");
                ConfigurarColumna(tabla, "ClienteNombre", "Cliente", "");
                ConfigurarColumna(tabla, "Total", "Total", "N2");
            }
            else if (tabla == dgvDetalleVenta)
            {
                OcultarColumna(tabla, "Id");
                OcultarColumna(tabla, "VentaId");
                ConfigurarColumna(tabla, "ProductoCodigo", "Código", "");
                ConfigurarColumna(tabla, "ProductoNombre", "Producto", "");
                ConfigurarColumna(tabla, "Cantidad", "Cant.", "");
                ConfigurarColumna(tabla, "PrecioUnitario", "Precio unit.", "N2");
                ConfigurarColumna(tabla, "Subtotal", "Subtotal", "N2");
            }
            else if (tabla == dgvProductos)
            {
                OcultarColumna(tabla, "CategoriaId");
                ConfigurarColumna(tabla, "Codigo", "Código", "");
                ConfigurarColumna(tabla, "Nombre", "Nombre", "");
                ConfigurarColumna(tabla, "Descripcion", "Descripción", "");
                ConfigurarColumna(tabla, "Precio", "Precio", "N2");
                ConfigurarColumna(tabla, "Stock", "Stock", "");
                ConfigurarColumna(tabla, "CategoriaNombre", "Categoría", "");
                PintarStockBajo();
            }
            else if (tabla == dgvMasVendidos)
            {
                ConfigurarColumna(tabla, "Codigo", "Código", "");
                ConfigurarColumna(tabla, "Nombre", "Producto", "");
                ConfigurarColumna(tabla, "CantidadVendida", "Cantidad vendida", "");
                ConfigurarColumna(tabla, "TotalVendido", "Total vendido", "N2");
            }
            else if (tabla == dgvClientes)
            {
                ConfigurarColumna(tabla, "Id", "ID", "");
                ConfigurarColumna(tabla, "Nombre", "Nombre", "");
                ConfigurarColumna(tabla, "Telefono", "Teléfono", "");
                ConfigurarColumna(tabla, "Correo", "Correo", "");
                ConfigurarColumna(tabla, "CantidadCompras", "Compras", "");
                ConfigurarColumna(tabla, "TotalComprado", "Total comprado", "N2");
            }
        }

        /// <summary>
        ///     Pinta de rojo claro las filas de productos con stock bajo.
        /// </summary>
        private void PintarStockBajo()
        {
            int limite = (int)nudLimiteStock.Value;

            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                if (fila.DataBoundItem is ProductoDTO producto && producto.Stock <= limite)
                {
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }

        /// <summary>
        ///     Id del cliente elegido en el filtro (0 = todos).
        /// </summary>
        private int ClienteSeleccionadoId()
        {
            ClienteDTO? cliente = cboClienteFiltro.SelectedItem as ClienteDTO;
            if (cliente == null)
            {
                return 0;
            }
            return cliente.Id;
        }

        /// <summary>
        ///     Oculta una columna de la tabla si existe.
        /// </summary>
        private void OcultarColumna(DataGridView tabla, string nombreColumna)
        {
            if (tabla.Columns[nombreColumna] != null)
            {
                tabla.Columns[nombreColumna]!.Visible = false;
            }
        }

        /// <summary>
        ///     Cambia el encabezado de una columna y, si se indica, su formato (N2, fechas...).
        /// </summary>
        private void ConfigurarColumna(DataGridView tabla, string nombreColumna, string encabezado, string formato)
        {
            DataGridViewColumn? columna = tabla.Columns[nombreColumna];
            if (columna == null)
            {
                return;
            }

            columna.HeaderText = encabezado;
            if (formato != "")
            {
                columna.DefaultCellStyle.Format = formato;
            }

            // Los números se alinean a la derecha
            if (formato == "N2" || columna.ValueType == typeof(int))
            {
                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        // =====================================================
        // Eventos
        // =====================================================

        // Lo usan las 5 tablas: se ejecuta cada vez que una tabla termina de cargar sus datos
        private void tabla_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ConfigurarColumnas((DataGridView)sender);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTodo();
        }

        private void cboClienteFiltro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CargarVentas();
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

        private void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {
            if (!inicializado)
            {
                return;
            }

            try
            {
                CargarDetalleVentaSeleccionada();
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
            }
        }

        // Se usa tanto para el CheckBox como para el NumericUpDown
        private void filtroProductos_Changed(object sender, EventArgs e)
        {
            if (!inicializado)
            {
                return;
            }

            try
            {
                CargarProductos();
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

        // En el dashboard no hay tabla, así que se desactivan los botones de exportar
        private void tabReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hayTabla = tabReportes.SelectedTab != tabDashboard;
            btnExportarExcel.Enabled = hayTabla;
            btnExportarPdf.Enabled = hayTabla;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarPestanaActual(false);
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            ExportarPestanaActual(true);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
