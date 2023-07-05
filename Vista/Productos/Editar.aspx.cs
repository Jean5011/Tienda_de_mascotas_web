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
                    cargarCamposProducto(userId);
                }

            }
        }
        protected void btnVolverAtras_Click(object sender, EventArgs e) {
            Response.Redirect("/Productos/");
        }
        protected void BtnGuardar_Click(object sender, EventArgs e) {
            GuardarCambios();
        }

        protected void cargarCamposProducto(string cod) {
            var res = ProductoNegocio.ObtenerPorCodigo(cod);
            if (!res.ErrorFound) {
                Producto producto = res.ObjectReturned as Producto;
                txtNombre.Text = producto.Nombre;
                // txtTipoProducto.Text = producto.Categoria.Codigo;
                txtDescripcion.Text = producto.Descripcion;
                txtMarca.Text = producto.Marca;
                txtStock.Text = (producto.Stock).ToString();
                txtPrecioUnitario.Text = (producto.Precio).ToString();
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
                Categoria = new TipoProducto() { Codigo = productoViejo.Categoria.Codigo },
                Descripcion = txtDescripcion.Text,
                Marca = txtMarca.Text,
                Stock = int.Parse(txtStock.Text),
                Precio = double.Parse(txtPrecioUnitario.Text),
                Proveedor = productoViejo.Proveedor,
                Estado = productoViejo.Estado
            };
            var res = ProductoNegocio.ActualizarProducto(auth, producto);
            Utils.ShowSnackbar(res.Message, this);
        }
        protected void GuardarCambios() {
            if (Request.QueryString["ID"] != null) {
                string cod = Request.QueryString["ID"];
                var res = ProductoNegocio.ObtenerPorCodigo(cod);
                if (!res.ErrorFound) {
                    ActualizaProducto((Producto)res.ObjectReturned);
                }

            }
        }

    }
}