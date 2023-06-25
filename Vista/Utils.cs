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
        public static readonly string AUTH = "__auth";

        public class Authorization {
            public static class AccessLevel {
                public const int ANY = 0;
                public const int ONLY_LOGGED_IN_EMPLOYEE = 1;
                public const int ONLY_LOGGED_IN_ADMIN = 2;

            }
            public int AccessType { get; set; }
            public bool RequestAuthentication { get; set; }
            public bool RejectNonMatches { get; set; }
            public string Message { get; set; }

            public Authorization() {
                AccessType = Utils.Authorization.AccessLevel.ANY;
                RejectNonMatches = false;
                Message = "";
            }

            public SessionData Check() {
                Entidades.Response res_b = SesionNegocio.ObtenerDatosEmpleadoActual();

                bool onlyAdminsAllowed = AccessType == AccessLevel.ONLY_LOGGED_IN_ADMIN;
                bool onlyEmployeesAllowed = AccessType == AccessLevel.ONLY_LOGGED_IN_EMPLOYEE;
                bool anybodyAllowed = AccessType == AccessLevel.ANY;

                if (res_b.ErrorFound) {

                    bool noSessionFound = res_b.Message == SesionNegocio.ErrorCode.NO_SESSION_FOUND;
                    bool tokenExpired = res_b.Message == SesionNegocio.ErrorCode.EXPIRED_TOKEN;

                    int status = SessionData.StatusCode.UNSPECIFIED_ERROR;

                    if (noSessionFound || tokenExpired) {
                        if (noSessionFound) status = SessionData.StatusCode.NO_SESSION_FOUND;
                        if (tokenExpired) status = SessionData.StatusCode.TOKEN_EXPIRED;

                        if (AccessType == AccessLevel.ANY) {
                            // No hay sesión pero no importa
                        }
                        else {
                            // No hay sesión pero se solicita que mínimo haya una sesión activa. Se redirige a la página de login.
                        }
                    }

                    return new SessionData() {
                        Granted = false,
                        User = null,
                        Status = status
                    };
                }
                else {
                    if (res_b.ObjectReturned != null) {

                        Empleado obj = res_b.ObjectReturned as Empleado;

                        bool currentUserIsAdmin = obj.Rol == Empleado.Roles.ADMIN;
                        bool currentUserIsEmployee = obj.Rol == Empleado.Roles.NORMAL;


                        if (onlyAdminsAllowed) { // Si el acceso permitido es únicamente para administradores...
                            return new SessionData() {
                                Granted = currentUserIsAdmin,
                                User = obj,
                                Status = currentUserIsAdmin ? SessionData.StatusCode.OK : SessionData.StatusCode.UNAUTHORIZED
                            };
                        }

                        if (onlyEmployeesAllowed) { // Si el acceso permitido es para empleados únicamente...
                            bool approved = currentUserIsEmployee || currentUserIsAdmin;
                            return new SessionData() {
                                Granted = approved,
                                User = obj,
                                Status = approved ? SessionData.StatusCode.OK : SessionData.StatusCode.UNAUTHORIZED
                            };
                        }

                        if (anybodyAllowed) {
                            return new SessionData() {
                                Granted = true,
                                User = obj,
                                Status = SessionData.StatusCode.OK
                            };
                        }

                        return new SessionData() {
                            Granted = false,
                            User = obj,
                            Status = SessionData.StatusCode.UNSPECIFIED_ERROR
                        };

                    }
                    else {
                        return new SessionData() {
                            Granted = (AccessType == AccessLevel.ANY),
                            User = null,
                            Status = SessionData.StatusCode.UNSPECIFIED_ERROR
                        };
                    }
                }
            }


            public SessionData ValidateSession(Page currentWebForm) {
                MasterPage page = currentWebForm.Master;
                LinkButton btnPerfil = (LinkButton)page.FindControl("lbActualUser");
                LinkButton btnIniciarSesion = (LinkButton)page.FindControl("lbIniciarSesion");
                HtmlGenericControl spanPageTitle = (HtmlGenericControl)page.FindControl("spanPageTitle");
                HtmlGenericControl labelUserRole = (HtmlGenericControl)page.FindControl("lbAURol");
                HtmlGenericControl labelUserName = (HtmlGenericControl)page.FindControl("lbAUNombre");

                btnPerfil.Visible = false;
                btnIniciarSesion.Visible = true;

                SessionData y = Check();

                if(!page.IsPostBack) {
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
                    if (RejectNonMatches) GoLogin(page);
                    ShowSnackbar($"{Message}. (#{y.Status})", page, page.GetType());
                }

                return y;
            }



            public SessionData ValidateSession(MasterPage page) {
                LinkButton btnPerfil = (LinkButton)page.FindControl("lbActualUser");
                LinkButton btnIniciarSesion = (LinkButton)page.FindControl("lbIniciarSesion");
                HtmlGenericControl spanPageTitle = (HtmlGenericControl)page.FindControl("spanPageTitle");
                HtmlGenericControl labelUserRole = (HtmlGenericControl)page.FindControl("lbAURol");
                HtmlGenericControl labelUserName = (HtmlGenericControl)page.FindControl("lbAUNombre");

                btnPerfil.Visible = false;
                btnIniciarSesion.Visible = true;

                SessionData y = Check();

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
                    if (RejectNonMatches) GoLogin(page);
                    ShowSnackbar(Message, page, page.GetType());
                }

                return y;
            }


            public void GoLogin(Page page) {
                string login_url = "/Empleados/IniciarSesion.aspx";
                string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                page.Response.Redirect($"{login_url}?next={next_url}&msg={this.Message}");

            }
            public void GoLogin(MasterPage page) {
                string login_url = "/Empleados/IniciarSesion.aspx";
                string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                page.Response.Redirect($"{login_url}?next={next_url}&msg={this.Message}");

            }

        }

        public class SessionData {
            public static class StatusCode {
                public const int ERR_SQL = 0;
                public const int OK = 1;
                public const int AUTHENTICATION_FAILED = 2;
                public const int NO_SESSION_FOUND = 3;
                public const int TOKEN_EXPIRED = 4;
                public const int UNSPECIFIED_ERROR = 5;
                public const int UNAUTHORIZED = 6;
            }
            public bool Granted { get; set; }
            public Empleado User { get; set; }
            public int Status { get; set; }

        }

        public static void ShowSnackbar(string message, Control control, Type type) {
            const string key = "MostrarMensaje";
            string script = $"MostrarMensaje('{message}');";
            ScriptManager.RegisterStartupScript(
                control: control,
                type: type,
                key: key,
                script: script,
                addScriptTags: true
            );
        }

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



            Entidades.Response res_b = SesionNegocio.ObtenerDatosEmpleadoActual();
            if (res_b.ErrorFound) {
                if (res_b.Message == SesionNegocio.ErrorCode.NO_SESSION_FOUND || res_b.Message == SesionNegocio.ErrorCode.EXPIRED_TOKEN) {
                    // De no haber iniciado sesión, se envía a la página de Inicio de Sesión con argumento "next" para que luego pueda volver.
                }
                MostrarMensaje($"Error verificando tu sesión. Detalles: {res_b.Message}.", page.Page, page.GetType());
                return false;
            }
            else {
                Session[actualUser] = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;

                // Si llega acá es porque cargó todo bien.
                // Utils.MostrarMensaje($"Empleado asignado. Nombre: {(res_b.ObjectReturned as Empleado).Nombre}", this.Page, GetType());
            }
            Session[actualUser] = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;
            return true;
        }
        public static bool CargarAdmin(Page page, bool redirigirSiNoHaySesionIniciada = true, string mensajeError = "Iniciá sesión para acceder a este recurso. ") {
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
                    if (redirigirSiNoHaySesionIniciada) page.Response.Redirect($"{login_url}?next={next_url}&msg={mensajeError}");
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
            if (Session[actualUser] == null || (Session[actualUser] as Empleado).Rol != Empleado.Roles.ADMIN) {
                string login_url = "/Empleados/IniciarSesion.aspx";
                string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                if (redirigirSiNoHaySesionIniciada) page.Response.Redirect($"{login_url}?next={next_url}&msg={mensajeError}");
            }
            return true;
        }

        public static void EsperarSegundos(double cantSeg)
        {
            // Creo la cadena para convertir en TimeSpan:
            string s = "0.00:00:" + cantSeg.ToString().Replace(",", ".");
            TimeSpan ts = TimeSpan.Parse(s);

            // Le añado la diferencia a la hora actual:
            DateTime t1 = DateTime.Now.Add(ts);

            // Guardo la fecha y hora actual en una variable DateTime:
            DateTime t2 = DateTime.Now;

            // Mientras no haya pasado el tiempo indicado:
            while (t2 < t1)
            {
                // Asignar la hora actual:
                t2 = DateTime.Now;
            }
        }
    }
}