using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vista {
    public static class Utils {
        public static readonly string actualUser = "Usuario_Actual";

        public static void MostrarMensaje(string mensaje, Control control, Type type) {
            string script = "MostrarMensaje('" + mensaje + "');";
            ScriptManager.RegisterStartupScript(control, type, "MostrarMensaje", script, true);
        }

        public static bool CargarSesion(Page page, bool redirigirSiNoHaySesionIniciada = true, string mensajeError = "Iniciá sesión para acceder a este recurso. ") {
            LinkButton lbActualUser = (LinkButton)page.FindControl("lbActualUser");
            LinkButton lbIniciarSesion = (LinkButton)page.FindControl("lbIniciarSesion");
            HtmlGenericControl spanPageTitle = (HtmlGenericControl)page.FindControl("spanPageTitle");
            HtmlGenericControl lbAURol = (HtmlGenericControl)page.FindControl("lbAURol");
            HtmlGenericControl lbAUNombre = (HtmlGenericControl)page.FindControl("lbAUNombre");

            // Accede a la variable Session utilizando "HttpContext.Current.Session"
            HttpSessionState Session = HttpContext.Current.Session;

            lbActualUser.Visible = false;
            lbIniciarSesion.Visible = true;


            Entidades.Response res_b = SesionNegocio.ObtenerDatosEmpleadoActual();
            if (res_b.ErrorFound) {
                if (res_b.Message == SesionNegocio.ErrorCode.NO_SESSION_FOUND || res_b.Message == SesionNegocio.ErrorCode.EXPIRED_TOKEN) {
                    // De no haber iniciado sesión, se envía a la página de Inicio de Sesión con argumento "next" para que luego pueda volver.
                    string login_url = "/Empleados/IniciarSesion.aspx";
                    string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                    if(redirigirSiNoHaySesionIniciada) page.Response.Redirect($"{login_url}?next={next_url}&msg={mensajeError}");
                }
                MostrarMensaje($"Error verificando tu sesión. Detalles: {res_b.Message}.", page.Page, page.GetType());
                return false;
            }
            else {
                Session[actualUser] = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;
                lbAURol.InnerText = (Session[actualUser] as Empleado).Rol == Empleado.Roles.ADMIN ? "Administrador" : "Empleado";
                lbAUNombre.InnerText = (Session[actualUser] as Empleado).Nombre + " " + (Session[actualUser] as Empleado).Apellido;
                lbActualUser.Visible = true;
                lbIniciarSesion.Visible = false;
                // Si llega acá es porque cargó todo bien.
                // Utils.MostrarMensaje($"Empleado asignado. Nombre: {(res_b.ObjectReturned as Empleado).Nombre}", this.Page, GetType());
            }
            Session[actualUser] = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;
            return true;
        }
    }
}