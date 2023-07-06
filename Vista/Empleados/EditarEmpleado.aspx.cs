using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Empleados {
    public partial class EditarEmpleado : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                // Página accesible sólo para administradores
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

                CargarPerfil();
                UsuarioPerfil = Session[editingUser] as Empleado;
                CargarValores(UsuarioPerfil);
            }
        }
        protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args) {
            args.IsValid = true;
        }
        protected void BtnGuardarCambios_Click(object sender, EventArgs e) {
            GuardarCambios();
        }
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
            // txtDNI.Text = obj.DNI;
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
            // chkEstado.Checked = obj.Estado;
            chkAdmin.Checked = (obj.Rol == Empleado.Roles.ADMIN);
        }
        protected Empleado RescatarValores() {
            UsuarioPerfil = Session[editingUser] as Empleado;
            DateTime fn = DateTime.ParseExact(txtFechaNacimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime fi = DateTime.ParseExact(txtFechaContrato.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var empleado = new Empleado() {
                DNI = UsuarioPerfil.DNI,
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
                Estado = UsuarioPerfil.Estado,
                Rol = chkAdmin.Checked ? Empleado.Roles.ADMIN : Empleado.Roles.NORMAL
            };
            return empleado;
        }
        protected void GuardarCambios() {
            var auth = Session[Utils.AUTH] as SessionData;
            UsuarioPerfil = Session[editingUser] as Empleado;
            var empleado = RescatarValores();
            var respuesta = EmpleadoNegocio.ModificarEmpleado(auth, empleado);
            Utils.ShowSnackbar(respuesta.Message, this);
        }


    }

}
