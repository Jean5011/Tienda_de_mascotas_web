using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vista.Empleados {
    public partial class IniciarSesion : System.Web.UI.Page {
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                string msg = Request.QueryString["msg"];
                if(!string.IsNullOrEmpty(msg)) {
                    Utils.MostrarMensaje(msg, this.Page, GetType());
                }
                bool haySesion = Utils.CargarSesion(this, false);
                
                if(haySesion) {
                    Empleado actual = Session[Utils.actualUser] as Empleado;
                    lblResultado.Text = ($"Si cambiás de cuenta, se cerrará tu sesión actual.");
                }
                lbIniciarSesion.Visible = false;
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e) {
            string dni = txtDNI.Text;
            string clave = txtClave.Text;
            Response res = EmpleadoNegocio.IniciarSesion(dni, clave);
            if(!res.ErrorFound) {
                var buscar_empleado = SesionNegocio.ObtenerDatosEmpleadoActual();
                if(!buscar_empleado.ErrorFound) {
                    var emp = buscar_empleado.ObjectReturned as Empleado;
                    string wm = $"¡Bienvenido, {emp.Nombre}!";
                    Utils.MostrarMensaje(wm, this.Page, GetType());
                    string goNext = Request.QueryString["next"];
                    if(!string.IsNullOrEmpty(goNext)) {
                        Response.Redirect(HttpUtility.UrlDecode(goNext));
                    }
                }
            }

        }
    }
}