using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    public class TipoProducto 
    {
        public static class Columns
        {
            public const string Codigo = "PK_CodTipoProducto_TP";
            public const string CodAnimal = "CodAnimales_Tp";
            public const string TipoDeProducto= "TipoDeProducto_Tp";
            public const string Descripcion = "Descripcion_TP";
        }
        public const string Table = "TipoDeProductos";

            public string Codigo { get; set; }
            public string CodAnimal { get; set; }
            public string tipoDeProducto { get; set; }
            public string Descripcion { get; set; }



    }
}

