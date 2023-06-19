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
    public class DaoAnimales
    {
        public DaoAnimales() { }
        private static readonly string ALL_COLUMNS = $"[{Animal.Columns.Codigo}], " +
                                    $"[{Animal.Columns.Nombre}], " +
                                    $"[{Animal.Columns.Raza}], ";

        public static class Procedures
        {
            public static string Igresar = "SP_IngresarAnimal";
            public static string Eliminar = "SP_EliminarAnimal";
            public static string Actializar = "SP_ActualizarAnimales";
        }


        public static Response ObtenerListaDeAnimales()
        {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Animal.Table}"
                    );
        }
        public static Response BuscarAnimalPorCod(string ID)
        {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Animal.Table} WHERE [{Animal.Columns.Codigo}] = @ID ";
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

        public static Response IgresarAnimal(Animal An)
        {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Igresar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodAnimales_An", An.Codigo },
                            { "@nombre_An", An.Nombre },
                            { "@NombreDeRaza_An", An.Raza }
                        }
                    );
        }

        public static Response EliminarAnimal(Animal An)
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

        public static Response ActualizarAnimal(Animal An)
        {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Actializar,
                        parameters: new Dictionary<string, object> {
                            { "@PK_CodAnimales_An", An.Codigo },
                            { "@nombre_An", An.Nombre },
                            { "@NombreDeRaza_An", An.Raza }
                        }
                    );
        }
    }
}
