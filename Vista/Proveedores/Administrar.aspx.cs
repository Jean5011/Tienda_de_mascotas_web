using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
namespace Vista.Proveedores {
    public partial class Administrar : System.Web.UI.Page {
        protected void CargarTabla(Response res) {
            DataSet myDataSet = res.ObjectReturned as DataSet;
            GvDatos.DataSource = myDataSet.Tables["root"];
            GvDatos.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e) {

            if (IsPostBack != true) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                Response res = ProveedorNegocio.ObtenerListaDeProveedores();
                if (!res.ErrorFound) {
                    CargarTabla(res);
                }
            }

        }

        protected void filtrarProveedor_Click(object sender, EventArgs e) {
            string cuit = txtBuscar.Text;
            Response resObtProveedorByCUIT = ProveedorNegocio.ObtenerProveedorByCUIT(cuit);
            if ((!string.IsNullOrEmpty(txtBuscar.Text)) && (!resObtProveedorByCUIT.ErrorFound)) {
                CargarTabla(resObtProveedorByCUIT);
            }
            else {
                Response resObtListDeProv = ProveedorNegocio.ObtenerListaDeProveedores();
                CargarTabla(resObtListDeProv);
            }

        }

        protected void GvDatos_RowEditing(object sender, GridViewEditEventArgs e) {

        }


        protected void EliminarProveedor(string CUIT) {
            var auth = Session[Utils.AUTH] as SessionData;
            var user = auth.User;
            if(user.Rol == Empleado.Roles.ADMIN) {
                // El usuario puede eliminar el registro.
                SesionNegocio.Autenticar(success => {
                    var respProveedorActualizado = ProveedorNegocio.EliminadoLogicoProveedor(CUIT);
                    Utils.ShowSnackbar(
                            message: !respProveedorActualizado.ErrorFound
                                ? "¡El proveedor ha sido eliminado correctamente!"
                                : "Hubo un error al intentar eliminar el registro.",
                            control: this,
                            type: GetType()
                        );
                }, err => {
                    Utils.ShowSnackbar("El token caducó. Volvé a iniciar sesión para continuar. ", this, GetType());
                });
            } else {
                Utils.ShowSnackbar("Carece de privilegios para realizar esta acción. ", this, GetType());
            }
        }

        protected void Lb_Command(object sender, CommandEventArgs e) {
            if(e.CommandName == "EliminarProveedor") {
                string cuit = e.CommandArgument.ToString();
                EliminarProveedor(cuit);
            }
        }

        protected void GvDatos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GvDatos.EditIndex = -1;
            Response res = ProveedorNegocio.ObtenerListaDeProveedores();
            if (!res.ErrorFound) {
                CargarTabla(res);
            }
        }


        protected void GvDatos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            Proveedor proveedor = new Proveedor {
                CUIT = ((Label)GvDatos.Rows[e.RowIndex].FindControl("cuitEditar_lb")).Text,
                RazonSocial = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("RazonSocial_Prov_tb")).Text,
                NombreContacto = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("NombreDeContacto_Prov_tb")).Text,
                CorreoElectronico = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("CorreoElectronico_Prov_tb")).Text,
                Telefono = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("Telefono_Prov_tb")).Text,
                Direccion = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("Direccion_Prov_tb")).Text,
                Provincia = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("Provincia_Prov_tb")).Text,
                Localidad = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("Localidad_Prov_tb")).Text,
                Pais = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("Pais_Prov_tb")).Text,
                CodigoPostal = ((TextBox)GvDatos.Rows[e.RowIndex].FindControl("CodigoPostal_Prov_tb")).Text
            };

            Response res = ProveedorNegocio.ActualizarProveedor(proveedor);
            Response resMain = ProveedorNegocio.ObtenerListaDeProveedores();

            if (!res.ErrorFound) {
                GvDatos.EditIndex = -1;
                Utils.ShowSnackbar("Proveedor ha sido actualizado correctamente!.", this, GetType());
                if (!resMain.ErrorFound) {
                    CargarTabla(resMain);
                }
                else Utils.ShowSnackbar("Algo salio mal.", this, GetType());

            }
            else {
                Utils.ShowSnackbar("No se pudo actualizar el proveedor.", this, GetType());
            }
        }

        //EDITAR PROVEEDOR A PARTIR DE ACA
        protected void EditarProveedor(String CUIT) {
            string url = "Editar.aspx?cuit=" + CUIT;
            Response.Redirect(url);
        }

        protected void GvDatos_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "EditarProveedor") {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GvDatos.Rows[rowIndex];
                String CUIT = row.Cells[0].Text; //ya que el cuit esta en la primera celda

                EditarProveedor(CUIT);
            }
        }





        public void CargarDatos() {
            Response res = ProveedorNegocio.ObtenerListaDeProveedores();
            if (!res.ErrorFound) {
                CargarTabla(res);
            }
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