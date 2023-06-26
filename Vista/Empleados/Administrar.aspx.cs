using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Empleados {
    public partial class Administrar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                CargarDatos();
            }
        }

        public void CargarDatos() {
            bool soloActivos = !chkEstado.Checked;
            string searchquery = txtBuscar.Text;
            bool hayParaBuscar = !string.IsNullOrEmpty(searchquery);
            Response data = hayParaBuscar 
                            ? EmpleadoNegocio.FiltrarEmpleadosPorNombreCompleto(searchquery, soloActivos)
                            : EmpleadoNegocio.ObtenerEmpleados(soloActivos);
            if (!data.ErrorFound) {
                var dt = data.ObjectReturned as DataSet;
                var auth = (Session[Utils.AUTH] as SessionData);
                if (auth.User != null) {
                    var UsuarioActual = auth.User;
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                        gvAdmin.DataSource = dt;
                        gvAdmin.DataBind();
                        gvEmpleado.Visible = false;
                        gvEmpleado.Enabled = false;
                    }
                    else {
                        gvEmpleado.DataSource = dt;
                        gvEmpleado.DataBind();
                        gvAdmin.Visible = false;
                        gvAdmin.Enabled = false;

                    }
                }
            }
            else {
                Utils.MostrarMensaje($"Error. {data.Details} . {data.Message} .", this.Page, GetType());
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void lbActualUser_Click(object sender, EventArgs e) {

        }

        protected void lbIniciarSesion_Click(object sender, EventArgs e) {

        }










        protected void gvAdmin_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvAdmin.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void gvAdmin_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                TextBox txtPagerTextBox = e.Row.FindControl("gvAdminPagerPageTxtBox") as TextBox;
                if (txtPagerTextBox != null) {
                    txtPagerTextBox.Text = (gvAdmin.PageIndex + 1) + "";
                }
                DropDownList ddlPager = e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") as DropDownList;
                if (ddlPager != null) {
                    ddlPager.SelectedValue = gvAdmin.PageSize + "";
                }
            }
        }
        protected void gvAdminPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvAdmin.PageCount - 1) {
                gvAdmin.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvAdmin.PageIndex + "";
            }
        }

        protected void ddlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvAdmin.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void gvAdmin_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }











        protected void gvEmpleado_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvEmpleado.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void gvEmpleado_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                TextBox txtPagerTextBox = e.Row.FindControl("gvEmpleadoPagerPageTxtBox") as TextBox;
                if (txtPagerTextBox != null) {
                    txtPagerTextBox.Text = (gvEmpleado.PageIndex + 1) + "";
                }
                DropDownList ddlPager = e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") as DropDownList;
                if (ddlPager != null) {
                    ddlPager.SelectedValue = gvEmpleado.PageSize + "";
                }
            }
        }
        protected void gvEmpleadoPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvEmpleado.PageCount - 1) {
                gvEmpleado.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvEmpleado.PageIndex + "";
            }
        }

        protected void gvEmpleadoddlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvEmpleado.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void gvEmpleado_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }



    }
}