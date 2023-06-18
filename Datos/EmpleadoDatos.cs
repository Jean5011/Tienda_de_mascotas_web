using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos {
    public class EmpleadoDatos {
        public EmpleadoDatos() { }
        private static readonly string ALL_COLUMNS = $"[{Empleado.Columns.DNI}], " +
                                    $"[{Empleado.Columns.Nombre}], " +
                                    $"[{Empleado.Columns.Apellido}], " +
                                    $"[{Empleado.Columns.Sexo}], " +
                                    $"[{Empleado.Columns.FechaNacimiento}], " +
                                    $"[{Empleado.Columns.FechaInicio}], " +
                                    $"[{Empleado.Columns.Sueldo}], " +
                                    $"[{Empleado.Columns.Direccion}], " +
                                    $"[{Empleado.Columns.Provincia}], " +
                                    $"[{Empleado.Columns.Localidad}], " +
                                    $"[{Empleado.Columns.Nacionalidad}], " +
                                    $"[{Empleado.Columns.Estado}], " +
                                    $"[{Empleado.Columns.Hash}], " +
                                    $"[{Empleado.Columns.Salt}]";
        public static class Procedures {
            public static string CambiarClave = "CambiarClave";
        }

        public static Response ObtenerListaDeEmpleados() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.Estado}] = '1'"
                    );
        }
        public static Response BuscarEmpleadoPorDNI(string DNI) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.DNI}] = @dni AND [{Empleado.Columns.Estado}] = '1'";
            Connection connection = new Connection(Connection.Database.Pets);
            Trace.Write("BuscarEmpleadoPorDNI", $"Consulta: {consulta}");
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@dni", DNI }
                        }
                    );
        }
        public static Response BuscarEmpleadoPorNombre(string Nombre) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.Nombre}] LIKE '%@name%' AND [{Empleado.Columns.Estado}] = '1'",
                        parameters: new Dictionary<string, object> {
                            { "@name", Nombre }
                        }
                    );
        }
        public static Response BuscarEmpleadoPorApellido(string Apellido) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.Apellido}] LIKE '%@surname%' AND [{Empleado.Columns.Estado}] = '1'",
                        parameters: new Dictionary<string, object> {
                            { "@surname", Apellido }
                        }
                    );
        }

        public static Response CambiarClave(string hash, string salt, string DNI) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.CambiarClave,
                        parameters: new Dictionary<string, object> {
                            { "@DNI", DNI },
                            { "@HASH", hash },
                            { "@SALT", salt }
                        }
                    );
        }
    }
}
