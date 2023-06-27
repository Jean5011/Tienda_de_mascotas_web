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

namespace Negocio {
    public class NegocioAnimales {
        public Response GetAnimales()
        {
            return DaoAnimales.ObtenerListaDeAnimales();
        }

        public Response ObtenerPorCod(String cod)
        {
            return DaoAnimales.BuscarAnimalPorCod(cod);
        }

        public static Response IngresarAnimal(Animal A) {
            return DaoAnimales.IgresarAnimal(A);
        }

        public Response ActualizarAnimal(Animal A) {
            return DaoAnimales.ActualizarAnimal(A);
        }

        public Response EliminarAnimal(Animal A)
        {
            return DaoAnimales.EliminarAnimal(A);
        }

        public Response GettAnimales() {
            return DaoAnimales.ObtenerLista();
        }

        /***************************************************************************************************************************/
        public Response GetAnimalesBaja()
        {
            return DaoAnimales.ObtenerListaBaja();
        }

        public Response ObtenerPorCodBaja(Animal cod)
        {
            return DaoAnimales.BuscarAnimalPorCodBaja(cod.Codigo);
        }

        public Response AltaAnimal(Animal A)
        {
            return DaoAnimales.AltaAnimal(A);
        }
    }
}