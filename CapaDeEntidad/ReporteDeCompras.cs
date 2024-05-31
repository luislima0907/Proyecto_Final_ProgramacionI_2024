using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad ReporteDeCompras hará reportes de las compras realizadas en el programa
    public class ReporteDeCompras
    {
        public string FechaDeRegistro { get; set; }

        public string TipoDeDocumento { get; set; }

        public string NumeroDeDocumento { get; set; }

        public string MontoTotal { get; set; }

        public string TipoDeUsuarioRegistrado { get; set; }

        public string DocumentoDelProveedor { get; set; }

        public string RazonSocialDelProveedor { get; set; }

        public string CodigoDelProducto { get; set; }

        public string NombreDelProducto { get; set; }

        public string Categoria { get; set; }

        public string PrecioDeCompra { get; set; }

        public string PrecioDeVenta { get; set; }
        
        public string Cantidad { get; set; }

        public string SubTotal { get; set; }

    }
}
