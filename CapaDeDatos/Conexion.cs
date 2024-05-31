using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDeDatos
{
    // Esta clase manejará todas las consultas en nuestra base de datos por medio de la cadena de conexión que creemos.
    public class Conexion
    {
        // la hacemos estatica y publica para poderla usar en cualquier parte de nuestro programa
        public static string cadena = ConfigurationManager.ConnectionStrings["cadena_conexion"].ToString();
    }
}
