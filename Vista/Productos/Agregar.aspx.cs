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
            if (!codigos.ErrorFound)
            {
                ds = codigos.ObjectReturned as DataSet;
                ddlTipoProducto.DataSource = ds;
                int startingIndex = 1; // Posición de inicio para cargar los datos

                // Cargar los datos a partir de la posición 1
                for (int i = startingIndex; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    ddlTipoProducto.Items.Add(new ListItem(row["Descripcion_TP"].ToString(), row["PK_CodTipoProducto_TP"].ToString()));
                }
            }
            //ddlTipoProducto.Items.Insert(0, new ListItem("Seleccionar Tipo", "0"));

        }
        protected void vaciarCampos()
        {
            txtCodigo.Text = "";
            txtCUITProveedor.Text = "";
            txtNombre.Text = "";
            txtMarca.Text = "";
            txtDescripcion.Text = "";
            txtPrecioUnitario.Text = "";
            txtStock.Text = "";
            ddlTipoProducto.SelectedIndex = 0;
        }
        protected void RellenarVectorProductoNuevo(bool[] productoNuevo)
        {
            Response existeProducto = ProductoNegocio.VerificarExistenciaProducto(txtCodigo.Text);
            if (!existeProducto.ErrorFound)
            {
                DataSet dt = existeProducto.ObjectReturned as DataSet;
                int cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["Cantidad"]);
                productoNuevo[0] = cantidad == 0;
            }
            Response existeProveedor = ProductoNegocio.VerificarExistenciaProveedor(txtCUITProveedor.Text);
            if (!existeProveedor.ErrorFound)
            {
                DataSet dt = existeProveedor.ObjectReturned as DataSet;
                int cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["Cantidad"]);
                productoNuevo[1] = cantidad == 1;
            }
 
        }
        bool validarCamposProducto(bool[] productoNuevo, int tam)
        {
            List<string> messages = new List<string>();
            messages.Add("Error, el codigo de producto ingresado ya existe.");
            messages.Add("Error, el cuit de proveedor ingresado no existe.");
            bool pNew = true;
            for (int i = 0; i < tam; i++)
            {
                if (productoNuevo[i] == false)
                {
                    Utils.MostrarMensaje(messages[i], this.Page, GetType());
                    pNew = false;
                }
            }
            return pNew;
        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            const int tam = 2;
            bool[] productoNuevo = new bool[tam] { false, false };

            SesionNegocio.Autenticar(res =>
            {
                RellenarVectorProductoNuevo(productoNuevo);
                if (validarCamposProducto(productoNuevo, tam))
                {
                    Producto Prod = new Producto()
                    {
                        Codigo = txtCodigo.Text,
                        Proveedor = new Proveedor() { CUIT = txtCUITProveedor.Text },
                        Categoria = new TipoProducto() { Codigo = ddlTipoProducto.SelectedValue },
                        Nombre = txtNombre.Text,
                        Marca = txtMarca.Text,
                        Descripcion = txtDescripcion.Text,
                        Stock = int.Parse(txtStock.Text),
                        Precio = double.Parse(txtPrecioUnitario.Text),
                        Estado = true,
                    };
                    Response response = ProductoNegocio.IngresarProducto(Prod);
                    if (!response.ErrorFound)
                    {
                        vaciarCampos();
                        Utils.MostrarMensaje($"Producto guardado correctamente. ", this.Page, GetType());
                    }
                    else
                    {
                        string error = response.Message;
                        Utils.MostrarMensaje(error, this.Page, GetType());
                    }
                }

            }, err =>
            {
                Utils.ShowSnackbar("El token caducó. Volvé a iniciar sesión. ", this.Page, GetType());
            });
        }

        protected void BtnVolverAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Productos/Administrar.aspx");

        }


    }
}