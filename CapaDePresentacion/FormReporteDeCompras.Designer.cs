namespace CapaDePresentacion
{
    partial class FormReporteDeCompras
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFechaDeInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnDescargarExcel = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFechaDeFin = new System.Windows.Forms.DateTimePicker();
            this.cboProveedor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBusquedaProveedor = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.cboBusqueda = new System.Windows.Forms.ComboBox();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.FechaDeRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDeDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroDeDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDeUsuarioRegistrado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoDelProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RazonSocialDelProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoDelProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreDelProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioDeCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioDeVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 23);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(2, 35, 0, 0);
            this.label3.Size = new System.Drawing.Size(1569, 154);
            this.label3.TabIndex = 59;
            this.label3.Text = "Reporte De Compras";
            // 
            // txtFechaDeInicio
            // 
            this.txtFechaDeInicio.CustomFormat = "dd/MM/yyyy";
            this.txtFechaDeInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaDeInicio.Location = new System.Drawing.Point(251, 123);
            this.txtFechaDeInicio.Name = "txtFechaDeInicio";
            this.txtFechaDeInicio.Size = new System.Drawing.Size(157, 31);
            this.txtFechaDeInicio.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 196);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2, 35, 0, 0);
            this.label1.Size = new System.Drawing.Size(2113, 750);
            this.label1.TabIndex = 67;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaDeRegistro,
            this.TipoDeDocumento,
            this.NumeroDeDocumento,
            this.MontoTotal,
            this.TipoDeUsuarioRegistrado,
            this.DocumentoDelProveedor,
            this.RazonSocialDelProveedor,
            this.CodigoDelProducto,
            this.NombreDelProducto,
            this.Categoria,
            this.PrecioDeCompra,
            this.PrecioDeVenta,
            this.Cantidad,
            this.SubTotal});
            this.dgvData.Location = new System.Drawing.Point(57, 295);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 82;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(2065, 651);
            this.dgvData.TabIndex = 68;
            // 
            // btnDescargarExcel
            // 
            this.btnDescargarExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDescargarExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDescargarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDescargarExcel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDescargarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargarExcel.ForeColor = System.Drawing.Color.Black;
            this.btnDescargarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnDescargarExcel.IconColor = System.Drawing.Color.Black;
            this.btnDescargarExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDescargarExcel.IconSize = 35;
            this.btnDescargarExcel.Location = new System.Drawing.Point(72, 220);
            this.btnDescargarExcel.Name = "btnDescargarExcel";
            this.btnDescargarExcel.Size = new System.Drawing.Size(236, 55);
            this.btnDescargarExcel.TabIndex = 69;
            this.btnDescargarExcel.Text = "Descargar Excel";
            this.btnDescargarExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDescargarExcel.UseVisualStyleBackColor = false;
            this.btnDescargarExcel.Click += new System.EventHandler(this.btnDescargarExcel_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(76, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 31);
            this.label2.TabIndex = 70;
            this.label2.Text = "Fecha de Inicio:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(433, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 31);
            this.label4.TabIndex = 72;
            this.label4.Text = "Fecha de Fin:";
            // 
            // txtFechaDeFin
            // 
            this.txtFechaDeFin.CustomFormat = "dd/MM/yyyy";
            this.txtFechaDeFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaDeFin.Location = new System.Drawing.Point(588, 123);
            this.txtFechaDeFin.Name = "txtFechaDeFin";
            this.txtFechaDeFin.Size = new System.Drawing.Size(168, 31);
            this.txtFechaDeFin.TabIndex = 71;
            // 
            // cboProveedor
            // 
            this.cboProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProveedor.FormattingEnabled = true;
            this.cboProveedor.Location = new System.Drawing.Point(954, 123);
            this.cboProveedor.Name = "cboProveedor";
            this.cboProveedor.Size = new System.Drawing.Size(345, 33);
            this.cboProveedor.TabIndex = 74;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(819, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 31);
            this.label5.TabIndex = 73;
            this.label5.Text = "Proveedor:";
            // 
            // btnBusquedaProveedor
            // 
            this.btnBusquedaProveedor.BackColor = System.Drawing.Color.White;
            this.btnBusquedaProveedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBusquedaProveedor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBusquedaProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBusquedaProveedor.ForeColor = System.Drawing.Color.White;
            this.btnBusquedaProveedor.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBusquedaProveedor.IconColor = System.Drawing.Color.Black;
            this.btnBusquedaProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBusquedaProveedor.IconSize = 35;
            this.btnBusquedaProveedor.Location = new System.Drawing.Point(1305, 112);
            this.btnBusquedaProveedor.Name = "btnBusquedaProveedor";
            this.btnBusquedaProveedor.Size = new System.Drawing.Size(61, 56);
            this.btnBusquedaProveedor.TabIndex = 75;
            this.btnBusquedaProveedor.UseVisualStyleBackColor = false;
            this.btnBusquedaProveedor.Click += new System.EventHandler(this.btnBusquedaProveedor_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.Black;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.Location = new System.Drawing.Point(2067, 219);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 56);
            this.btnLimpiar.TabIndex = 80;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscar.IconColor = System.Drawing.Color.Black;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 35;
            this.btnBuscar.Location = new System.Drawing.Point(2000, 219);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(61, 56);
            this.btnBuscar.TabIndex = 79;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cboBusqueda
            // 
            this.cboBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusqueda.FormattingEnabled = true;
            this.cboBusqueda.Location = new System.Drawing.Point(1267, 232);
            this.cboBusqueda.Name = "cboBusqueda";
            this.cboBusqueda.Size = new System.Drawing.Size(345, 33);
            this.cboBusqueda.TabIndex = 78;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(1633, 232);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(345, 31);
            this.txtBusqueda.TabIndex = 77;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1132, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 31);
            this.label6.TabIndex = 76;
            this.label6.Text = "Busca por:";
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
            // DocumentoDelProveedor
            // 
            this.DocumentoDelProveedor.HeaderText = "Documento del Proveedor";
            this.DocumentoDelProveedor.MinimumWidth = 10;
            this.DocumentoDelProveedor.Name = "DocumentoDelProveedor";
            this.DocumentoDelProveedor.ReadOnly = true;
            this.DocumentoDelProveedor.Width = 200;
            // 
            // RazonSocialDelProveedor
            // 
            this.RazonSocialDelProveedor.HeaderText = "Razon Social del Proveedor";
            this.RazonSocialDelProveedor.MinimumWidth = 10;
            this.RazonSocialDelProveedor.Name = "RazonSocialDelProveedor";
            this.RazonSocialDelProveedor.ReadOnly = true;
            this.RazonSocialDelProveedor.Width = 200;
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
            // PrecioDeCompra
            // 
            this.PrecioDeCompra.HeaderText = "Precio de Compra";
            this.PrecioDeCompra.MinimumWidth = 10;
            this.PrecioDeCompra.Name = "PrecioDeCompra";
            this.PrecioDeCompra.ReadOnly = true;
            this.PrecioDeCompra.Width = 200;
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
            // FormReporteDeCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2196, 982);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cboBusqueda);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBusquedaProveedor);
            this.Controls.Add(this.cboProveedor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFechaDeFin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDescargarExcel);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFechaDeInicio);
            this.Controls.Add(this.label3);
            this.Name = "FormReporteDeCompras";
            this.Text = "FormReporteDeCompras";
            this.Load += new System.EventHandler(this.FormReporteDeCompras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker txtFechaDeInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvData;
        private FontAwesome.Sharp.IconButton btnDescargarExcel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker txtFechaDeFin;
        private System.Windows.Forms.ComboBox cboProveedor;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconButton btnBusquedaProveedor;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.ComboBox cboBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaDeRegistro;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDeDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroDeDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDeUsuarioRegistrado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoDelProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn RazonSocialDelProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoDelProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreDelProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioDeCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioDeVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
    }
}