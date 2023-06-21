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
    }
}
