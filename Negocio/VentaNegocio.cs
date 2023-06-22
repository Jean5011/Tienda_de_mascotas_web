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
        public static Response ExtractDataFromDataSet(DataSet resultDataSet) {
            if (resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0) {
                DataRow primerRegistro = resultDataSet.Tables[0].Rows[0];
                Venta obj = new Venta() {
                    Id = Convert.ToInt32(primerRegistro[Venta.Columns.Id].ToString()),
                    EmpleadoGestor = new Empleado() { DNI = primerRegistro[Venta.Columns.DNI].ToString()},
                    TipoPago = primerRegistro[Venta.Columns.TipoPago].ToString(),
                    Fecha = primerRegistro[Venta.Columns.Fecha].ToString(),
                    Total = Convert.ToDouble(primerRegistro[Venta.Columns.Total].ToString())
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

        public static Response BuscarVentaPorID(int id) {
            var res = VentaDatos.GetVentaByID(id);
            if (!res.ErrorFound) {
                var res2 = ExtractDataFromDataSet(res.ObjectReturned as DataSet);
                return res2;
            }
            else return res;
        }

        public static Response GetVentas() {
            var res = VentaDatos.GetVentas();
            return res;
        }
        public static Response GetVentaPorID(int id) {
            return VentaDatos.GetVentaByID(id);
        }

        public static Response IniciarVenta(Venta obj) {
            var res = VentaDatos.IniciarVenta(obj);
            if (!res.ErrorFound) {
                var n = ExtractPreliminarFromDataSet(res.ObjectReturned as DataSet);
                return n;
            }
            else return res;
        }

        public static Response VentasPorEmp(string dni) {
            return VentaDatos.GetVentaByDNI(dni);
        }
    }
}
