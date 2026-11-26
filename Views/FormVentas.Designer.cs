namespace SIGIVE.Views
{
    partial class FormVentas
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
            lblCliente = new Label();
            lblProducto = new Label();
            lblCantidad = new Label();
            lblDetalle = new Label();
            lblTotal = new Label();
            lblVentas = new Label();
            cboCliente = new ComboBox();
            cboProducto = new ComboBox();
            nudCantidad = new NumericUpDown();
            btnAgregarProducto = new Button();
            btnQuitarProducto = new Button();
            btnNuevaVenta = new Button();
            btnRegistrarVenta = new Button();
            dgvDetalle = new DataGridView();
            dgvVentas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            SuspendLayout();
            //
            // lblCliente
            //
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(20, 23);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(58, 20);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Cliente:";
            //
            // cboCliente
            //
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCliente.Location = new Point(110, 20);
            cboCliente.Name = "cboCliente";
            cboCliente.Size = new Size(300, 28);
            cboCliente.TabIndex = 1;
            //
            // lblProducto
            //
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(20, 63);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(72, 20);
            lblProducto.TabIndex = 2;
            lblProducto.Text = "Producto:";
            //
            // cboProducto
            //
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProducto.Location = new Point(110, 60);
            cboProducto.Name = "cboProducto";
            cboProducto.Size = new Size(300, 28);
            cboProducto.TabIndex = 3;
            //
            // lblCantidad
            //
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(20, 103);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(72, 20);
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cantidad:";
            //
            // nudCantidad
            //
            nudCantidad.Location = new Point(110, 100);
            nudCantidad.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(100, 27);
            nudCantidad.TabIndex = 5;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnAgregarProducto
            //
            btnAgregarProducto.Location = new Point(440, 18);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(170, 32);
            btnAgregarProducto.TabIndex = 6;
            btnAgregarProducto.Text = "Agregar producto";
            btnAgregarProducto.UseVisualStyleBackColor = true;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            //
            // btnQuitarProducto
            //
            btnQuitarProducto.Location = new Point(440, 58);
            btnQuitarProducto.Name = "btnQuitarProducto";
            btnQuitarProducto.Size = new Size(170, 32);
            btnQuitarProducto.TabIndex = 7;
            btnQuitarProducto.Text = "Quitar producto";
            btnQuitarProducto.UseVisualStyleBackColor = true;
            btnQuitarProducto.Click += btnQuitarProducto_Click;
            //
            // btnNuevaVenta
            //
            btnNuevaVenta.Location = new Point(440, 98);
            btnNuevaVenta.Name = "btnNuevaVenta";
            btnNuevaVenta.Size = new Size(170, 32);
            btnNuevaVenta.TabIndex = 8;
            btnNuevaVenta.Text = "Nueva venta";
            btnNuevaVenta.UseVisualStyleBackColor = true;
            btnNuevaVenta.Click += btnNuevaVenta_Click;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(20, 145);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(140, 20);
            lblDetalle.TabIndex = 9;
            lblDetalle.Text = "Detalle de la venta:";
            //
            // dgvDetalle
            //
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(20, 170);
            dgvDetalle.MultiSelect = false;
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.Size = new Size(860, 200);
            dgvDetalle.TabIndex = 10;
            //
            // lblTotal
            //
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotal.Location = new Point(20, 385);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(110, 28);
            lblTotal.TabIndex = 11;
            lblTotal.Text = "Total: 0.00";
            //
            // btnRegistrarVenta
            //
            btnRegistrarVenta.Location = new Point(710, 380);
            btnRegistrarVenta.Name = "btnRegistrarVenta";
            btnRegistrarVenta.Size = new Size(170, 36);
            btnRegistrarVenta.TabIndex = 12;
            btnRegistrarVenta.Text = "Registrar venta";
            btnRegistrarVenta.UseVisualStyleBackColor = true;
            btnRegistrarVenta.Click += btnRegistrarVenta_Click;
            //
            // lblVentas
            //
            lblVentas.AutoSize = true;
            lblVentas.Location = new Point(20, 435);
            lblVentas.Name = "lblVentas";
            lblVentas.Size = new Size(140, 20);
            lblVentas.TabIndex = 13;
            lblVentas.Text = "Ventas registradas:";
            //
            // dgvVentas
            //
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(20, 460);
            dgvVentas.MultiSelect = false;
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.RowHeadersWidth = 51;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.Size = new Size(860, 220);
            dgvVentas.TabIndex = 14;
            //
            // FormVentas
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 700);
            Controls.Add(lblCliente);
            Controls.Add(cboCliente);
            Controls.Add(lblProducto);
            Controls.Add(cboProducto);
            Controls.Add(lblCantidad);
            Controls.Add(nudCantidad);
            Controls.Add(btnAgregarProducto);
            Controls.Add(btnQuitarProducto);
            Controls.Add(btnNuevaVenta);
            Controls.Add(lblDetalle);
            Controls.Add(dgvDetalle);
            Controls.Add(lblTotal);
            Controls.Add(btnRegistrarVenta);
            Controls.Add(lblVentas);
            Controls.Add(dgvVentas);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormVentas";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SIGIVE - Ventas";
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCliente;
        private Label lblProducto;
        private Label lblCantidad;
        private Label lblDetalle;
        private Label lblTotal;
        private Label lblVentas;
        private ComboBox cboCliente;
        private ComboBox cboProducto;
        private NumericUpDown nudCantidad;
        private Button btnAgregarProducto;
        private Button btnQuitarProducto;
        private Button btnNuevaVenta;
        private Button btnRegistrarVenta;
        private DataGridView dgvDetalle;
        private DataGridView dgvVentas;
    }
}
