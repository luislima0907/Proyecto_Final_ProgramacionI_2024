namespace CapaDePresentacion
{
    partial class FormReporteDeVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.cboBusqueda = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBusquedaPorFecha = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFechaDeFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDescargarExcel = new FontAwesome.Sharp.IconButton();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.FechaDeRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDeDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroDeDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDeUsuarioRegistrado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoDelCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreDelCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoDelProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreDelProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioDeVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFechaDeInicio = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Indigo;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLimpiar.FlatAppearance.BorderSize = 3;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.White;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.Location = new System.Drawing.Point(2050, 233);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 56);
            this.btnLimpiar.TabIndex = 96;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Indigo;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBuscar.FlatAppearance.BorderSize = 3;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscar.IconColor = System.Drawing.Color.White;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 35;
            this.btnBuscar.Location = new System.Drawing.Point(1983, 233);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(61, 56);
            this.btnBuscar.TabIndex = 95;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cboBusqueda
            // 
            this.cboBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBusqueda.FormattingEnabled = true;
            this.cboBusqueda.Location = new System.Drawing.Point(1250, 246);
            this.cboBusqueda.Name = "cboBusqueda";
            this.cboBusqueda.Size = new System.Drawing.Size(345, 63);
            this.cboBusqueda.TabIndex = 94;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Indigo;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1100, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 31);
            this.label6.TabIndex = 92;
            this.label6.Text = "Busca por:";
            // 
            // btnBusquedaPorFecha
            // 
            this.btnBusquedaPorFecha.BackColor = System.Drawing.Color.Indigo;
            this.btnBusquedaPorFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBusquedaPorFecha.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBusquedaPorFecha.FlatAppearance.BorderSize = 3;
            this.btnBusquedaPorFecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBusquedaPorFecha.ForeColor = System.Drawing.Color.White;
            this.btnBusquedaPorFecha.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBusquedaPorFecha.IconColor = System.Drawing.Color.White;
            this.btnBusquedaPorFecha.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBusquedaPorFecha.IconSize = 35;
            this.btnBusquedaPorFecha.Location = new System.Drawing.Point(809, 126);
            this.btnBusquedaPorFecha.Name = "btnBusquedaPorFecha";
            this.btnBusquedaPorFecha.Size = new System.Drawing.Size(61, 56);
            this.btnBusquedaPorFecha.TabIndex = 91;
            this.btnBusquedaPorFecha.UseVisualStyleBackColor = false;
            this.btnBusquedaPorFecha.Click += new System.EventHandler(this.btnBusquedaPorFecha_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Indigo;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(449, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 31);
            this.label4.TabIndex = 88;
            this.label4.Text = "Fecha de Fin:";
            // 
            // txtFechaDeFin
            // 
            this.txtFechaDeFin.CustomFormat = "dd/MM/yyyy";
            this.txtFechaDeFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaDeFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaDeFin.Location = new System.Drawing.Point(635, 137);
            this.txtFechaDeFin.Name = "txtFechaDeFin";
            this.txtFechaDeFin.Size = new System.Drawing.Size(168, 62);
            this.txtFechaDeFin.TabIndex = 87;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Indigo;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(44, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 31);
            this.label2.TabIndex = 86;
            this.label2.Text = "Fecha de Inicio:";
            // 
            // btnDescargarExcel
            // 
            this.btnDescargarExcel.AutoSize = true;
            this.btnDescargarExcel.BackColor = System.Drawing.Color.Indigo;
            this.btnDescargarExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDescargarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDescargarExcel.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnDescargarExcel.FlatAppearance.BorderSize = 3;
            this.btnDescargarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargarExcel.ForeColor = System.Drawing.Color.White;
            this.btnDescargarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnDescargarExcel.IconColor = System.Drawing.Color.White;
            this.btnDescargarExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDescargarExcel.IconSize = 35;
            this.btnDescargarExcel.Location = new System.Drawing.Point(55, 234);
            this.btnDescargarExcel.Name = "btnDescargarExcel";
            this.btnDescargarExcel.Size = new System.Drawing.Size(301, 55);
            this.btnDescargarExcel.TabIndex = 85;
            this.btnDescargarExcel.Text = "Descargar Excel";
            this.btnDescargarExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDescargarExcel.UseVisualStyleBackColor = false;
            this.btnDescargarExcel.Click += new System.EventHandler(this.btnDescargarExcel_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(1616, 246);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(345, 62);
            this.txtBusqueda.TabIndex = 93;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvData.BackgroundColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkOrchid;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Orchid;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaDeRegistro,
            this.TipoDeDocumento,
            this.NumeroDeDocumento,
            this.MontoTotal,
            this.TipoDeUsuarioRegistrado,
            this.DocumentoDelCliente,
            this.NombreDelCliente,
            this.CodigoDelProducto,
            this.NombreDelProducto,
            this.Categoria,
            this.PrecioDeVenta,
            this.Cantidad,
            this.SubTotal});
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.Location = new System.Drawing.Point(40, 309);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MediumOrchid;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.RowHeadersWidth = 82;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(2065, 651);
            this.dgvData.TabIndex = 84;
            // 
            // FechaDeRegistro
            // 
            this.FechaDeRegistro.HeaderText = "Fecha de Registro";
            this.FechaDeRegistro.MinimumWidth = 10;
            this.FechaDeRegistro.Name = "FechaDeRegistro";
            this.FechaDeRegistro.ReadOnly = true;
            this.FechaDeRegistro.Width = 200;
            // 
            // TipoDeDocumento
            // 
            this.TipoDeDocumento.HeaderText = "Tipo de Documento";
            this.TipoDeDocumento.MinimumWidth = 10;
            this.TipoDeDocumento.Name = "TipoDeDocumento";
            this.TipoDeDocumento.ReadOnly = true;
            this.TipoDeDocumento.Width = 180;
            // 
            // NumeroDeDocumento
            // 
            this.NumeroDeDocumento.HeaderText = "Numero de Documento";
            this.NumeroDeDocumento.MinimumWidth = 10;
            this.NumeroDeDocumento.Name = "NumeroDeDocumento";
            this.NumeroDeDocumento.ReadOnly = true;
            this.NumeroDeDocumento.Width = 200;
            // 
            // MontoTotal
            // 
            this.MontoTotal.HeaderText = "Monto Total";
            this.MontoTotal.MinimumWidth = 10;
            this.MontoTotal.Name = "MontoTotal";
            this.MontoTotal.ReadOnly = true;
            this.MontoTotal.Width = 200;
            // 
            // TipoDeUsuarioRegistrado
            // 
            this.TipoDeUsuarioRegistrado.HeaderText = "Tipo de Usuario Registrado";
            this.TipoDeUsuarioRegistrado.MinimumWidth = 10;
            this.TipoDeUsuarioRegistrado.Name = "TipoDeUsuarioRegistrado";
            this.TipoDeUsuarioRegistrado.ReadOnly = true;
            this.TipoDeUsuarioRegistrado.Width = 200;
            // 
            // DocumentoDelCliente
            // 
            this.DocumentoDelCliente.HeaderText = "Documento del Cliente";
            this.DocumentoDelCliente.MinimumWidth = 10;
            this.DocumentoDelCliente.Name = "DocumentoDelCliente";
            this.DocumentoDelCliente.ReadOnly = true;
            this.DocumentoDelCliente.Width = 200;
            // 
            // NombreDelCliente
            // 
            this.NombreDelCliente.HeaderText = "Nombre del Cliente";
            this.NombreDelCliente.MinimumWidth = 10;
            this.NombreDelCliente.Name = "NombreDelCliente";
            this.NombreDelCliente.ReadOnly = true;
            this.NombreDelCliente.Width = 200;
            // 
            // CodigoDelProducto
            // 
            this.CodigoDelProducto.HeaderText = "Codigo del Producto";
            this.CodigoDelProducto.MinimumWidth = 10;
            this.CodigoDelProducto.Name = "CodigoDelProducto";
            this.CodigoDelProducto.ReadOnly = true;
            this.CodigoDelProducto.Width = 200;
            // 
            // NombreDelProducto
            // 
            this.NombreDelProducto.HeaderText = "Nombre del Producto";
            this.NombreDelProducto.MinimumWidth = 10;
            this.NombreDelProducto.Name = "NombreDelProducto";
            this.NombreDelProducto.ReadOnly = true;
            this.NombreDelProducto.Width = 200;
            // 
            // Categoria
            // 
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.MinimumWidth = 10;
            this.Categoria.Name = "Categoria";
            this.Categoria.ReadOnly = true;
            this.Categoria.Width = 200;
            // 
            // PrecioDeVenta
            // 
            this.PrecioDeVenta.HeaderText = "Precio de Venta";
            this.PrecioDeVenta.MinimumWidth = 10;
            this.PrecioDeVenta.Name = "PrecioDeVenta";
            this.PrecioDeVenta.ReadOnly = true;
            this.PrecioDeVenta.Width = 200;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 10;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 200;
            // 
            // SubTotal
            // 
            this.SubTotal.HeaderText = "SubTotal";
            this.SubTotal.MinimumWidth = 10;
            this.SubTotal.Name = "SubTotal";
            this.SubTotal.ReadOnly = true;
            this.SubTotal.Width = 200;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Indigo;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 210);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2, 35, 0, 0);
            this.label1.Size = new System.Drawing.Size(2113, 750);
            this.label1.TabIndex = 83;
            // 
            // txtFechaDeInicio
            // 
            this.txtFechaDeInicio.CustomFormat = "dd/MM/yyyy";
            this.txtFechaDeInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaDeInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaDeInicio.Location = new System.Drawing.Point(256, 140);
            this.txtFechaDeInicio.Name = "txtFechaDeInicio";
            this.txtFechaDeInicio.Size = new System.Drawing.Size(157, 62);
            this.txtFechaDeInicio.TabIndex = 82;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Indigo;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(35, 37);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.label3.Size = new System.Drawing.Size(1569, 154);
            this.label3.TabIndex = 81;
            this.label3.Text = "Reporte de Ventas";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormReporteDeVentas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(2170, 1020);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cboBusqueda);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBusquedaPorFecha);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFechaDeFin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDescargarExcel);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFechaDeInicio);
            this.Controls.Add(this.label3);
            this.Name = "FormReporteDeVentas";
            this.Text = "FormReporteDeVentas";
            this.Load += new System.EventHandler(this.FormReporteDeVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.ComboBox cboBusqueda;
        private System.Windows.Forms.Label label6;
        private FontAwesome.Sharp.IconButton btnBusquedaPorFecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker txtFechaDeFin;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnDescargarExcel;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txtFechaDeInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaDeRegistro;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDeDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroDeDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDeUsuarioRegistrado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoDelCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreDelCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoDelProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreDelProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioDeVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
    }
}