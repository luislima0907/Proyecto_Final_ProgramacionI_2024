using CapaDeEntidad;
using CapaDeNegocio;
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

namespace CapaDePresentacion.Modales
{
    public partial class MD_Producto : Form
    {
        // propiedad global
        public Producto _Producto { get; set; }
        public MD_Producto()
        {
            InitializeComponent();
        }

        private void MD_Producto_Load(object sender, EventArgs e)
        {

            foreach (DataGridViewColumn columnas in dgvData.Columns)
            {
                if (columnas.Visible == true) cboBusqueda.Items.Add(new OpcionCombo() { Valor = columnas.Name, Texto = columnas.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;


            // Con esto mostramos a los Productos que tengamos en nuestra base de datos en el formulario de Productos al momento de iniciarlo
            List<Producto> ListaProductos = new CN_Producto().Listar();

            foreach (Producto item in ListaProductos)
            {
                dgvData.Rows.Add(new object[] {
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.oCategoria.Descripcion,
                    item.Stock,
                    item.PrecioDeCompra,
                    item.PrecioDeVenta
                });
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int indiceFila = e.RowIndex;
            int indiceColumna = e.ColumnIndex;

            if (indiceFila >= 0 && indiceColumna > 0)
            {
                _Producto = new Producto()
                {
                    IdProducto = Convert.ToInt32(dgvData.Rows[indiceFila].Cells["Id"].Value.ToString()),
                    Codigo = dgvData.Rows[indiceFila].Cells["Codigo"].Value.ToString(),
                    Nombre = dgvData.Rows[indiceFila].Cells["Nombre"].Value.ToString(),
                    Stock = Convert.ToInt32(dgvData.Rows[indiceFila].Cells["Stock"].Value.ToString()),
                    PrecioDeCompra = Convert.ToDecimal(dgvData.Rows[indiceFila].Cells["PrecioDeCompra"].Value.ToString()),
                    PrecioDeVenta = Convert.ToDecimal(dgvData.Rows[indiceFila].Cells["PrecioDeVenta"].Value.ToString())
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }



        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                // Iteramos y encontramos las filas que esten dentro de nuestro formulario de usuarios para acceder a sus datos por medio del ComboBox
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

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
