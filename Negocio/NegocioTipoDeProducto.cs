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
            return DaoAnimales.ObtenerListaDeAnimales();
        }

        public Response ObtenerPorCod(String cod)
        {
            return DaoAnimales.BuscarAnimalPorCod(cod);
        }

        public Response IgresarAnimal(Animal A)
        {
            return DaoAnimales.IgresarAnimal(A);
        }

        public Response ActualizarAnimal(Animal A)
        {
            return DaoAnimales.ActualizarAnimal(A);
        }

        public Response EliminarAnimal(Animal A)
        {
            return DaoAnimales.EliminarAnimal(A);
        }
    }
}
