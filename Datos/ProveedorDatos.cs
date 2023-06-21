using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;


namespace Datos
{
    public class ProveedorDatos
    {
        private static string ALL_COLUMNS = $"[{Proveedor.Columns.CUIT}]," +
            $"[{Proveedor.Columns.RazonSocial}]," +
            $"[{Proveedor.Columns.NombreContacto}]," +
            $"[{Proveedor.Columns.CorreoElectronico}]," +
            $"[{Proveedor.Columns.Telefono}]," +
            $"[{Proveedor.Columns.Direccion}]," +
            $"[{Proveedor.Columns.Provincia}]," +
            $"[{Proveedor.Columns.Localidad}]," +
            $"[{Proveedor.Columns.Pais}]," +
            $"[{Proveedor.Columns.CodigoPostal}]";

     public static Response ObtenerListaDeProveedores()
        {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} "
                    );
        }
        public static Response ObtenerProveedorByCUIT(string CUIT)
        {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] = @cuit";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@cuit", CUIT }
                        }
                    );
        }
        public static Response InsertarProveedor(Proveedor proveedor)
        {
            string consulta = $"INSERT INTO {ALL_COLUMNS}"+
                              $"VALUES (@CUIT, @RazonSocial, @NombreContacto,@CorreoElectronico,@Telefono,@Direccion,@Provincia,@Localidad,@Pais,@CodigoPostal)";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                    { "@CUIT", proveedor.CUIT },
                    { "@RazonSocial", proveedor.RazonSocial },
                    { "@NombreContacto", proveedor.NombreContacto },
                    { "@CorreoElectronico", proveedor.CorreoElectronico },
                    { "@Telefono", proveedor.Telefono },
                    { "@Direccion", proveedor.Direccion },
                    { "@Provincia", proveedor.Provincia },
                    { "@Localidad", proveedor.Localidad },
                    { "@Pais", proveedor.Pais },
                    { "@CodigoPostal", proveedor.CodigoPostal }
                        }
                    );
        }
        /*borrar registro
        public static Response EliminaProveedor(string CUIT)
        {
            string consulta = $"DELETE FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] = @cuit";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@cuit", CUIT }
                        }
                    );
        }*/

        //modificar estado
        public static Response EliminadoLogicoProveedor(string CUIT)
        {
            string consulta = $"UPDATE {Proveedor.Table} set Estado_Prov = 0  WHERE [{Proveedor.Columns.CUIT}] = @cuit";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@cuit", CUIT }
                        }
                    );
        }
        public static Response ActualizarProveedor(Proveedor proveedor)
        {
            string consulta = $"UPDATE {Proveedor.Table}" +
            "SET RazonSocial_Prov = @RazonSocial," +
            "NombreDeContacto_Prov = @NombreContacto," +
            "CorreoElectronico_Prov = @CorreoElectronico," +
            "Telefono_Prov = @Telefono," +
            "Direccion_Prov = @Direccion," +
            "Provincia_Prov = @Provincia," +
            "Localidad_Prov = @Localidad," +
            "Pais_Prov = @Pais," +
            "CodigoPostal_Prov = @CodigoPostal " +
            "WHERE CUIT_Prov = @CUIT; ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                    { "@RazonSocial", proveedor.RazonSocial },
                    { "@NombreContacto", proveedor.NombreContacto },
                    { "@CorreoElectronico", proveedor.CorreoElectronico },
                    { "@Telefono", proveedor.Telefono },
                    { "@Direccion", proveedor.Direccion },
                    { "@Provincia", proveedor.Provincia },
                    { "@Localidad", proveedor.Localidad },
                    { "@Pais", proveedor.Pais },
                    { "@CodigoPostal", proveedor.CodigoPostal }
                        }
                    );
        }
       
    }
}
