using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using System.Data;

namespace Vista.Productos {
    public partial class Agregar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);
                CargarDDL();
            }
        }
        protected void btnVolverAtras_Click(object sender, EventArgs e) {
            Response.Redirect("/Productos/");
        }
        protected void BtnGuardar_Click(object sender, EventArgs e) {
            Insertar();
        }

        /// <summary>
        /// Carga el DropDownList, se llama a la funcion obtener IDS, los datos se guardan en el response y luego 
        /// son asignados al dataset para cargarlos al DDL
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación. </returns>
        protected void CargarDDL() {
            Response codigos = NegocioTipoDeProducto.ObtenerIDS();
            if (!codigos.ErrorFound) {
                var ds = codigos.ObjectReturned as DataSet;
                ddlTipoProducto.DataSource = ds;
                ddlTipoProducto.DataTextField = "Descripcion_TP";
                ddlTipoProducto.DataValueField = "PK_CodTipoProducto_TP";
                ddlTipoProducto.DataBind();
                ddlTipoProducto.Items.Insert(0, new ListItem("<Selecciona Tipo>", "0"));
            }
        }
        /// <summary>
        /// Manda a insertar un registro, e informa el resultado obtenido.
        /// </summary>
        protected void Insertar() {
            if (ddlTipoProducto.SelectedIndex == 0) {
                Utils.ShowSnackbar("Seleccione un valor válido para Tipo de Producto. ", this);
                return;
            }
            var auth = Session[Utils.AUTH] as SessionData;
            var producto = new Producto() {
                Codigo = txtCodigo.Text,
                Proveedor = new Proveedor() { CUIT = txtCUITProveedor.Text },
                Categoria = new TipoProducto() { Codigo = ddlTipoProducto.SelectedValue },
                Nombre = txtNombre.Text,
                Marca = txtMarca.Text,
                Descripcion = txtDescripcion.Text,
                Stock = int.Parse(txtStock.Text),
                Precio = double.Parse(txtPrecioUnitario.Text),
                Estado = true
            };
            var respuesta = ProductoNegocio.IngresarProducto(auth, producto);
            Utils.ShowSnackbar(respuesta.Message, this);
        }
    }
}