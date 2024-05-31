using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class CN_Proveedor
    {
        // instanciamos CD_Proveedor en la capa de Negocio para acceder a los metodos que contienen las consultas
        // en sql server
        private CD_Proveedor objcd_Proveedor = new CD_Proveedor();

        public List<Proveedor> Listar()
        {
            // devolvemos el metodo de listar la tabla de Proveedores
            return objcd_Proveedor.Listar();
        }

        // creamos y llamamos a los metodos de registrar, editar y eliminar Proveedor
        public int RegistrarProveedor(Proveedor objProveedor, out string Mensaje)
        {
            // Hacemos unas validaciones al momento de registrar un nuevo Proveedor
            Mensaje = string.Empty;

            if (objProveedor.Documento == "") Mensaje += "Es necesario añadir el documento del Proveedor.\n";
            if (objProveedor.RazonSocial == "") Mensaje += "Es necesario añadir la razón social del Proveedor.\n";
            if (objProveedor.Correo == "") Mensaje += "Es necesario añadir un correo al Proveedor.\n";

            if (Mensaje != string.Empty) return 0;
            else return objcd_Proveedor.RegistrarProveedor(objProveedor, out Mensaje);
        }

        public bool EditarProveedor(Proveedor objProveedor, out string Mensaje)
        {
            // Hacemos las mismas validaciones de registrar un Proveedor pero ahora en editar para que se mantenga la igualdad en ambos metodos
            Mensaje = string.Empty;

            if (objProveedor.Documento == "") Mensaje += "Es necesario añadir el documento del Proveedor.\n";
            if (objProveedor.RazonSocial == "") Mensaje += "Es necesario añadir la razón social del Proveedor.\n";
            if (objProveedor.Correo == "") Mensaje += "Es necesario añadir un correo al Proveedor.\n";

            if (Mensaje != string.Empty) return false;
            else return objcd_Proveedor.EditarProveedor(objProveedor, out Mensaje);
        }

        public bool EliminarProveedor(Proveedor objProveedor, out string Mensaje)
        {
            return objcd_Proveedor.EliminarProveedor(objProveedor, out Mensaje);
        }
    }
}
