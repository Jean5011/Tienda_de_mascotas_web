using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Ventas {
    public partial class Administrar : System.Web.UI.Page {
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }

        public void CargarDatos() {
            string tbuscar = txtBuscar.Text;
            var res = tbuscar == "" ? VentaNegocio.GetVentas() : VentaNegocio.GetVentaPorID(Convert.ToInt32(tbuscar));
            if(res.ErrorFound) {
                Utils.MostrarMensaje("Error cargando ventas. ", this.Page, GetType());

            } else {
                DataSet dt = res.ObjectReturned as DataSet;
                gvVentas.DataSource = dt;
                gvVentas.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                bool inicioSesion = Utils.CargarSesion(this, false);
                CargarDatos();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }
    }
}