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
    public class DaoProductos {
        public DaoProductos() { }
        private static readonly string ALL_COLUMNS = $"[{Producto.Columns.Codigo_Prod}], " +
                                    $"[{Producto.Columns.CUITProv}], " +
                                   $"[{Producto.Columns.CodTipoProducto}], " +
                                   $"[{Producto.Columns.Nombre}], " +
                                   $"[{Producto.Columns.Marca}], " +
                                   $"[{Producto.Columns.Descripcion}], " +
                                   $"[{Producto.Columns.Stock}], " +
                                   $"[{Producto.Columns.Imagen}], " +
                                   $"[{Producto.Columns.Precio}], " +
                                   $"[{Producto.Columns.Estado}] ";


        public static class Procedures {
            public static string Crear = "SP_Productos_Crear";
            public static string ActualizarEstado = "SP_Productos_ActualizarEstado";
            public static string ActualizarPrecio = "SP_Productos_ActualizarPrecio";
            public static string ActualizarStock = "SP_Productos_ActualizarStock";
        }


        public static Response ObtenerListaDeProductos() {
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: $"SELECT {ALL_COLUMNS} FROM {Producto.Table}"
                    );
        }


        public static Response BuscarProductoPorCod(string ID) {
            string consulta = $"SELECT {ALL_COLUMNS} FROM {Producto.Table} WHERE [{Producto.Columns.Codigo_Prod}] = @ID ";
            Connection connection = new Connection(Connection.Database.Pets);
            return connection.Response.ErrorFound
                ? connection.Response
                : connection.FetchData(
                        query: consulta,
                        parameters: new Dictionary<string, object> {
                            { "@ID", ID }
                        }
                    );
        }

        public static Response IgresarProducto(Producto Pr) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.Crear,
                        parameters: new Dictionary<string, object> {
                            { "@CodProducto_Prod", Pr.Codigo },
                            { "@CUITProveedor_Prod", Pr.Proveedor },
                            { "@CodTipoProducto_Prod", Pr.Categoria },
                            { "@Nombre_Prod", Pr.Nombre },
                            { "@Marca_Prod", Pr.Marca },
                            { "@Descripcion_Prod", Pr.Descripcion },
                            { "@Stock_Prod", Pr.Stock },
                            { "@Imagen_Prod", Pr.Imagen },
                            { "@PrecioUnitario_Prod", Pr.Precio },
                            { "@Estado_Prod", Pr.Estado }
                        }
                    );
        }

        public static Response ActualizarEstadoProducto(Producto Pr) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.ActualizarEstado,
                        parameters: new Dictionary<string, object> {
                            { "@CodProducto_Prod", Pr.Codigo },
                            { "@Estado",Pr.Estado }
                        }
                    );
        }

        public static Response ActualizarPrecio(Producto Pr) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.ActualizarPrecio,
                        parameters: new Dictionary<string, object> {
                            { "@CodProducto_Prod", Pr.Codigo },
                            { "@PrecioUnitario_Prod", Pr.Precio }

                        }
                    );
        }

        public static Response ActualizarStock(Producto Pr) {
            Connection con = new Connection(Connection.Database.Pets);
            return con.Response.ErrorFound
                ? con.Response
                : con.ExecuteStoredProcedure(
                        storedProcedureName: Procedures.ActualizarPrecio,
                        parameters: new Dictionary<string, object> {
                            { "@CodProducto_Prod", Pr.Codigo },
                            { "@Stock_Prod", Pr.Stock }

                        }
                    );
        }
    }

}
