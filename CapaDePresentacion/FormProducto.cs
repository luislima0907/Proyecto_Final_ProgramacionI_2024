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
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace CapaDePresentacion
{
    public partial class FormProducto : Form
    {
        public FormProducto()
        {
            InitializeComponent();
        }

        private void FormProducto_Load(object sender, EventArgs e)
        {

            // Cuando el estado del Producto devuelva true, se dira que esta activo
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            // Cuando el estado del Producto devuelva false, se dira que no esta activo
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            // Con esto decimos que solo nos devuelva el texto almacenado en el objeto de la OpcionCombo en nuestro formulario de Productos
            cboEstado.DisplayMember = "Texto";

            // Con esto almacenamos el valor de true o false que nos devuelva la OpcionCombo en el formulario de Productos
            cboEstado.ValueMember = "Valor";

            // Con esto decimos que solo seleccione el primer elemento mostrado en el display del estado del formulario de Productos.
            cboEstado.SelectedIndex = 0;

            // Creamos una lista de la tabla Categoria y luego la iteramos con un foreach para encontrar sus valores, dependiendo el Producto y su estado
            List<Categoria> ListaCategoria = new CN_Categoria().Listar();

            // Como la lista del Categoria estara vacia, le agregamos los valores del IdCategoria y la Descripcion al ComboBox de los Categoriaes del formulario de Productos
            foreach (Categoria item in ListaCategoria)
            {
                cboCategoria.Items.Add(new OpcionCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
            }
            // ahora se mostrara el valor que tenga en el display del comboBox
            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            cboCategoria.SelectedIndex = 0;

            // Con este foreach almacenamos los elementos que se nos muestren en la lista de Productos en un boton con opcion multiple que nos servira para buscar a los Productos de una manera especificia, es decir, por su documento, nombre, etc
            foreach (DataGridViewColumn columnas in dgvData.Columns)
            {
                if (columnas.Visible == true && columnas.Name != "btnSeleccionar") cboBusqueda.Items.Add(new OpcionCombo() { Valor = columnas.Name, Texto = columnas.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;


            // Con esto mostramos a los Productos que tengamos en nuestra base de datos en el formulario de Productos al momento de iniciarlo
            List<Producto> ListaProductos = new CN_Producto().Listar();

            foreach (Producto item in ListaProductos)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.PrecioDeCompra,
                    item.PrecioDeVenta,
                    item.Stock,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // hacemos la validacion de un codigo que tenga el siguiente formato: Ejemplo G-789
            string patronDelCodigo = @"\w-\d\d\d"; // expresion regular para el codigo
            Regex confirmarPatronDelCodigo = new Regex(patronDelCodigo); // usamos regex para guardar la expresion regular y poder compararla mas adelante
            MatchCollection codigoDelProducto = confirmarPatronDelCodigo.Matches(txtCodigo.Text); // usamos matchcollection para que compare con la caja de texto del codigo

            // hacemos la validacion de un nombre que tenga el siguiente formato: Nombre completo del producto
            string formatoDelNombre = @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+(?:\s+[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+){1,5}(?<!\s)$"; // expresion regular para el nombre del producto
            Regex confirmarElFormatoDelNombre = new Regex(formatoDelNombre); // usamos regex para guardar la expresion regular y poder compararla mas adelante
            MatchCollection nombreDelProducto = confirmarElFormatoDelNombre.Matches(txtNombre.Text); // usamos matchcollection para que compare con la caja de texto del nombre

            // hacemos la validacion de una descripcion para el producto: Cantidad Unidad_de_medida
            string formatoDeLaDescripcion = @"^\d+(\.\d+)?\s[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+(?:\s[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+)*$"; // expresion regular para la descripcion del producto
            Regex confirmarElFormatoDeLaDescripcion = new Regex(formatoDeLaDescripcion); // usamos regex para guardar la expresion regular y poder compararla mas adelante
            MatchCollection descripcionDelProducto = confirmarElFormatoDeLaDescripcion.Matches(txtDescripcion.Text); // usamos matchcollection para que compare con la caja de texto de la descripcion

            if (nombreDelProducto.Count > 0 && codigoDelProducto.Count > 0 && descripcionDelProducto.Count > 0)
            {
                Producto objProducto = new Producto()
                {
                    // Obtenemos la informacion que ingresemos en los campos de texto y la almacenamos en nuestra base de datos
                    IdProducto = Convert.ToInt32(txtId.Text),
                    Codigo = txtCodigo.Text,
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                    Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                };

                if (objProducto.IdProducto == 0)
                {
                    // de esta manera generamos el id del nuevo Producto
                    int idProductoGenerado = new CN_Producto().RegistrarProducto(objProducto, out mensaje);

                    // Como el id del nuevo Producto no puede ser 0 entonces se hace esta validacion
                    if (idProductoGenerado != 0)
                    {
                        dgvData.Rows.Add(new object[] {
                        "",
                        idProductoGenerado,
                        txtCodigo.Text,
                        txtNombre.Text,
                        txtDescripcion.Text,

                        // De esta manera accedemos a los valores de una clase para agregarlos a un objeto
                        ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
                        "0.00",
                        "0.00",
                        "0",
                        ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
                        });

                        // al momento de agregar un Producto, que limpie los campos de texto que usamos anteriormente
                        LimpiarCamposDeTexto();
                    }
                    else MessageBox.Show(mensaje);
                }
                else
                {
                    bool resultado = new CN_Producto().EditarProducto(objProducto, out mensaje);

                    if (resultado)
                    {
                        // Obtenemos el indice de la fila seleccionada en nuestro formulario de Productos
                        DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                        // Obtenemos los demas datos de la fila seleccionada
                        row.Cells["Id"].Value = txtId.Text;
                        row.Cells["Codigo"].Value = txtCodigo.Text;
                        row.Cells["Nombre"].Value = txtNombre.Text;
                        row.Cells["Descripcion"].Value = txtDescripcion.Text;
                        row.Cells["IdCategoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString();
                        row.Cells["Categoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString();

                        //row.Cells["PrecioDeCompra"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                        //row.Cells["PrecioDeVenta"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                        //row.Cells["Stock"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();

                        row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                        row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                        LimpiarCamposDeTexto();
                    }
                    else MessageBox.Show(mensaje);
                }
            }
            else
            {
                MessageBox.Show("Asegurese de ingresar un formato valido:\n\nCodigo: Una_letra-3_digitos_enteros_sin_espacios.\n\nNombre: Al menos dos palabras separadas que describan su el nombre.\n\nDescripcion: CANTIDAD UNIDAD_DE_MEDIDA");
            }
        }

        // Creamos un metodo para limpiar los campos de texto al momento de ingresar un nuevo Producto
        private void LimpiarCamposDeTexto()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            cboCategoria.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;

            // aqui decimos que cuando se limpien los campos de texto, que mantenga seleccionado el campo de texto donde se escribira el documento del Producto
            txtCodigo.Select();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Hacemos esta condicional para que no tome en cuenta el encabezado de la tabla
            if (e.RowIndex < 0) return;

            // Implementamos el icono que tengamos en nuestros archivos y le damos sus respectivas dimensiones
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // variables con las dimensiones del icono que acompañara a la fila con la información del Producto.
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
            // Se activa cuando hacemos click en el boton que contiene el icono para seleccionar un Producto
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
                    txtCodigo.Text = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();

                    // hacemos un foreach para almacenar el tipo de Producto
                    foreach (OpcionCombo oc in cboCategoria.Items)
                    {
                        // el momento que encuentra el Tipo de Producto que es el Producto que seleccionamos en la tabla el ciclo foreach se rompe
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            int indice_combo = cboCategoria.Items.IndexOf(oc);
                            cboCategoria.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    // hacemos lo mismo que el tipo de Producto pero esta vez con su estado
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
                if (MessageBox.Show("¿Desea eliminar este producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Producto objProducto = new Producto() { IdProducto = Convert.ToInt32(txtId.Text) };

                    bool respuesta = new CN_Producto().EliminarProducto(objProducto, out mensaje);

                    // Eliminamos a un Producto por medio del indice de la fila seleccionada
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

        // Creamos un metodo para un boton que nos sirva para descargar en un excel con las tablas de nuestros productos ingresados o mostrados en el formulario
        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            // aqui estamos diciendo que si no hay ninguna fila por mostrar en el formulario, entonces que no haga la exportacion
            if (dgvData.Rows.Count < 1) MessageBox.Show("No hay datos para exportar","Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            fila.Cells[6].Value.ToString(),
                            fila.Cells[7].Value.ToString(),
                            fila.Cells[8].Value.ToString(),
                            fila.Cells[9].Value.ToString(),
                            fila.Cells[11].Value.ToString(),
                        });
                    }
                }

                // inicializamos este objeto para poder guardar archivos en cualquier parte de la pc con la ayuda del nombre
                SaveFileDialog guardarArchivo = new SaveFileDialog();
                // le damos el formato por defecto que tendra el nombre del archivo al momento de quererlo guardar
                guardarArchivo.FileName = string.Format($"Reporte_del_Producto_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx");
                
                // que busque por defecto todos los archivos que terminen con esa extension
                guardarArchivo.Filter = "Excel Files | *.xlsx";

                // con esta validacion logramos guardar los cambios en excel
                if (guardarArchivo.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook xLWorkbook = new XLWorkbook();
                        var hoja = xLWorkbook.Worksheets.Add(dataTable, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        xLWorkbook.SaveAs(guardarArchivo.FileName);
                        MessageBox.Show("Reporte de productos generado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al generar el reporte de los productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
