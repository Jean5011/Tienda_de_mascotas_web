using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class DaoTiposDeProductos
    {
        public DaoTiposDeProductos() { }
        private static readonly string ALL_COLUMNS = $"[{TipoProducto.Columns.Codigo}], " +
                                    $"[{TipoProducto.Columns.CodAnimal}], " +
                                   $"[{TipoProducto.Columns.TipoDeProducto}], " +
                                   $"[{TipoProducto.Columns.Descripcion}], ";

        public static class Procedures
        {
            public static string Igresar = "SP_IngresarTipoDeProductos";
            public static string Eliminar = "SP_EliminarTipoDeProductos";
            public static string Actualizar = "SP_ActualizarTipoProducto";
        }


        public static Response ObtenerListaDeTipoProducto()
        {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {TipoProducto.Table}"
                    );
        }
        public static Response BuscarTipoProductoPorCod(string ID)
        {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {TipoProducto.Table} WHERE [{TipoProducto.Columns.Codigo}] = @ID ";
            Connection connection = new Connection(Connection.Database.Pets);
            Trace.Write("BuscarAnimalPorCod", $"Consulta: {consulta}");
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }
                        }
                    );
        }

        public static Response IgresarTipoProducto(TipoProducto An)
        {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Igresar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodTipoProducto_TP", An.Codigo },
                            { "@CodAnimales_Tp", An.CodAnimal },
                            { "@TipoDeProducto_Tp", An.tipoDeProducto },
                            { "@Descripcion_TP", An.Descripcion }
                        }
                    );
        }

        public static Response EliminarTipoProducto(TipoProducto An)
        {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Eliminar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodAnimales_An", An.Codigo }
                        }
                    );
        }

        public static Response ActualizarTipoProducto(TipoProducto An)
        {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Actualizar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodAnimales_An", An.Codigo },
                            { "@CodAnimales_Tp", An.CodAnimal },
                            { "@TipoDeProducto_Tp", An.tipoDeProducto },
                            { "@Descripcion_TP", An.Descripcion }
                        }
                    );
        }
    }
}
