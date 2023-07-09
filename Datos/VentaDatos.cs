using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos {
    public class VentaDatos {

        /// <summary>
        /// Lista de procedimientos utilizados en esta clase.
        /// </summary>
        public static class Procedures {
            public const string IniciarVenta = "IniciarVenta";
            public const string TotalVentasUltimoDia = "Widget_TotalVentas_UltimoDia";
            public const string TotalVentasUltimaSemana = "Widget_TotalVentas_UltimaSemana";
            public const string ProductoMasVendidoUltimaSemana = "Widget_ProductoMasVendido_UltimaSemana";
            public const string CantidadDeProductosPorAgotarse = "Widget_ContarProductosConBajoStock";
            public const string CantidadDeProductosAgotados = "Widget_ContarProductosSinStock";
            public const string EliminarVenta = "EliminarVenta";
            public const string Reporte_VentasOrdenadasPorTotales = "Reporte_VentasOrdenadasPorTotales";
        }

        /// <summary>
        /// Todas las columnas de la tabla Ventas.
        /// </summary>
        public static string ALL_COLUMNS {
            get {
                return $"[{Venta.Columns.Id}], [{Venta.Columns.DNI}], [{Venta.Columns.TipoPago}], [{Venta.Columns.Fecha}], [{Venta.Columns.Total}]";
            }
        }
        private static string[] SEARCHABLE_COLUMNS = new string[] {
            Venta.Columns.DNI,
            Venta.Columns.Id,
            Venta.Columns.TipoPago,
            Venta.Columns.Fecha,
            Empleado.Columns.Apellido,
            Empleado.Columns.Nombre
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
        /// Todas las columnas de la tabla Ventas, con total formateado a pesos argentinos.
        /// </summary>
        public static string ALL_COLUMNS_BUT_TOTAL_FORMATTED {
            get {
                return $"[{Venta.Columns.Id}], [{Venta.Columns.DNI}], [{Venta.Columns.TipoPago}], [{Venta.Columns.Fecha}], FORMAT([{Venta.Columns.Total}], 'C', 'es-AR') as [{Venta.Columns.Total}]";
            }
        }

        public static string ALL_COLUMNS_FOR_PRESENTATION { get { return $"{ALL_COLUMNS_BUT_TOTAL_FORMATTED}, [{Empleado.Columns.Nombre}], [{Empleado.Columns.Apellido}]"; } }

        /// <summary>
        /// Crear un registro Venta.
        /// </summary>
        /// <param name="obj">Objeto Venta a insertar en la tabla.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public static Response IniciarVenta(Venta obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.FetchStoredProcedure(
                        storedProcedureName: Procedures.IniciarVenta,
                        parameters: new Dictionary<string, object> {
                            { "@DNI", obj.EmpleadoGestor.DNI },
                            { "@MEDIO", obj.TipoPago },
                            { "@FECHA", obj.Fecha },
                            { "@TOTAL", obj.Total }
                        }
                    );
        }

        public static Response Buscar(string key) {
            var con = new Connection(Connection.Database.Pets);
            return con.FetchData(
                    query: $"SELECT {ALL_COLUMNS_FOR_PRESENTATION} FROM [{Venta.Table}]  INNER JOIN [{Empleado.Table}] ON [{Venta.Columns.DNI}] = [{Empleado.Columns.DNI}] WHERE {GenerateSearchQuery("@query")}",
                    parameters: new Dictionary<string, object> {
                        { "@query", key }
                    }
                );
        }


        /// <summary>
        /// Obtener un registro por su ID.
        /// </summary>
        /// <param name="id">ID a buscar.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response GetVentaByID(int id) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM [{Venta.Table}] WHERE [{Venta.Columns.Id}] = @id",
                        parameters: new Dictionary<string, object> {
                            { "@id", id }
                        }
                    );
        }

        /// <summary>
        /// Obtener ventas hechas/creadas por un empleado en particular.
        /// </summary>
        /// <param name="dni">DNI del empleado.</param>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response GetVentaByDNI(string dni) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.FetchData(
                        query: $"SELECT {ALL_COLUMNS_BUT_TOTAL_FORMATTED} FROM [{Venta.Table}] WHERE [{Venta.Columns.DNI}] LIKE '%' + @id + '%'  ORDER BY [{Venta.Columns.Id}] DESC",
                        parameters: new Dictionary<string, object> {
                            { "@id", dni }
                        }
                    );
        }

        /// <summary>
        /// Obtener todas las ventas.
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación.</returns>
        public static Response GetVentas() {
            Connection con = new Connection(Connection.Database.Pets);
            return con.FetchData(
                        query: $"SELECT {ALL_COLUMNS_FOR_PRESENTATION} FROM [{Venta.Table}] INNER JOIN [{Empleado.Table}] ON [{Venta.Columns.DNI}] = [{Empleado.Columns.DNI}] ORDER BY [{Venta.Columns.Id}] DESC"
                    );
        }

        public static Response Reporte_VentasOrdenadasPorTotales(string fechaInicio, string fechaFin) {
            var con = new Connection(Connection.Database.Pets);
            return con.FetchStoredProcedure(
                    storedProcedureName: Procedures.Reporte_VentasOrdenadasPorTotales,
                    parameters: new Dictionary<string, object> {
                        { "@FECHAINICIO", fechaInicio },
                        { "@FECHAFIN", fechaFin }
                    }
                );
        }



        /// <summary>
        /// Uso para Widgets en la página de Inicio.
        /// </summary>
        public static class Widgets {

            /// <summary>
            /// Total recaudado en ventas de las últimas 24 horas.
            /// </summary>
            /// <returns>Objeto Response con el resultado de la operación.</returns>
            public static Response TotalDeVentasUltimoDia() {
                Connection con = new Connection(Connection.Database.Pets);
                return con.FetchStoredProcedure(
                            storedProcedureName: Procedures.TotalVentasUltimoDia
                        );
            }

            /// <summary>
            /// Total recaudado en ventas de los últimos siete días.
            /// </summary>
            /// <returns>Objeto Response con el resultado de la operación.</returns>
            public static Response TotalDeVentasUltimaSemana() {
                Connection con = new Connection(Connection.Database.Pets);
                return con.FetchStoredProcedure(
                            storedProcedureName: Procedures.TotalVentasUltimaSemana
                        );
            }

            /// <summary>
            /// Producto con mayor cantidad vendida en los últimos siete días.
            /// </summary>
            /// <returns>Objeto Response con el resultado de la operación.</returns>
            public static Response ProductoMasVendidoUltimaSemana() {
                Connection con = new Connection(Connection.Database.Pets);
                return con.FetchStoredProcedure(
                            storedProcedureName: Procedures.ProductoMasVendidoUltimaSemana
                        );
            }

            /// <summary>
            /// Cantidad de productos con stock menor a cinco.
            /// </summary>
            /// <returns>Objeto Response con el resultado de la operación.</returns>
            public static Response CantidadDeProductosPorAgotarse() {
                Connection con = new Connection(Connection.Database.Pets);
                return con.FetchStoredProcedure(
                            storedProcedureName: Procedures.CantidadDeProductosPorAgotarse
                        );
            }

            /// <summary>
            /// Cantidad de productos con stock cero.
            /// </summary>
            /// <returns>Objeto Response con el resultado de la operación.</returns>
            public static Response CantidadDeProductosAgotados() {
                Connection con = new Connection(Connection.Database.Pets);
                return con.FetchStoredProcedure(
                            storedProcedureName: Procedures.CantidadDeProductosAgotados
                        );
            }

            
        }
        public static Response EliminarVentaYDetalles(Venta venta) {
            var con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                    storedProcedureName: Procedures.EliminarVenta,
                    parameters: new Dictionary<string, object> {
                            { "@ID", venta.Id }
                    }
                );
        }

    }
}
