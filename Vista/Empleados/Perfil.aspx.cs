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
        private Empleado PerfilActual;

        protected void RellenarDatos() {
            NombreEmpleadoTitulo.InnerText = PerfilActual.Nombre + " " + PerfilActual.Apellido;
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                string dni_empleado = Request.QueryString["DNI"];
                Response res_b = string.IsNullOrEmpty(dni_empleado)
                                ? SesionNegocio.ObtenerDatosEmpleadoActual() 
                                : EmpleadoNegocio.BuscarEmpleadoPorDNI(dni_empleado);
                PerfilActual = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;

                RellenarDatos();

            }
        }
    }
}