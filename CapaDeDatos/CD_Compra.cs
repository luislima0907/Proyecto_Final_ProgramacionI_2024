using CapaDeEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    // Esta clase manejará todas las consultas en nuestra base de datos relacionadas a la tabla de Compra
    public class CD_Compra
    {
        // Creamos un método para crear un id para una compra
        public int ObtenerCorrelativo()
        {
            int IdCorrelativo = 0;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from COMPRA");

                    // instanciamos SqlCommand para realizar la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);

                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera de tipo texto
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    IdCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    IdCorrelativo = 0;
                }
            }
            return IdCorrelativo;
        }

        // Creamos un método para registrar las compras de los productos que hagamos dentro del programa
        public bool Registrar(Compra objCompra, DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_COMPRA", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", objCompra.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdProveedor", objCompra.oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("TipoDeDocumento", objCompra.TipoDeDocumento);
                    cmd.Parameters.AddWithValue("NumeroDeDocumento", objCompra.NumeroDeDocumento);
                    cmd.Parameters.AddWithValue("MontoTotal", objCompra.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleDeLaCompra", DetalleCompra);

                    // Para los datos de salida tenemos que darle el nombre del parámetro en sql y despues definir de que tipo es,
                    // así mismo, le específicamos por medio de Direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    // aqui le estamos diciendo que el tipo de comando que ejecutará sera una procedura
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }
            return Respuesta;
        }

        // Creamos un método para obtener las compras de los productos que hicimos en el programa
        public Compra ObtenerCompra(string numero)
        {
            // Creamos un objeto de tipo compra
            Compra objCompra = new Compra();

            // Usamos la cadena de conexión para acceder a nuestra base de datos
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.IdCompra,");
                    query.AppendLine("u.NombreCompleto, pr.Documento, pr.RazonSocial,");
                    query.AppendLine("c.TipoDeDocumento, c.NumeroDeDocumento, c.MontoTotal,");
                    query.AppendLine("convert(char(10), c.FechaDeRegistro,103)[FechaDeRegistro]");
                    query.AppendLine("from COMPRA c");
                    query.AppendLine("inner join USUARIO u on u.IdUsuario = c.IdUsuario");
                    query.AppendLine("inner join PROVEEDOR pr on pr.IdProveedor = c.IdProveedor");
                    query.AppendLine("where c.NumeroDeDocumento = @numero");

                    // instanciamos SqlCommand para poder realizar la seleccion de la tabla con la query(consulta) y la conexión a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@numero", numero);

                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera de tipo texto
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de compra
                        while (dr.Read())
                        {
                            // que instancie un objeto de tipo compra
                            objCompra = new Compra()
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"]),
                                oUsuario = new Usuario() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                oProveedor = new Proveedor() { Documento = dr["Documento"].ToString(), RazonSocial = dr["RazonSocial"].ToString() },
                                TipoDeDocumento = dr["TipoDeDocumento"].ToString(),
                                NumeroDeDocumento = dr["NumeroDeDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaDeRegistro = dr["FechaDeRegistro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // si hay algún error a la hora de crear la compra, que la devuelva vacia.
                    objCompra = new Compra();
                }
            }
            return objCompra;
        }

        // Creamos un método que almacene la información de los detalles acerca de la compra de los productos
        public List<Detalle_Compra> ObtenerDetalleDeLaCompra(int IdCompra)// recibe como parámetro el id de la compra para poder recuperarla
        {
            // Creamos una lista de con el detalle de la compra y la instanciamos
            List<Detalle_Compra> oLista = new List<Detalle_Compra>();

            // Hacemos un try catch en caso de que falle la conexión
            try
            {
                // Usamos la cadena de conexión para acceder a nuestra base de datos
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // abrimos la conexión
                    oConexion.Open();

                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.Nombre,dc.PrecioDeCompra,dc.Cantidad,dc.MontoTotal from DETALLE_COMPRA dc");
                    query.AppendLine("inner join PRODUCTO p on p.IdProducto = dc.IdProducto");
                    query.AppendLine("where dc.IdCompra = @IdCompra");

                    // instanciamos SqlCommand para poder realizar la seleccion de la tabla con la query(consulta) y la conexión a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdCompra", IdCompra);

                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera de tipo texto
                    cmd.CommandType = System.Data.CommandType.Text;

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de compra
                        while (dr.Read())
                        {
                            // que pueda agregar a la lista un objeto de tipo detalle de la compra
                            oLista.Add(new Detalle_Compra()
                            {
                                oProducto = new Producto() { Nombre = dr["Nombre"].ToString() },
                                PrecioDeCompra = Convert.ToDecimal(dr["PrecioDeCompra"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString())
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // si hay algún error al momento de crear o agregar un objeto a la lista, que la devuelva vacía
                oLista = new List<Detalle_Compra>();
            }
            return oLista;
        }
    }
}
