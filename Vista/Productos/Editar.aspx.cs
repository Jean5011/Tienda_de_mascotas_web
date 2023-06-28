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
            if (IsPostBack == false)
            {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                if (Request.QueryString["ID"] != null)
                {
                    string userId = Request.QueryString["ID"];
                    cargarCamposProducto(userId);
                }

            }
        }

        protected void cargarCamposProducto(string cod)
        {
            var res = ProductoNegocio.ObtenerPorCodigo(cod);
            if (!res.ErrorFound)
            {
                Producto producto = res.ObjectReturned as Producto;
                txtNombre.Text = producto.Nombre;
                txtTipoProducto.Text = producto.Categoria.Codigo;
                txtDescripcion.Text = producto.Descripcion;
                txtMarca.Text = producto.Marca;
                txtStock.Text = (producto.Stock).ToString();
                txtPrecioUnitario.Text = (producto.Precio).ToString();
            }

        }
        protected void EliminaProducto(string cod)
        {
            Response resProductoEliminado = ProductoNegocio.EliminarProducto(new Producto() { Codigo = cod });
            if (!resProductoEliminado.ErrorFound)
            {
                Utils.MostrarMensaje("El Producto con el código " + cod + " fue eliminado!. ", this.Page, GetType());
            }
            else
            {
                Utils.MostrarMensaje("Hubo un error, no se puedo eliminar el producto.", this.Page, GetType());
            }
        }
        protected void ActualizaProducto(Producto productoViejo )
        {
            Producto p = new Producto()
            {
                Codigo = productoViejo.Codigo,
                Nombre = txtNombre.Text,
                Categoria = new TipoProducto() { Codigo = txtTipoProducto.Text },
                Descripcion = txtDescripcion.Text,
                Marca = txtMarca.Text,
                Stock = int.Parse(txtStock.Text),
                Precio = int.Parse(txtPrecioUnitario.Text),
                Proveedor=productoViejo.Proveedor,
                Estado= productoViejo.Estado

            };
            Response resProductoEliminado = ProductoNegocio.ActualizarProducto(p);
            if (!resProductoEliminado.ErrorFound)
            {
                Utils.MostrarMensaje("El Producto con el código " + productoViejo.Codigo + " fue actualizado!. ", this.Page, GetType());
            }
            else
            {
                Utils.MostrarMensaje("Hubo un error, no se puedo actualizar el producto.", this.Page, GetType());
            }
            

        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                string cod = Request.QueryString["ID"];
                var res = ProductoNegocio.ObtenerPorCodigo(cod);
                if (DesactivarProducto.Checked)
                {
                    EliminaProducto(cod);
                }
                else
                {
                    if(!res.ErrorFound) ActualizaProducto((Producto)res.ObjectReturned);

                }
            }

        }

        protected void btnVolverAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Productos/Administrar.aspx");
        }

    }
}