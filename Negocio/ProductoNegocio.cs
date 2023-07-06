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
        public static Response ListarTodo() {
            return DaoProductos.ObtenerListaDeProductos();
        }

        public static Response BuscarPorCodigo(string codigo) {
            return DaoProductos.BuscarProductoPorCod(codigo);
        }


        public static Response BuscarProductos(string codigo = null) {
            return string.IsNullOrEmpty(codigo)
                ? ListarTodo()
                : BuscarPorCodigo(codigo);
        }
        public static Response ObtenerPorCodigo(string cod) {
            var res1 = DaoProductos.BuscarProductoPorCod(cod);
            if(!res1.ErrorFound) {
                var res2 = ProductoNegocio.ExtractDataFromDataSet(res1.ObjectReturned as DataSet);
                return res2;
            }
            return res1;
        }

        public static Response IngresarProducto(SessionData auth, Producto producto) {
            var respuesta = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN) {
                //verificamos que el proveedor ingresado sea valido
                var verificarProv = VerificarExistenciaProveedor(producto.Proveedor.CUIT);
                if(!verificarProv.ErrorFound)
                {
                    var dataT = verificarProv.ObjectReturned as DataSet;
                    int cant = Convert.ToInt32(dataT.Tables[0].Rows[0]["Cantidad"]);
                    if (cant == 0) {
                        return new Response
                        {
                            ErrorFound = true,
                            Message = "El CUIT de Proveedor ingresado no existe. "
                        };
                    }
                }
                // Verificamos si existe un producto con mismo codigo y si pertenece al mismo proveedor.
                var verificarExistencia = VerificarExistenciaProductoProveedor(producto.Codigo,producto.Proveedor.CUIT);
                if (!verificarExistencia.ErrorFound) {
                    var dt = verificarExistencia.ObjectReturned as DataSet;
                    int cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["Cantidad"]);
                    if(cantidad != 0) {
                        // Hay un registro bajo ese código y bajo mismo proveedor. Se aborta la operación.
                        return new Response {
                            ErrorFound = true,
                            Message = "Ya existe un registro con ese código y proveedor. Intente con otro."
                        };
                    } else {
                        // No existe ningún registro bajo ese código con mismo proveedor.
                        SesionNegocio.Autenticar(ok => {
                            var operacion = DaoProductos.IngresarProducto(producto);
                            respuesta = new Response {
                                ErrorFound = operacion.ErrorFound,
                                Message = !operacion.ErrorFound
                                    ? "El registro se agregó correctamente. "
                                    : "Hubo un error al intentar agregar el registro. "
                            };
                        }, err => {
                            respuesta = Response.TokenCaducado;
                        });
                    }
                } else {
                    return new Response {
                        ErrorFound = true,
                        Message = "No se pudo comprobar la existencia del registro. Intente más tarde. "
                    };
                }

                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }
       

        public static Response ActualizarProducto(SessionData auth, Producto producto) {
            var respuesta = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var operacion = DaoProductos.ActualizarProducto(producto);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = !operacion.ErrorFound
                            ? "Se actualizó correctamente el registro. "
                            : "Hubo un error al intentar actualizar el registro. "
                    };
                }, err => {
                    respuesta = Response.TokenCaducado;
                });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }

        public static Response EliminarProducto(SessionData auth, Producto producto){
            var respuesta = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var operacion = DaoProductos.EliminarProducto(producto);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = !operacion.ErrorFound
                            ? "El registro se eliminó correctamente. "
                            : "Hubo un problema al intentar eliminar el registro. "
                    };
                }, err => {
                    respuesta = Response.TokenCaducado;
                });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }

        public static Response VerificarExistenciaProducto(string ID)
        {
            return DaoProductos.VerificarExistenciaProducto(ID);
        }
        public static Response VerificarExistenciaProveedor(string CUIT)
        {
            return DaoProductos.VerificarExistenciaProveedor(CUIT);
        }

        public static Response VerificarExistenciaProductoProveedor(string ID,string CUIT)
        {
            return DaoProductos.VerificarExistenciaProductoYProveedor(ID, CUIT);
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
