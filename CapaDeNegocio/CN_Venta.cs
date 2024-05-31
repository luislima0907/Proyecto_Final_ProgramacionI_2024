using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class CN_Venta
    {
        // instanciamos CD_Venta en la capa de Negocio para acceder a los metodos que contienen las consultas
        // en sql server
        private CD_Venta objcd_Venta = new CD_Venta();

        public int ObtenerCorrelativo()
        {
            // devolvemos el metodo de listar la tabla de Ventas
            return objcd_Venta.ObtenerCorrelativo();
        }

        // creamos y llamamos a los metodos de Registrar, RestarStock, SumarStock y ObtenerVenta de la venta
        public bool RegistrarVenta(Venta objVenta, DataTable DetalleVenta, out string Mensaje)
        {
            // devolvemos el metodo de listar la tabla de Ventas
            return objcd_Venta.Registrar(objVenta, DetalleVenta, out Mensaje);
        }
        public bool RestarStock(int IdProducto, int Cantidad)
        {
            return objcd_Venta.RestarStock(IdProducto, Cantidad);
        }

        public bool SumarStock(int IdProducto, int Cantidad)
        {
            return objcd_Venta.SumarStock(IdProducto, Cantidad);
        }

        public Venta ObtenerVenta(string numero)
        {
            // Obtenemos una Venta por su numero de Documento
            Venta oVenta = objcd_Venta.ObtenerVenta(numero);

            // Si existe al menos una Venta entonces nos crea una lista que almacene esa Venta
            if (oVenta.IdVenta != 0)
            {
                List<Detalle_Venta> oDetalleVenta = objcd_Venta.ObtenerDetalleDeLaVenta(oVenta.IdVenta);

                oVenta.oDetalleVenta = oDetalleVenta;
            }
            return oVenta;
        }
    }
}
