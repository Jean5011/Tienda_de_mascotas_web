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

        public static Response DarDeBaja (int Cod)
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
    }
}
