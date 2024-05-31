using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // Esta entidad se encargará de brindarle una categoría a los productos que tengamos en nuestro inventario
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string FechaDeRegistro { get; set; }

    }
}
