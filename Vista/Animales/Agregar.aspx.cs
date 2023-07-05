using System;
using Entidades;
using Negocio;

namespace Vista.Animales {
    public partial class Agregar : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                // Página accesible sólo para administradores.
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

            }
        }

        protected void BT_Datos_Click(object sender, EventArgs e) {
            Ingresar();
        }

        /// <summary>
        /// Manda a ingresar un registro e informa el resultado obtenido.
        /// </summary>
        protected void Ingresar() {
            var auth = Session[Utils.AUTH] as SessionData;
            var animal = new Animal() {
                Codigo = TB_Cod.Text,
                Nombre = TB_Nombre.Text,
                Raza = string.IsNullOrWhiteSpace(TB_Raza.Text) ? TB_Raza.Text : "---"
            };

            var respuesta = NegocioAnimales.IngresarAnimal(auth, animal);
            
            if(respuesta.ErrorFound) {
                btnGuardarCambios.Visible = false;
                btnGuardarCambios.Enabled = false;
            }

            Utils.ShowSnackbar(respuesta.Message, this);
            
        }

    }
}