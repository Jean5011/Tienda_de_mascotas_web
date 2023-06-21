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
    public partial class Editar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGridView();
            }
        }


        public void cargarGridView()
        {
            ProductoNegocio ProdN = new ProductoNegocio();
            Response response = ProdN.ObtenerProductos();
            if (!response.ErrorFound)
            {
                grdProductos.DataSource = (DataSet)response.ObjectReturned;
                grdProductos.DataBind();
            }



        }

        protected void grdProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProductos.EditIndex = e.NewEditIndex;
            cargarGridView();
        }

        protected void grdProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProductos.EditIndex = -1;
            cargarGridView();
        }

        protected void grdProductos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String Cod = ((Label)grdProductos.Rows[e.RowIndex].FindControl("lbl_eit_codigo")).Text;
            String Prov = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Prov")).Text;
            String Tipo = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Tipo")).Text;
            String Nombre = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Nombre")).Text;
            String Marca = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Marca")).Text;
            String Desc = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Desc")).Text;
            String Stck = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Stock")).Text;
            String Img = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Imagen")).Text;
            String Precio = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Precio")).Text;
            bool Estado = ((CheckBox)grdProductos.Rows[e.RowIndex].FindControl("chk_eit_estado")).Checked;

            
            double PrecioDouble;//VARIABLE PARA CONVERTIR EL STRING A DOUBLE de precio
           

            if (double.TryParse(Precio, out PrecioDouble))
            {
                int Sto;//VARIABLE PARA CONVERTIR EL STRING A INT de stock
                if (int.TryParse(Stck, out Sto))
                {
                    Producto prod = new Producto()
                    {
                        Codigo = Cod,
                        Proveedor = new Proveedor() { CUIT = Prov },
                        Categoria = new TipoProducto() { tipoDeProducto = Tipo },
                        Nombre = Nombre,
                        Marca = Marca,
                        Descripcion = Desc,
                        Stock = Sto,
                        Imagen = Img,
                        Precio = PrecioDouble,
                        Estado = Estado
                    };
                    //lbl_mensaje_error.Text = Cod + " - " + Prov + " - " + Tipo + " - " + Nombre + " - " + Marca + " - " + Desc + " - " + Sto + " - " + Img + " - " + PrecioDouble + " - " + Estado;
                    Response response = ProductoNegocio.ActualizarProducto(prod);
                    if (!response.ErrorFound)
                    {
                        grdProductos.EditIndex = -1;
                        cargarGridView();
                        //lbl_mensaje_error.Text = "PRODUCTO ACTUALIZADO CORRECTAMENTE" ;
                        //Mostrar cartel de producto actualizado correctamente
                    }
                    else
                    {
                        lbl_mensaje_error.Text = response.Message;
                        grdProductos.EditIndex = -1;
                    }
                }
                else
                {
                    grdProductos.EditIndex = -1;
                    cargarGridView();
                    lbl_mensaje_error.Text = "ERROR AL CONVERTIR STOCK";
                    //NO SE PUDO CONVERTIR EL STOCK A INT, MOSTRAR MENSAJE DE ERROR "STOCK INGRESADO NO VALIDO"
                }
            }
            else
            {
                grdProductos.EditIndex = -1;
                cargarGridView();
                lbl_mensaje_error.Text = "ERROR AL CONVERTIR PRECIO";
                // No se pudo convertir a double. Mostrar mensaje de error
            }
         
        }
    }


   
}