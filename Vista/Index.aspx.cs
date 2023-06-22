using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vista {
    public partial class Index : System.Web.UI.Page {
        private readonly string actualUser = Utils.actualUser;
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                Utils.CargarSesion(this, false);
            }

        }
    }
}