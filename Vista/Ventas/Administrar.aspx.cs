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
            if(res.ErrorFound) {
                Utils.MostrarMensaje("Error cargando ventas. ", this.Page, GetType());

            } else {
                DataSet dt = res.ObjectReturned as DataSet;
                gvDatos.DataSource = dt;
                gvDatos.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;

                CargarDatos();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void gvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvDatos.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                TextBox txtPagerTextBox = e.Row.FindControl("gvDatosPagerPageTxtBox") as TextBox;
                if (txtPagerTextBox != null) {
                    txtPagerTextBox.Text = (gvDatos.PageIndex + 1) + "";
                }
                DropDownList ddlPager = e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") as DropDownList;
                if (ddlPager != null) {
                    ddlPager.SelectedValue = gvDatos.PageSize + "";
                }
            }
        }
        protected void gvProductsPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvDatos.PageCount - 1) {
                gvDatos.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvDatos.PageIndex + "";
            }
        }

        protected void ddlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvDatos.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void gvDatos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }




    }
}