using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Text.RegularExpressions;
using System.Data;

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
            Response response = new Response();
            Response existe = ProveedorDatos.VerificarExiste(proveedor.CUIT);
            
            if (!existe.ErrorFound)
            {
                int cantidad;
                DataSet dt = existe.ObjectReturned as DataSet;
                cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["CUIT"]);
                if (cantidad > 0)
                {   //si cantidad es mayor a cero es pq ya existe
                    response.ErrorFound = true;
                    response.Message = " El CUIT de proveedor ingresado ya existe.";
                }
                else
                {   //si es igual a cero es pq no existe, se crea el proveedor
                    Response resInsertarProveedor = ProveedorDatos.InsertarProveedor(proveedor);
                    if (!resInsertarProveedor.ErrorFound)
                    {
                        response.ErrorFound = false;
                        response.Message = "Proveedor ha sido agregado correctamente!.";
                    }
                    else
                    {
                        response.ErrorFound = true;
                        response.Message = "Hubo un error, no se pudo agregar el proveedor.";
                    }
                }
            }

            return response;
        }
        public static Response EliminadoLogicoProveedor(String CUIT)
        {
            return ProveedorDatos.EliminadoLogicoProveedor(new Proveedor() { CUIT = CUIT, Estado = "0" });
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
