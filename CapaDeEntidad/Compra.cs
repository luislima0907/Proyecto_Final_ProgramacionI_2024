using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad Compra será la encargada de administrar la información al momento de hacer una compra de algún producto que necesitemos en nuestro inventario
    public class Compra
    {
        public int IdCompra { get; set; }

        // Relacionamos un usuario a una compra para poderla realizar y decir quien la hizo.
        public Usuario oUsuario { get; set; }

        // Obtenemos los datos del proveedor al momento de realizar una compra.
        public Proveedor oProveedor { get; set; }
        public string TipoDeDocumento { get; set; }
        public string NumeroDeDocumento { get; set; }
        public decimal MontoTotal { get; set; }

        // se crea una lista para almacenar todos los productos involucrados en una venta, ya que una compra puede contener mas de un producto
        public List<Detalle_Compra> oDetalleCompra { get; set; }
        public string FechaDeRegistro { get; set; }
    }
}
