﻿using System;
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
        public static class Procedures
        {
            public static string Crear = "SP_Proveedor_Crear";
            public static string ActualizarProducto = "SP_Proveedores_Actualizar";
            public static string EliminarProducto = "SP_Proveedores_ActualizarEstado";
        }
        public static Response ObtenerListaDeProveedores()
        {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} where {Proveedor.Columns.Estado}=1 "
                    );
        }
        public static Response ObtenerProveedorByCUIT(string CUIT)
        {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] =@cuit and {Proveedor.Columns.Estado}=1 ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@cuit", CUIT }
                        }
                    );
        }
        public static Response InsertarProveedor(Proveedor proveedor)
        {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Crear,
                        parameters: new Dictionary<string, object>
                        {
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
        public static Response EliminadoLogicoProveedor(Proveedor prov)
        {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.EliminarProducto,
                        parameters: new Dictionary<string, object> {
                            { "@CUIT", prov.CUIT },
                            { "@Estado", prov.Estado },
                        }
                    );
        }
        public static Response ActualizarProveedor(Proveedor proveedor)
        {
            Connection con = new Connection(Connection.Database.Pets);
            return con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.ActualizarProducto,
                        parameters: new Dictionary<string, object>
                        {
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



        public static Response VerificarExiste(string CUIT)
        {
            string consulta = $"SELECT COUNT ({Proveedor.Columns.CUIT}) AS [CUIT] FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] = @CUIT ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@CUIT", CUIT }
                        }
                    );
        }


        public static Response ObtenerProveedorCUITEditar(string CUIT)
        {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Proveedor.Table} WHERE [{Proveedor.Columns.CUIT}] =@cuit";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@cuit", CUIT }
                        }
                    );
        }
    }
}
