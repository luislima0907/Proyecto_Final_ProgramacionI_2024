using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad Proveedor será necesaria para saber a quién le estamos comprando un producto
    // para nuestro inventario
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public string FechaDeRegistro { get; set; }

    }
}
