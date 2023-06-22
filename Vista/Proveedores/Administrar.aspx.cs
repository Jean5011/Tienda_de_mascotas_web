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
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        protected void CargarTabla(Response res)
        {
            DataSet myDataSet = res.ObjectReturned as DataSet;
            GridView1.DataSource = myDataSet.Tables[0];
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Utils.CargarSesion(this, true, "Iniciá sesión para acceder a la lista de proveedores");
            Response res = ProveedorNegocio.ObtenerListaDeProveedores();
            if (!res.ErrorFound)
            {
                CargarTabla(res);
            }
           
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string cuit = txtBuscar.Text;
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            Response res = proveedorNegocio.ObtenerProveedorByCUIT(cuit);
            if (!string.IsNullOrEmpty(txtBuscar.Text) && !res.ErrorFound)
            {
                CargarTabla(res);

            }
            else
            {
                Response ress = ProveedorNegocio.ObtenerListaDeProveedores();
                CargarTabla(ress);
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}