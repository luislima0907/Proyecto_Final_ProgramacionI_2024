using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeEntidad;

namespace CapaDeDatos
{
    public class CD_Venta
    {
        public int ObtenerCorrelativo()
        {
            int IdCorrelativo = 0;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from VENTA");

                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    
                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera un texto
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

        // Creamos un metodo para el manejo del stock de un producto al momento de realizar una venta
        public bool RestarStock(int IdProducto, int Cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    
                    // en esta consulta actualizamos el stock de nuestra tabla producto cuando realicemos una venta
                    // la funcion de esta consulta es de cuando nosotros pongamos cierta cantidad de un producto a la hora de venderlo
                    // se le reste al stock que hay del producto
                    query.AppendLine("update PRODUCTO set Stock -= @Cantidad where IdProducto = @IdProducto");
                    
                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@IdProducto", IdProducto);

                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera un texto
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    // ExecuteNonQuery() devuelve las filas afectadas en una tabla de sql
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool SumarStock(int IdProducto, int Cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();

                    // en esta consulta actualizamos el stock de nuestra tabla producto cuando eliminemos del formulario de venta un producto
                    // la funcion de esta consulta es de cuando nosotros pongamos cierta cantidad de un producto a la hora de venderlo, pero luego le damos al boton de borrar producto de la venta, es decir, deshacemos la venta
                    // entonces la cantidad que ingresamos anteriormente se le sumara nuevamente al stock que hay del producto
                    query.AppendLine("update PRODUCTO set Stock += @Cantidad where IdProducto = @IdProducto");

                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@IdProducto", IdProducto);

                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera un texto
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    // ExecuteNonQuery() devuelve las filas afectadas en una tabla de sql
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Registrar(Venta objVenta, DataTable DetalleVenta, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_VENTA", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", objVenta.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("TipoDeDocumento", objVenta.TipoDeDocumento);
                    cmd.Parameters.AddWithValue("NumeroDeDocumento", objVenta.NumeroDeDocumento);
                    cmd.Parameters.AddWithValue("DocumentoDelCliente", objVenta.DocumentoDelCliente);
                    cmd.Parameters.AddWithValue("NombreDelCliente", objVenta.NombreDelCliente);
                    cmd.Parameters.AddWithValue("MontoDePago", objVenta.MontoDePago);
                    cmd.Parameters.AddWithValue("MontoDeCambio", objVenta.MontoDeCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", objVenta.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleDeLaVenta", DetalleVenta);

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera una procedura
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Resultado = false;
                    Mensaje = ex.Message;
                }
            }
            return Resultado;
        }

        public Venta ObtenerVenta(string numero)
        {
            Venta objVenta = new Venta();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oConexion.Open();
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    // el metodo AppendLine nos permite hacer consultas con saltos de linea
                    query.AppendLine("select v.Idventa,");
                    query.AppendLine("u.NombreCompleto,v.DocumentoDelCliente,v.NombreDelCliente,");
                    query.AppendLine("v.TipoDeDocumento,v.NumeroDeDocumento,v.MontoDePago,");
                    query.AppendLine("v.MontoDeCambio,v.MontoTotal,");
                    query.AppendLine("convert(char(10),v.FechaDeRegistro,103)[FechaDeRegistro]");
                    query.AppendLine("from VENTA v");
                    query.AppendLine("inner join USUARIO u on u.IdUsuario = v.IdUsuario");
                    query.AppendLine("where v.NumeroDeDocumento = @numero");

                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera un texto
                    cmd.CommandType = CommandType.Text;

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de usuarios
                        while (dr.Read())
                        {
                            objVenta = new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                oUsuario = new Usuario() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                DocumentoDelCliente = dr["DocumentoDelCliente"].ToString(),
                                NombreDelCliente = dr["NombreDelCliente"].ToString(),
                                TipoDeDocumento = dr["TipoDeDocumento"].ToString(),
                                NumeroDeDocumento = dr["NumeroDeDocumento"].ToString(),
                                MontoDePago = Convert.ToDecimal(dr["MontoDePago"].ToString()),
                                MontoDeCambio = Convert.ToDecimal(dr["MontoDeCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaDeRegistro = dr["FechaDeRegistro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    // si hay algun error a la hora de crear la lista, que la devuelva vacia
                    objVenta = new Venta();
                }
            }
            return objVenta;
        }

        public List<Detalle_Venta> ObtenerDetalleDeLaVenta(int IdVenta)
        {
            List<Detalle_Venta> oLista = new List<Detalle_Venta>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    oConexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select p.Nombre,dv.PrecioDeVenta,dv.Cantidad,dv.SubTotal from DETALLE_VENTA dv");
                    query.AppendLine("inner join PRODUCTO p on p.IdProducto = dv.IdProducto");
                    query.AppendLine("where dv.IdVenta = @IdVenta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdVenta", IdVenta);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Detalle_Venta()
                            {
                                oProducto = new Producto() { Nombre = dr["Nombre"].ToString() },
                                PrecioDeVenta = Convert.ToDecimal(dr["PrecioDeVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(dr["SubTotal"].ToString())
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Detalle_Venta>();
            }
            return oLista;
        }
    }
}
