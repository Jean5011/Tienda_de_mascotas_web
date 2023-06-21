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
        private readonly string actualUser = Utils.actualUser;
        private readonly string editingUser = "Usuario_Perfil";
        private Empleado UsuarioPerfil;
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        protected bool CargarPerfil() {
            var UsuarioActual = Session[actualUser] as Empleado;
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
                bool inicioSesion = Utils.CargarSesion(this, true, "Iniciá sesión para poder deshabilitar empleados. ");
                bool cargoPerfil = CargarPerfil();
                if (inicioSesion && cargoPerfil) {
                    var UsuarioActual = Session[actualUser] as Empleado;
                    UsuarioPerfil = Session[editingUser] as Empleado;
                    H2Titulo.InnerText = UsuarioPerfil.Apellido + ", " + UsuarioPerfil.Nombre;
                    LabelDescripcion.Text = "¿Estás seguro de deshabilitar a este usuario?";
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                        // El usuario actual es ADMINISTRADOR. Puede proceder a eliminar
                        if(UsuarioActual.DNI == UsuarioPerfil.DNI) {
                            Utils.MostrarMensaje("No te podés eliminar a vos mismo. Otro administrador debe realizar esa acción. ", this.Page, GetType());
                            btnDeshabilitar.Enabled = false;
                            string login_url = "/Empleados/IniciarSesion.aspx";
                            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                            Response.Redirect($"{login_url}?next={next_url}&msg=No podés deshabilitarte a vos mismo. Iniciá sesión con otra cuenta de administrador para continuar.");
                        }
                    }
                    else {
                        btnDeshabilitar.Enabled = false;
                        string login_url = "/Empleados/IniciarSesion.aspx";
                        string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                        Response.Redirect($"{login_url}?next={next_url}&msg=Iniciá sesión con una cuenta de administrador para continuar.");
                        Utils.MostrarMensaje($"No tenés permiso para borrar registros. ", this.Page, GetType());
                        
                        // *** Redirigir a página principal *** ///

                    }
                }

            }
        }

        protected void btnDeshabilitar_Click(object sender, EventArgs e) {
            var UsuarioActual = Session[actualUser] as Empleado;
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