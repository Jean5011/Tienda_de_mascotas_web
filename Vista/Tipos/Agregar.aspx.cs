using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Negocio;
using Entidades;

namespace Vista.Tipos {
    public partial class Agregar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;

                CargarDDL();
            }
        }

        protected void CargarDDL() {

            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = nt.GettAnimales();
            DataSet dt = resultado.ObjectReturned as DataSet;
            DD_Animal.DataSource = dt;
            DD_Animal.DataTextField = Animal.Columns.Nombre;/// NombreDeRaza_An";
            DD_Animal.DataValueField = Animal.Columns.Codigo;
            DD_Animal.DataBind();
        }

        protected void BT_Guardar_Click(object sender, EventArgs e) {
            SesionNegocio.Autenticar(res => {
                TipoProducto t = new TipoProducto();
                t.Codigo = TB_cod.Text;
                t.tipoDeProducto = DD_Tpd.SelectedValue;
                t.CodAnimal = DD_Animal.SelectedValue;
                t.Descripcion = TB_Descripcion.Text;
                NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
                nt.IgresarTipoDeProducto(t);

            }, err => {
                Utils.ShowSnackbar("El token caducó, volvé a iniciar sesión. ", this, GetType());
            });
        }
    }
}