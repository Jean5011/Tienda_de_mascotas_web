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

        public static Response CrearEmpleado(Empleado obj, string clave) {
            // Generamos el hash y el salt.
            byte[] newSalt = GenerarSalt();
            byte[] newHash = GenerarHash(clave, newSalt);
            obj.Hash = Convert.ToBase64String(newHash);
            obj.Salt = Convert.ToBase64String(newSalt);
            var existeEmpleado = BuscarEmpleadoPorDNI(obj.DNI);
            if(!existeEmpleado.ErrorFound && existeEmpleado.Message != SesionNegocio.ErrorCode.NO_ROWS) {
                return new Response() {
                    ErrorFound = true,
                    Message = EmpleadoNegocio.ErrorCode.ALREADY_EXISTS
                };
            }
            return EmpleadoDatos.CrearEmpleado(obj);
        }



        /// <summary>
        /// Verifica una clave [DEPRECATED]
        /// </summary>
        /// <param name="password">Contraseña ingresada.</param>
        /// <param name="savedHash">Hash rescatado de la base de datos.</param>
        /// <param name="savedSalt">Salt rescatado de la base de datos.</param>
        /// <returns>True si las claves concuerdan. False en otro caso.</returns>
        public static bool VerificarClave(string password, byte[] savedHash, byte[] savedSalt) {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + savedSalt.Length];

            Array.Copy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Array.Copy(savedSalt, 0, combinedBytes, passwordBytes.Length, savedSalt.Length);

            using (var sha256 = SHA256.Create()) {
                byte[] inputHash = sha256.ComputeHash(combinedBytes);
                return savedHash.SequenceEqual(inputHash);
            }
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

        public static Response CrearClaves(string DNI, string password) {
            byte[] salt = GenerarSalt();
            byte[] hash = GenerarHash(password, salt);
            string nhash = Convert.ToBase64String(hash);
            string nsalt = Convert.ToBase64String(salt);
            return EmpleadoDatos.CambiarClave(new Empleado() {
                DNI = DNI,
                Hash = nhash,
                Salt = nsalt
            });
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
                return res;
            }
            return resultadoClaves;
            
        }

        public static Response ModificarEmpleado(Empleado obj, string oldDNI) {
            if (SesionNegocio.Autenticar()) {
                return EmpleadoDatos.Modificar(obj, oldDNI);
            }
            else return new Response() {
                ErrorFound = true,
                Message = SesionNegocio.ErrorCode.NO_SESSION_FOUND
            };
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


        public static Response ObtenerEmpleados(bool chkSoloActivosChecked) {
            return EmpleadoDatos.ObtenerListaDeEmpleados(chkSoloActivosChecked);
        }

        public static Response FiltrarEmpleadosPorNombreCompleto(string nombre, bool chkSoloActivosChecked) {
            return EmpleadoDatos.FiltrarEmpleadosPorNombreCompleto(nombre, chkSoloActivosChecked);
        }



    }


}
