using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Empleados {
    public partial class EditarEmpleado : System.Web.UI.Page {
        private readonly string actualUser = "Usuario_Actual";
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
            UsuarioPerfil = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;
            return true;
        }
        protected void CargarValores(Empleado obj) {
            txtDNI.Text = obj.DNI;
            txtNombre.Text = obj.Nombre;
            txtApellido.Text = obj.Apellido;
            ddlGenero.SelectedValue = obj.Sexo;
            txtFechaNacimiento.Text = obj.FechaNacimiento;
            txtFechaContrato.Text = obj.FechaContrato;
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
                bool inicioSesion = CargarSesion();
                bool cargoPerfil = CargarPerfil();
                if (inicioSesion && cargoPerfil) {
                    UsuarioActual = Session[actualUser] as Empleado;
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                        CargarValores(UsuarioActual);
                    }
                    else {
                        Utils.MostrarMensaje($"No tenés permiso para editar registros. ", this.Page, GetType());
                        btnGuardarCambios.Enabled = false;
                        // *** Redirigir a página principal *** ///

                    }
                }

            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e) {
            UsuarioActual = Session[actualUser] as Empleado;
            string oldDNI = UsuarioActual.DNI;

            Empleado obj = new Empleado() {
                DNI = txtDNI.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Sexo = ddlGenero.SelectedValue,
                FechaNacimiento = txtFechaNacimiento.Text,
                FechaContrato = txtFechaContrato.Text,
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

            });

        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args) {
            string dni = txtDNI.Text;

            Response rs = EmpleadoNegocio.BuscarEmpleadoPorDNI(dni);
            if(!rs.ErrorFound) {
                DataSet resultado = rs.ObjectReturned as DataSet;
                Response emp = EmpleadoNegocio.ExtractDataFromDataSet(resultado);
                if(emp.ErrorFound && emp.Message == SesionNegocio.ErrorCode.NO_ROWS) {
                    args.IsValid = true;
                } else {
                    args.IsValid = false;
                }
            }

        }
    }

}
