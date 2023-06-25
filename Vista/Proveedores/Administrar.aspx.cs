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

            var settings = new Utils.Authorization() {
                AccessType = Utils.Authorization.AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
                RejectNonMatches = true,
                Message = "Iniciá sesión para acceder a la lista de proveedores. "
            };

            Session[Utils.AUTH] = settings.ValidateSession(this);

            var auth = Session[Utils.AUTH] as Utils.SessionData;
            var UsuarioActual = auth.User;
            Response res = ProveedorNegocio.ObtenerListaDeProveedores();
            if (!res.ErrorFound) {
                CargarTabla(res);
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e) {

        }
    }
}