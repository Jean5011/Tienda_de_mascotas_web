using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Data;

namespace Vista.Empleados {
    public partial class Perfil : System.Web.UI.Page {
        private readonly string editingUser = "Usuario_Perfil";
        private Empleado UsuarioPerfil;
        protected bool CargarPerfil() {
            var auth = Session[Utils.AUTH] as Utils.SessionData;
            var UsuarioActual = auth.User;
            string dni_empleado = Request.QueryString["DNI"];
            if (string.IsNullOrEmpty(dni_empleado)) {
                if (UsuarioActual != null && !string.IsNullOrEmpty(UsuarioActual.DNI)) {
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
        class DetalleEmpleado {
            public string Valor { get; set; }
            public string Propiedad { get; set; }
            public string Icon { get; set; }

            public DetalleEmpleado(string primaryText, string secondaryText, string icon) {
                Valor = primaryText;
                Propiedad = secondaryText;
                Icon = icon;
            }
        }
        protected void RellenarDatos() {
            NombreEmpleadoTitulo.InnerText = UsuarioPerfil.Nombre + " " + UsuarioPerfil.Apellido;
            List<DetalleEmpleado> items = new List<DetalleEmpleado> {
                new DetalleEmpleado(UsuarioPerfil.DNI, "D.N.I.", "badge"),
                new DetalleEmpleado(UsuarioPerfil.FechaNacimiento, "Fecha de nacimiento", "cake"),
                new DetalleEmpleado(UsuarioPerfil.FechaContrato, "Fecha de contratación", "work"),
                new DetalleEmpleado($"${UsuarioPerfil.Sueldo}", "Salario bruto mensual", "payments"),
                new DetalleEmpleado($"{UsuarioPerfil.Direccion}, {UsuarioPerfil.Localidad}", "Dirección", "location_city")
            };
            DetallesList.DataSource = items;
            DetallesList.DataBind();
        }

        protected void CargarVentas() {
            if(Session[editingUser] != null) {
                UsuarioPerfil = Session[editingUser] as Empleado;
                var res = VentaNegocio.VentasPorEmp(UsuarioPerfil.DNI);
                if(!res.ErrorFound) {
                    DataSet dt = res.ObjectReturned as DataSet;
                    gvVentas.DataSource = dt;
                    gvVentas.DataBind();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            
            if (!IsPostBack) {
                var settings = new Utils.Authorization() {
                    AccessType = Utils.Authorization.AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
                    RejectNonMatches = true,
                    Message = "Iniciá sesión para acceder a los perfiles. "
                };
                Session[Utils.AUTH] = settings.ValidateSession(this);

                var auth = Session[Utils.AUTH] as Utils.SessionData;
                var UsuarioActual = auth.User;

                bool cargoPerfil = CargarPerfil();
                if (auth.Granted && cargoPerfil) {
                    UsuarioPerfil = Session[editingUser] as Empleado;
                    CargarVentas();
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN || UsuarioActual.DNI == UsuarioPerfil.DNI) {
                        RellenarDatos();
                    }
                    else {
                        Utils.ShowSnackbar("No tenés permiso de acceder a esta información", this, GetType());
                        new Utils.Authorization() {
                            Message = "Ingresá como administrador para ver perfiles de otros empleados. "
                        }.GoLogin(this);

                    }
                }

            }
        }

        protected void BtnDeshabilitar_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as Utils.SessionData;
            var UsuarioActual = auth.User;
            UsuarioPerfil = Session[editingUser] as Empleado;
            if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar((res) => {
                    Response.Redirect("/Empleados/Deshabilitar.aspx?DNI=" + UsuarioPerfil.DNI);
                }, (err) => {
                    Utils.MostrarMensaje("Error. La sesión fue cerrada.", this.Page, GetType());
                });
            }
            else {
                Utils.MostrarMensaje("No estás autorizado a realizar esta acción. ", this.Page, GetType());
            }
        }

        protected void BtnEditarDetalles_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as Utils.SessionData;
            var UsuarioActual = auth.User;
            UsuarioPerfil = Session[editingUser] as Empleado;
            if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar((res) => {
                    Response.Redirect("/Empleados/EditarEmpleado.aspx?DNI=" + UsuarioPerfil.DNI);
                }, (err) => {
                    Utils.MostrarMensaje("Error. La sesión fue cerrada.", this.Page, GetType());
                });
            }
            else {
                Utils.MostrarMensaje("No estás autorizado a realizar esta acción. ", this.Page, GetType());
            }
        }

        protected void BtnCambiarClave_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as Utils.SessionData;
            var UsuarioActual = auth.User;
            UsuarioPerfil = Session[editingUser] as Empleado;
            if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar((res) => {
                    Response.Redirect("/Empleados/CambiarClave.aspx?DNI=" + UsuarioPerfil.DNI);
                }, (err) => {
                    Utils.MostrarMensaje("Error. La sesión fue cerrada.", this.Page, GetType());
                });
            }
            else {
                Utils.MostrarMensaje("No estás autorizado a realizar esta acción. ", this.Page, GetType());
            }

        }
    }
}