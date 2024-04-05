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
    public partial class MD_Proveedor : Form
    {

        public Proveedor _Proveedor {  get; set; }
        public MD_Proveedor()
        {
            InitializeComponent();
        }

        private void MD_Proveedor_Load(object sender, EventArgs e)
        {

            foreach (DataGridViewColumn columnas in dgvData.Columns)
            {
                if (columnas.Visible == true) cboBusqueda.Items.Add(new OpcionCombo() { Valor = columnas.Name, Texto = columnas.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Con esto mostramos a los Proveedors que tengamos en nuestra base de datos en el formulario de Proveedors al momento de iniciarlo
            List<Proveedor> ListaProveedors = new CN_Proveedor().Listar();

            foreach (Proveedor item in ListaProveedors)
            {
                dgvData.Rows.Add(new object[] {item.IdProveedor,item.Documento,item.RazonSocial});
            }
        }

        // este evento se llama cuando hacemos doble click a una celda en una tabla
        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indiceFila = e.RowIndex;
            int indiceColumna = e.ColumnIndex;

            if (indiceFila >= 0 && indiceColumna > 0)
            {
                _Proveedor = new Proveedor()
                {
                    IdProveedor = Convert.ToInt32(dgvData.Rows[indiceFila].Cells["Id"].Value.ToString()),
                    Documento = dgvData.Rows[indiceFila].Cells["Documento"].Value.ToString(),
                    RazonSocial = dgvData.Rows[indiceFila].Cells["RazonSocial"].Value.ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            } 
        }

        private void btnBuscar_Click(object sender, EventArgs e)
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

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
