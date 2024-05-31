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
    // Esta clase manejará todas las consultas en nuestra base de datos relacionadas a la tabla de Reporte
    public class CD_Reporte
    {
        // Creamos un método para listar las compras que hagamos de los productos
        public List<ReporteDeCompras> Compra(string fechaDeInicio, string fechaDeFin, int idProveedor)
        {
            // Creamos una lista de los reportes de las compras
            List<ReporteDeCompras> lista = new List<ReporteDeCompras>();

            // Utilizamos la cadena de conexión para acceder a la base de datos
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                // Hacemos un try catch para manejar los errores de la conexión
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();

                    // instanciamos SqlCommand para realizar la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand("SP_REPORTE_DE_COMPRAS", oConexion);
                    cmd.Parameters.AddWithValue("FechaDeInicio", fechaDeInicio);
                    cmd.Parameters.AddWithValue("FechaDeFin", fechaDeFin);
                    cmd.Parameters.AddWithValue("IdProveedor", idProveedor);

                    // le decimos que el tipo de comando sera una procedura
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de ReporteDeCompras
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga los ReporteDeCompras de la tabla
                            lista.Add(new ReporteDeCompras()
                            {
                                // aqui convertimos el tipo de valor que contendra cada dato de nuestra tabla, tenemos que definirlo igual que cuando lo declaramos en sql
                                FechaDeRegistro = dr["FechaDeRegistro"].ToString(),
                                TipoDeDocumento = dr["TipoDeDocumento"].ToString(),
                                NumeroDeDocumento = dr["NumeroDeDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                TipoDeUsuarioRegistrado = dr["TipoDeUsuarioRegistrado"].ToString(),
                                DocumentoDelProveedor = dr["DocumentoDelProveedor"].ToString(),
                                RazonSocialDelProveedor = dr["RazonSocial"].ToString(),
                                CodigoDelProducto = dr["CodigoDelProducto"].ToString(),
                                NombreDelProducto = dr["NombreDelProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioDeCompra = dr["PrecioDeCompra"].ToString(),
                                PrecioDeVenta = dr["PrecioDeVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // si hay algún error a la hora de crear la lista, que la devuelva vacia
                    lista = new List<ReporteDeCompras>();
                }
            }
            return lista;
        }

        // Creamos un método para listar las ventas que le hagamos a los clientes
        public List<ReporteDeVentas> Venta(string fechaDeInicio, string fechaDeFin)
        {
            // Creamos una lista para los reportes de las ventas
            List<ReporteDeVentas> lista = new List<ReporteDeVentas>();

            // Utilizamos la cadena de conexión para acceder a la base de datos
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                // Hacemos un try catch para manejar los errores de la conexión
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();

                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand("SP_REPORTE_DE_VENTAS", oConexion);
                    cmd.Parameters.AddWithValue("FechaDeInicio", fechaDeInicio);
                    cmd.Parameters.AddWithValue("FechaDeFin", fechaDeFin);

                    // le decimos que el tipo de comando es una procedura
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de ReporteDeComprass
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga los ReporteDeComprass de la tabla
                            lista.Add(new ReporteDeVentas()
                            {
                                // aqui convertimos el tipo de valor que contendra cada dato de nuestra tabla, tenemos que definirlo igual que cuando lo declaramos en sql
                                FechaDeRegistro = dr["FechaDeRegistro"].ToString(),
                                TipoDeDocumento = dr["TipoDeDocumento"].ToString(),
                                NumeroDeDocumento = dr["NumeroDeDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                TipoDeUsuarioRegistrado = dr["TipoDeUsuarioRegistrado"].ToString(),
                                DocumentoDelCliente = dr["DocumentoDelCliente"].ToString(),
                                NombreDelCliente = dr["NombreDelCliente"].ToString(),
                                CodigoDelProducto = dr["CodigoDelProducto"].ToString(),
                                NombreDelProducto = dr["NombreDelProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioDeVenta = dr["PrecioDeVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // si hay algún error a la hora de crear la lista, que la devuelva vacia
                    lista = new List<ReporteDeVentas>();
                }
            }
            return lista;
        }
    }
}
