using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class CN_Negocio
    {
        // instanciamos CD_Negocio en la capa de Negocio para acceder a los metodos que contienen las consultas
        // en sql server
        private CD_Negocio objcd_Negocio = new CD_Negocio();

        public Negocio ObtenerDatos()
        {
            // devolvemos el metodo de listar la tabla de Negocios
            return objcd_Negocio.ObtenerDatos();
        }

        // creamos y llamamos a los metodos de Guardar, ObtenerLogo y ActualizarLogo del Negocio
        public bool GuardarDatos(Negocio objNegocio, out string Mensaje)
        {
            // Hacemos unas validaciones al momento de registrar un nuevo Negocio
            Mensaje = string.Empty;

            if (objNegocio.Nombre == "") Mensaje += "Es necesario añadir el nombre del Negocio.\n";
            if (objNegocio.RUC == "") Mensaje += "Es necesario añadir el número de RUC del Negocio.\n";
            if (objNegocio.Direccion == "") Mensaje += "Es necesario añadir la dirección del Negocio.\n";

            if (Mensaje != string.Empty) return false;
            else return objcd_Negocio.GuardarDatos(objNegocio, out Mensaje);
        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            // devolvemos el metodo de listar la tabla de Negocios
            return objcd_Negocio.ObtenerLogo(out obtenido);
        }


        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            // devolvemos el metodo de listar la tabla de Negocios
            return objcd_Negocio.ActualizarLogo(imagen, out mensaje);
        }

    }
}
