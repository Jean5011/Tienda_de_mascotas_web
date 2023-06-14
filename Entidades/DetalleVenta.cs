using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    class DetalleVenta {
        public Venta Cabecera { get; set; } // ¿Dejamos Venta (Cabecera) o implementamos un Id?
        public Producto Producto { get; set; }
        public Proveedor Proveedor { get; set; } // ¿Producto no trae ya el CUIT del Proveedor?
        public string Fecha { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double PrecioTotal { get; set; }

    }
}
