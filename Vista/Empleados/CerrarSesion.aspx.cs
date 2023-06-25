using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Empleados {
    public partial class CerrarSesion : System.Web.UI.Page {
        private readonly string actualUser = Utils.actualUser;
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                var settings = new Utils.Authorization() {
                    AccessType = Utils.Authorization.AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
                    RejectNonMatches = true,
                    Message = "No había sesión que cerrar"
                };
                Session[Utils.AUTH] = settings.ValidateSession(this);

                var auth = Session[Utils.AUTH] as Utils.SessionData;
                var UsuarioActual = auth.User;
                H2Titulo.InnerText = $"Cerrar sesión";
                LabelDescripcion.Text = $"Iniciaste sesión como {UsuarioActual.Nombre}.";
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e) {
            SesionNegocio.CerrarSesion();
            Utils.MostrarMensaje("Has cerrado sesión. ", this.Page, GetType()); // ver cómo evitar que no aparezca el mensaje si no comento las siguientes 2 líneas de código.
            Utils.EsperarSegundos(2);
            Response.Redirect("/Empleados/IniciarSesion.aspx");
        }
    }
}