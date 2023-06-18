using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos {
    public class SesionDatos {
        public SesionDatos() { }
        public static string ALL_COLUMNS = $"[{Sesion.Columns.Codigo}], [{Sesion.Columns.DNI}], " +
                                           $"[{Sesion.Columns.FechaAlta}], [{Sesion.Columns.Token}], " +
                                           $"[{Sesion.Columns.Estado}]";
        /// <summary>
        /// Inserta un registro en la tabla Sesiones
        /// </summary>
        /// <param name="obj">El objeto Sesion con los datos a agregar.</param>
        /// <returns>Response con el resultado de la operación</returns>
        public static Response AbrirSesion(Sesion obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.RunTransaction(
                        query: $"INSERT INTO [{Sesion.Table}] ([{Sesion.Columns.DNI}], [{Sesion.Columns.Token}]) SELECT @dni, @token",
                        parameters: new Dictionary<string, object> {
                            { "@dni", obj.Empleado.DNI },
                            { "@token", obj.Token }
                        }
                    );
        }

        /// <summary>
        /// Rescata los datos de una sesión a partir de un token y DNI
        /// </summary>
        /// <param name="token">El token en cuestión.</param>
        /// <param name="dni">El DNI en cuestión.</param>
        /// <returns>Response con el resultado de la operación.</returns>
        public static Response ObtenerSesion(string token, string dni) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.FetchData(
                        query: $"SELECT {ALL_COLUMNS} WHERE [{Sesion.Columns.Token}] = @token AND [{Sesion.Columns.DNI}] = @dni ORDER BY [{Sesion.Columns.FechaAlta}] DESC",
                        parameters: new Dictionary<string, object> {
                            { "@token", token },
                            { "@dni", dni }
                        }
                    );
        }

        /// <summary>
        /// Deshabilita un registro de la tabla Sesiones.
        /// </summary>
        /// <param name="obj">Objeto Sesion con los datos.</param>
        /// <returns>Response con el resultado de la operación.</returns>
        public static Response CerrarSesion(Sesion obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.RunTransaction(
                        query: $"UPDATE [{Sesion.Table}] SET [{Sesion.Columns.Estado}] = '0' WHERE [{Sesion.Columns.Codigo}] = @codigo",
                        parameters: new Dictionary<string, object> {
                            { "@codigo", obj.Codigo }
                        }
                    );
        }

        /// <summary>
        /// Deshabilita todos los registros/tokens de un usuario en particular.
        /// </summary>
        /// <param name="obj">Objeto Sesion con el DNI del empleado especificado.</param>
        /// <returns>Response con el resultado de la operación.</returns>
        public static Response CerrarTodasLasSesiones(Sesion obj) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.RunTransaction(
                        query: $"UPDATE [{Sesion.Table}] SET [{Sesion.Columns.Estado}] = '0' WHERE [{Sesion.Columns.DNI}] = @dni",
                        parameters: new Dictionary<string, object> {
                            { "@codigo", obj.Empleado.DNI }
                        }
                    );
        }
     }
}
