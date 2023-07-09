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

        private static string[] SEARCHABLE_COLUMNS = new string[] {
            Producto.Columns.Codigo_Prod,
            Producto.Columns.CUITProv,
            Producto.Columns.CodTipoProducto,
            Producto.Columns.Nombre,
            Producto.Columns.Marca,
            Producto.Columns.Descripcion,
            Proveedor.Columns.RazonSocial,
            TipoProducto.Columns.TipoDeProducto
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
        /// Lista de procedimientos que se utilizan en esta clase.
        /// </summary>
        public static class Procedures {
            public static string Crear = "SP_Productos_Crear";
            public static string ActualizarProducto = "SP_Productos_Actualizar";
            public static string EliminarProducto = "SP_Productos_Eliminar";
            public static string Reporte_ProductosMasVendidos = "Reporte_ProductosMasVendidos";
        }

        public static Response ProductosMasVendidos(string fechaInicio, string fechaFin) {
            var con = new Connection(Connection.Database.Pets);
            return con.FetchStoredProcedure(
                    storedProcedureName: Procedures.Reporte_ProductosMasVendidos,
                    parameters: new Dictionary<string, object> {
                        { "@FECHAINICIO", fechaInicio },
                        { "@FECHAFIN", fechaFin }
                    }
                );
        }

        /// <summary>
        /// Obtener tabla de todos los productos.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerListaDeProductos(bool est = true) {
            Connection connection = new Connection(Connection.Database.Pets);
            int estado = est ? 1 : 0;
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS}, [{Proveedor.Columns.RazonSocial}], [{TipoProducto.Columns.TipoDeProducto}] FROM {Producto.Table} INNER JOIN [{Proveedor.Table}] ON [{Proveedor.Columns.CUIT}] = [{Producto.Columns.CUITProv}] INNER JOIN [{TipoProducto.Table}] on [{Producto.Columns.CodTipoProducto}] = [{TipoProducto.Columns.Codigo}] WHERE [{Producto.Columns.Estado}]={estado}"
                    );
        }
        public static Response Buscar(string q) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS}, [{Proveedor.Columns.RazonSocial}], [{TipoProducto.Columns.TipoDeProducto}]  FROM {Producto.Table} INNER JOIN [{Proveedor.Table}] ON [{Proveedor.Columns.CUIT}] = [{Producto.Columns.CUITProv}] INNER JOIN [{TipoProducto.Table}] on [{Producto.Columns.CodTipoProducto}] = [{TipoProducto.Columns.Codigo}] WHERE {GenerateSearchQuery("@q")}",
                        parameters: new Dictionary<string, object> {
                            { "@q", q }
                        }
                    );
        }


    

        /// <summary>
        /// Obtener tabla de todos los productos activos.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerListaDeProductosActivos()
        {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Producto.Table} WHERE [{Producto.Columns.Estado}] = '1'"
                    );
        }

        /// <summary>
        /// Obtener tabla de todos los productos activos con stock mayor a 0.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerListaActivosSinRepetir()
        {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {Producto.Columns.Codigo_Prod},{Producto.Columns.Nombre} FROM {Producto.Table} WHERE [{Producto.Columns.Estado}] = '1' and {Producto.Columns.Stock}>0 GROUP BY {Producto.Columns.Codigo_Prod},{Producto.Columns.Nombre}"
                    );
        }
        public static Response ObtenerNombre(string ID)
        {

            string consulta = $"SELECT {Producto.Columns.Nombre} FROM {Producto.Table} WHERE [{Producto.Columns.Estado}] = '1' and {Producto.Columns.Stock}>0 and {Producto.Columns.Codigo_Prod}= @ID";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }
                        }
                    );
           
        }




        /// <summary>
        /// Obtiene los Codigos y Cuits concatenados de cada producto activo.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response ObtenerCodigoYCuit()
        {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT [{Producto.Columns.Codigo_Prod}],[{Producto.Columns.Nombre}],[{Proveedor.Columns.RazonSocial}], STRING_AGG(CONCAT({Producto.Columns.Codigo_Prod},'.',{Producto.Columns.CUITProv}), ',') AS CODyCUIT FROM {Producto.Table} inner join {Proveedor.Table} on [{Proveedor.Columns.CUIT}] = [{Producto.Columns.CUITProv}] WHERE [{Producto.Columns.Estado}] = '1' AND [{Producto.Columns.Stock}] > 0 GROUP BY [{Producto.Columns.Codigo_Prod}],[{Producto.Columns.Nombre}],[{Proveedor.Columns.RazonSocial}]"
                    );
        }

        /// <summary>
        /// Obtener un producto por su ID y su Proveedor.
        /// </summary>
        
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarProductoPorCodYCuit(string ID, string cuit) {
            string consulta = $"SELECT {ALL_COLUMNS}, [{Proveedor.Columns.RazonSocial}], [{TipoProducto.Columns.TipoDeProducto}]  FROM {Producto.Table} INNER JOIN [{Proveedor.Table}] ON [{Proveedor.Columns.CUIT}] = [{Producto.Columns.CUITProv}] INNER JOIN [{TipoProducto.Table}] on [{Producto.Columns.CodTipoProducto}] = [{TipoProducto.Columns.Codigo}] WHERE [{Producto.Columns.Codigo_Prod}] = @ID and {Producto.Columns.CUITProv} = @CUIT";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID },
                            { "@CUIT",cuit }
                        }
                    );
        }


        /// <summary>
        /// Obtener un producto por su ID.
        /// </summary>
        /// <param name="ID">ID a buscar</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response BuscarProductoPorCod(string ID)
        {
            string consulta = $"SELECT {ALL_COLUMNS}, [{Proveedor.Columns.RazonSocial}], [{TipoProducto.Columns.TipoDeProducto}]  FROM {Producto.Table} INNER JOIN [{Proveedor.Table}] ON [{Proveedor.Columns.CUIT}] = [{Producto.Columns.CUITProv}] INNER JOIN [{TipoProducto.Table}] on [{Producto.Columns.CodTipoProducto}] = [{TipoProducto.Columns.Codigo}] WHERE [{Producto.Columns.Codigo_Prod}] = @ID";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }                           
                        }
                    );
        }



        //SIN STOCK
        public static Response ObtenerListaDeProductosSinStock(bool est = true)
        {
            Connection connection = new Connection(Connection.Database.Pets);
            int estado = est ? 1 : 0;
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS}, [{Proveedor.Columns.RazonSocial}], [{TipoProducto.Columns.TipoDeProducto}] FROM {Producto.Table} INNER JOIN [{Proveedor.Table}] ON [{Proveedor.Columns.CUIT}] = [{Producto.Columns.CUITProv}] INNER JOIN [{TipoProducto.Table}] on [{Producto.Columns.CodTipoProducto}] = [{TipoProducto.Columns.Codigo}] WHERE [{Producto.Columns.Estado}]={estado} and [{Producto.Columns.Stock}]= 0"
                    );
        }
        //BAJO STOCK
        public static Response ObtenerListaDeProductosBajoStock(bool est = true)
        {
            Connection connection = new Connection(Connection.Database.Pets);
            int estado = est ? 1 : 0;
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS}, [{Proveedor.Columns.RazonSocial}], [{TipoProducto.Columns.TipoDeProducto}] FROM {Producto.Table} INNER JOIN [{Proveedor.Table}] ON [{Proveedor.Columns.CUIT}] = [{Producto.Columns.CUITProv}] INNER JOIN [{TipoProducto.Table}] on [{Producto.Columns.CodTipoProducto}] = [{TipoProducto.Columns.Codigo}] WHERE [{Producto.Columns.Estado}]={estado} and [{Producto.Columns.Stock}]<=5"
                    );
        }
        //ALTO STOCK
        public static Response ObtenerListaDeProductosAltoStock(bool est = true)
        {
            Connection connection = new Connection(Connection.Database.Pets);
            int estado = est ? 1 : 0;
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS}, [{Proveedor.Columns.RazonSocial}], [{TipoProducto.Columns.TipoDeProducto}] FROM {Producto.Table} INNER JOIN [{Proveedor.Table}] ON [{Proveedor.Columns.CUIT}] = [{Producto.Columns.CUITProv}] INNER JOIN [{TipoProducto.Table}] on [{Producto.Columns.CodTipoProducto}] = [{TipoProducto.Columns.Codigo}] WHERE [{Producto.Columns.Estado}]={estado} and [{Producto.Columns.Stock}]>5"
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
