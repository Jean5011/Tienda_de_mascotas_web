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
    public class DaoAnimales {
        public DaoAnimales() { }

        /// <summary>
        /// Todas las columnas de la tabla Animales.
        /// </summary>
        private static string ALL_COLUMNS {
            get {
                return $"[{Animal.Columns.Codigo}], " +
                        $"[{Animal.Columns.Nombre}], " +
                        $"[{Animal.Columns.Raza}], " +
                        $"[{Animal.Columns.Estado}] ";
            }
        }

        /// <summary>
        /// Listado de procedimientos que se utilizan en esta clase.
        /// </summary>
        public static class Procedures {
            public static string Ingresar = "SP_IngresarAnimal";
            public static string Eliminar = "SP_EliminarAnimal";
            public static string Actializar = "SP_ActualizarAnimales";
            public static string Alta = "SP_AltaAnimal";
        }

        /// <summary>
        /// Obtener tabla de animales.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerListaDeAnimales() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Animal.Table}"
                    );
        }

        /// <summary>
        /// Obtener lista de animales activos.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerLista() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT ({Animal.Columns.Nombre}+    +{Animal.Columns.Raza}) as 'Ani', {Animal.Columns.Codigo}, [{Animal.Columns.Estado}]  FROM {Animal.Table} WHERE [{Animal.Columns.Estado}] = 1"
                    );
        }

        /// <summary>
        /// Buscar animal activo cuyo ID coincida con uno dado.
        /// </summary>
        /// <param name="ID">ID a buscar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarAnimalPorCod(string ID) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Animal.Table} WHERE [{Animal.Columns.Codigo}] = @ID and {Animal.Columns.Estado}=1 ";
            Connection connection = new Connection(Connection.Database.Pets);
            Trace.Write("BuscarAnimalPorCod", $"Consulta: {consulta}");
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }
                        }
                    );
        }

        /// <summary>
        /// Agrega un registro Animal a la base de datos.
        /// </summary>
        /// <param name="An">Objeto Animal con los datos a ingresar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response IngresarAnimal(Animal An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Ingresar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodAnimales_An", An.Codigo },
                            { "@nombre_An", An.Nombre },
                            { "@NombreDeRaza_An", An.Raza }
                        }
                    );
        }

        /// <summary>
        /// Elimina un animal de la base de datos. 
        /// </summary>
        /// <param name="An">Objeto Animal con el código establecido.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response EliminarAnimal(Animal An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Eliminar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodAnimales_An", An.Codigo }
                        }
                    );
        }

        public static Response HabilitarAnimal(Animal An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.RunTransaction(
                        query: $"UPDATE [{Animal.Table}] SET [{Animal.Columns.Estado}] = 1 WHERE [{Animal.Columns.Codigo}] = @COD",
                        parameters: new Dictionary<string, object> {
                            { "@COD", An.Codigo }
                        }
                    );
        }

        /// <summary>
        /// Actualiza un registro Animal en la base de datos con los datos de un objeto Animal dado.
        /// </summary>
        /// <param name="An">Objeto Animal con los datos a actualizar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response ActualizarAnimal(Animal An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Actializar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodAnimales_An", An.Codigo },
                            { "@nombre_An", An.Nombre },
                            { "@NombreDeRaza_An", An.Raza }
                        }
                    );
        }

        /// <summary>
        /// Obtener lista de animales inactivos.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerListaBaja() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Animal.Table} where {Animal.Columns.Estado}=0"
                    );
        }

        /// <summary>
        /// Buscar animal inactivo cuyo ID coincida con uno dado.
        /// </summary>
        /// <param name="ID">ID a buscar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarAnimalPorCodBaja(string ID) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Animal.Table} WHERE [{Animal.Columns.Codigo}] = @ID and {Animal.Columns.Estado}=0 ";
            Connection connection = new Connection(Connection.Database.Pets);
            Trace.Write("BuscarAnimalPorCod", $"Consulta: {consulta}");
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }
                        }
                    );
        }

        /// <summary>
        /// Da de alta un registro animal en la base de datos a partir de su código.
        /// </summary>
        /// <param name="An">Objeto Animal con el código establecido.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response AltaAnimal(Animal An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Alta,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodAnimales_An", An.Codigo }
                        }
                    );
        }

    }
}
