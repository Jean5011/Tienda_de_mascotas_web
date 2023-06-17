using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos {
    public class EmpleadoDatos {
        private static string ALL_COLUMNS = $"[{Empleado.Columns.DNI}], " +
                                    $"[{Empleado.Columns.Nombre}]" +
                                    $"[{Empleado.Columns.Apellido}]" +
                                    $"[{Empleado.Columns.Sexo}]" +
                                    $"[{Empleado.Columns.FechaNacimiento}]" +
                                    $"[{Empleado.Columns.FechaInicio}]" +
                                    $"[{Empleado.Columns.Sueldo}]" +
                                    $"[{Empleado.Columns.Direccion}]" +
                                    $"[{Empleado.Columns.Provincia}]" +
                                    $"[{Empleado.Columns.Localidad}]" +
                                    $"[{Empleado.Columns.Nacionalidad}]" +
                                    $"[{Empleado.Columns.Estado}]" +
                                    $"[{Empleado.Columns.Hash}]" +
                                    $"[{Empleado.Columns.Salt}]";
        public static Response ObtenerListaDeEmpleados() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table}"
                    );
        }
        public static Response BuscarEmpleadoPorDNI(string DNI) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.DNI}] = @dni",
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
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.Nombre}] LIKE '%@name%'",
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
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.Apellido}] LIKE '%@surname%'",
                        parameters: new Dictionary<string, object> {
                            { "@surname", Apellido }
                        }
                    );
        }
    }
}
