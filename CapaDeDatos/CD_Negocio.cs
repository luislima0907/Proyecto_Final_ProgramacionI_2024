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
    public class CD_Negocio
    {
        public Negocio ObtenerDatos()
        {
            Negocio objNegocio = new Negocio();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select IdNegocio,Nombre,RUC,Direccion from NEGOCIO where IdNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dataReader = cmd.ExecuteReader()) 
                    {
                        while (dataReader.Read())
                        {
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
                objNegocio = new Negocio();
            }
            return objNegocio;
        }

        public bool GuardarDatos(Negocio objNegocio, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Nombre = @Nombre,");
                    query.AppendLine("RUC = @RUC,");
                    query.AppendLine("Direccion = @Direccion");
                    query.AppendLine("where IdNegocio = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@Nombre", objNegocio.Nombre);
                    cmd.Parameters.AddWithValue("@RUC", objNegocio.RUC);
                    cmd.Parameters.AddWithValue("@Direccion", objNegocio.Direccion);

                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudieron guardar los datos del negocio";
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

        public byte[] ObtenerLogo(out bool obtenido)
        {
            obtenido = true;
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
