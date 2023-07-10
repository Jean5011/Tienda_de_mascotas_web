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
    public class NegocioAnimales {

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ListarTodo(bool estado = true) {
            return DaoAnimales.ObtenerListaDeAnimales(estado);
        }

        /// <summary>
        /// Buscá un registro a partir de un código dado.
        /// </summary>
        /// <param name="codigo">Código a buscar</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarPorCodigo(string codigo) {
            return DaoAnimales.BuscarAnimalPorCod(codigo);
        }

        public static Response Buscar(string busqueda) {
            return DaoAnimales.Buscar(busqueda);
        }

        /// <summary>
        /// Lista todos los registros, o filtra por código si corresponde.
        /// </summary>
        /// <param name="codigo">Código a buscar. Si está vacío no filtra.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarAnimales(string codigo = null,bool estado = true) {
            return string.IsNullOrEmpty(codigo)
                ? ListarTodo(estado)
                : Buscar(codigo);
        }


        /// <summary>
        /// Manda a insertar un registro a Datos, si la sesión es válida y el usuario tiene los permisos necesarios.
        /// </summary>
        /// <param name="auth">Objeto SessionData con la información sobre la sesión actual.</param>
        /// <param name="animal">Objeto Animal que se pretende insertar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response IngresarAnimal(SessionData auth, Animal animal) {
            var respuesta = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar((success) => {
                    Response operacion = DaoAnimales.IngresarAnimal(animal);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = operacion.ErrorFound 
                            ? "El registro se agregó correctamente. "
                            : "Hubo un problema al intentar agregar el registro. "
                    };
                }, (error) => {
                    respuesta = Response.TokenCaducado;
                });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }

        /// <summary>
        /// Manda a actualizar un registro a Datos, si la sesión es válida y el usuario posee los permisos requeridos.
        /// </summary>
        /// <param name="auth">Objeto SessionData con la información sobre la sesión actual.</param>
        /// <param name="animal">Objeto Animal que se pretende actualizar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ActualizarAnimal(SessionData auth, Animal animal) {
            var respuesta = Response.ErrorDesconocido;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(res => {
                    var operacion = DaoAnimales.ActualizarAnimal(animal);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = operacion.ErrorFound
                            ? "Hubo un error al intentar actualizar el registro. "
                            : "El registro se actualizó correctamente. "
                    };
                }, err => {
                    respuesta = Response.TokenCaducado;
                });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }

        /// <summary>
        /// Manda a eliminar un registro a Datos, si la sesión es válida y el usuario posee los permisos requeridos.
        /// </summary>
        /// <param name="auth">Objeto SessionData con la información sobre la sesión actual.</param>
        /// <param name="animal">Objeto Animal que se pretende eliminar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response EliminarAnimal(SessionData auth, Animal animal) {
            Response respuesta = Response.ErrorDesconocido;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(res => {
                    var operacion = DaoAnimales.EliminarAnimal(animal);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = operacion.ErrorFound
                            ? "Hubo un error al intentar eliminar el registro. "
                            : "El registro se eliminó correctamente. "
                    };
                }, err => {
                    respuesta = Response.TokenCaducado;
                });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }

        public static Response HabilitarAnimal(SessionData auth, Animal animal) {
            Response respuesta = Response.ErrorDesconocido;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(res => {
                    var operacion = DaoAnimales.HabilitarAnimal(animal);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = operacion.ErrorFound
                            ? "Hubo un error al intentar habilitar el registro. "
                            : "El registro se habilitó correctamente. "
                    };
                }, err => {
                    respuesta = Response.TokenCaducado;
                });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }

        // Queda a revisión
       public DataSet GettAnimales() {
            Response resultado =  DaoAnimales.ObtenerLista();
            DataSet dt = resultado.ObjectReturned as DataSet;
            return dt;
        }
    }
}