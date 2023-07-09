using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;


namespace Datos {
    public class ProveedorDatos {

        /// <summary>
        /// Todas las columnas de la tabla Proveedores.
        /// </summary>
        private static string ALL_COLUMNS {
            get {
                return $"[{Proveedor.Columns.CUIT}]," +
          $"[{Proveedor.Columns.RazonSocial}]," +
          $"[{Proveedor.Columns.NombreContacto}]," +
          $"[{Proveedor.Columns.CorreoElectronico}]," +
          $"[{Proveedor.Columns.Telefono}]," +
          $"[{Proveedor.Columns.Direccion}]," +
          $"[{Proveedor.Columns.Provincia}]," +
          $"[{Proveedor.Columns.Localidad}]," +
          $"[{Proveedor.Columns.Pais}]," +
          $"[{Proveedor.Columns.CodigoPostal}], " +
          $"[{Proveedor.Columns.Estado}]";
            }
        }

        /// <summary>
        /// Lista de procedimientos utilizados en esta clase.
        /// </summary>
        public static class Procedures {
            public static string Crear = "SP_Proveedor_Crear";
            public static string ActualizarProveedor = "SP_Proveedores_Actualizar";
            public static string ActualizarEstado = "SP_Proveedores_ActualizarEstado";
        }

        /// <summary>
        /// Obtener lista de proveedores activos.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerListaDeProveedores(bool est=true) {
            Connection connection = new Connection(Connection.Database.Pets);
            int estado = est ? 1 : 0;
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} where {Proveedor.Columns.Estado}={estado}"
                    );
        }

        /// <summary>
        /// Obtener un registro Proveedor a partir de un CUIT dado.
        /// </summary>
        /// <param name="CUIT">CUIT del Proveedor en cuestión.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerProveedorByCUIT(string CUIT) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] = @cuit ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@cuit", CUIT }
                        }
                    );
        }

        /// <summary>
        /// Agrega un registro a la tabla Proveedores.
        /// </summary>
        /// <param name="proveedor">Objeto Proveedor a insertar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response InsertarProveedor(Proveedor proveedor) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Crear,
                        parameters: new Dictionary<string, object>
                        {
                            { "@CUIT", proveedor.CUIT },
                            { "@RazonSocial", proveedor.RazonSocial },
                            { "@NombreContacto", proveedor.NombreContacto },
                            { "@CorreoElectronico", proveedor.CorreoElectronico },
                            { "@Telefono", proveedor.Telefono },
                            { "@Direccion", proveedor.Direccion },
                            { "@Provincia", proveedor.Provincia },
                            { "@Localidad", proveedor.Localidad },
                            { "@Pais", proveedor.Pais },
                            { "@CodigoPostal", proveedor.CodigoPostal }
                        }
                    );

        }

        /// <summary>
        /// Realiza un borrado lógico de un registro.
        /// </summary>
        /// <param name="prov">Objeto Proveedor a eliminar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response EliminadoLogicoProveedor(Proveedor prov) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.ActualizarEstado,
                        parameters: new Dictionary<string, object> {
                            { "@CUIT", prov.CUIT },
                            { "@Estado", false }
                        }
                    );
        }

        public static Response HabilitarProveedor(Proveedor prov) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.ActualizarEstado,
                        parameters: new Dictionary<string, object> {
                            { "@CUIT", prov.CUIT },
                            { "@Estado", true }
                        }
                    );
        }

        /// <summary>
        /// Actualizar proveedor en la tabla.
        /// </summary>
        /// <param name="proveedor">Objeto Proveedor a actualizar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response ActualizarProveedor(Proveedor proveedor) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.ActualizarProveedor,
                        parameters: new Dictionary<string, object>
                        {
                            { "@CUIT", proveedor.CUIT },
                            { "@RazonSocial", proveedor.RazonSocial },
                            { "@NombreContacto", proveedor.NombreContacto },
                            { "@CorreoElectronico", proveedor.CorreoElectronico },
                            { "@Telefono", proveedor.Telefono },
                            { "@Direccion", proveedor.Direccion },
                            { "@Provincia", proveedor.Provincia },
                            { "@Localidad", proveedor.Localidad },
                            { "@Pais", proveedor.Pais },
                            { "@CodigoPostal", proveedor.CodigoPostal }
                        }
                    );
        }

        /// <summary>
        /// Comprueba si existe un registro en la tabla Proveedores cuyo CUIT coincida con el dado.
        /// </summary>
        /// <param name="CUIT">CUIT a comprobar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response VerificarExiste(string CUIT) {
            string consulta = $"SELECT COUNT ({Proveedor.Columns.CUIT}) AS [CUIT] FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] = @CUIT ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@CUIT", CUIT }
                        }
                    );
        }

        /// <summary>
        /// Obtener un proveedor a partir de un CUIT.
        /// </summary>
        /// <param name="CUIT">CUIT a buscar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerProveedorCUITEditar(string CUIT) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] =@cuit";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@cuit", CUIT }
                        }
                    );
        }
    }
}
