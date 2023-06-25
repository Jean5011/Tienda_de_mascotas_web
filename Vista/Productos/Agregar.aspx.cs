using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Productos {
    public partial class Agregar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                var settings = new Utils.Authorization() {
                    AccessType = Utils.Authorization.AccessLevel.ONLY_LOGGED_IN_ADMIN,
                    RejectNonMatches = true,
                    Message = "Ingresá como administrador para agregar productos. "
                };

                Session[Utils.AUTH] = settings.ValidateSession(this);

                var auth = Session[Utils.AUTH] as Utils.SessionData;
                var UsuarioActual = auth.User;

            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as Utils.SessionData;
            var UsuarioActual = auth.User;
            SesionNegocio.Autenticar(res => {
                string numero = txtPrecioUnitario.Text;
                double Pre;
                if (double.TryParse(numero, out Pre)) {
                    string stock = txtStock.Text;
                    int st;
                    if (int.TryParse(stock, out st)) {
                        Producto Prod = new Producto() {
                            Codigo = txtID.Text,
                            Proveedor = new Proveedor() { CUIT = txtCUITProveedor.Text },
                            Categoria = new TipoProducto() { tipoDeProducto = txtTipoProducto.Text },
                            Nombre = txtNombre.Text,
                            Marca = txtMarca.Text,
                            Descripcion = txtDescripcion.Text,
                            Stock = st,
                            Imagen = txtURLImagen.Text,
                            Precio = Pre,
                            Estado = true,
                        };
                        Response response = ProductoNegocio.IngresarProducto(Prod);
                        if (!response.ErrorFound) {
                            Utils.MostrarMensaje($"Producto guardado correctamente. ", this.Page, GetType());
                        }
                        else {
                            //Utils.MostrarMensaje($"Error al guardar producto. ", this.Page, GetType());
                            string error = response.Message;
                            Utils.MostrarMensaje(error, this.Page, GetType());
                        }
                    }
                    else {
                        Utils.MostrarMensaje($"El stock ingresado no es valido. ", this.Page, GetType());
                    }
                }
                else {
                    Utils.MostrarMensaje($"El precio ingresado no es valido. ", this.Page, GetType());
                }
            }, err => {
                Utils.ShowSnackbar("El token caducó. Volvé a iniciar sesión. ", this.Page, GetType());
            });
        }

    }
}