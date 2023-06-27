using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Data;
using Negocio;

namespace Vista.Animales {
    public partial class Agregar : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

            }
        }

        protected void BT_Datos_Click(object sender, EventArgs e) {
            SesionNegocio.Autenticar((success) => { // Sólo autenticamos porque esta es una página Admin-only
                Animal obj = new Animal() {
                    Codigo = TB_Cod.Text,
                    Nombre = TB_Nombre.Text
                };
                if (string.IsNullOrWhiteSpace(TB_Raza.Text)) {
                    obj.Raza = TB_Raza.Text;
                }
                else obj.Raza = "---";
                Response operacion = NegocioAnimales.IngresarAnimal(obj);
                if (!operacion.ErrorFound) {
                    Utils.MostrarMensaje($"Error. {operacion.Message}. {operacion.Details}. ", this.Page, GetType());
                }
                else {
                    Utils.MostrarMensaje("Se agregó con éxito. ", this.Page, GetType());
                }
            }, (error) => {
                Utils.MostrarMensaje("El token caducó. Volvé a iniciar sesión para continuar. ", this.Page, GetType());
                btnGuardarCambios.Visible = false;
                btnGuardarCambios.Enabled = false;
            });
        }
    }
}