using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos {
    public class VentaDatos {

        public static class Procedures {
            public const string IniciarVenta = "IniciarVenta";
            public const string TotalVentasUltimoDia = "Widget_TotalVentas_UltimoDia";
            public const string TotalVentasUltimaSemana = "Widget_TotalVentas_UltimaSemana";
            public const string ProductoMasVendidoUltimaSemana = "Widget_ProductoMasVendido_UltimaSemana";
            public const string CantidadDeProductosPorAgotarse = "Widget_ContarProductosConBajoStock";
            public const string CantidadDeProductosAgotados = "Widget_ContarProductosSinStock";
        }

        public readonly static string ALL_COLUMNS = $"[{Venta.Columns.Id}], [{Venta.Columns.DNI}], [{Venta.Columns.TipoPago}], " +
                                          $"[{Venta.Columns.Fecha}], [{Venta.Columns.Total}]";

        public readonly static string ALL_COLUMNS_BUT_TOTAL_FORMATTED = $"[{Venta.Columns.Id}], [{Venta.Columns.DNI}], [{Venta.Columns.TipoPago}], " +
                                          $"[{Venta.Columns.Fecha}], FORMAT([{Venta.Columns.Total}], 'C', 'es-AR') as [{Venta.Columns.Total}]";



        public static Response IniciarVenta(Venta obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.FetchStoredProcedure(
                        storedProcedureName: Procedures.IniciarVenta,
                        parameters: new Dictionary<string, object> {
                            { "@DNI", obj.EmpleadoGestor.DNI },
                            { "@MEDIO", obj.TipoPago },
                            { "@FECHA", obj.Fecha },
                            { "@TOTAL", obj.Total }
                        }
                    );
        }
        public static Response GetVentaByID(int id) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM [{Venta.Table}] WHERE [{Venta.Columns.Id}] = @id",
                        parameters: new Dictionary<string, object> {
                            { "@id", id }
                        }
                    );
        }
        public static Response GetVentaByDNI(string dni) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.FetchData(
                        query: $"SELECT {ALL_COLUMNS_BUT_TOTAL_FORMATTED} FROM [{Venta.Table}] WHERE [{Venta.Columns.DNI}] LIKE '%' + @id + '%'  ORDER BY [{Venta.Columns.Id}] DESC",
                        parameters: new Dictionary<string, object> {
                            { "@id", dni }
                        }
                    );
        }
        public static Response GetVentas() {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.FetchData(
                        query: $"SELECT {ALL_COLUMNS_BUT_TOTAL_FORMATTED}, [{Empleado.Columns.Nombre}], [{Empleado.Columns.Apellido}] FROM [{Venta.Table}] INNER JOIN [{Empleado.Table}] ON [{Venta.Columns.DNI}] = [{Empleado.Columns.DNI}] ORDER BY [{Venta.Columns.Id}] DESC"
                    );
        }

        public static class Widgets {
            public static Connection con = new Connection(Connection.Database.Pets);
            public static Response TotalDeVentasUltimoDia() {
                return con.Response.ErrorFound
                    ? con.Response
                    : con.FetchStoredProcedure(
                            storedProcedureName: Procedures.TotalVentasUltimoDia
                        );
            }
            public static Response TotalDeVentasUltimaSemana() {
                return con.Response.ErrorFound
                    ? con.Response
                    : con.FetchStoredProcedure(
                            storedProcedureName: Procedures.TotalVentasUltimaSemana
                        );
            }
            public static Response ProductoMasVendidoUltimaSemana() {
                return con.Response.ErrorFound
                    ? con.Response
                    : con.FetchStoredProcedure(
                            storedProcedureName: Procedures.ProductoMasVendidoUltimaSemana
                        );
            }
            public static Response CantidadDeProductosPorAgotarse() {
                return con.Response.ErrorFound
                    ? con.Response
                    : con.FetchStoredProcedure(
                            storedProcedureName: Procedures.CantidadDeProductosPorAgotarse
                        );
            }
            public static Response CantidadDeProductosAgotados() {
                return con.Response.ErrorFound
                    ? con.Response
                    : con.FetchStoredProcedure(
                            storedProcedureName: Procedures.CantidadDeProductosAgotados
                        );
            }
        }

    }
}
