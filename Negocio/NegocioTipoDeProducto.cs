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

namespace Negocio {
    public class NegocioTipoDeProducto {

        public DataSet GetTipoDeProducto() {
            Response resultado = DaoTiposDeProductos.ObtenerListaDeTipoProducto();
            DataSet dt = resultado.ObjectReturned as DataSet;// Cómo manejás los errores?
            return dt;
        }

        public DataSet ObtenerPorCod(String cod) {
            Response resultado = DaoTiposDeProductos.BuscarTipoProductoPorCod(cod);
            DataSet dt = resultado.ObjectReturned as DataSet; // Cómo manejás los errores?
            return dt;
        }

        public static Response BuscarPorCodigo(string cod) {
            return DaoTiposDeProductos.BuscarTipoProductoPorCod(cod);
        }

        public static Response Buscar(string query) {
            return DaoTiposDeProductos.Buscar(query);
        }



        public bool IgresarTipoDeProducto(string cod, string tipoDeProducto, string CodAnimal, string Descripcion) {
            TipoProducto A = new TipoProducto();
            A.Codigo = cod;
            A.tipoDeProducto = tipoDeProducto;
            A.CodAnimal = CodAnimal;
            A.Descripcion = Descripcion;
            Response RES = DaoTiposDeProductos.IgresarTipoProducto(A);
            if (!RES.ErrorFound) {
                return true;
            }
            else {
                return false;
            }
        }

        public static Response Agregar(SessionData auth, TipoProducto tp) {
            var res = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var op = DaoTiposDeProductos.IgresarTipoProducto(tp);
                    res = new Response {
                        ErrorFound = op.ErrorFound,
                        Message = !op.ErrorFound
                            ? "El registro se agregó correctamente. "
                            : "Hubo un error al intentar agregar el registro. "
                    };
                }, err => { res = Response.TokenCaducado; });
                return res;
            } return Response.PermisosInsuficientes;
        }

        public static Response ActualizarTipoDeProducto(SessionData auth, TipoProducto tp) {
            var res = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var op = DaoTiposDeProductos.ActualizarTipoProducto(tp);
                    res = new Response {
                        ErrorFound = op.ErrorFound,
                        Message = !op.ErrorFound
                            ? "Se actualizó correctamente el registro. "
                            : "Hubo un error al intentar actualizar el registro. "
                    };
                }, err => { res = Response.TokenCaducado; });
                return res;
            }
            return Response.PermisosInsuficientes;
        }

        public Response EliminarTipoDeProducto(string A) {
            TipoProducto t = new TipoProducto();
            t.CodAnimal = A;
            return DaoTiposDeProductos.EliminarTipoProducto(t);
        }
        /*public Response GetTipoDeProductoBaja() {
            return DaoTiposDeProductos.ObtenerListaDeTipoProductoBajas();
        }
        
        public Response ObtenerPorCodBaja(String cod) {
            return DaoTiposDeProductos.BuscarTipoProductoPorCodBajas(cod);
        }*/

        public static Response Habilitar(SessionData auth, TipoProducto cat) {
            var res = Response.ErrorDesconocido;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var alta = DaoTiposDeProductos.AltaTipoProducto(cat);
                    res = new Response {
                        ErrorFound = alta.ErrorFound,
                        Message = alta.ErrorFound
                            ? "Hubo un error al intentar habilitar. "
                            : "Se habilitó con éxito el registro. "
                    };
                }, err => { res = Response.TokenCaducado; });
                return res;
            }
            return Response.PermisosInsuficientes;
        }

        public static Response Deshabilitar(SessionData auth, TipoProducto cat) {
            var res = Response.ErrorDesconocido;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var alta = DaoTiposDeProductos.EliminarTipoProducto(cat);
                    res = new Response {
                        ErrorFound = alta.ErrorFound,
                        Message = alta.ErrorFound
                            ? "Hubo un error al intentar deshabilitar. "
                            : "Se deshabilitó con éxito el registro. "
                    };
                }, err => { res = Response.TokenCaducado; });
                return res;
            }
            return Response.PermisosInsuficientes;
        }

        public static Response ObtenerIDS() {
            return DaoTiposDeProductos.ObtenerListaDeIDS();
        }

        //Solo devuelve aquellas categorias las cuales se encuentran activas
        public static Response ObtenerListaActivos()
        {
            return DaoTiposDeProductos.ObtenerListaDeTipoProductoActivos();
        }
        //Devuelve todas las categorias sin importar su estado
        public static Response ObtenerLista(bool estado=true)
        {
            return DaoTiposDeProductos.ObtenerListaDeTipoProducto(estado);
        }

    }
}
