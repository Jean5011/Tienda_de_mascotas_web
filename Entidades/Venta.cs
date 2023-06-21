using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    public class Venta {
        public int Id { get; set; }
        public Empleado EmpleadoGestor { get; set; }
        public int TipoPago { get; set; }
        public string Fecha { get; set; }
        public double Total { get; set; }

    }
}

