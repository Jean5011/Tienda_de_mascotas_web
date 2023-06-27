using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
namespace Vista.Productos
{
    public partial class Editar1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack==false)
            {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                CargarGridView();
            }
        }
        public void CargarGridView()
        {
            ProductoNegocio ProdN = new ProductoNegocio();

            Response response = ProdN.ObtenerProductos();
            if (!response.ErrorFound)
            {
                Tabla_Productos_Gdv.DataSource = (DataSet)response.ObjectReturned;
                Tabla_Productos_Gdv.DataBind();
            }



        }

        protected void Tabla_Productos_Gdv_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRowIndex = Tabla_Productos_Gdv.SelectedIndex;
            GridViewRow selectedRow = Tabla_Productos_Gdv.Rows[selectedRowIndex];
            String Cod = ((Label)selectedRow.FindControl("Codigo_Prod_lb")).Text;
            String Prov = ((TextBox)selectedRow.FindControl("CUITProv_tb")).Text;
            String Tipo = ((TextBox)selectedRow.FindControl("CodTipoProducto_tb")).Text;
            String Nombre = ((TextBox)selectedRow.FindControl("Nombre_tb")).Text;
            String Marca = ((TextBox)selectedRow.FindControl("Marca_tb")).Text;
            String Desc = ((TextBox)selectedRow.FindControl("Descripcion_tb")).Text;
            String Stck = ((TextBox)selectedRow.FindControl("Stock_tb")).Text;
            String Precio = ((TextBox)selectedRow.FindControl("Precio_tb")).Text;
            var resProductoActualizado = ProductoNegocio.ActualizarProducto(new Producto()
            {
                Codigo = Cod,
                Proveedor = new Proveedor() { CUIT = Prov },
                Categoria = new TipoProducto() { tipoDeProducto = Tipo },
                Nombre = Nombre,
                Marca = Marca,
                Descripcion = Desc,
                Stock = int.Parse(Stck),
                Precio = double.Parse(Precio),

            });
            if (!resProductoActualizado.ErrorFound)
            {
                Utils.MostrarMensaje("El Producto con el código"+ Cod+" actualizado correctamente!. ", this.Page, GetType());
            }
            else
            {
                Utils.MostrarMensaje("Hubo un error, no se puedo actualizar el producto.", this.Page, GetType());
            }
            Label1.Text = resProductoActualizado.Details;
            // Hacer algo con el ID (por ejemplo, mostrarlo en una etiqueta o redirigir a otra página)
        }
    }
}