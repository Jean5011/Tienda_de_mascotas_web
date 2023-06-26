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
            GridView1.DataSource = myDataSet.Tables["root"];
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
            Response resObtProveedorByCUIT = ProveedorNegocio.ObtenerProveedorByCUIT(cuit);
            if ((!string.IsNullOrEmpty(txtBuscar.Text)) && (!resObtProveedorByCUIT.ErrorFound)) {
                CargarTabla(resObtProveedorByCUIT);
            }
            else {
                Response resObtListDeProv = ProveedorNegocio.ObtenerListaDeProveedores();
                CargarTabla(resObtListDeProv);
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Proveedor proveedor = new Proveedor();
            proveedor.CUIT=((Label)GridView1.Rows[e.RowIndex].FindControl("cuitEditar_lb")).Text;
            proveedor.RazonSocial = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("RazonSocial_Prov_tb")).Text;
            proveedor.NombreContacto = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("NombreDeContacto_Prov_tb")).Text;
            proveedor.CorreoElectronico = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("CorreoElectronico_Prov_tb")).Text;
            proveedor.Telefono = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Telefono_Prov_tb")).Text;
            proveedor.Direccion = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Direccion_Prov_tb")).Text;
            proveedor.Provincia = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Provincia_Prov_tb")).Text;
            proveedor.Localidad = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Localidad_Prov_tb")).Text;
            proveedor.Pais = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Pais_Prov_tb")).Text;
            proveedor.CodigoPostal = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("CodigoPostal_Prov_tb")).Text;

            Response res = ProveedorNegocio.ActualizarProveedor(proveedor);
            Response resMain = ProveedorNegocio.ObtenerListaDeProveedores();
           
            if (!res.ErrorFound)
             {
                GridView1.EditIndex = -1;
                if (!resMain.ErrorFound) { CargarTabla(resMain); }
               
            }
        }
    }
}