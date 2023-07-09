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
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;
                H2Titulo.InnerText = $"Cerrar sesión";
                LabelDescripcion.Text = $"Iniciaste sesión como {UsuarioActual.Nombre}.";
            }
        }

        protected void BtnCerrarSesion_Click(object sender, EventArgs e) {
            Response res = SesionNegocio.CerrarSesion();
            if (!res.ErrorFound)
            {
                Utils.EsperarSegundos(2);
                Response.Redirect("/Empleados/IniciarSesion.aspx");
            }
            else
            {
                Utils.ShowSnackbar("Ocurrió un error al intentar cerrar la sesión. Por favor, pruebe cerrar sesión nuevamente.", this);
            }
        }
    }
}