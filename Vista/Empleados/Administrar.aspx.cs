using System;
using System.Data;
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
      
        protected void BtnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void LbActualUser_Click(object sender, EventArgs e) {

        }

        protected void LbIniciarSesion_Click(object sender, EventArgs e) {

        }

        protected void GvAdmin_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            int newPageIndex = e.NewPageIndex;
            if (newPageIndex >= 0 && newPageIndex < gvAdmin.PageCount) {
                gvAdmin.PageIndex = newPageIndex;
                CargarDatos();
            }
        }

        protected void GvAdmin_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("gvAdminPagerPageTxtBox") is TextBox txtPagerTextBox) {
                    txtPagerTextBox.Text = (gvAdmin.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager) {
                    ddlPager.SelectedValue = gvAdmin.PageSize + "";
                }
            }
        }
        protected void GvAdminPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvAdmin.PageCount - 1) {
                gvAdmin.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvAdmin.PageIndex + "";
            }
        }

        protected void DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvAdmin.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void GvAdmin_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }











        protected void GvEmpleado_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvEmpleado.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void GvEmpleado_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("gvEmpleadoPagerPageTxtBox") is TextBox txtPagerTextBox) {
                    txtPagerTextBox.Text = (gvEmpleado.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager) {
                    ddlPager.SelectedValue = gvEmpleado.PageSize + "";
                }
            }
        }
        protected void GvEmpleadoPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvEmpleado.PageCount - 1) {
                gvEmpleado.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvEmpleado.PageIndex + "";
            }
        }

        protected void GvEmpleadoddlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvEmpleado.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void GvEmpleado_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }


        public void CargarDatos() {

            var filtros = new Empleado.Busqueda {
                Texto = txtBuscar.Text,
                MostrarInactivos = ddlEstado.SelectedValue == "T",
                Rol = ddlRol.SelectedValue,
                Sexo = ddlSexo.SelectedValue
            };

            Response data = EmpleadoNegocio.CargarDatos(filtros);
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

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e) {
            CargarDatos();
        }
    }
}