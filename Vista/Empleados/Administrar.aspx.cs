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
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else {
                Utils.MostrarMensaje($"Error. {data.Details} . {data.Message} .", this.Page, GetType());
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }
    }
}