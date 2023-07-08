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
    public class DaoProductos {
        public DaoProductos() { }

        /// <summary>
        /// Todas las columnas de la tabla Producto.
        /// </summary>
        private static string ALL_COLUMNS {
            get {
                return $"[{Producto.Columns.Codigo_Prod}], " +
                      $"[{Producto.Columns.CUITProv}], " +
                     $"[{Producto.Columns.CodTipoProducto}], " +
                     $"[{Producto.Columns.Nombre}], " +
                     $"[{Producto.Columns.Marca}], " +
                     $"[{Producto.Columns.Descripcion}], " +
                     $"[{Producto.Columns.Stock}], " +
                     $"[{Producto.Columns.Precio}], " +
                     $"[{Producto.Columns.Estado}] ";
            }
        }

        /// <summary>
        /// Lista de procedimientos que se utilizan en esta clase.
        /// </summary>
        public static class Procedures {
            public static string Crear = "SP_Productos_Crear";
            public static string ActualizarProducto = "SP_Productos_Actualizar";
            public static string EliminarProducto = "SP_Productos_Eliminar";
        }

        /// <summary>
        /// Obtener tabla de productos activos.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerListaDeProductos() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Producto.Table}"
                    );
        }

        /// <summary>
        /// Obtener un producto por su ID.
        /// </summary>
        /// <param name="ID">ID a buscar</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarProductoPorCod(string ID) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Producto.Table} WHERE [{Producto.Columns.Codigo_Prod}] = @ID ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }
                        }
                    );
        }

        /// <summary>
        /// Agregar un registro a la tabla Productos.
        /// </summary>
        /// <param name="Pr">Objeto Producto con sus propiedades establecidas.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response IngresarProducto(Producto Pr) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Crear,
                        parameters: new Dictionary<string, object>
                        {
                            { "@Codigo", Pr.Codigo },
                            { "@CUIT", Pr.Proveedor.CUIT },
                            { "@Tipo", Pr.Categoria.Codigo},
                            { "@Nombre", Pr.Nombre },
                            { "@Marca", Pr.Marca },
                            { "@Desc", Pr.Descripcion },
                            { "@Stock", Pr.Stock },
                            { "@Precio", Pr.Precio },
                            { "@Estado", Pr.Estado }
                        }
                    );
        }

        /// <summary>
        /// Actualizar registro en la tabla Productos a partir de su código.
        /// </summary>
        /// <param name="Pr">Objeto Producto a actualizar.</param>
        /// <returns>Objeto Response con el resultado de la operación. </returns>
        public static Response ActualizarProducto(Producto Pr) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.ActualizarProducto,
                        parameters: new Dictionary<string, object>
                        {
                            { "@Codigo", Pr.Codigo },
                            { "@CUIT", Pr.Proveedor.CUIT },
                            { "@Tipo", Pr.Categoria.Codigo},
                            { "@Nombre", Pr.Nombre },
                            { "@Marca", Pr.Marca },
                            { "@Desc", Pr.Descripcion },
                            { "@Stock", Pr.Stock },
                            { "@Precio", Pr.Precio },
                            { "@Estado", Pr.Estado}
                        }
                    );
        }

        /// <summary>
        /// Realiza la baja lógica de un registro en la tabla Productos.
        /// </summary>
        /// <param name="Pr">Objeto Producto a eliminar.</param>
        /// <returns>Objeto Response con el resultado de la operación. </returns>
        public static Response EliminarProducto(Producto Pr) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.EliminarProducto,
                        parameters: new Dictionary<string, object> {
                            { "@Codigo", Pr.Codigo },
                        }
                    );
        }

        public static Response Habilitar(Producto Pr) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.RunTransaction(
                    query: $"UPDATE [{Producto.Table}] SET [{Producto.Columns.Estado}] = 1 WHERE [{Producto.Columns.Codigo_Prod}] = @codigo",
                    parameters: new Dictionary<string, object> {
                        { "@codigo", Pr.Codigo }
                    }
                );
        }


        /// <summary>
        /// Verifica si un registro con un ID dado existe en la tabla.
        /// </summary>
        /// <param name="ID">El ID del producto en cuestión.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response VerificarExistenciaProducto(string ID) {
            string consulta = $"SELECT COUNT([{Producto.Columns.Codigo_Prod}]) AS [Cantidad] FROM {Producto.Table} WHERE [{Producto.Columns.Codigo_Prod}] = @ID ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }
                        }
                    );
        }
        public static Response VerificarExistenciaProveedor(string CUIT)
        {
            string consulta = $"SELECT COUNT([{Proveedor.Columns.CUIT}]) AS [Cantidad] FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] = @CUIT ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@CUIT", CUIT }
                        }
                    );
        }

        public static Response VerificarExistenciaProductoYProveedor(string ID,string CUIT)
        {
            string consulta = $"SELECT COUNT([{Producto.Columns.Codigo_Prod}]) AS [Cantidad] FROM {Producto.Table} WHERE [{Producto.Columns.Codigo_Prod}] = @ID AND [{Producto.Columns.CUITProv}] = @CUIT";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID },
                            { "@CUIT",CUIT }
                        }
                    );
        }
    }

}
