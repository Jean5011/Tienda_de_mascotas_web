using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    public class Sesion {
        public static class Columns {
            public static string Codigo = "CodSesion_Se";
            public static string DNI = "DNIEmpleado_Se";
            public static string FechaAlta = "FechaDeAlta_Se";
            public static string Token = "Token_Se";
            public static string Estado = "Estado_Se";
        }
        public static string Table = "Sesiones";

        public int Codigo { get; set; }
        public Empleado Empleado { get; set; }
        public string FechaAlta { get; set; }
        public string Token { get; set; }
        public bool Estado { get; set; }
    }
}
