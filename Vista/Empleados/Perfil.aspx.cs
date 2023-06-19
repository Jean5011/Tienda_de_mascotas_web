using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vista.Empleados {
    public partial class Perfil : System.Web.UI.Page {
        private Empleado UsuarioActual;
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
            UsuarioActual = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;
            return true;
        }
        protected bool CargarPerfil() {
            string dni_empleado = Request.QueryString["DNI"];
            if(string.IsNullOrEmpty(dni_empleado)) {
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
        class DetalleEmpleado {
            public string Valor { get; set; }
            public string Propiedad { get; set; }

            public DetalleEmpleado(string primaryText, string secondaryText) {
                Valor = primaryText;
                Propiedad = secondaryText;
            }
        }
        protected void RellenarDatos() {
            NombreEmpleadoTitulo.InnerText = UsuarioPerfil.Nombre + " " + UsuarioPerfil.Apellido;
            List<DetalleEmpleado> items = new List<DetalleEmpleado> {
                new DetalleEmpleado(UsuarioPerfil.DNI, "D.N.I."),
                new DetalleEmpleado(UsuarioPerfil.FechaNacimiento, "Fecha de nacimiento"),
                new DetalleEmpleado(UsuarioPerfil.FechaContrato, "Fecha de contratación"),
                new DetalleEmpleado($"${UsuarioPerfil.Sueldo}", "Salario bruto mensual"),
                new DetalleEmpleado($"{UsuarioPerfil.Direccion}, {UsuarioPerfil.Localidad}", "Dirección")
            };
            DetallesList.DataSource = items;
            DetallesList.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                bool inicioSesion = CargarSesion();
                bool cargoPerfil = CargarPerfil();
                if(inicioSesion && cargoPerfil) {
                    if(UsuarioActual.Rol == Empleado.Roles.ADMIN || UsuarioActual.DNI == UsuarioPerfil.DNI) {
                        RellenarDatos();
                    } else {
                        Utils.MostrarMensaje($"No tenés permiso para ver esta página. ", this.Page, GetType());
                        // *** Redirigir a página principal *** ///

                    }
                }

            }
        }
    }
}