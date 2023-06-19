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
        public bool ValidarStringNumerico(string input)
        {
            string patron = @"^\d{1,10}$";
            return Regex.IsMatch(input, patron);
        }
        public Response ObtenerProveedorByCUIT(string CUIT)
        {
            bool esValido = ValidarStringNumerico(CUIT);
            if (esValido)
            {
                return ProveedorDatos.ObtenerProveedorByCUIT(CUIT);
            }
            else
            {
                Response res = new Response();
                res.Message = "El CUIT INGRESADO ES INCORRECTO";
                return res;
            }
            
        }
       
    }
}
