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

        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e) {
            string dni = txtDNI.Text;
            string clave = txtClave.Text;
            EmpleadoNegocio obj = new EmpleadoNegocio();
            Response res = obj.IniciarSesion(dni, clave);
            Label1.Text = res.Message;

        }
    }
}