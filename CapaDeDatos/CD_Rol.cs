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
    // Esta clase manejará todas las consultas en nuestra base de datos relacionadas a la tabla de Rol
    public class CD_Rol
    {
        // Creamos un método para listar los roles
        public List<Rol> Listar()
        {
            // Creamos una lista de roles
            List<Rol> lista = new List<Rol>();

            // Utilizamos la cadena de conexión para acceder a la base de datos
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                // Hacemos un try catch para manejar los errores de la conexión
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdRol,Descripcion from ROL");

                    // instanciamos SqlCommand para realizar la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);

                    // aqui le estamos diciendo que el tipo de comando que ejecutara sera de tipo texto
                    cmd.CommandType = CommandType.Text;

                    // abrimos la conexion hacia nuestra base de datos
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de Rol
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga los usuarios de la tabla
                            lista.Add(new Rol()
                            {
                                // aqui convertimos el tipo de valor que contendra cada dato de nuestra tabla, tenemos que definirlo igual que cuando lo declaramos en sql
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // si hay algún error a la hora de llamar a la lista, que la devuelva vacia
                    lista = new List<Rol>();
                }
            }
            return lista;
        }
    }
}
