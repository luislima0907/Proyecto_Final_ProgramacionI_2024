using CapaDeEntidad;
using CapaDeNegocio;
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
    public partial class FormNegocio : Form
    {
        public FormNegocio()
        {
            InitializeComponent();
        }

        public Image DeByteAImagen(byte[] imagenBytes)
        {
            // este objeto nos permite guardar imagenes en memoria
            MemoryStream ms = new MemoryStream();
            ms.Write(imagenBytes,0,imagenBytes.Length);

            Image imagen = new Bitmap(ms);

            return imagen;
        }


        private void FormNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteImagen = new CN_Negocio().ObtenerLogo(out obtenido);

            if (obtenido) picLogo.Image = DeByteAImagen(byteImagen);

            Negocio datos = new CN_Negocio().ObtenerDatos();

            txtNombreDelNegocio.Text = datos.Nombre;
            txtRUC.Text = datos.RUC;
            txtDireccion.Text = datos.Direccion;

        }

        private void btnSubirLogo_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.FileName = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK) 
            {
                byte[] byteImagen = File.ReadAllBytes(oOpenFileDialog.FileName);
                bool respuesta = new CN_Negocio().ActualizarLogo(byteImagen, out mensaje);

                if (respuesta) picLogo.Image = DeByteAImagen(byteImagen);
                else MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Negocio objNegocio = new Negocio()
            {
                Nombre = txtNombreDelNegocio.Text,
                RUC = txtRUC.Text,
                Direccion = txtDireccion.Text
            };

            bool respuesta = new CN_Negocio().GuardarDatos(objNegocio, out mensaje);

            if (respuesta) MessageBox.Show("Los datos del negocio fueron actualizados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("No se pudieron actualizar los datos del negocio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}
