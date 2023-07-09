using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Reportes {
    public partial class ProductoMasVendido : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                DateTime fechaActual = DateTime.Today;
                DateTime fechaInicio = fechaActual.AddDays(-7);
                txtFechaInicio.Text = fechaInicio.ToString("yyyy-MM-dd");
                txtFechaFin.Text = fechaActual.ToString("yyyy-MM-dd");
                Cargar(null, null);
            }
        }
        public void Cargar(object sender, EventArgs e) {
            string fechaInicio = txtFechaInicio.Text;
            string fechaFin = txtFechaFin.Text;
            var res = ProductoNegocio.Reporte_ProductosMasVendidos(fechaInicio, fechaFin);
            if(!res.ErrorFound) {
                gvDatos.DataSource = res.ObjectReturned as DataSet;
                gvDatos.DataBind();
            }
        }
    }
}