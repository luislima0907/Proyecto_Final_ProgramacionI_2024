using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class CN_Cliente
    {

        // instanciamos CD_Cliente en la capa de Negocio
        private CD_Cliente objcd_Cliente = new CD_Cliente();

        public List<Cliente> Listar()
        {
            // devolvemos el metodo de listar la tabla de Clientes
            return objcd_Cliente.Listar();
        }

        // creamos y llamamos a los metodos de registrar, editar y eliminar Cliente
        public int RegistrarCliente(Cliente objCliente, out string Mensaje)
        {
            // Hacemos unas validaciones al momento de registrar un nuevo Cliente
            Mensaje = string.Empty;

            if (objCliente.Documento == "") Mensaje += "Es necesario añadir el documento del Cliente.\n";
            if (objCliente.NombreCompleto == "") Mensaje += "Es necesario añadir el nombre completo del Cliente.\n";
            if (objCliente.Correo == "") Mensaje += "Es necesario añadir un correo al Cliente.\n";

            if (Mensaje != string.Empty) return 0;
            else return objcd_Cliente.RegistrarCliente(objCliente, out Mensaje);
        }

        public bool EditarCliente(Cliente objCliente, out string Mensaje)
        {
            // Hacemos las mismas validaciones de registrar un Cliente pero ahora en editar para que se mantenga la igualdad en ambos metodos
            Mensaje = string.Empty;

            if (objCliente.Documento == "") Mensaje += "Es necesario añadir el documento del Cliente.\n";
            if (objCliente.NombreCompleto == "") Mensaje += "Es necesario añadir el nombre completo del Cliente.\n";
            if (objCliente.Correo == "") Mensaje += "Es necesario añadir un correo al Cliente.\n";

            if (Mensaje != string.Empty) return false;
            else return objcd_Cliente.EditarCliente(objCliente, out Mensaje);
        }

        public bool EliminarCliente(Cliente objCliente, out string Mensaje)
        {
            return objcd_Cliente.EliminarCliente(objCliente, out Mensaje);
        }



    }
}
