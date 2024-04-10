using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    public class ReporteDeVentas
    {
        public string FechaDeRegistro { get; set; }

        public string TipoDeDocumento { get; set; }

        public string NumeroDeDocumento { get; set; }

        public string MontoTotal { get; set; }

        public string TipoDeUsuarioRegistrado { get; set; }

        public string DocumentoDelCliente { get; set; }

        public string NombreDelCliente { get; set; }

        public string CodigoDelProducto { get; set; }

        public string NombreDelProducto { get; set; }

        public string Categoria { get; set; }

        public string PrecioDeVenta { get; set; }

        public string Cantidad { get; set; }

        public string SubTotal { get; set; }
    }
}
