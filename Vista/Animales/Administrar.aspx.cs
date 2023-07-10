using System;
using System.Data;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Animales {
    /// <summary>
    /// Vista Animales > Administrar.
    /// Contiene una tabla desde la que se pueden ver todos los registros.
    /// </summary>
    public partial class Administrar : System.Web.UI.Page {

        /// Eventos
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                // Página accesible para empleados y administradores.
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                CargarDatos();
                if (Request.QueryString["ID"] != null)
                {
                    string id = Request.QueryString["ID"];
                    CargarDatos(id:id);

                }
            }
        }
        protected void SwitchStatus_Command(object sender, CommandEventArgs e) {
            string codigo = e.CommandArgument.ToString();
            if(e.CommandName == "Habilitar") {
                HabilitarRegistro(codigo);
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void GvDatos_SelectedIndexChanged (object sender, EventArgs e) { }

        protected void GvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            EliminarRegistro(e);
        }

        protected void GvDatos_RowEditing(object sender, GridViewEditEventArgs e) {
            GvDatos.EditIndex = e.NewEditIndex;
            CargarDatos(false);
        }

        protected void GvDatos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            CargarDatos();
        }

        protected void GvDatos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            EditarRegistro(e);
        }

        protected void GvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            int newPageIndex = e.NewPageIndex;
            if (newPageIndex >= 0 && newPageIndex < GvDatos.PageCount) {
                GvDatos.PageIndex = newPageIndex;
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

        protected void GvDatos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) { }

        /// Métodos
        
        ///<summary>
        /// Carga los datos del GridView.
        /// </summary>
        /// <param name="reiniciarEditIndex">Indica si se debe establecer el EditIndex en -1.</param>
        protected void CargarDatos(bool reiniciarEditIndex = true,string id=null) {
            string textoABuscar = txtBuscar.Text;
            bool estado = CheckBox1.Checked ? false : true;
            if (id != null) textoABuscar = id;
            var response = NegocioAnimales.BuscarAnimales(textoABuscar,estado);
            if (!response.ErrorFound) {
                DataSet dt = response.ObjectReturned as DataSet;
                if (reiniciarEditIndex) GvDatos.EditIndex = -1;
                GvDatos.DataSource = dt;
                GvDatos.DataBind();
                txtBuscar.Text = "";
                return;
            }
           
            Utils.ShowSnackbar("Error cargando los registros. ", this);
        }

        /// <summary>
        /// Manda a eliminar un registro e informa el resultado obtenido.
        /// </summary>
        protected void EliminarRegistro(GridViewDeleteEventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            var animal = new Animal {
                Codigo = ((Label)GvDatos.Rows[e.RowIndex].FindControl("LV_Cod_Animal")).Text
            };
            var respuesta = NegocioAnimales.EliminarAnimal(auth, animal);
            if (!respuesta.ErrorFound) CargarDatos();
            Utils.ShowSnackbar(respuesta.Message, this);
            
        }

        /// <summary>
        /// Manda a habilitar un registro e informa el resultado obtenido.
        /// </summary>
        protected void HabilitarRegistro(string codigo) {
            var auth = Session[Utils.AUTH] as SessionData;
            var animal = new Animal {
                Codigo = codigo
            };
            var respuesta = NegocioAnimales.HabilitarAnimal(auth, animal);
            if (!respuesta.ErrorFound) CargarDatos();
            Utils.ShowSnackbar(respuesta.Message, this);

        }

        /// <summary>
        /// Manda a editar un registro e informa el resultado obtenido.
        /// </summary>
        protected void EditarRegistro(GridViewUpdateEventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            var animal = new Animal {
                Codigo = ((Label)GvDatos.Rows[e.RowIndex].FindControl("LV_EditCod")).Text,
                Nombre = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("TB_EditNombre")).Text,
                Raza = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("TB_EditRaza")).Text
            };
            var respuesta = NegocioAnimales.ActualizarAnimal(auth, animal);
            if (!respuesta.ErrorFound) CargarDatos();
            Utils.ShowSnackbar(respuesta.Message, this);
        }


    }
}