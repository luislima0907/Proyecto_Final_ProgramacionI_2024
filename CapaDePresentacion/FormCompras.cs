using CapaDeEntidad;
using CapaDeNegocio;
using CapaDePresentacion.Modales;
using CapaDePresentacion.Utilidades;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace CapaDePresentacion
{
    public partial class FormCompras : Form
    {
        // este objeto global almacenara a los usuarios que se registren en el login y tengan acceso al formulario de las compras
        private Usuario _Usuario;

        public FormCompras(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void FormCompras_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            // Cuando el estado del usuario devuelva false, se dira que no esta activo
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });

            // Con esto decimos que solo nos devuelva el texto almacenado en el objeto de la OpcionCombo en nuestro formulario de Compras
            cboTipoDocumento.DisplayMember = "Texto";

            // Con esto almacenamos el valor de true o false que nos devuelva la OpcionCombo en el formulario de Compras
            cboTipoDocumento.ValueMember = "Valor";

            // Con esto decimos que solo seleccione el primer elemento mostrado en el display del estado del formulario de Compras
            cboTipoDocumento.SelectedIndex = 0;

            // con esto le damos un formato a la fecha en la caja de texto de la fecha en nuestro formulario de compras
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtIdProveedor.Text = "0";
            txtIdProducto.Text = "0";

        }

        private void btnBusquedaProveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new MD_Proveedor())
            {
                var result = modal.ShowDialog();

                // si la seleccion del proveedor da un OK como resultado, entonces se agregaran los valores a las cajas de texto en el formulario
                if (result == DialogResult.OK)
                {
                    txtIdProveedor.Text = modal._Proveedor.IdProveedor.ToString();
                    txtDocumentoDelProveedor.Text = modal._Proveedor.Documento;
                    txtNombreDelProveedor.Text = modal._Proveedor.RazonSocial;
                }
                else txtDocumentoDelProveedor.Select();
            }
        }

        private void btnBusquedaProducto_Click(object sender, EventArgs e)
        {

            using (var modal = new MD_Producto())
            {
                var result = modal.ShowDialog();

                // si la seleccion del proveedor da un OK como resultado, entonces se agregaran los valores a las cajas de texto en el formulario
                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._Producto.IdProducto.ToString();
                    txtCodigoProducto.Text = modal._Producto.Codigo;
                    txtProducto.Text = modal._Producto.Nombre;
                    txtPrecioDeCompra.Select();
                }
                else txtCodigoProducto.Select();
            }
        }

        private void txtCodigoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCodigoProducto.Text && p.Estado == true).FirstOrDefault();


                if (oProducto != null) 
                {
                    txtCodigoProducto.BackColor = Color.Honeydew;
                    txtIdProducto.Text = oProducto.IdProducto.ToString();
                    txtProducto.Text = oProducto.Nombre;
                    txtPrecioDeCompra.Select();
                }
                else
                {
                    txtCodigoProducto.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtProducto.Text = "";
                    //txtPrecioDeCompra.Select();
                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            decimal precioDeCompra = 0;
            decimal precioDeVenta = 0;

            bool existeElProducto = false;

            // si no hay ningun producto aparecera un mensaje
            if (int.Parse(txtIdProducto.Text) == 0) 
            {
                MessageBox.Show("Debe seleccionar un producto o buscarlo por su codigo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Con estas condicionales le decimos que trate de pasar a decimal el precio de compra y de venta que ingrese el usario en las cajas de texto
            if (!decimal.TryParse(txtPrecioDeCompra.Text, out precioDeCompra)) 
            {
                MessageBox.Show("Este formato es incorrecto para el precio de compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrecioDeCompra.Select();
                return;
            }

            if (!decimal.TryParse(txtPrecioDeVenta.Text, out precioDeVenta))
            {
                MessageBox.Show("Este formato es incorrexto para el precio de venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioDeVenta.Select();
                return;
            }

            // recorremos nuestras tablas en el formulario para buscar los productos para ver si lo encuentra o no
            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    existeElProducto = true;
                    break;
                }
            }

            if (!existeElProducto) 
            {
                dgvData.Rows.Add(new object[]
                {
                    txtIdProducto.Text,
                    txtProducto.Text,
                    precioDeCompra.ToString("0.00"),
                    precioDeVenta.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (txtCantidad.Value * precioDeCompra).ToString("0.00")
                });
                CalcularTotal();
                LimpiarProducto();
                txtCodigoProducto.Select();
            }
        }

        private void LimpiarProducto()
        {
            txtIdProducto.Text = "0";
            txtCodigoProducto.Text = "";
            txtCodigoProducto.BackColor = Color.White;
            txtProducto.Text = "";
            txtPrecioDeCompra.Text = "";
            txtPrecioDeVenta.Text = "";
            txtCantidad.Value = 1;
        }

        private void CalcularTotal()
        {
            decimal total = 0;
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvData.Rows)
                {
                    total += Convert.ToDecimal(fila.Cells["SubTotal"].Value.ToString());
                }
            }
            txtTotalAPagar.Text = total.ToString("0.00");
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Hacemos esta condicional para que no tome en cuenta el encabezado de la tabla
            if (e.RowIndex < 0) return;

            // Implementamos el icono que tengamos en nuestros archivos y le damos sus respectivas dimensiones
            if (e.ColumnIndex == 6)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // variables con las dimensiones del icono que acompañara a la fila con la información del usuario.
                var ancho = Properties.Resources.trash25.Width;
                var alto = Properties.Resources.trash25.Height;
                var posicionEnX = e.CellBounds.Left + (e.CellBounds.Width - ancho) / 2;
                var posicionEnY = e.CellBounds.Top + (e.CellBounds.Height - alto) / 2;

                // Una vez dadas las dimensiones, ponemos el icono en la celda que indicamos
                e.Graphics.DrawImage(Properties.Resources.trash25, new Rectangle(posicionEnX, posicionEnY, ancho, alto));

                // Que al momento de darle click no se quede seleccionado para siempre.
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se activa cuando hacemos click en el boton que contiene el icono para seleccionar una compra
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                // ya que se comienza a contar desde la primera fila de la tabla sin contar el encabezado se inicia en el indice 0
                if (indice >= 0)
                {
                    // despues de encontrar la fila seleccionada para borrarla, se vuelve a calcular el total para la anterior fila si lo hubiera
                    dgvData.Rows.RemoveAt(indice);
                    CalcularTotal();
                }
            }
        }

        private void txtPrecioDeCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            // si el usuario escribe un numero en la caja de texto entonces es valido
            if (Char.IsDigit(e.KeyChar)) e.Handled = false; // se invierte el valor si la condicion se cumple
            else
            {
                // Si no tiene nada la caja de texto e inicia con un punto no es valido
                if (txtPrecioDeCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".") e.Handled = true;
                else
                {
                    // si escribio un numero, entonces se le permite usar la tecla de borrar y el punto
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".") e.Handled = false;
                    else e.Handled = true;
                }
            }
        }

        private void txtPrecioDeVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            // si el usuario escribe un numero en la caja de texto entonces es valido
            if (Char.IsDigit(e.KeyChar)) e.Handled = false; // se invierte el valor si la condicion se cumple
            else
            {
                // Si no tiene nada la caja de texto e inicia con un punto no es valido
                if (txtPrecioDeVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".") e.Handled = true;
                else
                {
                    // si escribio un numero, entonces se le permite usar la tecla de borrar y el punto
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".") e.Handled = false;
                    else e.Handled = true;
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // si no hay ningun proveedor entonces no se registra la compra
            if (Convert.ToInt32(txtIdProveedor.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // Si no hay productos en la compra no se registra
            if (dgvData.Rows.Count < 1) 
            {
                MessageBox.Show("Debe ingresar productos a la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalle_compra = new DataTable();

            detalle_compra.Columns.Add("IdProducto", typeof(int));
            detalle_compra.Columns.Add("PrecioDeCompra", typeof(decimal));
            detalle_compra.Columns.Add("PrecioDeVenta", typeof(decimal));
            detalle_compra.Columns.Add("Cantidad", typeof(int));
            detalle_compra.Columns.Add("MontoTotal", typeof(decimal));


            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                detalle_compra.Rows.Add(new object[]
                {
                    Convert.ToInt32(fila.Cells["IdProducto"].Value.ToString()),
                    fila.Cells["PrecioDeCompra"].Value.ToString(),
                    fila.Cells["PrecioDeVenta"].Value.ToString(),
                    fila.Cells["Cantidad"].Value.ToString(),
                    fila.Cells["SubTotal"].Value.ToString()
                });
            }
            int IdCorrelativo = new CN_Compra().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", IdCorrelativo);

            Compra oCompra = new Compra()
            {
                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario},
                oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(txtIdProveedor.Text)},
                TipoDeDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                NumeroDeDocumento = numeroDocumento,
                MontoTotal = Convert.ToDecimal(txtTotalAPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compra().RegistrarCompra(oCompra, detalle_compra, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show($"Numero de compra generada:\n{numeroDocumento}\n\n¿Desea copiar al portapapeles?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes) Clipboard.SetText(numeroDocumento);

                txtIdProveedor.Text = "0";
                txtDocumentoDelProveedor.Text = "";
                txtNombreDelProveedor.Text = "";
                dgvData.Rows.Clear();
                CalcularTotal();
            }
            else MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
