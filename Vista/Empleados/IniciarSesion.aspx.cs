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

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                var auth = AuthorizationVista.ValidateSession(this, Authorization.NO_RESTRICTIONS);


                txtDNI.Focus();

                string msg = Request.QueryString["msg"];

                if(!string.IsNullOrEmpty(msg)) {
                    Utils.MostrarMensaje(msg, this.Page, GetType());
                }


                
                if(auth.User != null) {
                    lblResultado.Text = ($"Si cambiás de cuenta, se cerrará tu sesión actual.");
                }
            }
        }

        protected void BtnIniciarSesion_Click(object sender, EventArgs e) {
            string dni = txtDNI.Text;
            string clave = txtClave.Text;
            Response res = EmpleadoNegocio.IniciarSesion(dni, clave);
            if(!res.ErrorFound) {
                var buscar_empleado = SesionNegocio.ObtenerDatosEmpleadoActual();
                if(!buscar_empleado.ErrorFound) {
                    var emp = buscar_empleado.ObjectReturned as Empleado;
                    string wm = $"¡Bienvenido, {emp.Nombre}!";
                    Utils.ShowSnackbar(wm, this, GetType());
                    string goNext = Request.QueryString["next"];
                    if (!string.IsNullOrEmpty(goNext)) {
                        Response.Redirect(HttpUtility.UrlDecode(goNext));
                    }
                    else Response.Redirect("/Index.aspx");
                }
            } else {
                string err = res.Message;
                string mes = err == "NO_ROWS" ? "El DNI ingresado no existe o está deshabilitado. " : (err == "INCORRECT_DATA" ? "La clave ingresada es incorrecta. " : "Error desconocido. " + err);
                Utils.ShowSnackbar(mes, this, GetType());
            }

        }
    }
}