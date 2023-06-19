using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Productos {
    public partial class Agregar : System.Web.UI.Page {
        private Empleado UsuarioActual; // Objeto que luego de CargarSesion() tendrá los datos del empleado activo.
        /// <summary>
        /// Se fija si se inició sesión y recupera los datos del empleado en cuestión.
        /// </summary>
        /// <returns>True si inició sesión, False si no hay sesión o si hubo un problema.</returns>
        protected bool CargarSesion() {
            Response res_b = SesionNegocio.ObtenerDatosEmpleadoActual();
            if (res_b.ErrorFound) {
                if (res_b.Message == SesionNegocio.ErrorCode.NO_SESSION_FOUND) {
                    // No inició sesión, mandamos al usuario a que se loguee, y le decimos que luego vuelva.
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
        protected void Page_Load(object sender, EventArgs e) {
            bool inicioSesion = CargarSesion();
            if (inicioSesion) {
                if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                    // El usuario logueado es un ADMINISTRADOR con derecho a crear y borrar productos.
                    // Si llegó acá está todo bien.
                }
                else {
                    // El usuario logueado es un CAJERO / EMPLEADO COMÚN , no tiene derecho a crear y borrar productos.
                    Utils.MostrarMensaje($"No tenés permiso para ver esta página. ", this.Page, GetType());
                    // Despedilo, bajale el sueldo, bardealo, hacé lo que quieras.
                    // Yo lo mandaría de nuevo a la página principal.

                }
            } 
        }
    }
}