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
            GridView1.DataSource = myDataSet.Tables[0];
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e) {

            if (IsPostBack != true) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;
                Response res = ProveedorNegocio.ObtenerListaDeProveedores();
                if (!res.ErrorFound) {
                    CargarTabla(res);
                }
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            string cuit = txtBuscar.Text;
            Response res = ProveedorNegocio.ObtenerProveedorByCUIT(cuit);
            if (!string.IsNullOrEmpty(txtBuscar.Text) && !res.ErrorFound) {
                CargarTabla(res);

            }
            else {
                Response ress = ProveedorNegocio.ObtenerListaDeProveedores();
                CargarTabla(ress);
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e) {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            // Obtener el valor de una celda específica, por ejemplo, la primera celda
            string cuit = ((Label)GridView1.Rows[e.RowIndex].FindControl("CUIT_Prov_lb")).Text;
            pruebalb.Text = "valor de fila:" + cuit;
            ProveedorNegocio.EliminadoLogicoProveedor(cuit);

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GridView1.EditIndex = -1;
            Response res = ProveedorNegocio.ObtenerListaDeProveedores();
            if (!res.ErrorFound) {
                CargarTabla(res);
            }
        }
    }
}