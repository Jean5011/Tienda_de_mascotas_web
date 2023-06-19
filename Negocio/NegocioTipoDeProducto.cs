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
    public class NegocioTipoDeProducto
    {
        
        public Response GetTipoDeProducto()
        {
            return DaoTiposDeProductos.ObtenerListaDeTipoProducto();
        }

        public Response ObtenerPorCod(String cod)
        {
            return DaoTiposDeProductos.BuscarTipoProductoPorCod(cod);
        }

        public Response IgresarTipoDeProducto(TipoProducto A)
        {
            return DaoTiposDeProductos.IgresarTipoProducto(A);
        }

        public Response ActualizarTipoDeProducto(TipoProducto A)
        {
            return DaoTiposDeProductos.ActualizarTipoProducto(A);
        }

        public Response EliminarTipoDeProducto(TipoProducto A)
        {
            return DaoTiposDeProductos.EliminarTipoProducto(A);
        }
    }
}
