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
            Response res = SesionNegocio.CerrarSesion();
            if (!res.ErrorFound)
            {
                Utils.EsperarSegundos(2);
                Response.Redirect("/Empleados/IniciarSesion.aspx");
            }
            else
            {
                Utils.MostrarMensaje("Ocurrió un error al intentar cerrar la sesión. Por favor, pruebe cerrar sesión nuevamente.", this.Page, GetType());
            }
        }
    }
}