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
    public class DaoDetalleVentas
    {

        public DaoDetalleVentas() { }
        private static readonly string ALL_COLUMNS = $"[{DetalleVenta.Columns.CodVenta_Dv}], " +
                                $"[{DetalleVenta.Columns.CodProducto_Dv}], " +
                               $"[{DetalleVenta.Columns.CUITProv}], " +
                               $"[{DetalleVenta.Columns.Cantidad_Dv}], " +
                               $"[{DetalleVenta.Columns.PrecioUnitario_Dv}], " +
                               $"[{DetalleVenta.Columns.PrecioTotal_Dv}], " +
                               $"[{DetalleVenta.Columns.Estado_Dv}], ";


        public static class Procedures
        {
            // public static string Crear = "";
            //public static string Eliminar = "";
        }


    }
}
