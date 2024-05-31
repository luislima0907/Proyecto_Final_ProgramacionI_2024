﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    // La entidad Venta administrará toda la información de una venta que le hagamos a un cliente.
    public class Venta
    {
        public int IdVenta { get; set; }

        // Necesitamos saber quien realizo la venta por medio de esta relacion entre tablas
        public Usuario oUsuario { get; set; }
        public string TipoDeDocumento { get; set; }
        public string NumeroDeDocumento { get; set; }
        public string DocumentoDelCliente { get; set; }
        public string NombreDelCliente { get; set; }
        public decimal MontoDePago { get; set; }
        public decimal MontoDeCambio { get; set; }
        public decimal MontoTotal { get; set; }

        // Creamos una lista para almacenar las ventas que tengamos, ya que puede involucrar varios productos como en el caso de las compras
        public List<Detalle_Venta> oDetalleVenta { get; set; }
        public string FechaDeRegistro { get; set; }
    }
}
