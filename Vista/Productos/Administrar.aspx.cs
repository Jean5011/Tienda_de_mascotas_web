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
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                CargarGridView();
            }
        }


        public void CargarGridView() {
            ProductoNegocio ProdN = new ProductoNegocio();
            string et = txtBuscar.Text;
            Response response = string.IsNullOrEmpty(et) ? ProdN.ObtenerProductos() : ProdN.ObtenerPorCod(et);
            if (!response.ErrorFound) {
                gvDatos.DataSource = (DataSet)response.ObjectReturned;
                gvDatos.DataBind();
            }



        }
        protected void GrdProductos_RowEditing(object sender, GridViewEditEventArgs e) {
            gvDatos.EditIndex = e.NewEditIndex;
            CargarGridView();
        }
        protected void GrdProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            gvDatos.EditIndex = -1;
            CargarGridView();
        }
        protected bool EsAdmin() {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            return UsuarioActual.Rol == Empleado.Roles.ADMIN;
        }
        protected void GrdProductos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            String Cod = ((Label)gvDatos.Rows[e.RowIndex].FindControl("lbl_eit_codigo")).Text;
            String Prov = ((TextBox)gvDatos.Rows[e.RowIndex].FindControl("txt_eit_Prov")).Text;
            String Tipo = ((TextBox)gvDatos.Rows[e.RowIndex].FindControl("txt_eit_Tipo")).Text;
            String Nombre = ((TextBox)gvDatos.Rows[e.RowIndex].FindControl("txt_eit_Nombre")).Text;
            String Marca = ((TextBox)gvDatos.Rows[e.RowIndex].FindControl("txt_eit_Marca")).Text;
            String Desc = ((TextBox)gvDatos.Rows[e.RowIndex].FindControl("txt_eit_Desc")).Text;
            String Stck = ((TextBox)gvDatos.Rows[e.RowIndex].FindControl("txt_eit_Stock")).Text;
            String Precio = ((TextBox)gvDatos.Rows[e.RowIndex].FindControl("txt_eit_Precio")).Text;
            //bool Estado = ((CheckBox)grdProductos.Rows[e.RowIndex].FindControl("chk_eit_estado")).Checked;


            //VARIABLE PARA CONVERTIR EL STRING A DOUBLE de precio
            if (double.TryParse(Precio, out double PrecioDouble)) {
                //VARIABLE PARA CONVERTIR EL STRING A INT de stock
                if (int.TryParse(Stck, out int Sto)) {
                    Producto prod = new Producto() {
                        Codigo = Cod,
                        Proveedor = new Proveedor() { CUIT = Prov },
                        Categoria = new TipoProducto() { tipoDeProducto = Tipo },
                        Nombre = Nombre,
                        Marca = Marca,
                        Descripcion = Desc,
                        Stock = Sto,
                        Precio = PrecioDouble,
                        //Estado = Estado
                    };
                    //lbl_mensaje_error.Text = Cod + " - " + Prov + " - " + Tipo + " - " + Nombre + " - " + Marca + " - " + Desc + " - " + Sto + " - " + Img + " - " + PrecioDouble + " - " + Estado;

                    if (!EsAdmin()) {
                        AuthorizationVista.GoLogin(this, new Authorization() {
                            Message = "Ingresá como administrador para realizar cambios. "
                        });
                        return;
                    }
                    SesionNegocio.Autenticar((res) => {
                        Response response = ProductoNegocio.ActualizarProducto(prod);
                        if (!response.ErrorFound) {
                            gvDatos.EditIndex = -1;
                            CargarGridView();
                            Utils.MostrarMensaje("Se actualizó correctamente el producto. ", this.Page, GetType());
                            //Mostrar cartel de producto actualizado correctamente
                        }
                        else {
                            Utils.MostrarMensaje(response.Message, this.Page, GetType());
                            gvDatos.EditIndex = -1;
                        }
                    }, (err) => {
                        Utils.MostrarMensaje("Venció el token. Volvé a iniciar sesión para continuar. ", this.Page, GetType());
                    });

                    CargarGridView();
                }
                else {
                    gvDatos.EditIndex = -1;
                    CargarGridView();
                    Utils.MostrarMensaje("Error al convertir Stock. ", this.Page, GetType());
                    //NO SE PUDO CONVERTIR EL STOCK A INT, MOSTRAR MENSAJE DE ERROR "STOCK INGRESADO NO VALIDO"
                }
            }
            else {
                gvDatos.EditIndex = -1;
                CargarGridView();
                Utils.MostrarMensaje("Error al convertir Precio. ", this.Page, GetType());
                // No se pudo convertir a double. Mostrar mensaje de error
            }

        }
        protected void GrdProductos_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            if(!EsAdmin()) {
                AuthorizationVista.GoLogin(this, new Authorization() {
                    Message = "Ingresá como administrador para realizar cambios. "
                });
                return;
            }
            String Cod = ((Label)gvDatos.Rows[e.RowIndex].FindControl("lbl_it_codigo")).Text;

            Producto prod = new Producto {
                Codigo = Cod
            };
            Response response = ProductoNegocio.EliminarProducto(prod);
            if (!response.ErrorFound) {
                Utils.MostrarMensaje("Producto eliminado correctamente. ", this.Page, GetType());
                CargarGridView();
            }
            else {
                Utils.MostrarMensaje("Err. " + response.Message, this.Page, GetType());
            }
        }
        protected void GrdProductos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvDatos.PageIndex = e.NewPageIndex;
            CargarGridView();
        } 
        protected void BtnBuscar_Click(object sender, EventArgs e) {
            CargarGridView();
        }


        public void CargarDatos() {
            CargarGridView();
        }
        protected void GvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            //gvDatos.PageIndex = e.NewPageIndex;
            //CargarDatos();

            //guardamos el nuevo indice
            int newPageIndex = e.NewPageIndex;
            //nos fijamos de que no pueda acceder a una pagina inexistente
            if (newPageIndex >= 0 && newPageIndex < gvDatos.PageCount)
            {
                //cargamos el nuevo indice
                gvDatos.PageIndex = newPageIndex;
                //cargamos datos
                CargarDatos();
            }
        }

        protected void GvDatos_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("gvDatosPagerPageTxtBox") is TextBox txtPagerTextBox) {
                    txtPagerTextBox.Text = (gvDatos.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager) {
                    ddlPager.SelectedValue = gvDatos.PageSize + "";
                }
            }
        }
        protected void GvDatosPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvDatos.PageCount - 1) {
                gvDatos.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvDatos.PageIndex + "";
            }
        }

        protected void DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvDatos.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void GvDatos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }




    }



}