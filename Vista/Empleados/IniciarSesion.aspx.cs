using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vista.Empleados {
    public partial class IniciarSesion : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                // Dado que se trata de la página de inicio de sesión, todos pueden acceder a esta página.
                var auth = AuthorizationVista.ValidateSession(this, Authorization.NO_RESTRICTIONS);

                txtDNI.Focus();
                string msg = Request.QueryString["msg"];
                if(!string.IsNullOrEmpty(msg)) {
                    Utils.ShowSnackbar(msg, this);
                }
                if(auth.User != null) {
                    lblResultado.Text = ($"Si cambiás de cuenta, se cerrará tu sesión actual.");
                }
            }
        }

        protected void BtnIniciarSesion_Click(object sender, EventArgs e) {
            Login();
        }

        /// <summary>
        /// Realiza el login y comunica al usuario el resultado.
        /// </summary>
        protected void Login() {
            string dni = txtDNI.Text;
            string clave = txtClave.Text;
            var res = EmpleadoNegocio.IniciarSesion(dni, clave);
            Utils.ShowSnackbar(res.Message, this);
            if (!res.ErrorFound) {
                string goNext = Request.QueryString["next"];
                if (!string.IsNullOrEmpty(goNext)) {
                    Response.Redirect(HttpUtility.UrlDecode(goNext));
                }
                else Response.Redirect("/");
            }
        }
    }
}