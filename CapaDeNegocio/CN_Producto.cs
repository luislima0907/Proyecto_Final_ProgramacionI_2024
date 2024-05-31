using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class CN_Producto
    {
        // instanciamos CD_Producto en la capa de Negocio para acceder a los metodos que contienen las consultas
        // en sql server
        private CD_Producto objcd_Producto = new CD_Producto();

        public List<Producto> Listar()
        {
            // devolvemos el metodo de listar la tabla de Productos
            return objcd_Producto.Listar();
        }

        // creamos y llamamos a los metodos de registrar, editar y eliminar Producto
        public int RegistrarProducto(Producto objProducto, out string Mensaje)
        {
            // Hacemos unas validaciones al momento de registrar un nuevo Producto
            Mensaje = string.Empty;

            if (objProducto.Codigo == "") Mensaje += "Es necesario añadir el codigo del producto.\n";
            if (objProducto.Nombre == "") Mensaje += "Es necesario añadir el nombre del producto.\n";
            if (objProducto.Descripcion == "") Mensaje += "Es necesario añadir una descripcion al producto.\n";

            if (Mensaje != string.Empty) return 0;
            else return objcd_Producto.RegistrarProducto(objProducto, out Mensaje);
        }

        public bool EditarProducto(Producto objProducto, out string Mensaje)
        {
            // Hacemos las mismas validaciones de registrar un Producto pero ahora en editar para que se mantenga la igualdad en ambos metodos
            Mensaje = string.Empty;

            if (objProducto.Codigo == "") Mensaje += "Es necesario añadir el código del producto.\n";
            if (objProducto.Nombre == "") Mensaje += "Es necesario añadir el nombre del producto.\n";
            if (objProducto.Descripcion == "") Mensaje += "Es necesario añadir una descripción al producto.\n";

            if (Mensaje != string.Empty) return false;
            else return objcd_Producto.EditarProducto(objProducto, out Mensaje);
        }

        public bool EliminarProducto(Producto objProducto, out string Mensaje)
        {
            return objcd_Producto.EliminarProducto(objProducto, out Mensaje);
        }
    }
}
