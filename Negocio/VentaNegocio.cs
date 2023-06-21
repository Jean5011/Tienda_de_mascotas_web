using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio {
    public class VentaNegocio {
        public static Response ExtractPreliminarFromDataSet(DataSet resultDataSet) {
            if (resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0) {
                DataRow primerRegistro = resultDataSet.Tables[0].Rows[0];
                Venta.Preliminar obj = new Venta.Preliminar() {
                    Id = Convert.ToInt32(primerRegistro["ID"].ToString()),
                    AffectedRows = Convert.ToInt32(primerRegistro["AFFECTEDROWS"].ToString())
                };
                return new Response() {
                    ErrorFound = false,
                    ObjectReturned = obj
                };
            }
            else {
                return new Response() {
                    ErrorFound = true,
                    Message = SesionNegocio.ErrorCode.NO_ROWS,
                    ObjectReturned = null
                };
            }

        }

        public static Response IniciarVenta(Venta obj) {
            var res = VentaDatos.IniciarVenta(obj);
            if (!res.ErrorFound) {
                var n = ExtractPreliminarFromDataSet(res.ObjectReturned as DataSet);
                return n;
            }
            else return res;
        }

    }
}
