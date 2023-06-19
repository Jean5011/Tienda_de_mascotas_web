using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades {
    public class Empleado {
        public static class Columns {
            public const string DNI = "DNI_Em";
            public const string Nombre = "Nombre_Em";
            public const string Apellido = "Apellido_Em";
            public const string Sexo = "Sexo_Em";
            public const string FechaNacimiento = "FechaDeNacimiento_Em";
            public const string FechaInicio = "FechaDeInicio_Em";
            public const string Sueldo = "Sueldo_Em";
            public const string Direccion = "Direccion_Em";
            public const string Provincia = "Provincia_Em";
            public const string Localidad = "Localidad_Em";
            public const string Nacionalidad = "Nacionalidad_Em";
            public const string Estado = "Estado_Em";
            public const string Hash = "Hash_Em";
            public const string Salt = "Salt_Em";
            public const string Rol = "Rol_Em";
        }
        public const string Table = "Empleados";
        public static class Roles {
            public const string ADMIN = "ADMIN";     // Puede ver perfiles de otros empleados, cambiar sueldo, eliminar cosas.
            public const string NORMAL = "NORMAL";   // Puede ver su propio perfil, sin editar, registrar ventas y modificar sus propias ventas. 
        }
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
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Rol { get; set; }
    }
}

