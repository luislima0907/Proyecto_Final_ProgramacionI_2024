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
    public class CN_Compra
    {
        // instanciamos CD_Compra en la capa de Negocio
        private CD_Compra objcd_Compra = new CD_Compra();

        public int ObtenerCorrelativo()
        {
            // devolvemos el metodo de listar la tabla de Compras
            return objcd_Compra.ObtenerCorrelativo();
        }

        // creamos y llamamos a los metodos de registrar, editar y eliminar Compra
        public bool RegistrarCompra(Compra objCompra, DataTable DetalleCompra, out string Mensaje)
        {
            // devolvemos el metodo de listar la tabla de Compras
            return objcd_Compra.Registrar(objCompra, DetalleCompra, out Mensaje);
        }
        public Compra ObtenerCompra(string numero)
        {
            // Obtenemos una compra por su numero de Documento
            Compra oCompra = objcd_Compra.ObtenerCompra(numero);

            // Si existe al menos una compra entonces nos crea una lista que almacene esa compra
            if (oCompra.IdCompra != 0)
            {
                List<Detalle_Compra> oDetalleCompra = objcd_Compra.ObtenerDetalleDeLaCompra(oCompra.IdCompra);

                oCompra.oDetalleCompra = oDetalleCompra;
            }
            return oCompra;
        }
    }
}
