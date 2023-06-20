using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    public class Producto {


        public static class Columns
        {
            public const string Codigo_Prod = "CodProducto_Prod";
            public const string CUITProv = "CUITProveedor_Prod";
            public const string CodTipoProducto = "CodTipoProducto_Prod";
            public const string Nombre = "Nombre_Prod";
            public const string Marca = "Marca_Prod";
            public const string Descripcion = "Descripcion_Prod";
            public const string Stock = "Stock_Prod";
            public const string Imagen = "Imagen_Prod";
            public const string Precio = "PrecioUnitario_Prod";
            public const string Estado = "Estado_Prod";
        }

        public const string Table = "Productos";

        public string Codigo { get; set; }
        public  Proveedor Proveedor { get; set; }
        public TipoProducto Categoria { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        public double Precio { get; set; }
        public bool Estado { get; set; }

    }
}

