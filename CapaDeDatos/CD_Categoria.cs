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
    // Esta clase manejará todas las consultas en nuestra base de datos relacionadas a la tabla de Categoría
    public class CD_Categoria
    {
        // Creamos una lista con las categorías que tengamos en nuestro programa
        public List<Categoria> Listar()
        {
            // Instanciamos la lista para poder almacenar valores(categorias) dentro de ella
            List<Categoria> lista = new List<Categoria>();

            // Usamos la cadena de conexión hacia nuestra base de datos para poder hacer las consultas
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                // Hacemos un try catch en caso de que la conexión falle
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCategoria,Descripcion,Estado from CATEGORIA");

                    // instanciamos SqlCommand para poder realizar la seleccion de la tabla con la query(consulta) y la conexión a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);

                    // aqui le estamos diciendo que el tipo de comando que ejecutará sera de tipo texto
                    cmd.CommandType = CommandType.Text;

                    // Abrimos la conexión hacia la base de datos 
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de Categorias
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga las Categorias de la tabla
                            lista.Add(new Categoria() // Creamos un nuevo objeto de tipo categoria para almacenarla en la lista
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

        // Creamos un metodo para registrar las Categorias y almacenarlas en nuestra base de datos
        public int RegistrarCategoria(Categoria objCategoria, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            // Hacemos un try catch en caso de que la conexión falle
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parámetros con valores hacemos posible el registro de las Categorías
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_CATEGORIA", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", objCategoria.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", objCategoria.Estado);

                    // Para los datos de salida tenemos que darle el nombre del parámetro en sql y despues definir de que tipo es,
                    // asi mismo le especificamos por medio de Direction el tipo de dato que es, si es de entrada o salida.
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    // Ejecutamos la consulta que hicimos en sql, en este caso una procedura
                    cmd.ExecuteNonQuery();

                    // Almacenamos el resultado que nos dió la consulta.
                    resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algún fallo a la hora de registrar, que nos muestre el error de la excepción por medio de un mensaje
            catch (Exception ex)
            {
                resultado = 0;
                Mensaje = ex.Message;
            }

            return resultado;

        }

        // Creamos un método para editar a los Categorias en nuestra base de datos
        public bool EditarCategoria(Categoria objCategoria, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parámetros con valores hacemos posible la edición de las Categorias
                    SqlCommand cmd = new SqlCommand("SP_EDITAR_CATEGORIA", oConexion);
                    cmd.Parameters.AddWithValue("IdCategoria", objCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", objCategoria.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", objCategoria.Estado);

                    // Para los datos de salida tenemos que darle el nombre del parámetro en sql y despues definir de que tipo es, asi mismo,
                    // le específicamos por medio de direction el tipo de dato que es, si es de entrada o salida.
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algún fallo a la hora de editar, que nos muestre el error de la excepción por medio de un mensaje
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        // Creamos un metodo para eliminar a las Categorias en nuestra base de datos
        public bool EliminarCategoria(Categoria objCategoria, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    // Por medio de los parámetros con valores hacemos posible la eliminación de las Categorias
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_CATEGORIA", oConexion);
                    cmd.Parameters.AddWithValue("IdCategoria", objCategoria.IdCategoria);

                    // Para los datos de salida tenemos que darle el nombre del parámetro en sql y despues definir de que tipo es, así mismo,
                    // le específicamos por medio de Direction el tipo de dato que es, si es de entrada o salida.
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            // Si causa algún fallo a la hora de editar, que nos muestre el error de la excepción por medio de un mensaje
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
