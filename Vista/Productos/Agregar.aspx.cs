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
                CargarDDL();
            }
        }

        /// <summary>
        /// Carga el DropDownList, se llama a la funcion obtener IDS, los datos se guardan en el response y luego 
        /// son asignados al dataset para cargarlos al DDL
        /// </summary>
        /// <returns>Objeto Response con el resultado de la operación. </returns>
        protected void CargarDDL()
        {

            Response codigos = NegocioTipoDeProducto.ObtenerIDS();
            DataSet ds = new DataSet();
            if(!codigos.ErrorFound)
            {
                ds = codigos.ObjectReturned as DataSet;
                ddlTipoProducto.DataSource= ds;
                ddlTipoProducto.DataTextField = "Descripcion_TP";
                ddlTipoProducto.DataValueField = "PK_CodTipoProducto_TP";
                ddlTipoProducto.DataBind();
                ddlTipoProducto.Items.Insert(0, new ListItem("<Selecciona Tipo>", "0"));
            }
           
        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            SesionNegocio.Autenticar(res =>
            {

                Response existe = ProductoNegocio.VerificarExiste(txtCodigo.Text);
                int cantidad;
                if (!existe.ErrorFound)
                {
                    DataSet dt = existe.ObjectReturned as DataSet;
                    cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["Cantidad"]);

                    if (cantidad > 0)
                    {   //Si es mayor a 0 significa que el producto existe


                        Utils.MostrarMensaje($"El codigo de producto ingresado ya existe. ", this.Page, GetType());

                    }
                    else
                    {
                        if (ddlTipoProducto.SelectedIndex == 0)
                        {
                            Utils.MostrarMensaje($"Seleccione un tipo de producto. ", this.Page, GetType());
                        }
                        else
                        {
                            //si encontre error significa que el producto no existe asi que se puede continuar con la creacion
                            string numero = txtPrecioUnitario.Text;
                            if (double.TryParse(numero, out double Pre))
                            {
                                string stock = txtStock.Text;
                                if (int.TryParse(stock, out int st))
                                {
                                    Producto Prod = new Producto()
                                    {
                                        Codigo = txtCodigo.Text,
                                        Proveedor = new Proveedor() { CUIT = txtCUITProveedor.Text },
                                        Categoria = new TipoProducto() { Codigo = ddlTipoProducto.SelectedValue },
                                        Nombre = txtNombre.Text,
                                        Marca = txtMarca.Text,
                                        Descripcion = txtDescripcion.Text,
                                        Stock = st,
                                        Precio = Pre,
                                        Estado = true,
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
                                }
                                else
                                {
                                    Utils.MostrarMensaje($"El stock ingresado no es valido. ", this.Page, GetType());
                                }
                            }
                            else
                            {
                                Utils.MostrarMensaje($"El precio ingresado no es valido. ", this.Page, GetType());
                            }
                        }
                    }
                }
            }, err => {
                Utils.ShowSnackbar("El token caducó. Volvé a iniciar sesión. ", this.Page, GetType());
            });
        }

        protected void btnVolverAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Productos/Administrar.aspx");

        }
    }
}