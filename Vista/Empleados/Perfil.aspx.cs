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
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                var tk = new SesionNegocio();
                var empleadoActual = tk.ObtenerDatosEmpleadoActual();
                if (!empleadoActual.ErrorFound) {
                    var empleado = empleadoActual.ObjectReturned as Empleado;
                    Label1.Text = empleado.Nombre + " " + empleado.Apellido + " es el usuario activo";
                } else {
                    Label1.Text = "No hay usuario activo / Problemas leyendo los datos.";
                }
            }
        }
    }
}