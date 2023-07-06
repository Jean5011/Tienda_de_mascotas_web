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
        
        public DataSet GetTipoDeProducto()
        {
            Response resultado = DaoTiposDeProductos.ObtenerListaDeTipoProducto();
            DataSet dt = resultado.ObjectReturned as DataSet;// Cómo manejás los errores?
            return dt;
        }

        public DataSet ObtenerPorCod(String cod)
        {
            Response resultado = DaoTiposDeProductos.BuscarTipoProductoPorCod(cod);
            DataSet dt = resultado.ObjectReturned as DataSet; // Cómo manejás los errores?
            return dt;
        }

        public bool IgresarTipoDeProducto(string cod, string tipoDeProducto, string CodAnimal, string Descripcion)
        {
            TipoProducto A = new TipoProducto();
            A.Codigo = cod;
            A.tipoDeProducto = tipoDeProducto;
            A.CodAnimal = CodAnimal;
            A.Descripcion = Descripcion;
            Response RES = DaoTiposDeProductos.IgresarTipoProducto(A);
            if (!RES.ErrorFound)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Response ActualizarTipoDeProducto(string Codigo, string CodAnimal, string tipoDeProducto, string Descripcion)
        {
            // FIXME: Reducir cantidad de parámetros. Considerar enviar un objeto TipoProducto.
            // FIXME: Recibir un objeto SessionData y realizar la autenticación acá.
            // FIXME: NO RETORNAR DIRECTAMENTE MÉTODOS DE DATOS/DAO.
            TipoProducto Tp = new TipoProducto();
            Tp.Codigo = Codigo;
            Tp.CodAnimal = CodAnimal;
            Tp.tipoDeProducto = tipoDeProducto;
            Tp.Descripcion = Descripcion;
            return DaoTiposDeProductos.ActualizarTipoProducto(Tp); // Evitar esto a toda costa.
        }

        public Response EliminarTipoDeProducto(string A)
        {
            TipoProducto t = new TipoProducto();
            t.CodAnimal = A;
            return DaoTiposDeProductos.EliminarTipoProducto(t);
        }
        public Response GetTipoDeProductoBaja()
        {
            return DaoTiposDeProductos.ObtenerListaDeTipoProductoBajas();
        }

        public Response ObtenerPorCodBaja(String cod)
        {
            return DaoTiposDeProductos.BuscarTipoProductoPorCodBajas(cod);
        }

        public Response AltaTipoDeProducto(string codigo)
        {
            TipoProducto t = new TipoProducto();
            t.Codigo = codigo;
            return DaoTiposDeProductos.AltaTipoProducto(t);
        }

        public static Response ObtenerIDS()
        {
            return DaoTiposDeProductos.ObtenerListaDeIDS();
        }
    }
}
