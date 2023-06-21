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
       
        public Response ObtenerDetalleVenta(int Cod)
        {
            return DaoDetalleVentas.ObtenerDetalleVenta(Cod);
        }

        public Response AgregarDetalleVenta(DetalleVenta v)
        {
            return DaoDetalleVentas.AgregarRegistro(v);
        }

        public static Response DarDeBaja (int Cod)
        {
            return DaoDetalleVentas.DarDeBajaRegistro(Cod);
        }
    }
}
