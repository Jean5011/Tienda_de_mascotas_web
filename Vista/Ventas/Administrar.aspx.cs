using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Ventas {
    public partial class Administrar : System.Web.UI.Page {

        public void CargarDatos() {
            string tbuscar = txtBuscar.Text;
            var res = tbuscar == "" ? VentaNegocio.GetVentas() : VentaNegocio.GetVentaPorID(Convert.ToInt32(tbuscar));
            if (res.ErrorFound) {
                Utils.MostrarMensaje("Error cargando ventas. ", this.Page, GetType());

            }
            else {
                DataSet dt = res.ObjectReturned as DataSet;
                gvDatos.DataSource = dt;
                gvDatos.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                CargarDatos();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void GvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            int newPageIndex = e.NewPageIndex;
            if (newPageIndex >= 0 && newPageIndex < gvDatos.PageCount) {
                gvDatos.PageIndex = newPageIndex;
                CargarDatos();
            }
        }

        protected void GvDatos_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("gvDatosPagerPageTxtBox") is TextBox txtPagerTextBox) {
                    txtPagerTextBox.Text = (gvDatos.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager) {
                    ddlPager.SelectedValue = gvDatos.PageSize + "";
                }
            }
        }
        protected void GvDatosPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvDatos.PageCount - 1) {
                gvDatos.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvDatos.PageIndex + "";
            }
        }

        protected void DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvDatos.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void GvDatos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }




    }
}