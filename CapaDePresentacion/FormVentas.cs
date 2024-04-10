using CapaDeEntidad;
using CapaDeNegocio;
using CapaDePresentacion.Modales;
using CapaDePresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDePresentacion
{
    public partial class FormVentas : Form
    {
        private Usuario _Usuario;
        public FormVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void FormVentas_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            // Cuando el estado del usuario devuelva false, se dira que no esta activo
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });

            // Con esto decimos que solo nos devuelva el texto almacenado en el objeto de la OpcionCombo en nuestro formulario de usuarios
            cboTipoDocumento.DisplayMember = "Texto";

            // Con esto almacenamos el valor de true o false que nos devuelva la OpcionCombo en el formulario de usuarios
            cboTipoDocumento.ValueMember = "Valor";

            // Con esto decimos que solo seleccione el primer elemento mostrado en el display del estado del formulario de usuarios.
            cboTipoDocumento.SelectedIndex = 0;

            // con esto le damos un formato a la fecha en la caja de texto de la fecha en nuestro formulario de Ventas
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdProducto.Text = "0";

            txtCantidadAPagar.Text = "";
            txtCambioDelPago.Text = "";
            txtTotalAPagar.Text = "0";
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (var modal = new MD_Cliente())
            {
                var result = modal.ShowDialog();

                // si la seleccion del proveedor da un OK como resultado, entonces se agregaran los valores a las cajas de texto en el formulario
                if (result == DialogResult.OK)
                {
                    txtNumeroDeDocumentoDelCliente.Text = modal._Cliente.Documento;
                    txtNombreDelCliente.Text = modal._Cliente.NombreCompleto;
                    txtCodigoProducto.Select();
                }
                else txtNumeroDeDocumentoDelCliente.Select();
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
                    txtNombreDelProducto.Text = modal._Producto.Nombre;
                    txtPrecioDeVenta.Text = modal._Producto.PrecioDeVenta.ToString("0.00");
                    txtStock.Text = modal._Producto.Stock.ToString();
                    txtCantidad.Select();
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
                    txtNombreDelProducto.Text = oProducto.Nombre;
                    txtPrecioDeVenta.Text = oProducto.PrecioDeVenta.ToString("0.00");
                    txtStock.Text = oProducto.Stock.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodigoProducto.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtNombreDelProducto.Text = "";
                    txtPrecioDeVenta.Text = "";
                    txtStock.Text = "";
                    txtCantidad.Value = 1;
                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {

            decimal precio = 0;
            bool existeElProducto = false;

            // si no hay ningun producto aparecera un mensaje
            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto o buscarlo por su codigo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Con estas condicionales le decimos que trate de pasar a decimal el precio de Venta y de venta que ingrese el usario en las cajas de texto
            if (!decimal.TryParse(txtPrecioDeVenta.Text, out precio))
            {
                MessageBox.Show("Este formato es incorrecto para el precio de Venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrecioDeVenta.Select();
                return;
            }

            // aca estamos diciendo de que si el stock que tenemos del producto es menor a la cantidad de productos a vender, entonces que no haga la venta
            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock del producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                bool respuesta = new CN_Venta().RestarStock(
                    Convert.ToInt32(txtIdProducto.Text),
                    Convert.ToInt32(txtCantidad.Value.ToString())
                );

                if (respuesta)
                {

                    dgvData.Rows.Add(new object[]
                    {
                    txtIdProducto.Text,
                    txtNombreDelProducto.Text,
                    precio.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (txtCantidad.Value * precio).ToString("0.00")
                    });
                    CalcularTotal();
                    LimpiarProducto();
                    txtCodigoProducto.Select();
                }
            }
        }

        private void LimpiarProducto()
        {
            txtIdProducto.Text = "0";
            txtCodigoProducto.Text = "";
            txtCodigoProducto.BackColor = Color.White;
            txtNombreDelProducto.Text = "";
            txtPrecioDeVenta.Text = "";
            txtStock.Text = "";
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
            if (e.ColumnIndex == 5)
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
            // Se activa cuando hacemos click en el boton que contiene el icono para seleccionar un usuario
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                // ya que se comienza a contar desde la primera fila de la tabla sin contar el encabezado se inicia en el indice 0
                if (indice >= 0)
                {
                    bool respuesta = new CN_Venta().SumarStock(
                        Convert.ToInt32(dgvData.Rows[indice].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvData.Rows[indice].Cells["Cantidad"].Value.ToString())
                    );

                    if (respuesta)
                    {
                        // despues de encontrar la fila seleccionada para borrarla, se vuelve a calcular el total para la anterior fila si lo hubiera
                        dgvData.Rows.RemoveAt(indice);
                        CalcularTotal();
                    }               
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

        private void txtCantidadAPagar_KeyPress(object sender, KeyPressEventArgs e)
        {

            // si el usuario escribe un numero en la caja de texto entonces es valido
            if (Char.IsDigit(e.KeyChar)) e.Handled = false; // se invierte el valor si la condicion se cumple
            else
            {
                // Si no tiene nada la caja de texto e inicia con un punto no es valido
                if (txtCantidadAPagar.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".") e.Handled = true;
                else
                {
                    // si escribio un numero, entonces se le permite usar la tecla de borrar y el punto
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".") e.Handled = false;
                    else e.Handled = true;
                }
            }
        }

        private void CalcularCambio()
        {
            if (txtTotalAPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal elClientePagaCon;
            decimal totalAPagar = Convert.ToDecimal(txtTotalAPagar.Text);

            if (txtCantidadAPagar.Text.Trim() == "")
            {
                txtCantidadAPagar.Text = "0";
            }

            if (decimal.TryParse(txtCantidadAPagar.Text.Trim(), out elClientePagaCon))
            {
                if (elClientePagaCon < totalAPagar)
                {
                    txtCambioDelPago.Text = "0.00";
                }
                else
                {
                    decimal cambioDelPago = elClientePagaCon - totalAPagar;
                    txtCambioDelPago.Text = cambioDelPago.ToString("0.00");
                }
            }
        }

        private void txtCantidadAPagar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) 
            {
                CalcularCambio();
            }
        }

        private void btnCrearVenta_Click(object sender, EventArgs e)
        {
            if (txtNumeroDeDocumentoDelCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar el documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtNombreDelCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar el nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Si no hay productos en el formulario de la venta entonces que no se haga
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalle_Venta = new DataTable();

            detalle_Venta.Columns.Add("IdProducto", typeof(int));
            detalle_Venta.Columns.Add("PrecioDeVenta", typeof(decimal));
            detalle_Venta.Columns.Add("Cantidad", typeof(int));
            detalle_Venta.Columns.Add("SubTotal", typeof(decimal));


            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                detalle_Venta.Rows.Add(new object[]
                {
                    fila.Cells["IdProducto"].Value.ToString(),
                    fila.Cells["Precio"].Value.ToString(),
                    fila.Cells["Cantidad"].Value.ToString(),
                    fila.Cells["SubTotal"].Value.ToString()
                });
            }

            int IdCorrelativo = new CN_Venta().ObtenerCorrelativo();

            // le damos el formato al numero de documento segun el idcorrelativo que se genere
            string numeroDocumento = string.Format("{0:00000}", IdCorrelativo);
            CalcularCambio();

            Venta oVenta = new Venta()
            {
                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                TipoDeDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                NumeroDeDocumento = numeroDocumento,
                DocumentoDelCliente = txtNumeroDeDocumentoDelCliente.Text,
                NombreDelCliente = txtNombreDelCliente.Text,
                MontoDePago = Convert.ToDecimal(txtCantidadAPagar.Text),
                MontoDeCambio = Convert.ToDecimal(txtCambioDelPago.Text),
                MontoTotal = Convert.ToDecimal(txtTotalAPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().RegistrarVenta(oVenta, detalle_Venta, out mensaje);

            if (respuesta)
            {
                // Creamos un mensaje al usuario para que le de el numero de venta generada para generar un reporte mas adelante
                var result = MessageBox.Show($"Numero de Venta generada:\n{numeroDocumento}\n\n¿Desea copiar al portapapeles?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // Si quizo guardar la venta se le copiara el numero de documento generado
                if (result == DialogResult.Yes) Clipboard.SetText(numeroDocumento);

                txtNumeroDeDocumentoDelCliente.Text = "";
                txtNombreDelCliente.Text = "";
                dgvData.Rows.Clear();
                CalcularTotal();
                txtCantidadAPagar.Text = "";
                txtCambioDelPago.Text = "";
            }
            else MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
