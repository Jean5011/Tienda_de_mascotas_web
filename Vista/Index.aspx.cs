using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Data;

namespace Vista {
    public partial class Index : System.Web.UI.Page {

        public void Widget__TotalVentasUltimoDia() {
            Response res = VentaNegocio.Widgets.TotalDeVentasUltimoDia();
            if (!res.ErrorFound) {
                DataSet dt = res.ObjectReturned as DataSet;
                if (dt.Tables[0].Rows.Count > 0) {
                    // Acceder a la primera fila del DataSet
                    DataRow primeraFila = dt.Tables[0].Rows[0];

                    // Ahora puedes acceder a los valores de las columnas de la primera fila
                    string total = primeraFila["Total"].ToString();
                    lblTotalVendidoUltimoDia.Text = $"{total}";

                }
            }
        }
        public void Widget__TotalVentasUltimaSemana() {
            Response res = VentaNegocio.Widgets.TotalDeVentasUltimaSemana();
            if (!res.ErrorFound) {
                DataSet dt = res.ObjectReturned as DataSet;
                if (dt.Tables[0].Rows.Count > 0) {
                    // Acceder a la primera fila del DataSet
                    DataRow primeraFila = dt.Tables[0].Rows[0];

                    // Ahora puedes acceder a los valores de las columnas de la primera fila
                    string total = primeraFila["Total"].ToString();
                    lblTotalVendidoUltimaSemana.Text = $"{total}";

                }
            }
        }
        public void Widget__ProductoMasVendidoUltimaSemana() {
            Response res = VentaNegocio.Widgets.ProductoMasVendidoUltimaSemana(out int cantidad);
            if (!res.ErrorFound) {
                Producto p = res.ObjectReturned as Producto;
                lblProductoMasVendidoUltimaSemana.Text = $"{p.Nombre}";
            }
        }
        public void Widget__CantidadDeProductosPorAgotarse() {
            Response res = VentaNegocio.Widgets.CantidadDeProductosPorAgotarse(out int cantidad);
            if (!res.ErrorFound) {
                Producto p = res.ObjectReturned as Producto;
                lblProductosPorAgotarse.Text = cantidad.ToString();
            }
        }
        public void Widget__CantidadDeProductosAgotados() {
            Response res = VentaNegocio.Widgets.CantidadDeProductosAgotados(out int cantidad);
            if (!res.ErrorFound) {
                Producto p = res.ObjectReturned as Producto;
                lblProductosAgotados.Text = cantidad.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                var auth = new Utils.Authorization() {
                    AccessType = Utils.Authorization.AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
                    RejectNonMatches = true
                };
                var op_data = auth.ValidateSession(this);
                Widget__TotalVentasUltimaSemana();
                Widget__TotalVentasUltimoDia();
                Widget__ProductoMasVendidoUltimaSemana();
                Widget__CantidadDeProductosPorAgotarse();
                Widget__CantidadDeProductosAgotados();

            }

        }
    }
}