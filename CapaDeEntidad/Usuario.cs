using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad Usuario se encargará de administrar la información del usuario que registremos dentro del programa
    // para su respectivo uso
    public class Usuario
    {
        public int IdUsuario { get; set; }

        // Le asignamos un rol al usuario que registremos, puede ser administrador o empleado
        public Rol oRol { get; set; }
        public string Documento { get; set; }
        public string Contraseña { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public string FechaDeRegistro { get; set; }

    }
}
