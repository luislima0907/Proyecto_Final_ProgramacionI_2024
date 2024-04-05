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
    public class CD_Compra
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
                    query.AppendLine("select count(*) + 1 from COMPRA");
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

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera una procedura
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
    }
}
