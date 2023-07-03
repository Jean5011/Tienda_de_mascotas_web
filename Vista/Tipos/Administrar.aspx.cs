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
            }
        }

        protected void CargarDatos() {
            if (txtBuscar.Text == "") BT_Todo_Click();
            else BT_Filtrar_Click();
        }
        protected bool EsAdmin() {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            return UsuarioActual.Rol == Empleado.Roles.ADMIN;
        }
        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void BT_Filtrar_Click() {
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            Response resultado = nt.ObtenerPorCod(txtBuscar.Text);
            DataSet dt = resultado.ObjectReturned as DataSet;
            GvDatos.DataSource = dt;
            GvDatos.DataBind();
        }

        protected void BT_Todo_Click() {
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            Response resultado = nt.GetTipoDeProducto();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GvDatos.DataSource = dt;
            GvDatos.DataBind();
        }

        protected void Habilitar(string codigo) {
            if(EsAdmin()) {
                SesionNegocio.Autenticar(success => {
                    /* Habilitar */
                    TipoProducto t = new TipoProducto();
                    t.Codigo = codigo;
                    NegocioTipoDeProducto NT = new NegocioTipoDeProducto();
                    Response resultado = NT.AltaTipoDeProducto(t);
                    Utils.ShowSnackbar(
                            message: !resultado.ErrorFound 
                                ? "Se habilitó con éxito el registro. "
                                : "Hubo un problema al intentar habilitar el registro. ",
                            control: this, 
                            type: GetType()
                        );
                }, err => {
                    Utils.ShowSnackbar("El token caducó. Volvé a iniciar sesión. ", this, GetType());
                });
            } else {
                Utils.ShowSnackbar("Carecés de privilegios suficientes para realizar esta acción. ", this, GetType());
            }
        }
        protected void Deshabilitar(string codigo) {
            if (EsAdmin()) {
                SesionNegocio.Autenticar(success => {
                    /* Deshabilitar */
                    TipoProducto t = new TipoProducto();
                    t.Codigo = codigo;
                    NegocioTipoDeProducto NT = new NegocioTipoDeProducto();
                    Response resultado = NT.AltaTipoDeProducto(t); // FIXME: Cambiar por función de BAJA.
                    Utils.ShowSnackbar(
                            message: !resultado.ErrorFound
                                ? "Se deshabilitó con éxito el registro. "
                                : "Hubo un problema al intentar habilitar el registro. ",
                            control: this,
                            type: GetType()
                        );
                }, err => {
                    Utils.ShowSnackbar("El token caducó. Volvé a iniciar sesión. ", this, GetType());
                });
            }
            else {
                Utils.ShowSnackbar("Carecés de privilegios suficientes para realizar esta acción. ", this, GetType());
            }
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
            if (EsAdmin()) {
                SesionNegocio.Autenticar(res => {
                    TipoProducto t = new TipoProducto();
                    string cod = ((Label)GvDatos.Rows[e.RowIndex].FindControl("LV_CodTipoDeProducto")).Text;
                    t.Codigo = cod;
                    NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
                    nt.EliminarTipoDeProducto(t);
                    CargarDatos();
                }, err => {
                    Utils.ShowSnackbar("Caducó tu token. Volvé a iniciar sesión. ", this.Page, GetType());
                });
            }
            else {
                Utils.ShowSnackbar("No tenés permiso para realizar esta acción", this.Page, GetType());
            }
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
            if (EsAdmin()) {
                SesionNegocio.Autenticar(res => {
                    TipoProducto Tp = new TipoProducto();
                    Tp.Codigo = ((Label)GvDatos.Rows[e.RowIndex].FindControl("LV_EditCod")).Text;
                    Tp.CodAnimal = ((DropDownList)GvDatos.Rows[e.RowIndex].FindControl("DD_EditAnimal")).SelectedValue;
                    Tp.tipoDeProducto = ((DropDownList)GvDatos.Rows[e.RowIndex].FindControl("DD_EditTdp")).SelectedValue;
                    Tp.Descripcion = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("TB_EditDesc")).Text;
                    NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
                    nt.ActualizarTipoDeProducto(Tp);
                }, err => {
                    Utils.ShowSnackbar("El token caducó, volvé a iniciar sesión", this.Page, GetType());
                });
            }
            else {
                Utils.ShowSnackbar("No tenés permiso para realizar esta acción", this.Page, GetType());
            }

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