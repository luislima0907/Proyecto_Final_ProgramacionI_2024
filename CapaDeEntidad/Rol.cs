using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad Rol asignará roles a los usuarios que se registren en el programa
    // Los roles son: Administrador y Empleado, cada uno tiene ciertas prioridades dentro del programa
    public class Rol
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public string FechaDeRegistro { get; set; }

    }
}
