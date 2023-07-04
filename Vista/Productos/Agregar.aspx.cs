using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using System.Data;

namespace Vista.Productos
{
    public partial class Agregar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_ADMINS_STRICT);

            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            SesionNegocio.Autenticar(res =>
            {
                string cadena = txtPrecioUnitario.Text;

                // Eliminar las comas de la cadena
                string valorSinComas = cadena.Replace(",", "");

                // Convertir la cadena a tipo "decimal"
                decimal money = decimal.Parse(valorSinComas);
                Producto Prod = new Producto()
                {
                    Codigo = txtCodigo.Text,
                    Nombre = txtNombre.Text,
                    Categoria = new TipoProducto() { Codigo = txtTipoProducto.Text },
                    Descripcion = txtDescripcion.Text,
                    Marca = txtMarca.Text,
                    Stock = int.Parse(txtStock.Text),
                    Precio = double.Parse(cadena),
                    Proveedor = new Proveedor() { CUIT = txtCUITProveedor.Text },
                    Estado = true
                };
                Response response = ProductoNegocio.IngresarProducto(Prod);
                if (!response.ErrorFound)
                {
                    Utils.MostrarMensaje($"Producto guardado correctamente. ", this.Page, GetType());
                }
                else
                {
                    //Utils.MostrarMensaje($"Error al guardar producto. ", this.Page, GetType());
                    string error = response.Message;
                    Utils.MostrarMensaje(error, this.Page, GetType());
                }
            }, err =>
            {
                Utils.ShowSnackbar("El token caducó. Volvé a iniciar sesión. ", this.Page, GetType());
            });
        }

        protected void btnVolverAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Productos/Administrar.aspx");

        }
    }
}