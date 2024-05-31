using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    // Esta clase manejará todas las consultas en nuestra base de datos relacionadas a la tabla de Negocio
    public class CD_Negocio
    {
        // Creamos un método para obtener los datos del negocio
        public Negocio ObtenerDatos()
        {
            // Creamos un objeto de tipo negocio
            Negocio objNegocio = new Negocio();

            // Hacemos un try catch para manejar los fallos al momento de conectar la base de datos
            try
            {
                // usamos la cadena de conexión para acceder a la base de datos
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    // abrimos la conexión
                    conexion.Open();

                    // Almacenamos la consulta de sql en una string, como este programa solo manejará un negocio
                    // le ponemos como id 1
                    string query = "select IdNegocio,Nombre,RUC,Direccion from NEGOCIO where IdNegocio = 1";

                    // Instanciamos SqlCommand para poder usar la query y la cadena de conexión hacia nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query, conexion);

                    // le decimos que el tipo de comando será de tipo texto
                    cmd.CommandType = CommandType.Text;

                    // Leemos la consulta y la ejecutamos
                    using (SqlDataReader dataReader = cmd.ExecuteReader()) 
                    {
                        // mientras se pueda leer la consulta que declaramos en sql
                        while (dataReader.Read())
                        {
                            // que nos cree un objeto de tipo negocio
                            objNegocio = new Negocio()
                            {
                                IdNegocio = int.Parse(dataReader["IdNegocio"].ToString()),
                                Nombre = dataReader["Nombre"].ToString(),
                                RUC = dataReader["RUC"].ToString(),
                                Direccion = dataReader["Direccion"].ToString()
                            };
                        } 
                    }

                }
            }
            catch
            {
                // en caso de algún fallo con la conexión, que nos devuelva el negocio vacio
                objNegocio = new Negocio();
            }
            return objNegocio;
        }

        // Creamos un método para guardar la información del negocio
        public bool GuardarDatos(Negocio objNegocio, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            // Hacemos un try catch para manejar los fallos al momento de conectar la base datos
            try
            {
                // usamos la cadena de conexión hacia nuestra base de datos
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    // abrimos la conexión
                    conexion.Open();

                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Nombre = @Nombre,");
                    query.AppendLine("RUC = @RUC,");
                    query.AppendLine("Direccion = @Direccion");
                    query.AppendLine("where IdNegocio = 1");

                    // Instanciamos SqlCommand para poder usar la query y la cadena de conexión hacia nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@Nombre", objNegocio.Nombre);
                    cmd.Parameters.AddWithValue("@RUC", objNegocio.RUC);
                    cmd.Parameters.AddWithValue("@Direccion", objNegocio.Direccion);

                    // le decimos el comando será de tipo texto
                    cmd.CommandType = CommandType.Text;

                    // si no afecta a ninguna fila nuestra consulta, que nos muestre un mensaje
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudieron guardar los datos del negocio";
                        respuesta = false;
                    }

                }
            }
            catch (Exception ex) 
            {
                // en caso de que falle la conexión se mostrará un mensaje
                mensaje = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

        // Creamos un método para Obtener el logo de nuestro negocio
        public byte[] ObtenerLogo(out bool obtenido)
        {
            obtenido = true;

            // creamos un array de tipo byte para almacenar el logo
            byte[] LogoBytes = new byte[0];

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string query = "select Logo from NEGOCIO where IdNegocio = 1";

                    SqlCommand cmd = new SqlCommand(query, conexion);

                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            // Hacemos esta conversion a byte ya que transformamos el logo a un formato de codigo y no a uno de base de datos 
                            LogoBytes = (byte[])dataReader["Logo"];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                obtenido = false;
                LogoBytes = new byte[0];
            }
            return LogoBytes;
        }

        // Creamos un método para actualizar y subir un logo nuevo para el negocio
        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Logo = @Imagen");
                    query.AppendLine("where IdNegocio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@Imagen", imagen);

                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";
                        respuesta = false;
                    }

                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}
