using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDePresentacion.Modales
{
    public partial class MD_EnvioDeCorreo : Form
    {
        List<string> ArchivoAdjuntos = new List<string>();
        public MD_EnvioDeCorreo()
        {
            InitializeComponent();
        }

        private void btnAdjunto_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string adjunto in ofd.SafeFileNames)
                {
                    txtAdjunto.Text = txtAdjunto.Text + adjunto + " | ";
                }
                foreach (string adjunto in ofd.FileNames)
                {
                    ArchivoAdjuntos.Add(adjunto);
                }

            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            ArchivoAdjuntos = new List<string>();
            txtAdjunto.Text = "";
        }

        private void btnEnviar_Click_1(object sender, EventArgs e)
        {
            try
            {
                //CONFIGURAR VALORES
                string Host = "smtp.gmail.com";
                int Puerto = 587;
                string Usuario = "sistemaventas99@gmail.com";
                string Clave = "yuhxeqcmjzlokicd​";//clave generada para aplicación en GMAIL

                //PROPORCIONAMOS AUTENTICACION DE GMAIL
                SmtpClient smtp = new SmtpClient(Host, Puerto);
                MailMessage msg = new MailMessage();


                //CREAMOS EL CONTENIDO DEL CORREO
                string[] Destinatario = txtPara.Text.Split(',');
                string[] DestinatarioCopia = txtCopia.Text.Split(',');
                string[] DestinatarioCopiaOculta = txtCopiaOculta.Text.Split(',');


                msg.From = new MailAddress(Usuario, "Tienda Rosita");
                foreach (string correo in Destinatario) if (correo != "") msg.To.Add(new MailAddress(correo));
                foreach (string correo in DestinatarioCopia) if (correo != "") msg.CC.Add(new MailAddress(correo));
                foreach (string correo in DestinatarioCopiaOculta) if (correo != "") msg.Bcc.Add(new MailAddress(correo));
                foreach (string adjunto in ArchivoAdjuntos) if (adjunto != "") msg.Attachments.Add(new Attachment(adjunto));
                msg.Subject = txtAsunto.Text;
                msg.IsBodyHtml = false;
                msg.Body = txtMensaje.Text;


                //ENVIAMOS EL CORREO
                smtp.Credentials = new NetworkCredential(Usuario, Clave);
                smtp.EnableSsl = true;
                smtp.Send(msg);
                MessageBox.Show("Mensaje Enviado");
            }
            catch
            {
                MessageBox.Show("Error al enviar");
            }
        }
    }
}
