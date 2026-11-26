namespace SIGIVE.Views
{
    partial class FormReportes
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnActualizar = new Button();
            tabReportes = new TabControl();
            tabVentas = new TabPage();
            lblClienteFiltro = new Label();
            cboClienteFiltro = new ComboBox();
            dgvVentas = new DataGridView();
            lblDetalleVenta = new Label();
            dgvDetalleVenta = new DataGridView();
            lblTotalVentas = new Label();
            tabProductos = new TabPage();
            chkBajoStock = new CheckBox();
            nudLimiteStock = new NumericUpDown();
            lblLeyendaStock = new Label();
            dgvProductos = new DataGridView();
            lblTotalProductos = new Label();
            tabMasVendidos = new TabPage();
            lblInfoMasVendidos = new Label();
            dgvMasVendidos = new DataGridView();
            tabClientes = new TabPage();
            lblInfoClientes = new Label();
            dgvClientes = new DataGridView();
            tabDashboard = new TabPage();
            lblCantidadVentas = new Label();
            lblTotalVendido = new Label();
            lblTicketPromedio = new Label();
            lblBajoStock = new Label();
            graficoVentasDia = new SIGIVE.Views.Controles.GraficoBarras();
            graficoTopProductos = new SIGIVE.Views.Controles.GraficoBarras();
            btnExportarExcel = new Button();
            btnExportarPdf = new Button();
            btnCerrar = new Button();
            tabReportes.SuspendLayout();
            tabVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleVenta).BeginInit();
            tabProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLimiteStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tabMasVendidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMasVendidos).BeginInit();
            tabClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            tabDashboard.SuspendLayout();
            SuspendLayout();
            //
            // lblDesde
            //
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(20, 22);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(54, 20);
            lblDesde.TabIndex = 0;
            lblDesde.Text = "Desde:";
            //
            // dtpDesde
            //
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(80, 18);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(150, 27);
            dtpDesde.TabIndex = 1;
            //
            // lblHasta
            //
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(250, 22);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(50, 20);
            lblHasta.TabIndex = 2;
            lblHasta.Text = "Hasta:";
            //
            // dtpHasta
            //
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(305, 18);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(150, 27);
            dtpHasta.TabIndex = 3;
            //
            // btnActualizar
            //
            btnActualizar.Location = new Point(475, 14);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(140, 34);
            btnActualizar.TabIndex = 4;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            //
            // tabReportes
            //
            tabReportes.Controls.Add(tabVentas);
            tabReportes.Controls.Add(tabProductos);
            tabReportes.Controls.Add(tabMasVendidos);
            tabReportes.Controls.Add(tabClientes);
            tabReportes.Controls.Add(tabDashboard);
            tabReportes.Location = new Point(20, 60);
            tabReportes.Name = "tabReportes";
            tabReportes.SelectedIndex = 0;
            tabReportes.Size = new Size(1060, 550);
            tabReportes.TabIndex = 5;
            tabReportes.SelectedIndexChanged += tabReportes_SelectedIndexChanged;
            //
            // tabVentas
            //
            tabVentas.Controls.Add(lblClienteFiltro);
            tabVentas.Controls.Add(cboClienteFiltro);
            tabVentas.Controls.Add(dgvVentas);
            tabVentas.Controls.Add(lblDetalleVenta);
            tabVentas.Controls.Add(dgvDetalleVenta);
            tabVentas.Controls.Add(lblTotalVentas);
            tabVentas.Location = new Point(4, 29);
            tabVentas.Name = "tabVentas";
            tabVentas.Padding = new Padding(3);
            tabVentas.Size = new Size(1052, 517);
            tabVentas.TabIndex = 0;
            tabVentas.Text = "Ventas";
            tabVentas.UseVisualStyleBackColor = true;
            //
            // lblClienteFiltro
            //
            lblClienteFiltro.AutoSize = true;
            lblClienteFiltro.Location = new Point(12, 17);
            lblClienteFiltro.Name = "lblClienteFiltro";
            lblClienteFiltro.Size = new Size(58, 20);
            lblClienteFiltro.TabIndex = 0;
            lblClienteFiltro.Text = "Cliente:";
            //
            // cboClienteFiltro
            //
            cboClienteFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClienteFiltro.Location = new Point(80, 13);
            cboClienteFiltro.Name = "cboClienteFiltro";
            cboClienteFiltro.Size = new Size(300, 28);
            cboClienteFiltro.TabIndex = 1;
            cboClienteFiltro.SelectionChangeCommitted += cboClienteFiltro_SelectionChangeCommitted;
            //
            // dgvVentas
            //
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(12, 50);
            dgvVentas.MultiSelect = false;
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.RowHeadersWidth = 51;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.Size = new Size(620, 420);
            dgvVentas.TabIndex = 2;
            dgvVentas.DataBindingComplete += tabla_DataBindingComplete;
            dgvVentas.SelectionChanged += dgvVentas_SelectionChanged;
            //
            // lblDetalleVenta
            //
            lblDetalleVenta.AutoSize = true;
            lblDetalleVenta.Location = new Point(648, 17);
            lblDetalleVenta.Name = "lblDetalleVenta";
            lblDetalleVenta.Size = new Size(238, 20);
            lblDetalleVenta.TabIndex = 3;
            lblDetalleVenta.Text = "Detalle de la venta seleccionada:";
            //
            // dgvDetalleVenta
            //
            dgvDetalleVenta.AllowUserToAddRows = false;
            dgvDetalleVenta.AllowUserToDeleteRows = false;
            dgvDetalleVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalleVenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleVenta.Location = new Point(648, 50);
            dgvDetalleVenta.MultiSelect = false;
            dgvDetalleVenta.Name = "dgvDetalleVenta";
            dgvDetalleVenta.ReadOnly = true;
            dgvDetalleVenta.RowHeadersVisible = false;
            dgvDetalleVenta.RowHeadersWidth = 51;
            dgvDetalleVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalleVenta.Size = new Size(390, 420);
            dgvDetalleVenta.TabIndex = 4;
            dgvDetalleVenta.DataBindingComplete += tabla_DataBindingComplete;
            //
            // lblTotalVentas
            //
            lblTotalVentas.AutoSize = true;
            lblTotalVentas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalVentas.Location = new Point(12, 480);
            lblTotalVentas.Name = "lblTotalVentas";
            lblTotalVentas.Size = new Size(0, 23);
            lblTotalVentas.TabIndex = 5;
            //
            // tabProductos
            //
            tabProductos.Controls.Add(chkBajoStock);
            tabProductos.Controls.Add(nudLimiteStock);
            tabProductos.Controls.Add(lblLeyendaStock);
            tabProductos.Controls.Add(dgvProductos);
            tabProductos.Controls.Add(lblTotalProductos);
            tabProductos.Location = new Point(4, 29);
            tabProductos.Name = "tabProductos";
            tabProductos.Padding = new Padding(3);
            tabProductos.Size = new Size(1052, 517);
            tabProductos.TabIndex = 1;
            tabProductos.Text = "Productos";
            tabProductos.UseVisualStyleBackColor = true;
            //
            // chkBajoStock
            //
            chkBajoStock.AutoSize = true;
            chkBajoStock.Location = new Point(12, 15);
            chkBajoStock.Name = "chkBajoStock";
            chkBajoStock.Size = new Size(350, 24);
            chkBajoStock.TabIndex = 0;
            chkBajoStock.Text = "Solo productos con stock bajo (menor o igual a):";
            chkBajoStock.UseVisualStyleBackColor = true;
            chkBajoStock.CheckedChanged += filtroProductos_Changed;
            //
            // nudLimiteStock
            //
            nudLimiteStock.Location = new Point(370, 13);
            nudLimiteStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudLimiteStock.Name = "nudLimiteStock";
            nudLimiteStock.Size = new Size(80, 27);
            nudLimiteStock.TabIndex = 1;
            nudLimiteStock.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudLimiteStock.ValueChanged += filtroProductos_Changed;
            //
            // lblLeyendaStock
            //
            lblLeyendaStock.AutoSize = true;
            lblLeyendaStock.ForeColor = Color.Firebrick;
            lblLeyendaStock.Location = new Point(470, 17);
            lblLeyendaStock.Name = "lblLeyendaStock";
            lblLeyendaStock.Size = new Size(254, 20);
            lblLeyendaStock.TabIndex = 2;
            lblLeyendaStock.Text = "Las filas en rojo tienen stock bajo.";
            //
            // dgvProductos
            //
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(12, 50);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(1026, 420);
            dgvProductos.TabIndex = 3;
            dgvProductos.DataBindingComplete += tabla_DataBindingComplete;
            //
            // lblTotalProductos
            //
            lblTotalProductos.AutoSize = true;
            lblTotalProductos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalProductos.Location = new Point(12, 480);
            lblTotalProductos.Name = "lblTotalProductos";
            lblTotalProductos.Size = new Size(0, 23);
            lblTotalProductos.TabIndex = 4;
            //
            // tabMasVendidos
            //
            tabMasVendidos.Controls.Add(lblInfoMasVendidos);
            tabMasVendidos.Controls.Add(dgvMasVendidos);
            tabMasVendidos.Location = new Point(4, 29);
            tabMasVendidos.Name = "tabMasVendidos";
            tabMasVendidos.Padding = new Padding(3);
            tabMasVendidos.Size = new Size(1052, 517);
            tabMasVendidos.TabIndex = 2;
            tabMasVendidos.Text = "Más vendidos";
            tabMasVendidos.UseVisualStyleBackColor = true;
            //
            // lblInfoMasVendidos
            //
            lblInfoMasVendidos.AutoSize = true;
            lblInfoMasVendidos.Location = new Point(12, 17);
            lblInfoMasVendidos.Name = "lblInfoMasVendidos";
            lblInfoMasVendidos.Size = new Size(497, 20);
            lblInfoMasVendidos.TabIndex = 0;
            lblInfoMasVendidos.Text = "Productos vendidos en el rango de fechas, de mayor a menor cantidad.";
            //
            // dgvMasVendidos
            //
            dgvMasVendidos.AllowUserToAddRows = false;
            dgvMasVendidos.AllowUserToDeleteRows = false;
            dgvMasVendidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMasVendidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMasVendidos.Location = new Point(12, 50);
            dgvMasVendidos.MultiSelect = false;
            dgvMasVendidos.Name = "dgvMasVendidos";
            dgvMasVendidos.ReadOnly = true;
            dgvMasVendidos.RowHeadersVisible = false;
            dgvMasVendidos.RowHeadersWidth = 51;
            dgvMasVendidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMasVendidos.Size = new Size(1026, 450);
            dgvMasVendidos.TabIndex = 1;
            dgvMasVendidos.DataBindingComplete += tabla_DataBindingComplete;
            //
            // tabClientes
            //
            tabClientes.Controls.Add(lblInfoClientes);
            tabClientes.Controls.Add(dgvClientes);
            tabClientes.Location = new Point(4, 29);
            tabClientes.Name = "tabClientes";
            tabClientes.Padding = new Padding(3);
            tabClientes.Size = new Size(1052, 517);
            tabClientes.TabIndex = 3;
            tabClientes.Text = "Clientes";
            tabClientes.UseVisualStyleBackColor = true;
            //
            // lblInfoClientes
            //
            lblInfoClientes.AutoSize = true;
            lblInfoClientes.Location = new Point(12, 17);
            lblInfoClientes.Name = "lblInfoClientes";
            lblInfoClientes.Size = new Size(437, 20);
            lblInfoClientes.TabIndex = 0;
            lblInfoClientes.Text = "Compras de cada cliente en el rango de fechas (de mayor a menor).";
            //
            // dgvClientes
            //
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(12, 50);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(1026, 450);
            dgvClientes.TabIndex = 1;
            dgvClientes.DataBindingComplete += tabla_DataBindingComplete;
            //
            // tabDashboard
            //
            tabDashboard.Controls.Add(lblCantidadVentas);
            tabDashboard.Controls.Add(lblTotalVendido);
            tabDashboard.Controls.Add(lblTicketPromedio);
            tabDashboard.Controls.Add(lblBajoStock);
            tabDashboard.Controls.Add(graficoVentasDia);
            tabDashboard.Controls.Add(graficoTopProductos);
            tabDashboard.Location = new Point(4, 29);
            tabDashboard.Name = "tabDashboard";
            tabDashboard.Padding = new Padding(3);
            tabDashboard.Size = new Size(1052, 517);
            tabDashboard.TabIndex = 4;
            tabDashboard.Text = "Dashboard";
            tabDashboard.UseVisualStyleBackColor = true;
            //
            // lblCantidadVentas
            //
            lblCantidadVentas.BackColor = Color.AliceBlue;
            lblCantidadVentas.BorderStyle = BorderStyle.FixedSingle;
            lblCantidadVentas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCantidadVentas.Location = new Point(12, 12);
            lblCantidadVentas.Name = "lblCantidadVentas";
            lblCantidadVentas.Size = new Size(245, 80);
            lblCantidadVentas.TabIndex = 0;
            lblCantidadVentas.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblTotalVendido
            //
            lblTotalVendido.BackColor = Color.AliceBlue;
            lblTotalVendido.BorderStyle = BorderStyle.FixedSingle;
            lblTotalVendido.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalVendido.Location = new Point(269, 12);
            lblTotalVendido.Name = "lblTotalVendido";
            lblTotalVendido.Size = new Size(245, 80);
            lblTotalVendido.TabIndex = 1;
            lblTotalVendido.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblTicketPromedio
            //
            lblTicketPromedio.BackColor = Color.AliceBlue;
            lblTicketPromedio.BorderStyle = BorderStyle.FixedSingle;
            lblTicketPromedio.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTicketPromedio.Location = new Point(526, 12);
            lblTicketPromedio.Name = "lblTicketPromedio";
            lblTicketPromedio.Size = new Size(245, 80);
            lblTicketPromedio.TabIndex = 2;
            lblTicketPromedio.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblBajoStock
            //
            lblBajoStock.BackColor = Color.MistyRose;
            lblBajoStock.BorderStyle = BorderStyle.FixedSingle;
            lblBajoStock.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBajoStock.Location = new Point(783, 12);
            lblBajoStock.Name = "lblBajoStock";
            lblBajoStock.Size = new Size(255, 80);
            lblBajoStock.TabIndex = 3;
            lblBajoStock.TextAlign = ContentAlignment.MiddleCenter;
            //
            // graficoVentasDia
            //
            graficoVentasDia.BackColor = Color.White;
            graficoVentasDia.ColorBarras = Color.SteelBlue;
            graficoVentasDia.FormatoValores = "N0";
            graficoVentasDia.Location = new Point(12, 105);
            graficoVentasDia.Name = "graficoVentasDia";
            graficoVentasDia.Size = new Size(507, 400);
            graficoVentasDia.TabIndex = 4;
            graficoVentasDia.Titulo = "Total vendido por día";
            //
            // graficoTopProductos
            //
            graficoTopProductos.BackColor = Color.White;
            graficoTopProductos.ColorBarras = Color.SeaGreen;
            graficoTopProductos.FormatoValores = "N0";
            graficoTopProductos.Location = new Point(531, 105);
            graficoTopProductos.Name = "graficoTopProductos";
            graficoTopProductos.Size = new Size(507, 400);
            graficoTopProductos.TabIndex = 5;
            graficoTopProductos.Titulo = "Top 5 productos (unidades vendidas)";
            //
            // btnExportarExcel
            //
            btnExportarExcel.Location = new Point(20, 622);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(190, 40);
            btnExportarExcel.TabIndex = 6;
            btnExportarExcel.Text = "Exportar a Excel";
            btnExportarExcel.UseVisualStyleBackColor = true;
            btnExportarExcel.Click += btnExportarExcel_Click;
            //
            // btnExportarPdf
            //
            btnExportarPdf.Location = new Point(220, 622);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(190, 40);
            btnExportarPdf.TabIndex = 7;
            btnExportarPdf.Text = "Exportar a PDF";
            btnExportarPdf.UseVisualStyleBackColor = true;
            btnExportarPdf.Click += btnExportarPdf_Click;
            //
            // btnCerrar
            //
            btnCerrar.Location = new Point(890, 622);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(190, 40);
            btnCerrar.TabIndex = 8;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            //
            // FormReportes
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 680);
            Controls.Add(lblDesde);
            Controls.Add(dtpDesde);
            Controls.Add(lblHasta);
            Controls.Add(dtpHasta);
            Controls.Add(btnActualizar);
            Controls.Add(tabReportes);
            Controls.Add(btnExportarExcel);
            Controls.Add(btnExportarPdf);
            Controls.Add(btnCerrar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormReportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SIGIVE - Reportes";
            tabReportes.ResumeLayout(false);
            tabVentas.ResumeLayout(false);
            tabVentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleVenta).EndInit();
            tabProductos.ResumeLayout(false);
            tabProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLimiteStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tabMasVendidos.ResumeLayout(false);
            tabMasVendidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMasVendidos).EndInit();
            tabClientes.ResumeLayout(false);
            tabClientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            tabDashboard.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnActualizar;
        private TabControl tabReportes;
        private TabPage tabVentas;
        private Label lblClienteFiltro;
        private ComboBox cboClienteFiltro;
        private DataGridView dgvVentas;
        private Label lblDetalleVenta;
        private DataGridView dgvDetalleVenta;
        private Label lblTotalVentas;
        private TabPage tabProductos;
        private CheckBox chkBajoStock;
        private NumericUpDown nudLimiteStock;
        private Label lblLeyendaStock;
        private DataGridView dgvProductos;
        private Label lblTotalProductos;
        private TabPage tabMasVendidos;
        private Label lblInfoMasVendidos;
        private DataGridView dgvMasVendidos;
        private TabPage tabClientes;
        private Label lblInfoClientes;
        private DataGridView dgvClientes;
        private TabPage tabDashboard;
        private Label lblCantidadVentas;
        private Label lblTotalVendido;
        private Label lblTicketPromedio;
        private Label lblBajoStock;
        private SIGIVE.Views.Controles.GraficoBarras graficoVentasDia;
        private SIGIVE.Views.Controles.GraficoBarras graficoTopProductos;
        private Button btnExportarExcel;
        private Button btnExportarPdf;
        private Button btnCerrar;
    }
}
