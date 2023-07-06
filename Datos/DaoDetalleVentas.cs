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
    public class DaoDetalleVentas {

        public DaoDetalleVentas() { }

        /// <summary>
        /// Todas las columnas de la tabla Detalles
        /// </summary>
        private static string ALL_COLUMNS {
            get {
                return  $"[{DetalleVenta.Columns.CodVenta_Dv}], " +
                        $"[{DetalleVenta.Columns.CodProducto_Dv}], " +
                        $"[{DetalleVenta.Columns.CUITProv}], " +
                        $"[{DetalleVenta.Columns.Cantidad_Dv}], " +
                        $"[{DetalleVenta.Columns.PrecioUnitario_Dv}], " +
                        $"[{DetalleVenta.Columns.PrecioTotal_Dv}], " +
                        $"[{DetalleVenta.Columns.Estado_Dv}] ";
            }
        }

        /// <summary>
        /// Lista de procedimientos que se utilizan en esta clase.
        /// </summary>
        public static class Procedures {
            public static string Agregar = "SP_DetalleDeVenta_Agregar";
            public static string Bajar = "SP_DetalleDeVenta_DarDeBaja";
            public static string DisminuirCantidadVendida = "SP_DetalleDeVenta_disminuirCantidadVendida";
            public static string AumentarCantidadVendida = "SP_DetalleDeVenta_aumentarCantidadVendida";
        }

        /// <summary>
        /// Obtener todos los detalles de venta que compartan un código de venta dado. También recibe el nombre del producto y la razón social del proveedor mencionados por registro.
        /// </summary>
        /// <param name="Cod">Código de la venta.</param>
        /// <returns>Objeto Response con el resultado de la operación</returns>
        public static Response ObtenerDetalleVenta(int Cod) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS}, [{Producto.Columns.Nombre}], [{Proveedor.Columns.RazonSocial}] FROM {DetalleVenta.Table} " +
                               $"INNER JOIN [{Producto.Table}] ON [{DetalleVenta.Columns.CodProducto_Dv}] = [{Producto.Columns.Codigo_Prod}] " +
                               $"INNER JOIN [{Proveedor.Table}] ON [{Proveedor.Columns.CUIT}] = [{DetalleVenta.Columns.CUITProv}] WHERE [{DetalleVenta.Columns.CodVenta_Dv}] = @Codigo AND [{DetalleVenta.Columns.Estado_Dv}] = 1",
                        new Dictionary<string, object> { 
                            {"@Codigo",Cod}
                        }
                    );
        }

        /// <summary>
        /// Elimina un detalle de venta de la base de datos.
        /// </summary>
        /// <param name="codVenta">Código de la venta.</param>
        /// <param name="codProducto">Código del producto a eliminar.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response EliminarDetalle(int codVenta, string codProducto) {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.RunTransaction(
                        query: $"DELETE FROM {DetalleVenta.Table} WHERE [{DetalleVenta.Columns.CodVenta_Dv}] = @Codigo AND [{DetalleVenta.Columns.CodProducto_Dv}] = @Prod",
                        new Dictionary<string, object> {
                            { "@Codigo", codVenta},
                            { "@Prod", codProducto }
                        }
                    );
        }

        /// <summary>
        /// Agrega un registro en la base de datos.
        /// </summary>
        /// <param name="Dv">Objeto DetalleVenta con los datos a agregar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response AgregarRegistro(DetalleVenta Dv) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Agregar,
                        parameters: new Dictionary<string, object>
                        {
                            { "@CodigoVenta", Dv.Id.Id },
                            { "@CodigoProducto", Dv.Producto.Codigo },
                            { "@CUITProveedor", Dv.Proveedor.CUIT},
                            { "@Cantidad", Dv.Cantidad }
                        }
                    );
        }

        /// @Deprecated
        public static Response DarDeBajaRegistro(int Cod) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Bajar,
                        parameters: new Dictionary<string, object> {
                            { "@Codigo", Cod },
                        }
                    );
        }

        /// <summary>
        /// Aumenta la cantidad vendida de un producto en una venta.
        /// </summary>
        /// <param name="codigo">Código de la venta @Deprecated</param>
        /// <param name="dv">Objeto DetalleVenta con los datos establecidos.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response aumentarCantidadVendida(string codigo, DetalleVenta dv) {
            Connection conexion = new Connection(Connection.Database.Pets);
            return conexion.ExecuteStoredProcedure(
                    storedProcedureName: Procedures.AumentarCantidadVendida,
                    parameters: new Dictionary<string, object> {
                        { "@CodigoVenta", codigo }, // TODO: Reemplazar codigo por dv.CodigoVenta o algo así.
                        { "@CodigoProducto", dv.Producto.Codigo }, 
                        { "@CUITProveedor", dv.Proveedor.CUIT }
                    }
                );
        }

        /// <summary>
        /// Disminuye la cantidad vendida de un producto en una venta.
        /// </summary>
        /// <param name="codigo">Código de la venta @Deprecated</param>
        /// <param name="dv">Objeto DetalleVenta con los datos establecidos</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response disminuirCantidadVendida(string codigo, DetalleVenta dv) {
            Connection conexion = new Connection(Connection.Database.Pets);
            return conexion.ExecuteStoredProcedure(
                    storedProcedureName: Procedures.DisminuirCantidadVendida,
                    parameters: new Dictionary<string, object>
                    {
                        { "@CodigoVenta", codigo }, // TODO: Reemplazar codigo por dv.CodigoVenta o algo así.
                        { "@CodigoProducto", dv.Producto.Codigo }, 
                        { "@CUITProveedor", dv.Proveedor.CUIT } 
                    }
                );
        }
    }
}
