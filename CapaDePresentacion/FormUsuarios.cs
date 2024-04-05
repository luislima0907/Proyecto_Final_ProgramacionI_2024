using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDePresentacion.Utilidades;

using CapaDeEntidad;
using CapaDeNegocio;
using FontAwesome.Sharp;
using System.Windows.Media;
using ClosedXML.Excel;

namespace CapaDePresentacion
{
    public partial class FormUsuarios : Form
    {
        public FormUsuarios()
        {
            InitializeComponent();
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            // Cuando el estado del usuario devuelva true, se dira que esta activo
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            // Cuando el estado del usuario devuelva false, se dira que no esta activo
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            // Con esto decimos que solo nos devuelva el texto almacenado en el objeto de la OpcionCombo en nuestro formulario de usuarios
            cboEstado.DisplayMember = "Texto";

            // Con esto almacenamos el valor de true o false que nos devuelva la OpcionCombo en el formulario de usuarios
            cboEstado.ValueMember = "Valor";

            // Con esto decimos que solo seleccione el primer elemento mostrado en el display del estado del formulario de usuarios.
            cboEstado.SelectedIndex = 0;

            // Creamos una lista de la tabla rol y luego la iteramos con un foreach para encontrar sus valores, dependiendo el usuario y su estado
            List<Rol> ListaRol = new CN_Rol().Listar();

            // Como la lista del rol estara vacia, le agregamos los valores del IdRol y la Descripcion al ComboBox de los Roles del formulario de Usuarios
            foreach (Rol item in ListaRol)
            {
                cboRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            // ahora se mostrara el valor que tenga en el display del comboBox
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

            // Con este foreach almacenamos los elementos que se nos muestren en la lista de usuarios en un boton con opcion multiple que nos servira para buscar a los usuarios de una manera especificia, es decir, por su documento, nombre, etc
            foreach (DataGridViewColumn columnas in dgvData.Columns)
            {
                if (columnas.Visible == true && columnas.Name != "btnSeleccionar") cboBusqueda.Items.Add(new OpcionCombo() { Valor = columnas.Name, Texto = columnas.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;


            // Con esto mostramos a los usuarios que tengamos en nuestra base de datos en el formulario de usuarios al momento de iniciarlo
            List<Usuario> ListaUsuarios = new CN_Usuario().Listar();

            foreach (Usuario item in ListaUsuarios)
            {
                dgvData.Rows.Add(new object[] {"",item.IdUsuario,item.Documento,item.NombreCompleto,item.Correo,item.Contraseña,
                item.oRol.IdRol,item.oRol.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        // creamos el metodo de guardar un usuario en nuestra lista de usuarios y mostrarlos en el formulario de usuarios
        // primero se tienen que rellenar los campo de texto en el formulario y luego darle al boton de guardar para que se reflejen los cambios
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Usuario objUsuario = new Usuario()
            {
                // Obtenemos la informacion que ingresemos en los campos de texto y la almacenamos en nuestra base de datos
                IdUsuario = Convert.ToInt32(txtId.Text),
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Contraseña = txtContraseña.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objUsuario.IdUsuario == 0)
            {
                // de esta manera generamos el id del nuevo usuario
                int idUsuarioGenerado = new CN_Usuario().RegistrarUsuario(objUsuario, out mensaje);

                // Como el id del nuevo usuario no puede ser 0 entonces se hace esta validacion
                if (idUsuarioGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] {"",idUsuarioGenerado,txtDocumento.Text,txtNombreCompleto.Text,txtCorreo.Text,txtContraseña.Text,
                // De esta manera accedemos a los valores de una clase para agregarlos a un objeto
                ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
                });

                    // al momento de agregar un usuario, que limpie los campos de texto que usamos anteriormente
                    LimpiarCamposDeTexto();
                }
                else MessageBox.Show(mensaje);
            }
            else
            {
                bool resultado = new CN_Usuario().EditarUsuario(objUsuario, out mensaje);

                if (resultado)
                {
                    // Obtenemos el indice de la fila seleccionada en nuestro formulario de usuarios
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    // Obtenemos los demas datos de la fila seleccionada
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtDocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["Contraseña"].Value = txtContraseña.Text;
                    row.Cells["IdRol"].Value = ((OpcionCombo)cboRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cboRol.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    LimpiarCamposDeTexto();
                }
                else MessageBox.Show(mensaje);
            }
        }

        // Creamos un metodo para limpiar los campos de texto al momento de ingresar un nuevo usuario
        private void LimpiarCamposDeTexto()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtContraseña.Text = "";
            txtConfirmarContraseña.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;

            // aqui decimos que cuando se limpien los campos de texto, que mantenga seleccionado el campo de texto donde se escribira el documento del usuario
            txtDocumento.Select();
        }

        // creamos un metodo que contenga un envento sobre poner un icono al lado de la filas en el formulario de usuarios
        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Hacemos esta condicional para que no tome en cuenta el encabezado de la tabla
            if (e.RowIndex < 0) return;

            // Implementamos el icono que tengamos en nuestros archivos y le damos sus respectivas dimensiones
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // variables con las dimensiones del icono que acompañara a la fila con la información del usuario.
                var ancho = Properties.Resources.check20.Width;
                var alto = Properties.Resources.check20.Height;
                var posicionEnX = e.CellBounds.Left + (e.CellBounds.Width - ancho) / 2;
                var posicionEnY = e.CellBounds.Top + (e.CellBounds.Height - alto) / 2;

                // Una vez dadas las dimensiones, ponemos el icono en la celda que indicamos
                e.Graphics.DrawImage(Properties.Resources.check, new Rectangle(posicionEnX, posicionEnY, ancho, alto));

                // Que al momento de darle click no se quede seleccionado para siempre.
                e.Handled = true;
            }
        }

        // Creamos un metodo que almacenara en los campos de texto el usuario que seleccionemos en nuestra tabla de usuarios del formulario usuarios
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se activa cuando hacemos click en el boton que contiene el icono para seleccionar un usuario
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                // ya que se comienza a contar desde la primera fila de la tabla sin contar el encabezado se inicia en el indice 0
                if (indice >= 0)
                {
                    // conseguimos el indice de la fila seleccionada
                    txtIndice.Text = indice.ToString();
                    // conseguimos el valor de cada fila y lo almacenamos en los campos de texto
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtContraseña.Text = dgvData.Rows[indice].Cells["Contraseña"].Value.ToString();
                    txtConfirmarContraseña.Text = dgvData.Rows[indice].Cells["Contraseña"].Value.ToString();

                    // hacemos un foreach para almacenar el tipo de usuario
                    foreach (OpcionCombo oc in cboRol.Items)
                    {
                        // el momento que encuentra el Tipo de usuario que es el usuario que seleccionamos en la tabla el ciclo foreach se rompe
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indice_combo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    // hacemos lo mismo que el tipo de usuario pero esta vez con su estado
                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }
        
        // Hacemos un metodo para el boton de eliminar un usuario en nuestra base de datos desde la interfaz
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                // Un ejemplo de como crear una ventana emergente que contenga un mensaje con el objeto MessageBox y su atributo o metodo Show
                if (MessageBox.Show("¿Desea eliminar a este usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Usuario objUsuario = new Usuario() { IdUsuario = Convert.ToInt32(txtId.Text) };

                    bool respuesta = new CN_Usuario().EliminarUsuario(objUsuario, out mensaje);

                    // Eliminamos a un usuario por medio del indice de la fila seleccionada
                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        LimpiarCamposDeTexto();
                    }

                    // ejemplo de una sobrecarga de metodos
                    else MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        // Creamos un metodo para buscar en un campo de texto un usuario dependiendo del filtro que le pasemos
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

        // Creamos un metodo para limpiar la busqueda que acabamos de hacer por medio del boton de busqueda
        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCamposDeTexto();
        }

        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            // aqui estamos diciendo que si no hay ninguna fila por mostrar en el formulario, entonces que no haga la exportacion
            if (dgvData.Rows.Count < 1) MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            fila.Cells[2].Value.ToString(),
                            fila.Cells[3].Value.ToString(),
                            fila.Cells[4].Value.ToString(),
                            fila.Cells[7].Value.ToString(),
                            fila.Cells[9].Value.ToString(),
                        });
                    }
                }

                // inicializamos este objeto para poder guardar archivos en cualquier parte de la pc con la ayuda del nombre
                SaveFileDialog guardarArchivo = new SaveFileDialog();
                // le damos el formato por defecto que tendra el nombre del archivo al momento de quererlo guardar
                guardarArchivo.FileName = string.Format($"Usuarios_Registrados_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx");

                // que busque por defecto todos los archivos que terminen con esa extension
                guardarArchivo.Filter = "Excel Files | *.xlsx";

                // con esta validacion logramos guardar los cambios en excel
                if (guardarArchivo.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook xLWorkbook = new XLWorkbook();
                        var hoja = xLWorkbook.Worksheets.Add(dataTable, "Registro_de_Usuarios");
                        hoja.ColumnsUsed().AdjustToContents();
                        xLWorkbook.SaveAs(guardarArchivo.FileName);
                        MessageBox.Show("Registro de los usuarios generado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al generar con el registro de los usuarios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
