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
    public class EmpleadoNegocio {
        public EmpleadoNegocio() { }

        public static class ErrorCode {
            public const string ALREADY_EXISTS = "ALREADY_EXISTS";
        }

        /// <summary>
        /// Generar salt (Texto aleatorio para fortalecer una contraseña)
        /// </summary>
        /// <returns>El Salt generado.</returns>
        public static byte[] GenerarSalt() {
            byte[] salt = new byte[16]; // 16 bytes = 128 bits
            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(salt);
            }
            return salt;
        }


        /// <summary>
        /// Genera un hash a partir de una contraseña.
        /// </summary>
        /// <param name="password">Contraseña ingresada.</param>
        /// <param name="salt">Salt generada.</param>
        /// <returns></returns>
        public static byte[] GenerarHash(string password, byte[] salt) {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];

            Array.Copy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Array.Copy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

            using (var sha256 = SHA256.Create()) {
                return sha256.ComputeHash(combinedBytes);
            }
        }

        public static Response CrearEmpleado(SessionData auth, Empleado obj, string clave) {
            // Generamos el hash y el salt.
            byte[] newSalt = GenerarSalt();
            byte[] newHash = GenerarHash(clave, newSalt);
            obj.Hash = Convert.ToBase64String(newHash);
            obj.Salt = Convert.ToBase64String(newSalt);
            var existeEmpleado = BuscarEmpleadoPorDNI(obj.DNI);
            if(!existeEmpleado.ErrorFound && existeEmpleado.Message != SesionNegocio.ErrorCode.NO_ROWS) {
                return new Response() {
                    ErrorFound = true,
                    Message = ErrorCode.ALREADY_EXISTS
                };
            }
            var respuesta = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var operacion = EmpleadoDatos.CrearEmpleado(obj);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = !operacion.ErrorFound
                            ? "El empleado se creó correctamente. "
                            : "Hubo un error al intentar crear el registro. "
                    };
                }, err => { respuesta = Response.TokenCaducado;  });
            }
            return Response.PermisosInsuficientes;
        }


        /// <summary>
        /// Verifica una clave usando un Hash en formato string.
        /// </summary>
        /// <param name="password">La contraseña ingresada.</param>
        /// <param name="savedHash">El hash rescatado de la base de datos, string.</param>
        /// <param name="savedSalt">El salt rescatado de la base de datos.</param>
        /// <returns>True si las claves concuerdan, False en otro caso.</returns>
        public static bool VerificarClaveString(string password, string savedHash, byte[] savedSalt) {
            byte[] newHash = GenerarHash(password, savedSalt);
            string newHashString = Convert.ToBase64String(newHash);
            bool coinciden = newHashString == savedHash;
            return coinciden;

        }

        public static Response CrearClaves(SessionData auth, Empleado empleado, string password) {
            var respuesta = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN || auth.User.DNI == empleado.DNI) {
                SesionNegocio.Autenticar(ok => {
                    byte[] salt = GenerarSalt();
                    byte[] hash = GenerarHash(password, salt);
                    string nhash = Convert.ToBase64String(hash);
                    string nsalt = Convert.ToBase64String(salt);
                    var operacion = EmpleadoDatos.CambiarClave(new Empleado() {
                        DNI = empleado.DNI,
                        Hash = nhash,
                        Salt = nsalt
                    });
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = !operacion.ErrorFound
                            ? "Se cambió la clave correctamente. "
                            : "Hubo un error al intentar cambiar la clave. "
                    };
                }, err => {
                    respuesta = Response.TokenCaducado;
                });
            } 
            return Response.PermisosInsuficientes;
        }

        public static Response Deshabilitar(SessionData auth, Empleado empleado) {
            var respuesta = Response.ErrorDesconocido;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var operacion = EmpleadoDatos.Deshabilitar(empleado);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = !operacion.ErrorFound
                            ? "El empleado ha sido deshabilitado y no podrá volver a iniciar sesión. "
                            : "Hubo un error al intentar deshabilitar al empleado. "
                    };
                }, err => { respuesta = Response.TokenCaducado; });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }

        public static Response Habilitar(SessionData auth, Empleado empleado) {
            var respuesta = Response.ErrorDesconocido;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var operacion = EmpleadoDatos.Habilitar(empleado);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = !operacion.ErrorFound
                            ? "El empleado ha sido habilitado, y podrá volver a iniciar sesión. "
                            : "Hubo un error al intentar habilitar al empleado. "
                    };
                }, err => { respuesta = Response.TokenCaducado; });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }


        /// <summary>
        /// Extrae los datos de un DataSet y los convierte en un objeto tipo Empleado.
        /// </summary>
        /// <param name="resultDataSet">El DataSet en cuestión.</param>
        /// <returns>Response con el resultado de la operación.</returns>
        public static Response ExtractDataFromDataSet(DataSet resultDataSet) {
            if (resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0) {
                DataRow primerRegistro = resultDataSet.Tables[0].Rows[0];
                Empleado obj = new Empleado() {
                    DNI = primerRegistro[Empleado.Columns.DNI].ToString(),
                    Nombre = primerRegistro[Empleado.Columns.Nombre].ToString(),
                    Apellido = primerRegistro[Empleado.Columns.Apellido].ToString(),
                    Sexo = primerRegistro[Empleado.Columns.Sexo].ToString(),
                    FechaNacimiento = primerRegistro[Empleado.Columns.FechaNacimiento].ToString(),
                    FechaContrato = primerRegistro[Empleado.Columns.FechaInicio].ToString(),
                    Sueldo = Convert.ToDouble(primerRegistro[Empleado.Columns.Sueldo].ToString()),
                    Direccion = primerRegistro[Empleado.Columns.Direccion].ToString(),
                    Provincia = primerRegistro[Empleado.Columns.Provincia].ToString(),
                    Localidad = primerRegistro[Empleado.Columns.Localidad].ToString(),
                    Nacionalidad = primerRegistro[Empleado.Columns.Nacionalidad].ToString(),
                    Estado = Convert.ToBoolean(primerRegistro[Empleado.Columns.Estado].ToString()),
                    Hash = primerRegistro[Empleado.Columns.Hash].ToString(),
                    Salt = primerRegistro[Empleado.Columns.Salt].ToString(),
                    Rol = primerRegistro[Empleado.Columns.Rol].ToString()
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

        /// <summary>
        /// Comprueba la veracidad de una clave ingresada al descargar la clave de la base de datos y compararlas.
        /// </summary>
        /// <param name="clave">La clave ingresada por el usuario.</param>
        /// <param name="DNI">El DNI ingresado por el usuario.</param>
        /// <returns></returns>
        public static Response ComprobarClaveIngresada(string clave, string DNI) {
            Response resultadoBusquedaEmpleado = EmpleadoDatos.BuscarEmpleadoPorDNI(DNI);
            if(resultadoBusquedaEmpleado.ErrorFound) {
                return resultadoBusquedaEmpleado;
            } else {
                DataSet dt = resultadoBusquedaEmpleado.ObjectReturned as DataSet;
                Response extracted_data = ExtractDataFromDataSet(dt);
                if(extracted_data.ErrorFound) {
                    return extracted_data;
                } else {
                    Empleado obj = extracted_data.ObjectReturned as Empleado;
                    byte[] _salt = Convert.FromBase64String(obj.Salt);
                    bool resultado = VerificarClaveString(clave, obj.Hash, _salt);

                    return new Response() {
                        ErrorFound = !resultado,
                        Message = resultado ? "SUCCESS" : "INCORRECT_DATA"
                    };
                }
            }
        }

        /// <summary>
        /// Inicia sesión a un usuario en específico y le genera un token, a partir de las credenciales ingresadas.
        /// </summary>
        /// <param name="DNI">El DNI ingresado.</param>
        /// <param name="clave">La clave ingresada.</param>
        /// <returns>Response con el resultado de la operación.</returns>
        public static Response IniciarSesion(string DNI, string clave) {
            Response resultadoClaves = ComprobarClaveIngresada(clave, DNI);
            bool clavesCorrectas = !(resultadoClaves.ErrorFound);
            if(clavesCorrectas) {
                Response res = SesionNegocio.AbrirSesion(DNI);
                if (res.ErrorFound) return new Response {
                    ErrorFound = true,
                    Message = "No se pudo guardar la sesión. ¿Tenés las cookies habilitadas?"
                };
                else {
                    var buscar_empleado = SesionNegocio.ObtenerDatosEmpleadoActual();
                    if(buscar_empleado.ErrorFound) {
                        return new Response {
                            ErrorFound = false,
                            Message = "Iniciaste sesión pero no pudimos recolectar tus datos. "
                        };
                    }
                    var empleado = buscar_empleado.ObjectReturned as Empleado;
                    bool esMasculino = (empleado.Sexo == "M");
                    return new Response {
                        ErrorFound = false,
                        Message = $"¡Bienvenid{(esMasculino ? "o" : "a")}, {empleado.Nombre}!"
                    };
                }

            }
            if(resultadoClaves.Message == "INCORRECT_DATA" || resultadoClaves.Message == "NO_ROWS") {
                return new Response {
                    ErrorFound = true,
                    Message = "Los datos ingresados son incorrectos. "
                };
            }
            return new Response {
                ErrorFound = true,
                Message = "Hubo un error al intentar iniciar sesión. "
            };
            
        }

        public static Response ModificarEmpleado(SessionData auth, Empleado obj) {
            var respuesta = Response.ErrorDesconocido;
            if(auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    var operacion = EmpleadoDatos.Modificar(obj, obj.DNI);
                    respuesta = new Response {
                        ErrorFound = operacion.ErrorFound,
                        Message = !operacion.ErrorFound
                            ? "El registro se modificó correctamente. "
                            : "Hubo un error al intentar modificar el registro. "
                    };
                }, err => {
                    respuesta = Response.TokenCaducado;
                });
                return respuesta;
            }
            return Response.PermisosInsuficientes;
        }

        /// <summary>
        /// Devuelve los datos del empleado a partir de su DNI.
        /// </summary>
        /// <returns>Response con el resultado de la operación.</returns>
        public static Response BuscarEmpleadoPorDNI(string dni) {
                Response empleado_data = EmpleadoDatos.BuscarEmpleadoPorDNI(dni);
                if (!empleado_data.ErrorFound) {
                    DataSet dt = empleado_data.ObjectReturned as DataSet;
                    Response emp = ExtractDataFromDataSet(dt);
                    return emp;
                }
                return empleado_data;
        }


        public static Response CargarDatos(Empleado.Busqueda data) {
            return EmpleadoDatos.CargarRegistros(data);
        }


    }


}
