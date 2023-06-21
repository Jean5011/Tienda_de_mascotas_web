using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    public class DetalleVenta {

        public static class Columns
        {
            public const string CodVenta_Dv = "CodVenta_Dv";
            public const string CodProducto_Dv = "CodProducto_Dv";
            public const string CUITProv = "CUITProveedor_Dv";
            public const string Cantidad_Dv = "Cantidad_Dv";
            public const string PrecioUnitario_Dv = "PrecioUnitario_Dv";
            public const string PrecioTotal_Dv = "PrecioTotal_Dv";
            public const string Estado_Dv = "Estado_Dv";
         
        }

        public const string Table = "DetalleDeVenta";

        public Venta Id { get; set; } 
        public Producto Producto { get; set; }
        public Proveedor Proveedor { get; set; } // ¿Producto no trae ya el CUIT del Proveedor?
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double PrecioTotal { get; set; }
        public bool Estado { get; set; }

    }
}

