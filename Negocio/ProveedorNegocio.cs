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
        public static Response InsertarProveedor(Proveedor proveedor)
        {
            return ProveedorDatos.InsertarProveedor(proveedor);
        }
        public static Response EliminadoLogicoProveedor(String CUIT)
        {
            return ProveedorDatos.EliminadoLogicoProveedor(CUIT);
        }
        public static Response ActualizarProveedor(Proveedor proveedor)
        {
            return ProveedorDatos.ActualizarProveedor(proveedor);
        }

        public static Response VerificarExiste(string CUIT)
        {
            return ProveedorDatos.VerificarExiste(CUIT);
        }

        public static Response ObtenerProveedorCUITEditar(String CUIT)
        {
            return ProveedorDatos.ObtenerProveedorCUITEditar(CUIT);
        }
    }
}
