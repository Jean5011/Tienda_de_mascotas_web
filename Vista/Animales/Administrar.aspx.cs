using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using Negocio;
using System.Web.UI.WebControls;
using System.Web;

namespace Vista.Animales {
    public partial class Administrar : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);



                CargarDatos();
            }
        }

        protected void GvDatos_SelectedIndexChanged(object sender, EventArgs e) {

        }

        public void CargarDatos(bool restartEditIndex = true) {
            string tbuscar = txtBuscar.Text;
            bool cargarTodo = tbuscar == "";
            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = cargarTodo ? nt.GetAnimales() : nt.ObtenerPorCod(tbuscar);
            if (!resultado.ErrorFound) {
                DataSet dt = resultado.ObjectReturned as DataSet;
                if (restartEditIndex) GvDatos.EditIndex = -1;
                GvDatos.DataSource = dt;
                GvDatos.DataBind();
            }
            else {
                Utils.ShowSnackbar("Error cargando los registros. ", this, GetType());
            }
        }

        public void BtnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();


        }


        protected void GvDatos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(res => {
                    Animal a = new Animal {
                        Codigo = ((Label)GvDatos.Rows[e.RowIndex].FindControl("LV_Cod_Animal")).Text
                    };
                    NegocioAnimales nt = new NegocioAnimales();
                    nt.EliminarAnimal(a);
                    CargarDatos();
                }, err => {
                    Utils.ShowSnackbar("El Token caducó. Iniciá sesión de nuevo.", this, GetType());
                });

            }
            else {
                Utils.ShowSnackbar("No disponés de los permisos suficientes para realizar esta acción. ", this, GetType());
            }
        }

        protected void GvDatos_RowEditing(object sender, GridViewEditEventArgs e) {
            GvDatos.EditIndex = e.NewEditIndex;
            CargarDatos(false);
        }

        protected void GvDatos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            CargarDatos();
        }

        protected void GvDatos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(res => {
                    Animal a = new Animal {
                        Codigo = ((Label)GvDatos.Rows[e.RowIndex].FindControl("LV_EditCod")).Text,
                        Nombre = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("TB_EditNombre")).Text,
                        Raza = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("TB_EditRaza")).Text
                    };
                    ////////////////////////////////////////////////////////////////////////////////
                    NegocioAnimales nt = new NegocioAnimales();
                    nt.ActualizarAnimal(a);
                    CargarDatos();
                }, err => {
                    Utils.ShowSnackbar("El token caducó. Deberás iniciar sesión de nuevo. ", this, GetType());
                });
            }
            else {
                Utils.ShowSnackbar("No disponés de los permisos suficientes para realizar esta acción. ", this, GetType());
            }
        }

        protected void GvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            //GvDatos.PageIndex = e.NewPageIndex;
            //CargarDatos();

            //guardamos el nuevo indice
            int newPageIndex = e.NewPageIndex;
            //nos fijamos de que no pueda acceder a una pagina inexistente
            if (newPageIndex >= 0 && newPageIndex < GvDatos.PageCount)
            {
                //cargamos el nuevo indice
                GvDatos.PageIndex = newPageIndex;
                //cargamos datos
                CargarDatos();
            }
        }

        protected void GvDatos_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("gvDatosPagerPageTxtBox") is TextBox txtPagerTextBox) {
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