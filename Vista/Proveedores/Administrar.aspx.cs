using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
namespace Vista.Proveedores
{
    public partial class Administrar : System.Web.UI.Page
    {
        protected void CargarTabla(Response res)
        {
            DataSet myDataSet = res.ObjectReturned as DataSet;
            GvDatos.DataSource = myDataSet.Tables["root"];
            GvDatos.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack != true)
            {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                Response res = ProveedorNegocio.ObtenerListaDeProveedores();
                if (!res.ErrorFound)
                {
                    CargarTabla(res);
                }
            }

        }

        protected void filtrarProveedor_Click(object sender, EventArgs e)
        {
            string cuit = txtBuscar.Text;
            Response resObtProveedorByCUIT = ProveedorNegocio.ObtenerProveedorByCUIT(cuit);
            if ((!string.IsNullOrEmpty(txtBuscar.Text)) && (!resObtProveedorByCUIT.ErrorFound))
            {
                CargarTabla(resObtProveedorByCUIT);
            }
            else
            {
                Response resObtListDeProv = ProveedorNegocio.ObtenerListaDeProveedores();
                CargarTabla(resObtListDeProv);
            }

        }

        protected void EliminarProveedor(string CUIT)
        {
            var auth = Session[Utils.AUTH] as SessionData;
            var user = auth.User;
            if (user.Rol == Empleado.Roles.ADMIN)
            {
                // El usuario puede eliminar el registro.
                SesionNegocio.Autenticar(success =>
                {
                    var respProveedorActualizado = ProveedorNegocio.EliminadoLogicoProveedor(CUIT);
                    Response res = ProveedorNegocio.ObtenerListaDeProveedores();

                    if (!respProveedorActualizado.ErrorFound)
                    {
                    Utils.ShowSnackbar("¡El proveedor ha sido eliminado correctamente!", this, GetType());
                     CargarTabla(res);
                    }
                    else
                    {
                        Utils.ShowSnackbar("Hubo un error al intentar eliminar el registro.", this, GetType());
                    }
                    
                }, err =>
                {
                    Utils.ShowSnackbar("El token caducó. Volvé a iniciar sesión para continuar. ", this, GetType());
                });
            }
            else
            {
                Utils.ShowSnackbar("Carece de privilegios para realizar esta acción. ", this, GetType());
            }
        }

        protected void Lb_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EliminarProveedor")
            {
                string cuit = e.CommandArgument.ToString();
                EliminarProveedor(cuit);
            }
        }

        //EDITAR PROVEEDOR A PARTIR DE ACA
        protected void EditarProveedor(String CUIT)
        {
            string url = "Editar.aspx?cuit=" + CUIT;
            Response.Redirect(url);
        }

        protected void GvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarProveedor")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GvDatos.Rows[rowIndex];
                String CUIT = row.Cells[0].Text; //ya que el cuit esta en la primera celda

                EditarProveedor(CUIT);
            }
        }

        public void CargarDatos()
        {
            Response res = ProveedorNegocio.ObtenerListaDeProveedores();
            if (!res.ErrorFound)
            {
                CargarTabla(res);
            }
        }
        protected void GvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

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

        protected void GvDatos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                if (e.Row.FindControl("GvDatosPagerPageTxtBox") is TextBox txtPagerTextBox)
                {
                    txtPagerTextBox.Text = (GvDatos.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager)
                {
                    ddlPager.SelectedValue = GvDatos.PageSize + "";
                }
            }
        }
        protected void GvDatosPagerPageTxtBox_TextChanged(object sender, EventArgs e)
        {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= GvDatos.PageCount - 1)
            {
                GvDatos.PageIndex = intendedPage;
                CargarDatos();
            }
            else
            {
                ((TextBox)sender).Text = GvDatos.PageIndex + "";
            }
        }

        protected void DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0)
            {
                GvDatos.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

    }
}