using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista {
    public class AuthorizationVista {
        public static SessionData ValidateSession(Page currentWebForm, Authorization auth) {
            MasterPage page = currentWebForm.Master;
            LinkButton btnPerfil = (LinkButton)page.FindControl("lbActualUser");
            LinkButton btnIniciarSesion = (LinkButton)page.FindControl("lbIniciarSesion");
            HtmlGenericControl labelUserRole = (HtmlGenericControl)page.FindControl("lbAURol");
            HtmlGenericControl labelUserName = (HtmlGenericControl)page.FindControl("lbAUNombre");

            btnPerfil.Visible = false;
            btnIniciarSesion.Visible = true;

            SessionData y = auth.Check();

            if (!page.IsPostBack) {
                btnIniciarSesion.Visible = (y.User == null);
                btnPerfil.Visible = (y.User != null);
            }

            if (y.User != null && !page.IsPostBack) {
                labelUserRole.InnerText = y.User.Rol == Empleado.Roles.ADMIN
                    ? "Administrador"
                    : (y.User.Rol == Empleado.Roles.NORMAL
                            ? "Empleado"
                            : "Rol desconocido");
                labelUserName.InnerText = $"{y.User.Nombre} {y.User.Apellido}";
            }

            if (y.Granted) {

            }
            else {
                if (auth.RejectNonMatches) GoLogin(page, auth);
                Utils.ShowSnackbar($"{auth.Message}. (#{y.Status})", page, page.GetType());
            }

            return y;
        }

        public static SessionData ValidateSession(MasterPage page, Authorization auth) {
            LinkButton btnPerfil = (LinkButton)page.FindControl("lbActualUser");
            LinkButton btnIniciarSesion = (LinkButton)page.FindControl("lbIniciarSesion");
            HtmlGenericControl labelUserRole = (HtmlGenericControl)page.FindControl("lbAURol");
            HtmlGenericControl labelUserName = (HtmlGenericControl)page.FindControl("lbAUNombre");

            btnPerfil.Visible = false;
            btnIniciarSesion.Visible = true;

            SessionData y = auth.Check();

            btnIniciarSesion.Visible = (y.User == null);
            btnPerfil.Visible = (y.User != null);

            if (y.User != null) {
                labelUserRole.InnerText = y.User.Rol == Empleado.Roles.ADMIN
                    ? "Administrador"
                    : (y.User.Rol == Empleado.Roles.NORMAL
                            ? "Empleado"
                            : "Rol desconocido");
                labelUserName.InnerText = $"{y.User.Nombre} {y.User.Apellido}";
            }

            if (y.Granted) {

            }
            else {
                if (auth.RejectNonMatches) GoLogin(page, auth);
                Utils.ShowSnackbar(auth.Message, page, page.GetType());
            }

            return y;
        }

        public static void GoLogin(Page page, Authorization auth) {
            string login_url = "/IniciarSesion";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            page.Response.Redirect($"{login_url}?next={next_url}&msg={auth.Message}");

        }
        public static void GoLogin(MasterPage page, Authorization auth) {
            string login_url = "/IniciarSesion";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            page.Response.Redirect($"{login_url}?next={next_url}&msg={auth.Message}");

        }
    }
}