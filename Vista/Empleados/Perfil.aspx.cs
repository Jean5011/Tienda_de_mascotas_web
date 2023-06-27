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
            var auth = Session[Utils.AUTH] as SessionData;
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
                    gvDatos.DataSource = dt;
                    gvDatos.DataBind();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                var auth = Session[Utils.AUTH] as SessionData;
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
                        AuthorizationVista.GoLogin(this, new Authorization() {
                            Message = "Ingresá como administrador para ver perfiles de otros empleados. "
                        });

                    }
                }

            }
        }

        protected void BtnDeshabilitar_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
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
            var auth = Session[Utils.AUTH] as SessionData;
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
            var auth = Session[Utils.AUTH] as SessionData;
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

        protected void BtnAdminAccess_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            UsuarioPerfil = Session[editingUser] as Empleado;
            if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar((res) => {
                    Response.Redirect("/Empleados/AdministrarAccesos.aspx?DNI=" + UsuarioPerfil.DNI);
                }, (err) => {
                    Utils.MostrarMensaje("Error. La sesión fue cerrada.", this.Page, GetType());
                });
            }
            else {
                Utils.MostrarMensaje("No estás autorizado a realizar esta acción. ", this.Page, GetType());
            }

        }


        // Funciones relacionadas al GridView:
        protected void CargarDatos() {
            CargarVentas();
        }
        protected void GvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvDatos.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void GvDatos_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("gvDatosPagerPageTxtBox") is TextBox txtPagerTextBox) {
                    txtPagerTextBox.Text = (gvDatos.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager) {
                    ddlPager.SelectedValue = gvDatos.PageSize + "";
                }
            }
        }
        protected void GvProductsPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvDatos.PageCount - 1) {
                gvDatos.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvDatos.PageIndex + "";
            }
        }

        protected void DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvDatos.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void GvDatos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }




    }
}