using CapaDeEntidad;
using CapaDeNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDePresentacion
{
    public partial class FormDetalleDeLaVenta : Form
    {
        public FormDetalleDeLaVenta()
        {
            InitializeComponent();
        }

        private void FormDetalleDeLaVenta_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // obtenemos una Venta por medio de la caja de texto de la busqueda
            Venta oVenta = new CN_Venta().ObtenerVenta(txtBusqueda.Text);

            // si encuentra una Venta, mostrara los datos de la Venta en sus cajas de texto
            if (oVenta.IdVenta != 0)
            {
                txtNumeroDeDocumento.Text = oVenta.NumeroDeDocumento;
                txtFecha.Text = oVenta.FechaDeRegistro;
                txtTipoDeDocumento.Text = oVenta.TipoDeDocumento;
                txtUsuario.Text = oVenta.oUsuario.NombreCompleto;
                txtDocumentoDelCliente.Text = oVenta.DocumentoDelCliente;
                txtNombreDelCliente.Text = oVenta.NombreDelCliente;

                // antes de agregar la informacion de la Venta al formulario, se limpia
                dgvData.Rows.Clear();

                //iteramos todo lo que contenga nuestra Venta
                foreach (Detalle_Venta detalle_Venta in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[] { detalle_Venta.oProducto.Nombre, detalle_Venta.PrecioDeVenta, detalle_Venta.Cantidad, detalle_Venta.SubTotal });
                }

                // definimos el formato que tendra nuestra caja de texto del monto total
                txtMontoTotal.Text = oVenta.MontoTotal.ToString("0.00");
                txtMontoDePago.Text = oVenta.MontoDePago.ToString("0.00");
                txtMontoDeCambio.Text = oVenta.MontoDeCambio.ToString("0.00");
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            txtFecha.Text = "";
            txtTipoDeDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocumentoDelCliente.Text = "";
            txtNombreDelCliente.Text = "";

            dgvData.Rows.Clear();
            txtMontoTotal.Text = "0.00";
            txtMontoDePago.Text = "0.00";
            txtMontoDeCambio.Text = "0.00";
            txtBusqueda.Select();
        }

        private void btnDescargarPDF_Click(object sender, EventArgs e)
        {

            if (txtNumeroDeDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // accedemos a nuestra plantilla html por medio de los recursos que subamos a la capa de presentacion
            string Texto_Html = Properties.Resources.PlantillaWebVenta.ToString();
            Negocio oDatos = new CN_Negocio().ObtenerDatos();

            // estructura del negocio en nuestro archivo html
            Texto_Html = Texto_Html.Replace("@nombreDelNegocio", oDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@documentoDelNegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direccionDelNegocio", oDatos.Direccion);

            // estructura de la compra en nuestro html
            Texto_Html = Texto_Html.Replace("@tipoDeDocumento", txtTipoDeDocumento.Text.ToUpper());
            Texto_Html = Texto_Html.Replace("@numeroDeDocumento", txtNumeroDeDocumento.Text);

            // estructura del proveedor en nuestro html
            Texto_Html = Texto_Html.Replace("@documentoDelCliente", txtDocumentoDelCliente.Text);
            Texto_Html = Texto_Html.Replace("@nombreDelCliente", txtNombreDelCliente.Text);
            Texto_Html = Texto_Html.Replace("@fechaDeRegistro", txtFecha.Text);
            Texto_Html = Texto_Html.Replace("@tipoDeUsuarioRegistrado", txtUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                // tr es una propiedad para crear filas en las tablas de un html
                filas += "<tr>";

                // td significa el dato que contenga la fila de una tabla
                filas += $"<td>{fila.Cells["Producto"].Value.ToString()}</td>";
                filas += $"<td>{fila.Cells["Precio"].Value.ToString()}</td>";
                filas += $"<td>{fila.Cells["Cantidad"].Value.ToString()}</td>";
                filas += $"<td>{fila.Cells["SubTotal"].Value.ToString()}</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montoTotal", txtMontoTotal.Text);
            Texto_Html = Texto_Html.Replace("@montoDePago", txtMontoDePago.Text);
            Texto_Html = Texto_Html.Replace("@montoDeCambio", txtMontoDeCambio.Text);

            // inicializamos este objeto para poder guardar archivos en cualquier parte de la pc con la ayuda del nombre
            SaveFileDialog guardarArchivo = new SaveFileDialog();

            // le damos el formato por defecto que tendra el nombre del archivo al momento de quererlo guardar
            guardarArchivo.FileName = string.Format($"Venta_{txtNumeroDeDocumento.Text}.pdf");

            // que busque por defecto todos los archivos que terminen con esa extension
            guardarArchivo.Filter = "Pdf Files|*.pdf";

            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardarArchivo.FileName, FileMode.Create))
                {
                    Document documentoPDF = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter pdfWriter = PdfWriter.GetInstance(documentoPDF, stream);
                    documentoPDF.Open();

                    bool obtenido = true;
                    byte[] byteImagen = new CN_Negocio().ObtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        iTextSharp.text.Image imagenLogo = iTextSharp.text.Image.GetInstance(byteImagen);
                        imagenLogo.ScaleToFit(60, 60);
                        imagenLogo.Alignment = iTextSharp.text.Image.UNDERLYING;
                        imagenLogo.SetAbsolutePosition(documentoPDF.Left, documentoPDF.GetTop(51));
                        documentoPDF.Add(imagenLogo);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, documentoPDF, sr);
                    }
                    documentoPDF.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
