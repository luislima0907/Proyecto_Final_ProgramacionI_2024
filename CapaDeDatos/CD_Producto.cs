using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProducto,Codigo,Nombre,p.Descripcion,c.IdCategoria,c.Descripcion[DescripcionCategoria],PrecioDeCompra,PrecioDeVenta,Stock,p.Estado from PRODUCTO p");
                    query.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");
                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera un texto
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de Productos
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga los Productos de la tabla
                            lista.Add(new Producto()
                            {
                                // aqui convertimos el tipo de valor que contendra cada dato de nuestra tabla, tenemos que definirlo igual que cuando lo declaramos en sql
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Codigo = dr["Codigo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DescripcionCategoria"].ToString() },
                                PrecioDeCompra = Convert.ToDecimal(dr["PrecioDeCompra"]),
                                PrecioDeVenta = Convert.ToDecimal(dr["PrecioDeVenta"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // si hay algun error a la hora de crear la lista, que la devuelva vacia
                    lista = new List<Producto>();
                }
            }
            return lista;
        }
        // Creamos un metodo para registrar los Productos y almacenarlos en nuestra base de datos
        public int RegistrarProducto(Producto objProducto, out string Mensaje)
        {
            int idProductoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Productos
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_PRODUCTO", oConexion);
                    cmd.Parameters.AddWithValue("Codigo", objProducto.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", objProducto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", objProducto.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", objProducto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Estado", objProducto.Estado);

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    idProductoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algun fallo a la hora de registrar, que nos muestre el error de la excepcion por medio de un mensaje
            catch (Exception ex)
            {
                idProductoGenerado = 0;
                Mensaje = ex.Message;
            }

            return idProductoGenerado;

        }
        // Creamos un metodo para editar a los Productos en nuestra base de datos
        public bool EditarProducto(Producto objProducto, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Productos
                    SqlCommand cmd = new SqlCommand("SP_EDITAR_PRODUCTO", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", objProducto.IdProducto);
                    cmd.Parameters.AddWithValue("Codigo", objProducto.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", objProducto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", objProducto.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", objProducto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Estado", objProducto.Estado);

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algun fallo a la hora de editar, que nos muestre el error de la excepcion por medio de un mensaje
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;

        }
        // Creamos un metodo para editar a los Productos en nuestra base de datos
        public bool EliminarProducto(Producto objProducto, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Productos
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_PRODUCTO", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", objProducto.IdProducto);
                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algun fallo a la hora de editar, que nos muestre el error de la excepcion por medio de un mensaje
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }
    }
}
