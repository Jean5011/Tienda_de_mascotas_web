using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vista.Ventas {
    public partial class Eliminar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);
                string codVenta = Request.QueryString["ID"];
                if(string.IsNullOrEmpty(codVenta)) {
                    Utils.ShowSnackbar("No hay código de venta válido. ", this);
                    btnBorrar.Visible = false;
                    btnBorrar.Enabled = false;
                }
            }
        }

        protected void btnBorrar_Click(object sender, EventArgs e) {
            BorrarVenta();
        }

        protected void BorrarVenta() {
            var auth = Session[Utils.AUTH] as SessionData;
            string codVenta = Request.QueryString["ID"];
            if(string.IsNullOrEmpty(codVenta)) {
                return;
            }

            var venta = new Venta { Id = Convert.ToInt32(codVenta) };
            var respuesta = VentaNegocio.EliminarPermanentementeVentaPorID(auth, venta);
            Utils.ShowSnackbar(respuesta.Message, this);


        }
    }
}