using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Empleados {
    public partial class EditarEmpleado : System.Web.UI.Page {
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
        protected void CargarValores(Empleado obj) {
            DateTime fn = DateTime.Parse(obj.FechaNacimiento);
            DateTime fi = DateTime.Parse(obj.FechaContrato);
            txtDNI.Text = obj.DNI;
            txtNombre.Text = obj.Nombre;
            txtApellido.Text = obj.Apellido;
            ddlGenero.SelectedValue = obj.Sexo;
            txtFechaNacimiento.Text = fn.ToString("yyyy-MM-dd");
            txtFechaContrato.Text = fi.ToString("yyyy-MM-dd");
            txtSueldo.Text = obj.Sueldo.ToString();
            txtDireccion.Text = obj.Direccion;
            txtProvincia.Text = obj.Provincia;
            txtLocalidad.Text = obj.Localidad;
            txtNacionalidad.Text = obj.Nacionalidad;
            chkEstado.Checked = obj.Estado;
            chkAdmin.Checked = (obj.Rol == Empleado.Roles.ADMIN);
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;


                bool cargoPerfil = CargarPerfil();
                if (auth.Granted && cargoPerfil) {
                    UsuarioPerfil = Session[editingUser] as Empleado;
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                        CargarValores(UsuarioPerfil);
                    }
                    else {
                        btnGuardarCambios.Enabled = false;
                        string login_url = "/Empleados/IniciarSesion.aspx";
                        string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
                        Response.Redirect($"{login_url}?next={next_url}&msg=Iniciá sesión con una cuenta de administrador para continuar.");
                        // *** Redirigir a página principal *** ///

                    }
                }

            }
        }

        protected void BtnGuardarCambios_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            UsuarioPerfil = Session[editingUser] as Empleado;
            string oldDNI = UsuarioPerfil.DNI;
            DateTime fn = DateTime.ParseExact(txtFechaNacimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime fi = DateTime.ParseExact(txtFechaContrato.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Empleado obj = new Empleado() {
                DNI = txtDNI.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Sexo = ddlGenero.SelectedValue,
                FechaNacimiento = fn.ToString("yyyy-MM-dd"),
                FechaContrato = fi.ToString("yyyy-MM-dd"),
                Sueldo = Convert.ToDouble(txtSueldo.Text),
                Direccion = txtDireccion.Text,
                Provincia = txtProvincia.Text,
                Localidad = txtLocalidad.Text,
                Nacionalidad = txtNacionalidad.Text,
                Estado = chkEstado.Checked,
                Rol = chkAdmin.Checked ? Empleado.Roles.ADMIN : Empleado.Roles.NORMAL
            };

            SesionNegocio.Autenticar(data => {
                // Función que se ejecuta si autenticó
                Response operacion = EmpleadoNegocio.ModificarEmpleado(obj, oldDNI);
                if(operacion.ErrorFound) {
                    string mensajeError = "";
                    switch(operacion.Message) {
                        case SesionNegocio.ErrorCode.NO_SESSION_FOUND:
                            mensajeError = "No hay sesion iniciada o el token caducó. ";
                            break;
                        case SesionNegocio.ErrorCode.UNAUTHORIZED:
                            mensajeError = "No tenés permiso para realizar esta acción. ";
                            break;
                        default:
                            mensajeError = $"Ocurrió un error. Detalles: [{operacion.Message}] {operacion.Details}.";
                            break;
                    }
                    Utils.MostrarMensaje(mensajeError, this.Page, GetType());
                } else {
                    Utils.MostrarMensaje("Se han guardado los cambios. ", this.Page, GetType());
                }

            }, error => {
                // Función que se ejecuta si NO autenticó
                bool huboError = error.ErrorFound;
                string mensajeError = error.Message;
                Utils.MostrarMensaje("Error de autenticación. " + mensajeError, this.Page, GetType());

            });

        }

        protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args) {
            
            args.IsValid = true;

        }
    }

}
