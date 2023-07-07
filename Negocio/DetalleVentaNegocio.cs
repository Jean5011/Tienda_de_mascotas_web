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
    public class DetalleVentaNegocio
    {
       
        public static Response ObtenerDetalleVenta(int Cod)
        {
            return DaoDetalleVentas.ObtenerDetalleVenta(Cod);
        }

        public static Response AgregarDetalleVenta(DetalleVenta v)
        {
            return DaoDetalleVentas.AgregarRegistro(v);
        }

        public static Response EliminarDetalle(int cv, string cp) {
            return DaoDetalleVentas.EliminarDetalle(cv, cp);
        }

        /// @Deprecated
        public static Response DarDeBaja(int Cod) 
        {
            return DaoDetalleVentas.DarDeBajaRegistro(Cod);
        }

        public static Response aumentarCantidadVendida(DetalleVenta dv)
        {
            return DaoDetalleVentas.aumentarCantidadVendida(dv);
        }

        public static Response disminuirCantidadVendida(DetalleVenta dv)
        {
            return DaoDetalleVentas.disminuirCantidadVendida(dv);
        }

        public static DetalleVenta obtenerRegistro(DataSet dataSet, Producto prod, Venta obj)
        {
            DetalleVenta dv = new DetalleVenta()
            {
                Id = obj,
                Producto = prod,
                Proveedor = prod.Proveedor,
            };
            foreach (DataRow fila in dataSet.Tables["DetalleDeVenta"].Rows) // acá está el error.
            {
                dv.Cantidad = Convert.ToInt32(fila[DetalleVenta.Columns.Cantidad_Dv]);
                dv.PrecioUnitario = Convert.ToDouble(fila[DetalleVenta.Columns.PrecioUnitario_Dv]);
                dv.PrecioTotal = Convert.ToDouble(fila[DetalleVenta.Columns.PrecioTotal_Dv]);
                dv.Estado = true;
                /*
                DetalleVenta dv = new DetalleVenta()
                {
                    Id = fila[DetalleVenta.Columns.CodVenta_Dv] as Venta, 
                    Producto = fila[DetalleVenta.Columns.CodProducto_Dv] as Producto,
                    Proveedor = fila[DetalleVenta.Columns.CUITProv] as Proveedor,
                    Cantidad = Convert.ToInt32(fila[DetalleVenta.Columns.Cantidad_Dv]),
                    PrecioUnitario = Convert.ToDouble(fila[DetalleVenta.Columns.PrecioUnitario_Dv]),
                    PrecioTotal = Convert.ToDouble(fila[DetalleVenta.Columns.PrecioTotal_Dv]),
                    Estado = true
                }; */
                return dv;
            }
            return null;
        }
    }
}
