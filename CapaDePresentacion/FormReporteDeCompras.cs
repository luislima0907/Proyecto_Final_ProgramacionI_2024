using CapaDeEntidad;
using CapaDeNegocio;
using CapaDePresentacion.Utilidades;
using ClosedXML.Excel;
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
    public partial class FormReporteDeCompras : Form
    {
        public FormReporteDeCompras()
        {
            InitializeComponent();
        }

        private void FormReporteDeCompras_Load(object sender, EventArgs e)
        {
            List<Proveedor> listaDeProveedores = new CN_Proveedor().Listar();

            // aqui estamos diciendo que por defecto cuando cargue el formulario, que se le den todos los proveedores a la hora de buscarlos
            cboProveedor.Items.Add(new OpcionCombo() { Valor = 0, Texto = "TODOS" });

            foreach (Proveedor item in listaDeProveedores) 
            {
                cboProveedor.Items.Add(new OpcionCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial});
            }
            cboProveedor.DisplayMember = "Texto";
            cboProveedor.ValueMember = "Valor";
            cboProveedor.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
        }

        private void btnBusquedaProveedor_Click(object sender, EventArgs e)
        {
            int idProveedor = Convert.ToInt32(((OpcionCombo)cboProveedor.SelectedItem).Valor.ToString());

            List<ReporteDeCompras> listaDeCompras = new List<ReporteDeCompras>();

            listaDeCompras = new CN_Reporte().Compra(
                txtFechaDeInicio.Value.ToString("dd/MM/yyyy"),
                txtFechaDeFin.Value.ToString("dd/MM/yyyy"),
                idProveedor
                );

            dgvData.Rows.Clear();

            foreach (ReporteDeCompras reporteDeCompra in listaDeCompras)
            {
                dgvData.Rows.Add(new object[]
                {
                    reporteDeCompra.FechaDeRegistro,
                    reporteDeCompra.TipoDeDocumento,
                    reporteDeCompra.NumeroDeDocumento,
                    reporteDeCompra.MontoTotal,
                    reporteDeCompra.TipoDeUsuarioRegistrado,
                    reporteDeCompra.DocumentoDelProveedor,
                    reporteDeCompra.RazonSocialDelProveedor,
                    reporteDeCompra.CodigoDelProducto,
                    reporteDeCompra.NombreDelProducto,
                    reporteDeCompra.Categoria,
                    reporteDeCompra.PrecioDeCompra,
                    reporteDeCompra.PrecioDeVenta,
                    reporteDeCompra.Cantidad,
                    reporteDeCompra.SubTotal
                });
            }
        }

        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            // aqui estamos diciendo que si no hay ninguna fila por mostrar en el formulario, entonces que no haga la exportacion
            if (dgvData.Rows.Count < 1) MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // sino, inicializamos nuestras tablas en el formulario para obtener su informacion y asi poder exportarla
                DataTable dataTable = new DataTable();

                // recorremos cada columna de nuestra tabla en el formulario 
                foreach (DataGridViewColumn columna in dgvData.Columns)
                {
                    // la columna no tiene que estar vacia y a su vez tiene que estar visible para poder moverlas al excel
                    if (columna.HeaderText != "" && columna.Visible) dataTable.Columns.Add(columna.HeaderText, typeof(string));
                }

                // recorremos cada fila de nuestra tabla en el formulario
                foreach (DataGridViewRow fila in dgvData.Rows)
                {
                    if (fila.Visible)
                    {
                        // creamos un objeto que sirva como un array para almacenar los valores que tengan las filas y las ponemos en formato de texto
                        dataTable.Rows.Add(new object[]
                        {
                            fila.Cells[0].Value.ToString(),
                            fila.Cells[1].Value.ToString(),
                            fila.Cells[2].Value.ToString(),
                            fila.Cells[3].Value.ToString(),
                            fila.Cells[4].Value.ToString(),
                            fila.Cells[5].Value.ToString(),
                            fila.Cells[6].Value.ToString(),
                            fila.Cells[7].Value.ToString(),
                            fila.Cells[8].Value.ToString(),
                            fila.Cells[9].Value.ToString(),
                            fila.Cells[10].Value.ToString(),
                            fila.Cells[11].Value.ToString(),
                            fila.Cells[12].Value.ToString(),
                            fila.Cells[13].Value.ToString(),
                        });
                    }
                }

                // inicializamos este objeto para poder guardar archivos en cualquier parte de la pc con la ayuda del nombre
                SaveFileDialog guardarArchivo = new SaveFileDialog();
                // le damos el formato por defecto que tendra el nombre del archivo al momento de quererlo guardar
                guardarArchivo.FileName = string.Format($"Reporte_de_Compras_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx");

                // que busque por defecto todos los archivos que terminen con esa extension
                guardarArchivo.Filter = "Excel Files | *.xlsx";

                // con esta validacion logramos guardar los cambios en excel
                if (guardarArchivo.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook xLWorkbook = new XLWorkbook();
                        var hoja = xLWorkbook.Worksheets.Add(dataTable, "Registro_de_compras");
                        hoja.ColumnsUsed().AdjustToContents();
                        xLWorkbook.SaveAs(guardarArchivo.FileName);
                        MessageBox.Show("Reporte de compras generado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al generar el reporte de las compras", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                // Iteramos y encontramos las filas que esten dentro de nuestro formulario de reporte de compras para acceder a sus datos por medio del ComboBox
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    // Hacemos una validacion que nos permita diferenciar entre mayusculas y minusculas o tambien entre palabras completas o letras
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
