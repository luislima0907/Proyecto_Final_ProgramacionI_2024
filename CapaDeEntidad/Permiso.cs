using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad Permiso le asignará ciertos permisos con respecto a la vistas de los menú
    // a los tipos de usuario (Administrador y Empleado) según que rol tenga el usuario tendrá acceso
    // a mas funciones en el programa
    public class Permiso
    {
        public int IdPermiso { set; get; }

        // Necesitamos saber el rol que tiene el usuario registrado para asignarle los permisos
        // para las vistas de los menú en el programa
        public Rol oRol { set; get; }
        public string NombreDeMenu { set; get; }
        public string FechaDeRegistro { set; get; }

    }
}
