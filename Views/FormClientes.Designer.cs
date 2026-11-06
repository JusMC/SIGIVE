namespace SIGIVE.Views
{
    partial class FormClientes
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
            lblNombre = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            txtNombre = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            btnAgregar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnVerInfo = new Button();
            btnLimpiar = new Button();
            dgvClientes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 23);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(64, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            //
            // lblTelefono
            //
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(20, 63);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(70, 20);
            lblTelefono.TabIndex = 2;
            lblTelefono.Text = "Teléfono:";
            //
            // lblCorreo
            //
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(20, 103);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(57, 20);
            lblCorreo.TabIndex = 4;
            lblCorreo.Text = "Correo:";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(100, 20);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(260, 27);
            txtNombre.TabIndex = 1;
            //
            // txtTelefono
            //
            txtTelefono.Location = new Point(100, 60);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(260, 27);
            txtTelefono.TabIndex = 3;
            //
            // txtCorreo
            //
            txtCorreo.Location = new Point(100, 100);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(260, 27);
            txtCorreo.TabIndex = 5;
            //
            // btnAgregar
            //
            btnAgregar.Location = new Point(400, 18);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(130, 32);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            //
            // btnEditar
            //
            btnEditar.Location = new Point(400, 58);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(130, 32);
            btnEditar.TabIndex = 7;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            //
            // btnEliminar
            //
            btnEliminar.Location = new Point(400, 98);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(130, 32);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            //
            // btnVerInfo
            //
            btnVerInfo.Location = new Point(550, 18);
            btnVerInfo.Name = "btnVerInfo";
            btnVerInfo.Size = new Size(130, 32);
            btnVerInfo.TabIndex = 9;
            btnVerInfo.Text = "Ver información";
            btnVerInfo.UseVisualStyleBackColor = true;
            btnVerInfo.Click += btnVerInfo_Click;
            //
            // btnLimpiar
            //
            btnLimpiar.Location = new Point(550, 58);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(130, 32);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            //
            // dgvClientes
            //
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(20, 150);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(660, 280);
            dgvClientes.TabIndex = 11;
            dgvClientes.CellClick += dgvClientes_CellClick;
            //
            // FormClientes
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 450);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Controls.Add(btnAgregar);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnVerInfo);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvClientes);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormClientes";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SIGIVE - Clientes";
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombre;
        private Label lblTelefono;
        private Label lblCorreo;
        private TextBox txtNombre;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnVerInfo;
        private Button btnLimpiar;
        private DataGridView dgvClientes;
    }
}
