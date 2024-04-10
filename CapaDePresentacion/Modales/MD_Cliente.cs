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
    public partial class MD_Cliente : Form
    {

        public Cliente _Cliente {  get; set; }
        public MD_Cliente()
        {
            InitializeComponent();
        }

        private void MD_Cliente_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columnas in dgvData.Columns)
            {
                if (columnas.Visible == true) cboBusqueda.Items.Add(new OpcionCombo() { Valor = columnas.Name, Texto = columnas.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Con esto mostramos a los Clientes que tengamos en nuestra base de datos en el formulario de Clientes al momento de iniciarlo
            List<Cliente> ListaClientes = new CN_Cliente().Listar();

            foreach (Cliente item in ListaClientes)
            {
                if (item.Estado) dgvData.Rows.Add(new object[] { item.Documento, item.NombreCompleto });
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indiceFila = e.RowIndex;
            int indiceColumna = e.ColumnIndex;

            if (indiceFila >= 0 && indiceColumna >= 0)
            {
                _Cliente = new Cliente()
                {
                    Documento = dgvData.Rows[indiceFila].Cells["Documento"].Value.ToString(),
                    NombreCompleto = dgvData.Rows[indiceFila].Cells["NombreCompleto"].Value.ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
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
