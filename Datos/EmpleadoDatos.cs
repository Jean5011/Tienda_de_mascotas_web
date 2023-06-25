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
        public static readonly string ALL_COLUMNS = $"[{Empleado.Columns.DNI}], " +
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
                                                    $"[{Empleado.Columns.Salt}], " +
                                                    $"[{Empleado.Columns.Rol}]";
        private static readonly string ALL_COLUMNS_BUT_FORMATTED = $"[{Empleado.Columns.DNI}], " +
                                                    $"[{Empleado.Columns.Nombre}], " +
                                                    $"[{Empleado.Columns.Apellido}], " +
                                                    $"[{Empleado.Columns.Sexo}], " +
                                                    $"[{Empleado.Columns.FechaNacimiento}], " +
                                                    $"[{Empleado.Columns.FechaInicio}], " +
                                                    $"FORMAT([{Empleado.Columns.Sueldo}], 'C', 'es-AR') AS [{Empleado.Columns.Sueldo}], " +
                                                    $"[{Empleado.Columns.Direccion}], " +
                                                    $"[{Empleado.Columns.Provincia}], " +
                                                    $"[{Empleado.Columns.Localidad}], " +
                                                    $"[{Empleado.Columns.Nacionalidad}], " +
                                                    $"[{Empleado.Columns.Estado}], " +
                                                    $"[{Empleado.Columns.Hash}], " +
                                                    $"[{Empleado.Columns.Salt}], " +
                                                    $"[{Empleado.Columns.Rol}]";
        public static class Procedures {
            public static string CambiarClave = "CambiarClave";
            public static string CrearEmpleado = "CrearEmpleado";
        }

        public static Response ObtenerListaDeEmpleados(bool soloActivos = true) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS_BUT_FORMATTED} FROM [{Empleado.Table}] { (soloActivos ? $"WHERE [{Empleado.Columns.Estado}] = '1' " : "" )}"
                    );
        }
        public static Response FiltrarEmpleadosPorNombreCompleto(string nombre, bool soloActivos = true) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS_BUT_FORMATTED} FROM [{Empleado.Table}] WHERE CONCAT([{Empleado.Columns.Nombre}], ' ', [{Empleado.Columns.Apellido}]) LIKE '%' + @nombre + '%' { (soloActivos ? $" AND [{Empleado.Columns.Estado}] = '1'" : "") }",
                        parameters: new Dictionary<string, object>() {
                            { "@nombre", nombre }
                        }
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

        public static Response CrearEmpleado(Empleado obj) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.CrearEmpleado,
                        parameters: new Dictionary<string, object>() {
                            { "@DNI", obj.DNI },
                            { "@NOMBRE", obj.Nombre },
                            { "@APELLIDO", obj.Apellido },
                            { "@SEXO", obj.Sexo },
                            { "@FECHANACIMIENTO", obj.FechaNacimiento },
                            { "@FECHAINICIO", obj.FechaContrato },
                            { "@SUELDO", obj.Sueldo },
                            { "@DIRECCION", obj.Direccion },
                            { "@PROVINCIA", obj.Provincia },
                            { "@LOCALIDAD", obj.Localidad },
                            { "@NACIONALIDAD", obj.Nacionalidad },
                            { "@HASH", obj.Hash },
                            { "@SALT", obj.Salt },
                            { "@ROL", obj.Rol }
                        }
                    );
        }

        public static Response Modificar(Empleado obj, string oldDNI) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.RunTransaction(
                        query: $"UPDATE [{Empleado.Table}] SET " +
                        $"[{Empleado.Columns.DNI}] = @DNI, " +
                        $"[{Empleado.Columns.Nombre}] = @NOMBRE, " +
                        $"[{Empleado.Columns.Apellido}] = @APELLIDO, " +
                        $"[{Empleado.Columns.Sexo}] = @SEXO, " +
                        $"[{Empleado.Columns.FechaNacimiento}] = @FECHANACIMIENTO, " +
                        $"[{Empleado.Columns.FechaInicio}] = @FECHAINICIO, " +
                        $"[{Empleado.Columns.Sueldo}] = @SUELDO, " +
                        $"[{Empleado.Columns.Direccion}] = @DIRECCION, " +
                        $"[{Empleado.Columns.Provincia}] = @PROVINCIA, " +
                        $"[{Empleado.Columns.Localidad}] = @LOCALIDAD, " +
                        $"[{Empleado.Columns.Estado}] = @ESTADO, " +
                        $"[{Empleado.Columns.Rol}] = @ROL " +
                        $"WHERE [{Empleado.Columns.DNI}] = @OLDDNI",
                        parameters: new Dictionary<string, object>() {
                            { "@DNI", obj.DNI },
                            { "@NOMBRE", obj.Nombre },
                            { "@APELLIDO", obj.Apellido },
                            { "@SEXO", obj.Sexo },
                            { "@FECHANACIMIENTO", obj.FechaNacimiento },
                            { "@FECHAINICIO", obj.FechaContrato },
                            { "@SUELDO", obj.Sueldo },
                            { "@DIRECCION", obj.Direccion },
                            { "@PROVINCIA", obj.Provincia },
                            { "@LOCALIDAD", obj.Localidad },
                            { "@ESTADO", obj.Estado },
                            { "@ROL", obj.Rol },
                            { "@OLDDNI", oldDNI }
                        }
                    );
        }

        public static Response CambiarClave(Empleado obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.CambiarClave,
                        parameters: new Dictionary<string, object> {
                            { "@DNI", obj.DNI },
                            { "@HASH", obj.Hash },
                            { "@SALT", obj.Salt }
                        }
                    );
        }
    }
}
