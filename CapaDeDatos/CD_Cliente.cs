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
    public class CD_Cliente
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCliente,Documento,NombreCompleto,Correo,Telefono,Estado from CLIENTE");
                    
                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    
                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera un texto
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de Clientes
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga los Clientes de la tabla
                            lista.Add(new Cliente()
                            {
                                // aqui convertimos el tipo de valor que contendra cada dato de nuestra tabla, tenemos que definirlo igual que cuando lo declaramos en sql
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
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
                    lista = new List<Cliente>();
                }
            }
            return lista;
        }

        // Creamos un metodo para registrar los Clientes y almacenarlos en nuestra base de datos
        public int RegistrarCliente(Cliente objCliente, out string Mensaje)
        {
            int idClienteGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Clientes
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_CLIENTE", oConexion);
                    cmd.Parameters.AddWithValue("Documento", objCliente.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", objCliente.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", objCliente.Correo);
                    cmd.Parameters.AddWithValue("Telefono", objCliente.Telefono);
                    cmd.Parameters.AddWithValue("Estado", objCliente.Estado);

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    idClienteGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algun fallo a la hora de registrar, que nos muestre el error de la excepcion por medio de un mensaje
            catch (Exception ex)
            {
                idClienteGenerado = 0;
                Mensaje = ex.Message;
            }
            return idClienteGenerado;
        }

        // Creamos un metodo para editar a los Clientes en nuestra base de datos
        public bool EditarCliente(Cliente objCliente, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Clientes
                    SqlCommand cmd = new SqlCommand("SP_EDITAR_CLIENTE", oConexion);
                    cmd.Parameters.AddWithValue("IdCliente", objCliente.IdCliente);
                    cmd.Parameters.AddWithValue("Documento", objCliente.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", objCliente.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", objCliente.Correo);
                    cmd.Parameters.AddWithValue("Telefono", objCliente.Telefono);
                    cmd.Parameters.AddWithValue("Estado", objCliente.Estado);

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

        // Creamos un metodo para editar a los Clientes en nuestra base de datos
        public bool EliminarCliente(Cliente objCliente, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores, hacemos posible la eliminacion de los Clientes
                    SqlCommand cmd = new SqlCommand("delete from cliente where IdCliente = @Id", oConexion);
                    cmd.Parameters.AddWithValue("@Id", objCliente.IdCliente);

                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    // Si encuentra filas por eliminar, devuelve true, de lo contrario, no elimina naday devuelve false
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
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
