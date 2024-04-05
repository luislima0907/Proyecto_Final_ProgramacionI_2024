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
    public class CD_Categoria
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCategoria,Descripcion,Estado from CATEGORIA");
                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera un texto
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de Categorias
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga los Categorias de la tabla
                            lista.Add(new Categoria()
                            {
                                // aqui convertimos el tipo de valor que contendra cada dato de nuestra tabla, tenemos que definirlo igual que cuando lo declaramos en sql
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // si hay algun error a la hora de crear la lista, que la devuelva vacia
                    lista = new List<Categoria>();
                }
            }
            return lista;
        }

        // Creamos un metodo para registrar los Categorias y almacenarlos en nuestra base de datos
        public int RegistrarCategoria(Categoria objCategoria, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Categorias
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_CATEGORIA", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", objCategoria.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", objCategoria.Estado);

                    // Para los datos de salida tenemos que darle el nombre del parametro en sql y despues definir de que tipo es, asi mismo le especificamos por medio de direction el tipo de dato que es, si es de entrada o salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algun fallo a la hora de registrar, que nos muestre el error de la excepcion por medio de un mensaje
            catch (Exception ex)
            {
                resultado = 0;
                Mensaje = ex.Message;
            }

            return resultado;

        }

        // Creamos un metodo para editar a los Categorias en nuestra base de datos
        public bool EditarCategoria(Categoria objCategoria, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Categorias
                    SqlCommand cmd = new SqlCommand("SP_EDITAR_CATEGORIA", oConexion);
                    cmd.Parameters.AddWithValue("IdCategoria", objCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", objCategoria.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", objCategoria.Estado);

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

        // Creamos un metodo para editar a los Categorias en nuestra base de datos
        public bool EliminarCategoria(Categoria objCategoria, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parametros con valores hacemos posible el registro de los Categorias
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_CATEGORIA", oConexion);
                    cmd.Parameters.AddWithValue("IdCategoria", objCategoria.IdCategoria);

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
    }
}
