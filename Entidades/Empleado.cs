using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    class Empleado {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaContrato { get; set; }
        public double Sueldo { get; set; }
        public string Direccion { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string Nacionalidad { get; set; }
        public bool Estado { get; set; }
    }
}

