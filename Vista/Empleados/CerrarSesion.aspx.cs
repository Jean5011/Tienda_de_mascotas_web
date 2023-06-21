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
                bool inicioSesion = Utils.CargarSesion(this, false);
                var UsuarioActual = Session[actualUser] as Empleado;
                H2Titulo.InnerText = $"Cerrar sesión";
                LabelDescripcion.Text = $"Iniciaste sesión como {UsuarioActual.Nombre}.";
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e) {
            SesionNegocio.CerrarSesion();
            Utils.MostrarMensaje("Has cerrado sesión. ", this.Page, GetType());
        }
    }
}