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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDePresentacion
{
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            // Cuando el estado del Cliente devuelva true, se dira que esta activo
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            // Cuando el estado del Cliente devuelva false, se dira que no esta activo
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            // Con esto decimos que solo nos devuelva el texto almacenado en el objeto de la OpcionCombo en nuestro formulario de Clientes
            cboEstado.DisplayMember = "Texto";

            // Con esto almacenamos el valor de true o false que nos devuelva la OpcionCombo en el formulario de Clientes
            cboEstado.ValueMember = "Valor";

            // Con esto decimos que solo seleccione el primer elemento mostrado en el display del estado del formulario de Clientes.
            cboEstado.SelectedIndex = 0;

            // Con este foreach almacenamos los elementos que se nos muestren en la lista de Clientes en un boton con opcion multiple que nos servira para buscar a los Clientes de una manera especificia, es decir, por su documento, nombre, etc
            foreach (DataGridViewColumn columnas in dgvData.Columns)
            {
                if (columnas.Visible == true && columnas.Name != "btnSeleccionar") cboBusqueda.Items.Add(new OpcionCombo() { Valor = columnas.Name, Texto = columnas.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Con esto mostramos a los Clientes que tengamos en nuestra base de datos en el formulario de Clientes al momento de iniciarlo
            List<Cliente> ListaClientes = new CN_Cliente().Listar();

            foreach (Cliente item in ListaClientes)
            {
                dgvData.Rows.Add(new object[] {"",item.IdCliente,item.Documento,item.NombreCompleto,item.Correo,item.Telefono,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // hacemos la validacion de un numero de documento que tenga 6 digitos con la ayuda de las expresiones regulares
            string patronDelNumeroDeDocumento = @"\d\d\d\d\d\d"; // expresion regular para el numero de documento
            Regex confirmarPatronDelDocumento = new Regex(patronDelNumeroDeDocumento); // usamos regex para guardar la expresion regular y poder compararla mas adelante
            MatchCollection numeroDeDocumento = confirmarPatronDelDocumento.Matches(txtDocumento.Text); // usamos matchcollection para que compare con la caja de texto del documento

            // hacemos la validacion de un correo electronico que tenga el siguiente formato: nombre_de_usuario@nombre_de_dominio.com
            string formatoDelCorreo = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}"; // expresion regular para el correo del usuario
            Regex confirmarElFormatoDelCorreo = new Regex(formatoDelCorreo); // usamos regex para guardar la expresion regular y poder compararla mas adelante
            MatchCollection correoDelUsuario = confirmarElFormatoDelCorreo.Matches(txtCorreo.Text); // usamos matchcollection para que compare con la caja de texto del correo

            // hacemos la validacion de un numero de telefono que tenga 8 digitos y que tenga como prefijo +502, con la ayuda de las expresiones regulares
            string patronDelNumeroDeTelefono = @"\+502\s\d\d\d\d\s\d\d\d\d"; // expresion regular para el numero de telefono
            Regex confirmarPatronDelTelefono = new Regex(patronDelNumeroDeTelefono); // usamos regex para guardar la expresion regular y poder compararla mas adelante
            MatchCollection numeroDeTelefono = confirmarPatronDelTelefono.Matches(txtTelefono.Text); // usamos matchcollection para que compare con la caja de texto del telefono

            // hacemos la validacion de un nombre que tenga el siguiente formato: NOMBRE APELLIDO
            string formatoDelNombre = @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+(?:\s+[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+){1,5}(?<!\s)$"; // expresion regular para el nombre 
            Regex confirmarElFormatoDelNombre = new Regex(formatoDelNombre); // usamos regex para guardar la expresion regular y poder compararla mas adelante
            MatchCollection nombreDelCliente = confirmarElFormatoDelNombre.Matches(txtNombreCompleto.Text); // usamos matchcollection para que compare con la caja de texto del nombre

            // si el metodo Matches devuelve 1 entonces que guarde el cliente
            if(nombreDelCliente.Count > 0 && numeroDeTelefono.Count > 0 && numeroDeDocumento.Count > 0 && correoDelUsuario.Count > 0)
            {
                Cliente objCliente = new Cliente()
                {
                    // Obtenemos la informacion que ingresemos en los campos de texto y la almacenamos en nuestra base de datos
                    IdCliente = Convert.ToInt32(txtId.Text),
                    Documento = txtDocumento.Text,
                    NombreCompleto = txtNombreCompleto.Text,
                    Correo = txtCorreo.Text,
                    Telefono = txtTelefono.Text,
                    Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                };

                if (objCliente.IdCliente == 0)
                {
                    // de esta manera generamos el id del nuevo Cliente
                    int idClienteGenerado = new CN_Cliente().RegistrarCliente(objCliente, out mensaje);

                    // Como el id del nuevo Cliente no puede ser 0 entonces se hace esta validacion
                    if (idClienteGenerado != 0)
                    {
                        dgvData.Rows.Add(new object[] {"",idClienteGenerado,txtDocumento.Text,txtNombreCompleto.Text,txtCorreo.Text,txtTelefono.Text,
                        // De esta manera accedemos a los valores de una clase para agregarlos a un objeto
                        ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
                        });

                        // al momento de agregar un Cliente, que limpie los campos de texto que usamos anteriormente
                        LimpiarCamposDeTexto();
                    }
                    else MessageBox.Show(mensaje);
                }
                else
                {
                    bool resultado = new CN_Cliente().EditarCliente(objCliente, out mensaje);

                    if (resultado)
                    {
                        // Obtenemos el indice de la fila seleccionada en nuestro formulario de Clientes
                        DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];

                        // Obtenemos los demas datos de la fila seleccionada
                        row.Cells["Id"].Value = txtId.Text;
                        row.Cells["Documento"].Value = txtDocumento.Text;
                        row.Cells["NombreCompleto"].Value = txtNombreCompleto.Text;
                        row.Cells["Correo"].Value = txtCorreo.Text;
                        row.Cells["Telefono"].Value = txtTelefono.Text;
                        row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                        row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                        LimpiarCamposDeTexto();
                    }
                    else MessageBox.Show(mensaje);
                }
            }
            else
            {
                MessageBox.Show("Asegurese de ingresar un formato valido:\n\nDocumento: 6 digitos enteros sin espacios.\n\nNombre Completo: NOMBRE APELLIDO.\n\nCorreo: nombre_del_cliente@nombre_de_dominio.com.\n\nTelefono: +502 4_digitos_enteros 4_digitos_enteros");
            }
        }

        private void LimpiarCamposDeTexto()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            cboEstado.SelectedIndex = 0;

            // aqui decimos que cuando se limpien los campos de texto, que mantenga seleccionado el campo de texto donde se escribira el documento del Cliente
            txtDocumento.Select();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Hacemos esta condicional para que no tome en cuenta el encabezado de la tabla
            if (e.RowIndex < 0) return;

            // Implementamos el icono que tengamos en nuestros archivos y le damos sus respectivas dimensiones
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // variables con las dimensiones del icono que acompañara a la fila con la información del Cliente.
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se activa cuando hacemos click en el boton que contiene el icono para seleccionar un Cliente
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
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();

                    // hacemos un foreach para almacenar el estado del cliente
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                // Un ejemplo de como crear una ventana emergente que contenga un mensaje con el objeto MessageBox y su atributo o metodo Show
                if (MessageBox.Show("¿Desea eliminar a este Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cliente objCliente = new Cliente() { IdCliente = Convert.ToInt32(txtId.Text) };

                    bool respuesta = new CN_Cliente().EliminarCliente(objCliente, out mensaje);

                    // Eliminamos a un Cliente por medio del indice de la fila seleccionada
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
                            fila.Cells[5].Value.ToString(),
                            fila.Cells[7].Value.ToString(),
                        });
                    }
                }

                // inicializamos este objeto para poder guardar archivos en cualquier parte de la pc con la ayuda del nombre
                SaveFileDialog guardarArchivo = new SaveFileDialog();
                // le damos el formato por defecto que tendra el nombre del archivo al momento de quererlo guardar
                guardarArchivo.FileName = string.Format($"Clientes_Registrados_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx");

                // que busque por defecto todos los archivos que terminen con esa extension
                guardarArchivo.Filter = "Excel Files | *.xlsx";

                // con esta validacion logramos guardar los cambios en excel
                if (guardarArchivo.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook xLWorkbook = new XLWorkbook();
                        var hoja = xLWorkbook.Worksheets.Add(dataTable, "Registro_de_Clientes");
                        hoja.ColumnsUsed().AdjustToContents();
                        xLWorkbook.SaveAs(guardarArchivo.FileName);
                        MessageBox.Show("Registro de los clientes generado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al generar el registro de los clientes", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
