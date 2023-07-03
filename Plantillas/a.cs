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
    }
}