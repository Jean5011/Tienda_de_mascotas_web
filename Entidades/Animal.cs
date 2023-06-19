using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Animal
    {
        public static class Columns
        {
            public const string Codigo = "PK_CodAnimales_An";
            public const string Nombre = "nombre_An";
            public const string Raza = "NombreDeRaza_An";
        }
        public const string Table = "Animales";
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
    }
}

