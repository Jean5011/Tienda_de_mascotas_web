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
        private readonly string actualUser = Utils.actualUser;
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                bool inicioSesion = Utils.CargarSesion(this, false);
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
                var UsuarioActual = Session[Utils.actualUser] as Empleado;
                if(UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                    gvAdmin.DataSource = dt;
                    gvAdmin.DataBind();
                    gvEmpleado.Visible = false;
                    gvEmpleado.Enabled = false;
                } else {
                    gvEmpleado.DataSource = dt;
                    gvEmpleado.DataBind();
                    gvAdmin.Visible = false;
                    gvAdmin.Enabled = false;

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