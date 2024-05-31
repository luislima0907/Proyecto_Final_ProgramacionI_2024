using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad Detalle_Venta se encargará de hacer un reporte sobre la venta que le hagamos a un cliente
    public class Detalle_Venta
    {
        public int IdDetalleDeLaVenta { get; set; }

        // Necesitamos saber la informacion del producto que vamos a vender a un cliente.
        public Producto oProducto { get; set; }
        public decimal PrecioDeVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public string FechaDeRegistro { get; set; }

    }
}
