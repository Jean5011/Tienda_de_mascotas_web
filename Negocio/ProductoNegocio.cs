using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace Negocio
{
    public class ProductoNegocio
    {
        public Response ObtenerProductos()
        {
            return DaoProductos.ObtenerListaDeProductos();
        }

        public Response ObtenerPorCod(String cod)
        {
            return DaoProductos.BuscarProductoPorCod(cod);
        }

        public static Response IngresarProducto(Producto P)
        {
            return DaoProductos.IngresarProducto(P);
        }
        public  Response ActualizarEstado(Producto P)
        {
            return DaoProductos.ActualizarEstadoProducto(P);
        }

        public static Response ActualizarProducto(Producto P)
        {
            return DaoProductos.ActualizarProducto(P);
        }

        public Response ActualizarPrecio(Producto P)
        {
            return DaoProductos.ActualizarPrecio(P);
        }

        public Response ActualizarStock(Producto P)
        {
            return DaoProductos.ActualizarStock(P);
        }
    }
}
