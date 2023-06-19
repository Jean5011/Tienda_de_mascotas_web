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
    class NegocioTipoDeProducto
    {
        
        public Response GetAnimales()
        {
            return DaoTiposDeProductos.ObtenerListaDeTipoProducto();
        }

        public Response ObtenerPorCod(String cod)
        {
            return DaoTiposDeProductos.BuscarTipoProductoPorCod(cod);
        }

        public Response IgresarAnimal(TipoProducto A)
        {
            return DaoTiposDeProductos.IgresarTipoProducto(A);
        }

        public Response ActualizarAnimal(TipoProducto A)
        {
            return DaoTiposDeProductos.ActualizarTipoProducto(A);
        }

        public Response EliminarAnimal(TipoProducto A)
        {
            return DaoTiposDeProductos.EliminarTipoProducto(A);
        }
    }
}
