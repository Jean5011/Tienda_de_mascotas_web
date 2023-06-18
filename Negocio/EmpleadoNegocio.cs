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
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//using Microsoft.IdentityModel.Tokens;

namespace Negocio {
    public class EmpleadoNegocio {
        public EmpleadoNegocio() { }
        byte[] GenerarSalt() {
            byte[] salt = new byte[16]; // 16 bytes = 128 bits
            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(salt);
            }
            return salt;
        }
        byte[] GenerarHash(string password, byte[] salt) {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];

            Array.Copy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Array.Copy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

            using (var sha256 = SHA256.Create()) {
                return sha256.ComputeHash(combinedBytes);
            }
        }
        bool VerificarClave(string password, byte[] savedHash, byte[] savedSalt) {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + savedSalt.Length];

            Array.Copy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Array.Copy(savedSalt, 0, combinedBytes, passwordBytes.Length, savedSalt.Length);

            using (var sha256 = SHA256.Create()) {
                byte[] inputHash = sha256.ComputeHash(combinedBytes);
                return savedHash.SequenceEqual(inputHash);
            }
        }
        bool VerificarClaveString(string password, string savedHash, byte[] savedSalt) {
            string estado = "NADA_TODAVIA";
            byte[] newHash = GenerarHash(password, savedSalt);
            string newHashString = Convert.ToBase64String(newHash);
            bool er = newHashString == savedHash;
            if (er) {
                estado = "SON_IGUALES";
            }
            else estado = "NO_COINCIDEN";
            return er;

        }
        public Response ExtractDataFromDataSet(DataSet resultDataSet) {
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
                    Salt = primerRegistro[Empleado.Columns.Salt].ToString()
                };
                return new Response() {
                    ErrorFound = false,
                    ObjectReturned = obj
                };
            }
            else {
                return new Response() {
                    ErrorFound = true,
                    Message = "NO_ROWS",
                    ObjectReturned = null
                };
            }

        }
        public Response ComprobarClaveIngresada(string clave, string DNI) {
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
                    byte[] _hash = Convert.FromBase64String(obj.Hash);
                    byte[] _salt = Convert.FromBase64String(obj.Salt);
                    bool resultado = VerificarClaveString(clave, obj.Hash, _salt);

                    return new Response() {
                        ErrorFound = !resultado,
                        Message = resultado ? "SUCCESS" : "INCORRECT_DATA"
                    };
                }
            }
        }
        public Response IniciarSesion(string DNI, string clave) {
            Response resultadoClaves = ComprobarClaveIngresada(clave, DNI);
            bool clavesCorrectas = !(resultadoClaves.ErrorFound);
            if(clavesCorrectas) {
                var tk = new SesionNegocio();
                string token = tk.GenerarToken(DNI);
                Trace.WriteLine("TOKEN GENERADO: " + token);
                tk.SetCookie("_au", token, 1);
            }
            return resultadoClaves;
            
        }
    }
}
