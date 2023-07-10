using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Productos {
    // TODO: ELIMINAR EN OTRA WEBFORM
    public partial class Editar1 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (IsPostBack == false) {
                // Página accesible para administradores.
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

                if (Request.QueryString["ID"] != null) {
                    string userId = Request.QueryString["ID"];
                    CargarDDL();
                    CargarCamposProducto(userId);

                }

            }
        }
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
        protected void BtnVolverAtras_Click(object sender, EventArgs e) {
            Response.Redirect("/Productos/");
        }
        protected void BtnGuardar_Click(object sender, EventArgs e) {
            if (Request.QueryString["ID"] != null) {
                string cod = Request.QueryString["ID"];
                var res = ProductoNegocio.ObtenerPorCodigo(cod);
                if (ddlTipoProducto.SelectedIndex == 0) {
                    Utils.ShowSnackbar("Seleccione un valor válido para Tipo de Producto. ", this);
                    return;
                }
                if (!res.ErrorFound) {
                    ActualizaProducto((Producto)res.ObjectReturned);
                }

            }
        }

        protected void CargarCamposProducto(string cod) {
            var res = ProductoNegocio.ObtenerPorCodigo(cod);
            if (!res.ErrorFound) {
                Producto producto = res.ObjectReturned as Producto;
                txtNombre.Text = producto.Nombre;
                txtCUITProveedor.Text = producto.Proveedor.CUIT;
                txtDescripcion.Text = producto.Descripcion;
                txtMarca.Text = producto.Marca;
                txtStock.Text = (producto.Stock).ToString();
                txtPrecioUnitario.Text = (producto.Precio).ToString();
                int indice = ddlTipoProducto.Items.IndexOf(ddlTipoProducto.Items.FindByValue(producto.Categoria.Codigo));
                ddlTipoProducto.SelectedIndex = indice;
            }

        }
        protected void EliminaProducto(string cod) {
            SessionData auth = Session[Utils.AUTH] as SessionData;
            var res = ProductoNegocio.EliminarProducto(auth, new Producto() { Codigo = cod });
            Utils.ShowSnackbar(res.Message, this);
        }
        protected void ActualizaProducto(Producto productoViejo) {
            var auth = Session[Utils.AUTH] as SessionData;
            var producto = new Producto() {
                Codigo = productoViejo.Codigo,
                Nombre = txtNombre.Text,
                Categoria = new TipoProducto() { Codigo = ddlTipoProducto.SelectedValue },
                Descripcion = txtDescripcion.Text,
                Marca = txtMarca.Text,
                Stock = int.Parse(txtStock.Text),
                Precio = double.Parse(txtPrecioUnitario.Text),
                Proveedor = new Proveedor() { CUIT = txtCUITProveedor.Text },
                Estado = productoViejo.Estado
            };
            var res = ProductoNegocio.ActualizarProducto(auth, producto);
            Utils.ShowSnackbar(res.Message, this);
        }


    }
}