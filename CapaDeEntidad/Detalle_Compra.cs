using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad Detalle_Compra se encargará de hacer un reporte de la compra de un producto que necesitemos en el programa
    public class Detalle_Compra
    {
        public int IdDetalleDeLaCompra { get; set; }

        // Necesitamos saber la informacion del producto que vamos a comprar
        public Producto oProducto { get; set; }
        public decimal PrecioDeCompra { get; set; }
        public decimal PrecioDeVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal MontoTotal { get; set; }
        public string FechaDeRegistro { get; set; }
    }
}
