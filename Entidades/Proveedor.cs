using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
   
    public class Proveedor {
        public static class Columns
        {
            public const string CUIT = "CUIT_Prov";
            public const string RazonSocial = "RazonSocial_Prov";
            public const string NombreContacto = "NombreDeContacto_Prov";
            public const string CorreoElectronico = "CorreoElectronico_Prov";
            public const string Telefono = "Telefono_Prov";
            public const string Direccion = "Direccion_Prov";
            public const string Provincia = "Provincia_Prov";
            public const string Localidad = "Localidad_Prov";
            public const string Pais = "Pais_Prov";
            public const string CodigoPostal = "CodigoPostal_Prov";
            public const string Estado = "Estado_Prov";
        }
        public const string Table = "Proveedores";
        public string CUIT { get; set; }
        public string RazonSocial { get; set; }
        public string NombreContacto { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }
        public bool Estado { get; set; }

    }
}

