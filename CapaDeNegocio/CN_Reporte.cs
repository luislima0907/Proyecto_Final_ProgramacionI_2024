using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class CN_Reporte
    {
        // instanciamos CD_Reporte en la capa de Negocio
        private CD_Reporte objcd_Reporte = new CD_Reporte();

        public List<ReporteDeCompras> Compra(string fechaDeInicio, string fechaDeFin, int idProveedor)
        {
            // devolvemos el metodo de listar la tabla de ReporteDeCompras
            return objcd_Reporte.Compra(fechaDeInicio, fechaDeFin, idProveedor);
        }

        public List<ReporteDeVentas> Venta(string fechaDeInicio, string fechaDeFin)
        {
            // devolvemos el metodo de listar la tabla de ReporteDeVentas
            return objcd_Reporte.Venta(fechaDeInicio, fechaDeFin);
        }

    }
}
