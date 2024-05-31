using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class CN_Permiso
    {
        // instanciamos CD_Permiso en la capa de Negocio para acceder a los metodos que contienen las consultas
        // en sql server
        private CD_Permiso objcd_permiso = new CD_Permiso();

        // el metodo recibe una lista con el parametro de IdUsuario, se usa el de usuario ya que cada
        // usuario según su rol tiene ciertos permisos en el programa
        public List<Permiso> Listar(int IdUsuario)
        {
            // devolvemos el metodo de listar la tabla de permisos con el parametro del IdUsuario
            return objcd_permiso.Listar(IdUsuario);
        }
    }
}
