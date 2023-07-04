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
    public class DaoTiposDeProductos {
        public DaoTiposDeProductos() { }
        /// <summary>
        /// Todas las columnas de la tabla Tipos de Productos (Categorías)
        /// </summary>
        private static string ALL_COLUMNS { get { return $"[{TipoProducto.Columns.Codigo}], " +
                                    $"[{TipoProducto.Columns.CodAnimal}], " +
                                   $"[{TipoProducto.Columns.TipoDeProducto}], " +
                                   $"[{TipoProducto.Columns.Descripcion}], " +
                                   $"[{TipoProducto.Columns.Estado}] ";
            } }

        /// <summary>
        /// Lista de procedimientos utilizados en esta clase.
        /// </summary>
        public static class Procedures {
            public static string Ingresar = "SP_IngresarTipoDeProductos";
            public static string Eliminar = "SP_EliminarTipoDeProductos";
            public static string Actualizar = "SP_ActualizarTipoProducto";
            public static string Alta = "SP_AltaTipoDeProductos";
        }

        /// <summary>
        /// Obtener lista de categorías.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación. </returns>
        public static Response ObtenerListaDeTipoProducto() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM [{TipoProducto.Table}]"
                    );
        }

        /// <summary>
        /// Buscar un producto a partir de su código.
        /// </summary>
        /// <param name="ID">Código a buscar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarTipoProductoPorCod(string ID) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM [{TipoProducto.Table}] WHERE [{TipoProducto.Columns.Codigo}] = @ID";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }
                        }
                    );
        }

        /// <summary>
        /// Añadir un registro a la tabla Tipos de Producto (Categorías)
        /// </summary>
        /// <param name="An">Objeto TipoProducto a agregar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response IgresarTipoProducto(TipoProducto An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Ingresar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodTipoProducto_TP", An.Codigo },
                            { "@CodAnimales_Tp", An.CodAnimal },
                            { "@TipoDeProducto_Tp", An.tipoDeProducto },
                            { "@Descripcion_TP", An.Descripcion }
                        }
                    );
        }

        /// <summary>
        /// Eliminar una categoría de la tabla.
        /// </summary>
        /// <param name="An">Objeto TipoProducto a eliminar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response EliminarTipoProducto(TipoProducto An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Eliminar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodTipoProducto_TP", An.Codigo }
                        }
                    );
        }

        /// <summary>
        /// Actualiza una categoría en la tabla.
        /// </summary>
        /// <param name="An">Objeto TipoProducto a actualizar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ActualizarTipoProducto(TipoProducto An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Actualizar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodTipoProducto_TP", An.Codigo },
                            { "@CodAnimales_Tp", An.CodAnimal },
                            { "@TipoDeProducto_Tp", An.tipoDeProducto },
                            { "@Descripcion_TP", An.Descripcion }
                        }
                    );
        }

        /// <summary>
        /// Obtener lista de categorías inactivas.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerListaDeTipoProductoBajas() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {TipoProducto.Table} whrere {TipoProducto.Columns.Estado} =0"
                    );
        }

        /// <summary>
        /// Busca un registro inactivo a partir de un ID dado.
        /// </summary>
        /// <param name="ID">ID del registro en cuestión.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarTipoProductoPorCodBajas(string ID) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {TipoProducto.Table} WHERE [{TipoProducto.Columns.Codigo}] = @ID and  {TipoProducto.Columns.Estado} =0 ";
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
        /// Alta de una categoría previamente inactiva.
        /// </summary>
        /// <param name="An">Objeto TipoProducto a dar de alta.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response AltaTipoProducto(TipoProducto An) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Alta,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodTipoProducto_TP", An.Codigo }
                        }
                    );
        }
    }
}
