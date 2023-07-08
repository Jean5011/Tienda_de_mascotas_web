using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using System.Data;

namespace Vista.Tipos {
    public partial class Administrar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;
                CargarDatos();
                if (Request.QueryString["ID"] != null)
                {
                    string id = Request.QueryString["ID"];
                    BT_Filtrar_Click( id);

                }
            }
        }

        protected void CargarDatos() {
            if (txtBuscar.Text == "") BT_Todo_Click();
            else BT_Filtrar_Click();
        }
        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void BT_Filtrar_Click(string id = null) {
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            string tipo = txtBuscar.Text;
            if (id != null) tipo = id;
            GvDatos.DataSource = nt.ObtenerPorCod(tipo);
            GvDatos.DataBind();
        }

        protected void BT_Todo_Click() {
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            GvDatos.DataSource = nt.GetTipoDeProducto();
            GvDatos.DataBind();
        }

        protected void Habilitar(string codigo) {
            var auth = Session[Utils.AUTH] as SessionData;
            var res = NegocioTipoDeProducto.Habilitar(auth, new TipoProducto { Codigo = codigo });
            Utils.ShowSnackbar(res.Message, this);
        }
        protected void Deshabilitar(string codigo) {
            var auth = Session[Utils.AUTH] as SessionData;
            var res = NegocioTipoDeProducto.Deshabilitar(auth, new TipoProducto { Codigo = codigo });
            Utils.ShowSnackbar(res.Message, this);

        }

        protected void H_command(object sender, CommandEventArgs e) {
            string codigo = e.CommandArgument.ToString();
            switch(e.CommandName) {
                case "Habilitar":
                    Habilitar(codigo);
                    break;
                case "Deshabilitar":
                    Deshabilitar(codigo);
                    break;
                default:
                    Utils.ShowSnackbar("El nombre del comando especificado no es válido. ", this, GetType());
                    break;
            }
            CargarDatos();
        }

        protected void GvDatos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e) {
            string cod = ((Label)GvDatos.Rows[e.RowIndex].FindControl("LV_CodTipoDeProducto")).Text;
            Deshabilitar(cod);
        }

        protected void GvDatos_RowEditing(object sender, GridViewEditEventArgs e) {
            GvDatos.EditIndex = e.NewEditIndex;
            CargarDatos();
        }

        protected void GvDatos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GvDatos.EditIndex = -1;
            CargarDatos();
        }

        protected void GvDatos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            TipoProducto tp = new TipoProducto {
                Codigo = ((Label)GvDatos.Rows[e.RowIndex].FindControl("LV_EditCod")).Text,
                CodAnimal = ((DropDownList)GvDatos.Rows[e.RowIndex].FindControl("DD_EditAnimal")).SelectedValue,
                tipoDeProducto = ((DropDownList)GvDatos.Rows[e.RowIndex].FindControl("DD_EditTdp")).SelectedValue,
                Descripcion = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("TB_EditDesc")).Text
            };
            var auth = Session[Utils.AUTH] as SessionData;
            var res = NegocioTipoDeProducto.ActualizarTipoDeProducto(auth, tp);
            Utils.ShowSnackbar(res.Message, this);
            GvDatos.EditIndex = -1;
            CargarDatos();
        }

        protected void GvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            GvDatos.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void GvDatos_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("GvDatosPagerPageTxtBox") is TextBox txtPagerTextBox) {
                    txtPagerTextBox.Text = (GvDatos.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager) {
                    ddlPager.SelectedValue = GvDatos.PageSize + "";
                }
            }
        }
        protected void GvDatosPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= GvDatos.PageCount - 1) {
                GvDatos.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = GvDatos.PageIndex + "";
            }
        }

        protected void DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                GvDatos.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void GvDatos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }
    }
}