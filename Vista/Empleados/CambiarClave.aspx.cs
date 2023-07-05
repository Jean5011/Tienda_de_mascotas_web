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
                // Página accesible para empleados y administradores.
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
                        btnGuardarCambios.Enabled = false;
                        AuthorizationVista.GoLogin(this, new Authorization {
                            Message = "Ingresá como administrador para cambiar la clave de otro usuario. "
                        });

                    }
                }

            }
        }

        protected void BtnGuardarCambios_Click(object sender, EventArgs e) {
            GenerarClaves();
        }

        protected void GenerarClaves() {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioPerfil = Session[editingUser] as Empleado;
            string claveNueva = txtClave.Text;
            var respuesta = EmpleadoNegocio.CrearClaves(auth, UsuarioPerfil, claveNueva);
            Utils.ShowSnackbar(respuesta.Message, this);

        }
    }
}
