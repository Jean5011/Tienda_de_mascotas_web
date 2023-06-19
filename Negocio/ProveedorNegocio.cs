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
    class ProveedorNegociocs
    {
        Proveedor Proveedor;
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
        /* public static Response ObtenerListaDeProveedores()
         
         public static Response ObtenerProveedorByCUIT(string CUIT)
         {
             string consulta = $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] = @cuit";
             Connection connection = new Connection(Connection.Database.Pets);
             return connection.Response.ErrorFound
                 ? connection.Response
                 : connection.FetchData(
                         query: consulta,
                         parameters: new Dictionary<string, object> {
                             { "@cuit", CUIT }
                         }
                     );
         }
         public static Response InsertarProveedor(Proveedor proveedor)
         {
             string consulta = $"INSERT INTO {ALL_COLUMNS}" +
                               $"VALUES (@CUIT, @RazonSocial, @NombreContacto,@CorreoElectronico,@Telefono,@Direccion,@Provincia,@Localidad,@Pais,@CodigoPostal)";
             Connection connection = new Connection(Connection.Database.Pets);
             return connection.Response.ErrorFound
                 ? connection.Response
                 : connection.FetchData(
                         query: consulta,
                         parameters: new Dictionary<string, object> {
                     { "@CUIT", proveedor.CUIT },
                     { "@RazonSocial", proveedor.RazonSocial },
                     { "@NombreContacto", proveedor.NombreContacto },
                     { "@CorreoElectronico", proveedor.CorreoElectronico },
                     { "@Telefono", proveedor.Telefono },
                     { "@Direccion", proveedor.Direccion },
                     { "@Provincia", proveedor.Provincia },
                     { "@Localidad", proveedor.Localidad },
                     { "@Pais", proveedor.Pais },
                     { "@CodigoPostal", proveedor.CodigoPostal }
                         }
                     );
         }
         public static Response EliminaProveedor(string CUIT)
         {
             string consulta = $"DELETE FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] = @cuit";
             Connection connection = new Connection(Connection.Database.Pets);
             return connection.Response.ErrorFound
                 ? connection.Response
                 : connection.FetchData(
                         query: consulta,
                         parameters: new Dictionary<string, object> {
                             { "@cuit", CUIT }
                         }
                     );
         }
     }*/
    }
