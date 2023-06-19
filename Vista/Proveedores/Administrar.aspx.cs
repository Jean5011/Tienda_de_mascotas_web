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
        protected void CargarTabla(Response res)
        {
            DataSet myDataSet = res.ObjectReturned as DataSet;
            GridView1.DataSource = myDataSet.Tables["root"];
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response res = ProveedorNegocio.ObtenerListaDeProveedores();
            if (!res.ErrorFound)
            {
                CargarTabla(res);
            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cuit = TextBox1.Text;
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            Response res = proveedorNegocio.ObtenerProveedorByCUIT(cuit);
            if (!string.IsNullOrEmpty(TextBox1.Text) && !res.ErrorFound)
            {
                CargarTabla(res);

            }
            else
            {
                Response ress = ProveedorNegocio.ObtenerListaDeProveedores();
                CargarTabla(ress);
            }

        }
    }
}