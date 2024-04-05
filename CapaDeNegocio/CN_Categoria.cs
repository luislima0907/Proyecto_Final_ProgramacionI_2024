using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class CN_Categoria
    {

        // instanciamos CD_Categoria en la capa de Negocio
        private CD_Categoria objcd_Categoria = new CD_Categoria();

        public List<Categoria> Listar()
        {
            // devolvemos el metodo de listar la tabla de Categorias
            return objcd_Categoria.Listar();
        }

        // creamos y llamamos a los metodos de registrar, editar y eliminar Categoria
        public int RegistrarCategoria(Categoria objCategoria, out string Mensaje)
        {
            // Hacemos unas validaciones al momento de registrar una nueva Categoria
            Mensaje = string.Empty;

            if (objCategoria.Descripcion == "") Mensaje += "Es necesario añadir una descripcion a la categoria.\n";
            if (Mensaje != string.Empty) return 0;
            else return objcd_Categoria.RegistrarCategoria(objCategoria, out Mensaje);
        }

        public bool EditarCategoria(Categoria objCategoria, out string Mensaje)
        {
            // Hacemos las mismas validaciones de registrar un Categoria pero ahora en editar para que se mantenga la igualdad en ambos metodos
            Mensaje = string.Empty;

            if (objCategoria.Descripcion == "") Mensaje += "Es necesario añadir una descripcion a la categoria.\n";
            if (Mensaje != string.Empty) return false;
            else return objcd_Categoria.EditarCategoria(objCategoria, out Mensaje);
        }

        public bool EliminarCategoria(Categoria objCategoria, out string Mensaje)
        {
            return objcd_Categoria.EliminarCategoria(objCategoria, out Mensaje);
        }
    }
}
