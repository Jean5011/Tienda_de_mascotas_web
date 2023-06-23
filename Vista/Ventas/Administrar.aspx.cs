using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Ventas {
    public partial class Administrar : System.Web.UI.Page {

        public void CargarDatos() {
            string tbuscar = txtBuscar.Text;
            var res = tbuscar == "" ? VentaNegocio.GetVentas() : VentaNegocio.GetVentaPorID(Convert.ToInt32(tbuscar));
            if(res.ErrorFound) {
                Utils.MostrarMensaje("Error cargando ventas. ", this.Page, GetType());

            } else {
                DataSet dt = res.ObjectReturned as DataSet;
                gvVentas.DataSource = dt;
                gvVentas.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                var settings = new Utils.Authorization() {
                    AccessType = Utils.Authorization.AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
                    RejectNonMatches = true,
                    Message = "Iniciá sesión para acceder al historial de ventas. "
                };

                Session[Utils.AUTH] = settings.ValidateSession(this);

                var auth = Session[Utils.AUTH] as Utils.SessionData;
                var UsuarioActual = auth.User;

                CargarDatos();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }
    }
}