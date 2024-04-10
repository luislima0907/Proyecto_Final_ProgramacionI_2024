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
    public class CD_Reporte
    {
        public List<ReporteDeCompras> Compra(string fechaDeInicio, string fechaDeFin, int idProveedor)
        {
            List<ReporteDeCompras> lista = new List<ReporteDeCompras>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();

                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand("SP_REPORTE_DE_COMPRAS", oConexion);
                    cmd.Parameters.AddWithValue("FechaDeInicio", fechaDeInicio);
                    cmd.Parameters.AddWithValue("FechaDeFin", fechaDeFin);
                    cmd.Parameters.AddWithValue("IdProveedor", idProveedor);

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();

                    // hacemos que pueda leer nuestro comando y ejecutarlo
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // mientras pueda leer la seleccion que hicimos hacia la tabla de ReporteDeComprass
                        while (dr.Read())
                        {
                            // que pueda crear una lista que contenga los ReporteDeComprass de la tabla
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
                    // si hay algun error a la hora de crear la lista, que la devuelva vacia
                    lista = new List<ReporteDeCompras>();
                }
            }
            return lista;
        }


        public List<ReporteDeVentas> Venta(string fechaDeInicio, string fechaDeFin)
        {
            List<ReporteDeVentas> lista = new List<ReporteDeVentas>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Instanciamos StringBuilder, ya que nos permite hacer consultas con saltos de linea en sql
                    StringBuilder query = new StringBuilder();

                    // instanciamos la seleccion de la tabla con el query y la conexion a nuestra base de datos
                    SqlCommand cmd = new SqlCommand("SP_REPORTE_DE_VENTAS", oConexion);
                    cmd.Parameters.AddWithValue("FechaDeInicio", fechaDeInicio);
                    cmd.Parameters.AddWithValue("FechaDeFin", fechaDeFin);

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
                    // si hay algun error a la hora de crear la lista, que la devuelva vacia
                    lista = new List<ReporteDeVentas>();
                }
            }
            return lista;
        }
    }
}
