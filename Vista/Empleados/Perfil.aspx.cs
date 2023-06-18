using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vista.Empleados {
    public partial class Perfil : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                var tk = new SesionNegocio();
                string token = tk.GetCookieValue("_au");
                string dni_loggeduser = "NOUSER";
                if(token != null)  tk.DecodificarToken(token, out dni_loggeduser);
                Label1.Text = dni_loggeduser + " es el usuario activo";
            }
        }
    }
}