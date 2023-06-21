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
        private readonly string actualUser = "Usuario_Actual";
        private readonly string editingUser = "Usuario_Perfil";
        public Empleado UsuarioActual;
        private Empleado UsuarioPerfil;
        protected bool CargarSesion() {
            Response res_b = SesionNegocio.ObtenerDatosEmpleadoActual();
            if (res_b.ErrorFound) {
                if (res_b.Message == SesionNegocio.ErrorCode.NO_SESSION_FOUND) {
                    // De no haber iniciado sesión, se envía a la página de Inicio de Sesión con argumento "next" para que luego pueda volver.
                    string login_url = "/Empleados/IniciarSesion.aspx";
                    string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                    Response.Redirect($"{login_url}?next={next_url}");
                }
                Utils.MostrarMensaje($"Error verificando tu sesión. Detalles: {res_b.Details}.", this.Page, GetType());
                return false;
            }
            else {
                //Utils.MostrarMensaje($"Empleado asignado. Nombre: {(res_b.ObjectReturned as Empleado).Nombre}", this.Page, GetType());
            }
            Session[actualUser] = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;
            return true;
        }
        protected bool CargarPerfil() {
            UsuarioActual = Session[actualUser] as Empleado;
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
                bool inicioSesion = CargarSesion();
                bool cargoPerfil = CargarPerfil();
                if (inicioSesion && cargoPerfil) {
                    UsuarioActual = Session[actualUser] as Empleado;
                    UsuarioPerfil = Session[editingUser] as Empleado;
                    H2Titulo.InnerText = UsuarioPerfil.Apellido + ", " + UsuarioPerfil.Nombre;
                    LabelDescripcion.Text = "¿Estás seguro de deshabilitar a este usuario?";
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                        // El usuario actual es ADMINISTRADOR. Puede proceder a eliminar
                        if(UsuarioActual.DNI == UsuarioPerfil.DNI) {
                            Utils.MostrarMensaje("No te podés eliminar a vos mismo. Otro administrador debe realizar esa acción. ", this.Page, GetType());
                            btnDeshabilitar.Enabled = false;
                        }
                    }
                    else {
                        Utils.MostrarMensaje($"No tenés permiso para borrar registros. ", this.Page, GetType());
                        btnDeshabilitar.Enabled = false;
                        // *** Redirigir a página principal *** ///

                    }
                }

            }
        }

        protected void btnDeshabilitar_Click(object sender, EventArgs e) {
            UsuarioActual = Session[actualUser] as Empleado;
            UsuarioPerfil = Session[editingUser] as Empleado;
            if(UsuarioActual.DNI != UsuarioPerfil.DNI) {
                // Nos volvemos a asegurar que el usuario no se quiera eliminar a sí mismo.
                SesionNegocio.Autenticar((res) => { 
                    DateTime fn = DateTime.ParseExact(UsuarioPerfil.FechaNacimiento, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                    DateTime fi = DateTime.ParseExact(UsuarioPerfil.FechaContrato, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                    UsuarioPerfil.FechaNacimiento = fn.ToString("yyyy-MM-dd");
                    UsuarioPerfil.FechaContrato = fn.ToString("yyyy-MM-dd");
                    UsuarioPerfil.Estado = false;
                    Response operacion = EmpleadoNegocio.ModificarEmpleado(UsuarioPerfil, UsuarioPerfil.DNI);
                    if (!operacion.ErrorFound) {
                        Utils.MostrarMensaje($"El/La empleado/a {UsuarioPerfil.Apellido} fue deshabilitado y no podrá volver a iniciar sesión con sus credenciales. ", this.Page, GetType());
                    }
                    else {
                        Utils.MostrarMensaje($"Error. {operacion.Message} : {operacion.Details}", this.Page, GetType());
                    }
                }, (err) => {
                    Utils.MostrarMensaje($"El token caducó y no se pudo completar la operación. Volvé a iniciar sesión. ", this.Page, GetType());
                });
            } else {
                Utils.MostrarMensaje("No te podés eliminar a vos mismo. Otro administrador debe realizar esa acción. ", this.Page, GetType());
                btnDeshabilitar.Enabled = false;
            }

        }
    }
}