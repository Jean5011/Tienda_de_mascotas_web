using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Text.RegularExpressions;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public static Response ObtenerListaDeProveedores()
        {
            return ProveedorDatos.ObtenerListaDeProveedores();
        }
       
        public static Response ObtenerProveedorByCUIT(string CUIT)
        {
            return ProveedorDatos.ObtenerProveedorByCUIT(CUIT);
        }
        public static Response EliminadoLogicoProveedor(string CUIT)
        {
            return ProveedorDatos.EliminadoLogicoProveedor(CUIT);
        }
    }
}
