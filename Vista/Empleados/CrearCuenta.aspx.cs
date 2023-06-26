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
    public partial class CrearCuenta : System.Web.UI.Page { 
        private readonly string actualUser = Utils.actualUser;

        protected void CargarDatosPrueba() {
            txtDNI.Text = "45009001";
            txtNombre.Text = "Derrick";
            txtApellido.Text = "Dime";
            ddlGenero.SelectedValue = "M";
            txtFechaNacimiento.Text = "1990-01-05";
            txtFechaContrato.Text = "2015-05-01";
            txtSueldo.Text = "456009";
            txtDireccion.Text = "Washington 2040";
            txtLocalidad.Text = "Belgrano R.";
            txtProvincia.Text = "Ciudad Autónoma de Buenos Aires";
            txtNacionalidad.Text = "Argentina";
            txtClave.Text = "tomato";
            txtConfirmarClave.Text = "tomato";
            chkAdmin.Checked = true;
            chkEstado.Checked = true;
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;
                //CargarDatosPrueba();
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e) {
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
            string claveIngresada = txtClave.Text;

            SesionNegocio.Autenticar((res) => { // Si todo está en orden (El token existe, y está habilitado), mandamos a crear el empleado.
                Response operacion = EmpleadoNegocio.CrearEmpleado(obj, claveIngresada);
                if (operacion.ErrorFound) {
                    Utils.MostrarMensaje($"Error. {operacion.Message + " / " + operacion.Details}.", this.Page, GetType());
                    //Trace.Write(operacion.Exception.ToString());
                }
                else {
                    Utils.MostrarMensaje("El empleado fue registrado exitosamente. ", this.Page, GetType());
                }
            }, (err) => { // Si hubo un error con el token, no hacemos nada.
                Utils.MostrarMensaje("Caducó el token de sesión. Volvé a iniciar sesión.", this.Page, GetType());
            });
            

        }
    }
}