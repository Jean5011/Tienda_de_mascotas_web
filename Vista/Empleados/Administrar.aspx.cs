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
    public partial class Administrar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                var settings = new Utils.Authorization() {
                    AccessType = Utils.Authorization.AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
                    RejectNonMatches = true,
                    Message = "Iniciá sesión para continuar"
                };
                Session[Utils.AUTH] = settings.ValidateSession(this);
                CargarDatos();
            }
        }

        public void CargarDatos() {
            bool soloActivos = !chkEstado.Checked;
            string searchquery = txtBuscar.Text;
            bool hayParaBuscar = !string.IsNullOrEmpty(searchquery);
            Response data = hayParaBuscar 
                            ? EmpleadoNegocio.FiltrarEmpleadosPorNombreCompleto(searchquery, soloActivos)
                            : EmpleadoNegocio.ObtenerEmpleados(soloActivos);
            if (!data.ErrorFound) {
                var dt = data.ObjectReturned as DataSet;
                var auth = (Session[Utils.AUTH] as Utils.SessionData);
                if (auth.User != null) {
                    var UsuarioActual = auth.User;
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                        gvAdmin.DataSource = dt;
                        gvAdmin.DataBind();
                        gvEmpleado.Visible = false;
                        gvEmpleado.Enabled = false;
                    }
                    else {
                        gvEmpleado.DataSource = dt;
                        gvEmpleado.DataBind();
                        gvAdmin.Visible = false;
                        gvAdmin.Enabled = false;

                    }
                }
            }
            else {
                Utils.MostrarMensaje($"Error. {data.Details} . {data.Message} .", this.Page, GetType());
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void lbActualUser_Click(object sender, EventArgs e) {

        }

        protected void lbIniciarSesion_Click(object sender, EventArgs e) {

        }
    }
}