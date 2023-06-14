using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    class Producto {
        public int Codigo { get; set; }
        public Proveedor Proveedor { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }
        public double Precio { get; set; }
        public bool Estado { get; set; }

    }
}
