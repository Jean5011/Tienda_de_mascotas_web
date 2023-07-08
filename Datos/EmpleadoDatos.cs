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

        /// <summary>
        /// Todas las columnas de la tabla Empleados.
        /// </summary>
        public static string ALL_COLUMNS {
            get {
                return $"[{Empleado.Columns.DNI}], " +
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
            }
        }

        /// <summary>
        /// Todas las columnas de la tabla Empleados, con el sueldo formateado en pesos argentinos.
        /// </summary>
        public static string ALL_COLUMNS_BUT_FORMATTED {
            get {
                return $"[{Empleado.Columns.DNI}], " +
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
            }
        }

        /// <summary>
        /// Lista de procedimientos utilizados en esta clase.
        /// </summary>
        public static class Procedures {
            public static string CambiarClave = "CambiarClave";
            public static string CrearEmpleado = "CrearEmpleado";
        }


        private static string[] SEARCHABLE_COLUMNS = new string[] {
            Empleado.Columns.Nombre,
            Empleado.Columns.Apellido,
            Empleado.Columns.Sueldo,
            Empleado.Columns.Direccion,
            Empleado.Columns.Provincia,
            Empleado.Columns.Nacionalidad,
            Empleado.Columns.Rol,
            Empleado.Columns.Nacionalidad
        };

        public static string GenerateSearchQuery(string key) {
            string resultat = "";
            for (int i = 0; i < SEARCHABLE_COLUMNS.Length; i++) {
                string column = SEARCHABLE_COLUMNS[i];
                resultat += i > 0 ? " OR " : "";
                resultat += $" [{column}] LIKE '%' + {key} + '%' ";
            }
            return resultat;
        }

        /// <summary>
        /// Obtener tabla con todos los empleados. Usa campos formateados.
        /// </summary>
        /// <param name="soloActivos">Indica si debe filtrar empleados activos.</param>
        /// <returns>Objeto Response con el resultado de la operación y datos obtenidos.</returns>
        public static Response ObtenerListaDeEmpleados(bool soloActivos = true) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS_BUT_FORMATTED} FROM [{Empleado.Table}] { (soloActivos ? $"WHERE [{Empleado.Columns.Estado}] = '1' " : "")}"
                    );
        }

        /// <summary>
        /// Obtener tabla con todos los empleados cuyos nombres completos tengan coincidencias con una cadena dada.
        /// </summary>
        /// <param name="nombre">Nombre a buscar</param>
        /// <param name="soloActivos">Indica si debe filtrar empleados habilitados.</param>
        /// <returns>Objeto Response con el resultado de la operación y datos obtenidos.</returns>
        public static Response FiltrarEmpleadosPorNombreCompleto(string nombre, bool soloActivos = true) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS_BUT_FORMATTED} FROM [{Empleado.Table}] WHERE CONCAT([{Empleado.Columns.Nombre}], ' ', [{Empleado.Columns.Apellido}]) LIKE '%' + @q + '%' { (soloActivos ? $" AND [{Empleado.Columns.Estado}] = '1'" : "") } AND {GenerateSearchQuery("@q")}",
                        parameters: new Dictionary<string, object>() {
                            { "@q", nombre }
                        }
                    );
        }

        /// <summary>
        /// Obtener tabla con un sólo empleado cuyo DNI coincida con uno dado.
        /// </summary>
        /// <param name="DNI">El DNI a buscar</param>
        /// <returns>Objeto Response con el resultado de la operación y datos obtenidos.</returns>
        public static Response BuscarEmpleadoPorDNI(string DNI) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.DNI}] = @dni";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@dni", DNI }
                        }
                    );
        }

        /// <summary>
        /// Obtener tabla con todos los empleados cuyos nombres tengan coincidencias con una cadena dada.
        /// </summary>
        /// <param name="Nombre">Nombre a buscar.</param>
        /// <returns>Objeto Response con el resultado de la operación y datos obtenidos.</returns>
        public static Response BuscarEmpleadoPorNombre(string Nombre) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.Nombre}] LIKE '%@name%' AND [{Empleado.Columns.Estado}] = '1'",
                        parameters: new Dictionary<string, object> {
                            { "@name", Nombre }
                        }
                    );
        }

        /// <summary>
        /// Obtener tabla con todos los empleados cuyos apellidos tengan coincidencias con una cadena dada.
        /// </summary>
        /// <param name="Apellido">Apellido a buscar.</param>
        /// <returns>Objeto Response con el resultado de la operación y datos obtenidos.</returns>
        public static Response BuscarEmpleadoPorApellido(string Apellido) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Empleado.Table} WHERE [{Empleado.Columns.Apellido}] LIKE '%@surname%' AND [{Empleado.Columns.Estado}] = '1'",
                        parameters: new Dictionary<string, object> {
                            { "@surname", Apellido }
                        }
                    );
        }


        /// <summary>
        /// Crea un registro Empleado mediante el uso de un procedimiento almacenado.
        /// </summary>
        /// <param name="obj">Objeto Empleado con los datos a subir.</param>
        /// <returns>Response con el resultado de la transacción.</returns>
        public static Response CrearEmpleado(Empleado obj) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.ExecuteStoredProcedure(
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

        /// <summary>
        /// Modifica un registro existente mediante el uso de un procedimiento almacenado.
        /// </summary>
        /// <param name="obj">Objeto Empleado con los nuevos valores.</param>
        /// <param name="oldDNI">DNI antes del cambio. Para identificar a qué empleado se debe de actualizar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response Modificar(Empleado obj, string oldDNI) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.RunTransaction(
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

        /// <summary>
        /// Cambia el hash y el salt de un registro mediante el uso de un procedimiento almacenado.
        /// </summary>
        /// <param name="obj">Objeto Empleado con los nuevos valores de hash, salt y con su DNI.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response CambiarClave(Empleado obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.CambiarClave,
                        parameters: new Dictionary<string, object> {
                            { "@DNI", obj.DNI },
                            { "@HASH", obj.Hash },
                            { "@SALT", obj.Salt }
                        }
                    );
        }

        public static Response Deshabilitar(Empleado obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.FetchData(
                    query: $"UPDATE [{Empleado.Table}] SET [{Empleado.Columns.Estado}] = 0 WHERE [{Empleado.Columns.DNI}] = @dni",
                    parameters: new Dictionary<string, object> {
                        { "@dni", obj.DNI }
                    }
                );
        }

        public static Response Habilitar(Empleado obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.FetchData(
                    query: $"UPDATE [{Empleado.Table}] SET [{Empleado.Columns.Estado}] = 1 WHERE [{Empleado.Columns.DNI}] = @dni",
                    parameters: new Dictionary<string, object> {
                        { "@dni", obj.DNI }
                    }
                );
        }
    }
}
