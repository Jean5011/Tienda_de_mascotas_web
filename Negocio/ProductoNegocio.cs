using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace Negocio
{
    public class ProductoNegocio
    {
        public static Response ExtractDataFromDataSet(DataSet resultDataSet) {
            if (resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0) {
                DataRow i = resultDataSet.Tables[0].Rows[0];
                Producto obj = new Producto() {
                    Codigo = i[Producto.Columns.Codigo_Prod].ToString(),
                    Proveedor = new Proveedor() { CUIT = i[Producto.Columns.CUITProv].ToString() },
                    Categoria = new TipoProducto() { Codigo = i[Producto.Columns.CodTipoProducto].ToString() },
                    Nombre = i[Producto.Columns.Nombre].ToString(),
                    Marca = i[Producto.Columns.Marca].ToString(),
                    Descripcion = i[Producto.Columns.Descripcion].ToString(),
                    Stock = Convert.ToInt32(i[Producto.Columns.Stock].ToString()),
                    Precio = Convert.ToDouble(i[Producto.Columns.Precio].ToString()),
                    Estado = Convert.ToBoolean(i[Producto.Columns.Estado].ToString())
                };
                return new Response() {
                    ErrorFound = false,
                    ObjectReturned = obj
                };
            }
            else {
                return new Response() {
                    ErrorFound = true,
                    Message = SesionNegocio.ErrorCode.NO_ROWS,
                    ObjectReturned = null
                };
            }

        }
        public Response ObtenerProductos()
        {
            return DaoProductos.ObtenerListaDeProductos();
        }

        public Response ObtenerPorCod(String cod) {
            return DaoProductos.BuscarProductoPorCod(cod);
        }
        public static Response ObtenerPorCodigo(string cod) {
            var res1 = DaoProductos.BuscarProductoPorCod(cod);
            if(!res1.ErrorFound) {
                var res2 = ProductoNegocio.ExtractDataFromDataSet(res1.ObjectReturned as DataSet);
                return res2;
            }
            return res1;
        }

        public static Response IngresarProducto(Producto P)
        {
            return DaoProductos.IngresarProducto(P);
        }
       

        public static Response ActualizarProducto(Producto P)
        {
            return DaoProductos.ActualizarProducto(P);
        }

        public static Response EliminarProducto(Producto P)
        {
            return DaoProductos.EliminarProducto(P);
        }
        /*
        public Response ActualizarPrecio(Producto P)
        {
            return DaoProductos.ActualizarPrecio(P);
        }

        public Response ActualizarStock(Producto P)
        {
            return DaoProductos.ActualizarStock(P);
        }

         public  Response ActualizarEstado(Producto P)
        {
            return DaoProductos.ActualizarEstadoProducto(P);
        }
        */
    }
}
