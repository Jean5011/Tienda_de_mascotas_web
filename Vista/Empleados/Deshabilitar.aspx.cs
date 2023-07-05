using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Empleados {
    public partial class Deshabilitar : System.Web.UI.Page {
        private readonly string editingUser = "Usuario_Perfil";
        private Empleado UsuarioPerfil;
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
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;
                bool cargoPerfil = CargarPerfil();

                if (auth.Granted && cargoPerfil) {
                    UsuarioPerfil = Session[editingUser] as Empleado;
                    H2Titulo.InnerText = UsuarioPerfil.Apellido + ", " + UsuarioPerfil.Nombre;

                    if (UsuarioPerfil.Estado) DeshabilitarSeccion();
                    else HabilitarSeccion();


                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                        if(UsuarioActual.DNI == UsuarioPerfil.DNI) {
                            btnDeshabilitar.Enabled = false;
                            AuthorizationVista.GoLogin(this, new Authorization {
                                Message = "No podés deshabilitarte a vos mismo. Iniciá sesión con una cuenta diferente. "
                            });
                        }
                    }
                    else {
                        btnDeshabilitar.Enabled = false;
                        AuthorizationVista.GoLogin(this, new Authorization {
                            Message = "Iniciá sesión con una cuenta de administrador para continuar. "
                        });
                    }
                }

            }
        }

        protected void HabilitarBtn(bool state) {
            btnHabilitar.Enabled = state;
            btnHabilitar.Visible = state;
        }
        protected void DeshabilitarBtn(bool state) {
            btnDeshabilitar.Enabled = state;
            btnDeshabilitar.Visible = state;
        }

        protected void HabilitarSeccion() {
            LabelDescripcion.Text = "¿Habilitar a este usuario?";
            DeshabilitarBtn(false);
            HabilitarBtn(true);

        }

        protected void DeshabilitarSeccion() {
            LabelDescripcion.Text = "¿Estás seguro de deshabilitar a este usuario?";
            DeshabilitarBtn(true);
            HabilitarBtn(false);

        }

        protected void Inhabilitar() {
            var auth = Session[Utils.AUTH] as SessionData;
            UsuarioPerfil = Session[editingUser] as Empleado;
            var respuesta = EmpleadoNegocio.Deshabilitar(auth, UsuarioPerfil);
            Utils.ShowSnackbar(respuesta.Message, this);
        }
        protected void Habilitar() {
            var auth = Session[Utils.AUTH] as SessionData;
            UsuarioPerfil = Session[editingUser] as Empleado;
            var respuesta = EmpleadoNegocio.Habilitar(auth, UsuarioPerfil);
            Utils.ShowSnackbar(respuesta.Message, this);
        }


        protected void BtnDeshabilitar_Click(object sender, EventArgs e) {
            Inhabilitar();
        }
        protected void BtnHabilitar_Click(object sender, EventArgs e) {
            Habilitar();
        }
    }
}