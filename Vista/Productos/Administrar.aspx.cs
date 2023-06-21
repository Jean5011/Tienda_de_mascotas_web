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
    public partial class Editar : System.Web.UI.Page {
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                bool inicioSesion = Utils.CargarSesion(this, false);
                cargarGridView();
            }
        }


        public void cargarGridView() {
            ProductoNegocio ProdN = new ProductoNegocio();
            string et = txtBuscar.Text;
            Response response = string.IsNullOrEmpty(et) ? ProdN.ObtenerProductos() : ProdN.ObtenerPorCod(et);
            if (!response.ErrorFound) {
                grdProductos.DataSource = (DataSet)response.ObjectReturned;
                grdProductos.DataBind();
            }



        }

        protected void grdProductos_RowEditing(object sender, GridViewEditEventArgs e) {
            grdProductos.EditIndex = e.NewEditIndex;
            cargarGridView();
        }

        protected void grdProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            grdProductos.EditIndex = -1;
            cargarGridView();
        }
        protected bool EsAdmin() {
            var UsuarioActual = Session[Utils.actualUser] as Empleado;
            return UsuarioActual.Rol == Empleado.Roles.ADMIN;
        }

        protected void grdProductos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            String Cod = ((Label)grdProductos.Rows[e.RowIndex].FindControl("lbl_eit_codigo")).Text;
            String Prov = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Prov")).Text;
            String Tipo = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Tipo")).Text;
            String Nombre = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Nombre")).Text;
            String Marca = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Marca")).Text;
            String Desc = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Desc")).Text;
            String Stck = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Stock")).Text;
            String Img = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Imagen")).Text;
            String Precio = ((TextBox)grdProductos.Rows[e.RowIndex].FindControl("txt_eit_Precio")).Text;
            //bool Estado = ((CheckBox)grdProductos.Rows[e.RowIndex].FindControl("chk_eit_estado")).Checked;


            double PrecioDouble;//VARIABLE PARA CONVERTIR EL STRING A DOUBLE de precio


            if (double.TryParse(Precio, out PrecioDouble)) {
                int Sto;//VARIABLE PARA CONVERTIR EL STRING A INT de stock
                if (int.TryParse(Stck, out Sto)) {
                    Producto prod = new Producto() {
                        Codigo = Cod,
                        Proveedor = new Proveedor() { CUIT = Prov },
                        Categoria = new TipoProducto() { tipoDeProducto = Tipo },
                        Nombre = Nombre,
                        Marca = Marca,
                        Descripcion = Desc,
                        Stock = Sto,
                        Imagen = Img,
                        Precio = PrecioDouble,
                        //Estado = Estado
                    };
                    //lbl_mensaje_error.Text = Cod + " - " + Prov + " - " + Tipo + " - " + Nombre + " - " + Marca + " - " + Desc + " - " + Sto + " - " + Img + " - " + PrecioDouble + " - " + Estado;
                    
                    if(!EsAdmin()) {
                        Utils.MostrarMensaje("Iniciá sesión con una cuenta de administrador para realizar cambios. ", this.Page, GetType());
                        return;
                    }
                    SesionNegocio.Autenticar((res) => {
                        Response response = ProductoNegocio.ActualizarProducto(prod);
                        if (!response.ErrorFound) {
                            grdProductos.EditIndex = -1;
                            cargarGridView();
                            Utils.MostrarMensaje("Se actualizó correctamente el producto. ", this.Page, GetType());
                            //Mostrar cartel de producto actualizado correctamente
                        }
                        else {
                            Utils.MostrarMensaje(response.Message, this.Page, GetType());
                            grdProductos.EditIndex = -1;
                        }
                    }, (err) => {
                        Utils.MostrarMensaje("Venció el token. Volvé a iniciar sesión para continuar. ", this.Page, GetType());
                    });
                    
                    
                }
                else {
                    grdProductos.EditIndex = -1;
                    cargarGridView();
                    lbl_mensaje_error.Text = "ERROR AL CONVERTIR STOCK";
                    //NO SE PUDO CONVERTIR EL STOCK A INT, MOSTRAR MENSAJE DE ERROR "STOCK INGRESADO NO VALIDO"
                }
            }
            else {
                grdProductos.EditIndex = -1;
                cargarGridView();
                lbl_mensaje_error.Text = "ERROR AL CONVERTIR PRECIO";
                // No se pudo convertir a double. Mostrar mensaje de error
            }

        }

        protected void grdProductos_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            String Cod = ((Label)grdProductos.Rows[e.RowIndex].FindControl("lbl_it_codigo")).Text;

            Producto prod = new Producto();
            prod.Codigo = Cod;
            //Response response3 = DetalleVentaNegocio.EliminarDV(aca mandas el otro codigo)
            //Response response2 = VentaNegocio.EliminarVenta(aca mandas el codigo);
            Response response = ProductoNegocio.EliminarProducto(prod);
            if (!response.ErrorFound) {
                lbl_mensaje_error.Text = "Producto eliminado correctamente";
                //mensaje de producto eliminado exitosamente
                cargarGridView();
            }
            else {
                lbl_mensaje_error.Text = response.Message;
                //mensaje de error al eliminar producto
            }
        }

        protected void grdProductos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            grdProductos.PageIndex = e.NewPageIndex;
            cargarGridView();
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            cargarGridView();
        }
    }



}