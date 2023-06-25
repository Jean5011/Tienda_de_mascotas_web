using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    public class Venta {
        public class Preliminar {
            public int Id { get; set; }
            public int AffectedRows { get; set; }
        }
        public static class Columns {
            public const string Id = "CodVenta_Vt";
            public const string DNI = "DNIEmpleado_Vt";
            public const string TipoPago = "TipoPago_Vt";
            public const string Fecha = "Fecha_Vt";
            public const string Total = "PrecioTotal_Vt";
        }
        public const string Table = "Ventas";
        public int Id { get; set; }
        public Empleado EmpleadoGestor { get; set; }
        public string TipoPago { get; set; }
        public string Fecha { get; set; }
        public double Total { get; set; }

    }
}

