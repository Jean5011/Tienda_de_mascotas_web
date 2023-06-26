using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Web.UI;
using Entidades;
using Negocio;
using System.Web;
using System.Globalization;


namespace Vista.Empleados {
    public partial class CambiarClave : System.Web.UI.Page {
        private readonly string editingUser = "Usuario_Perfil";
        protected bool CargarPerfil() {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            string dni_empleado = Request.QueryString["DNI"];
            if (string.IsNullOrEmpty(dni_empleado)) {
                if (!string.IsNullOrEmpty(UsuarioActual.DNI)) {
                    dni_empleado = UsuarioActual.DNI;
                }
                else return false;
            }
            Response res_b = EmpleadoNegocio.BuscarEmpleadoPorDNI(dni_empleado);
            if (res_b.ErrorFound) {
                return false;
            }
            Session[editingUser] = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;
            return true;
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;

                bool cargoPerfil = CargarPerfil();
                if (auth.Granted && cargoPerfil) {
                    var UsuarioActual = auth.User;
                    var UsuarioPerfil = Session[editingUser] as Empleado;
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN || UsuarioActual.DNI == UsuarioPerfil.DNI) {
                        // El usuario actual es ELLA/ÉL MISMO ó un ADMINISTRADOR.
                        
                    }
                    else {
                        Utils.MostrarMensaje($"No tenés permiso para cambiar la clave de alguien más. ", this.Page, GetType());
                        btnGuardarCambios.Enabled = false;
                        string login_url = "/Empleados/IniciarSesion.aspx";
                        string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                        Response.Redirect($"{login_url}?next={next_url}&msg=No podés cambiar la clave de otra persona sin ser administrador.");

                        // *** Redirigir a página principal *** ///

                    }
                }

            }
        }

        protected void BtnGuardarCambios_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual  = auth.User;
            var UsuarioPerfil = Session[editingUser] as Empleado;
            string claveNueva = txtClave.Text;
            SesionNegocio.Autenticar((res) => {
                Response op = EmpleadoNegocio.CrearClaves(UsuarioPerfil.DNI, claveNueva);
                if (!op.ErrorFound) {
                    Utils.MostrarMensaje("Se cambió la clave correctamente. ", this.Page, GetType());
                }
                else {
                    Utils.MostrarMensaje("Error. " + op.Message + " " + op.Details, this.Page, GetType());
                }
            }, (err) => {
                Utils.MostrarMensaje("Caducó el token. Tenés que volver a iniciar sesión. ", this.Page, GetType());
            });

        }
    }
}
