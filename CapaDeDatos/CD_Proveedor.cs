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
    public class CD_Proveedor
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProveedor,Documento,RazonSocial,Correo,Telefono,Estado from PROVEEDOR\r\n");

                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    
                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera un texto
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de Proveedors
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga los Proveedors de la tabla
                            lista.Add(new Proveedor()
                            {
                                // aqui convertimos el tipo de valor que contendra cada dato de nuestra tabla, tenemos que definirlo igual que cuando lo declaramos en sql
                                IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                Documento = dr["Documento"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // si hay algun error a la hora de crear la lista, que la devuelva vacia
                    lista = new List<Proveedor>();
                }
            }
            return lista;
        }

        // Creamos un metodo para registrar los Proveedors y almacenarlos en nuestra base de datos
        public int RegistrarProveedor(Proveedor objProveedor, out string Mensaje)
        {
            int idProveedorGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Proveedors
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_PROVEEDOR", oConexion);
                    cmd.Parameters.AddWithValue("Documento", objProveedor.Documento);
                    cmd.Parameters.AddWithValue("RazonSocial", objProveedor.RazonSocial);
                    cmd.Parameters.AddWithValue("Correo", objProveedor.Correo);
                    cmd.Parameters.AddWithValue("Telefono", objProveedor.Telefono);
                    cmd.Parameters.AddWithValue("Estado", objProveedor.Estado);

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    idProveedorGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algun fallo a la hora de registrar, que nos muestre el error de la excepcion por medio de un mensaje
            catch (Exception ex)
            {
                idProveedorGenerado = 0;
                Mensaje = ex.Message;
            }
            return idProveedorGenerado;
        }

        // Creamos un metodo para editar a los Proveedors en nuestra base de datos
        public bool EditarProveedor(Proveedor objProveedor, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Proveedors
                    SqlCommand cmd = new SqlCommand("SP_EDITAR_PROVEEDOR", oConexion);
                    cmd.Parameters.AddWithValue("IdProveedor", objProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("Documento", objProveedor.Documento);
                    cmd.Parameters.AddWithValue("RazonSocial", objProveedor.RazonSocial);
                    cmd.Parameters.AddWithValue("Correo", objProveedor.Correo);
                    cmd.Parameters.AddWithValue("Telefono", objProveedor.Telefono);
                    cmd.Parameters.AddWithValue("Estado", objProveedor.Estado);

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
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

        // Creamos un metodo para editar a los Proveedors en nuestra base de datos
        public bool EliminarProveedor(Proveedor objProveedor, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Proveedors
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_PROVEEDOR", oConexion);
                    cmd.Parameters.AddWithValue("IdProveedor", objProveedor.IdProveedor);

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
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
