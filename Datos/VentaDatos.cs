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
        }

        public readonly static string ALL_COLUMNS = $"[{Venta.Columns.Id}], [{Venta.Columns.DNI}], [{Venta.Columns.TipoPago}], " +
                                          $"[{Venta.Columns.Fecha}], [{Venta.Columns.Total}]";

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
    }
}
