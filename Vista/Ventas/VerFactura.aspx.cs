using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Ventas {
    public partial class VerFactura : System.Web.UI.Page {
        public const string VK = "VK";
        public void CargarCabecera(Venta obj) {
            lblEmpleadoGestor.Text = obj.EmpleadoGestor.DNI;
            lblFechaRegistro.Text = obj.Fecha;
            lblMedioPago.Text = obj.TipoPago;
            lblTotalCalculado.Text = $"${obj.Total}";
        }
        public void CargarDetalles(Venta obj) {
            int id = obj.Id;
            var res = DetalleVentaNegocio.ObtenerDetalleVenta(id);
            if(!res.ErrorFound) {
                DataSet data = res.ObjectReturned as DataSet;
                gvDetalles.DataSource = data;
                gvDetalles.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                string IDFactura = Request.QueryString["ID"];
                if(string.IsNullOrEmpty(IDFactura)) {
                    Utils.ShowSnackbar("No hay código de factura", this.Page, GetType());
                    BtnBorrar.Visible = false;
                    BtnBorrar.Enabled = false;
                } else {
                    int idFactura = Convert.ToInt32(IDFactura);
                    var res = VentaNegocio.BuscarVentaPorID(idFactura);
                    if(!res.ErrorFound && res.ObjectReturned != null) {
                        Venta obj = res.ObjectReturned as Venta;
                        Session[VK] = obj;
                        CargarCabecera(obj);
                        CargarDetalles(obj);
                    } else {
                        BtnBorrar.Visible = false;
                        BtnBorrar.Enabled = false;
                        Utils.ShowSnackbar("Error 32. " + res.Details + ". " + res.Message, this.Page, GetType());
                    }
                }
            }
        }

        protected bool TieneDerechosNecesarios() {
            var auth = Session[Utils.AUTH] as SessionData;
            if (Session[VK] == null) return false;
            return auth.User.Rol == Empleado.Roles.ADMIN || auth.User.DNI == (Session[VK] as Venta).EmpleadoGestor.DNI;

        }

        public void BtnBorrar_Click(object sender, EventArgs e) {
            Borrar();
        }

        public void Borrar() {
            var venta = Session[VK] as Venta;
            Response.Redirect("/Venta/Eliminar.aspx?ID=" + venta.Id);

        }

        protected void BtnAgregar_Click(object sender, EventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            Venta obj = Session[VK] as Venta;
            var prod = new Producto { Codigo = txtIDProducto.Text };
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            if(Session[VK] != null) {
                var respuesta = VentaNegocio.AgregarProducto(auth, obj, prod, cantidad);
                Utils.ShowSnackbar(respuesta.Message, this);
                if(!respuesta.ErrorFound) {
                    CargarDetalles(obj);
                }
            }



        }

        protected void GVDETALLESBTNELIMINAR_Command(object sender, CommandEventArgs e) {
            if(TieneDerechosNecesarios()) {
                SesionNegocio.Autenticar(rs => {
                    Venta obj = Session[VK] as Venta;
                    int idVenta = obj.Id;
                    if (e.CommandName == "ELIMINAR") {
                        string idProducto = e.CommandArgument.ToString();
                        var res = DetalleVentaNegocio.EliminarDetalle(idVenta, idProducto);
                        if (!res.ErrorFound) {
                            Utils.ShowSnackbar("Se eliminó el producto en cuestión de la compra. ", this.Page, GetType());
                            CargarDetalles(obj);
                            CargarCabecera(obj);
                        }
                        else Utils.ShowSnackbar("No se borró. ", this.Page, GetType());
                    }
                }, err => {
                    Utils.ShowSnackbar("Tu token caducó, volvé a iniciar sesión. ", this.Page, GetType());
                });
            } else {
                Utils.ShowSnackbar("No tenés permiso para realizar esta acción. ", this.Page, GetType());
            }
        }


        public void CargarDatos() {
            Venta obj = Session[VK] as Venta;
            CargarDetalles(obj);
        }

        protected void GvDetalles_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvDetalles.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void GvDetalles_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("gvDetallesPagerPageTxtBox") is TextBox txtPagerTextBox) {
                    txtPagerTextBox.Text = (gvDetalles.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager) {
                    ddlPager.SelectedValue = gvDetalles.PageSize + "";
                }
            }
        }
        protected void GvDetallesPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvDetalles.PageCount - 1) {
                gvDetalles.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvDetalles.PageIndex + "";
            }
        }

        protected void DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvDetalles.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }

        protected void modificarCantidadVendida_Command(object sender, CommandEventArgs e)
        {
            if (TieneDerechosNecesarios())
            {
                SesionNegocio.Autenticar(rs => {
                    Venta obj = Session[VK] as Venta;
                    int idVenta = obj.Id;
                    
                    string codigoProducto = e.CommandArgument.ToString();
                    Producto prod = new Producto()
                    {
                        Codigo = codigoProducto
                    };

                    var res = DetalleVentaNegocio.ObtenerDetalleVenta(idVenta);
                    if (!res.ErrorFound)
                    {
                        DataSet dsDetalleVenta = res.ObjectReturned as DataSet;
                        var resultado = DetalleVentaNegocio.obtenerRegistro(dsDetalleVenta, prod, obj); // el error está en esta función.
                        if (resultado != null)
                        {
                            DetalleVenta dv = resultado;
                            switch (e.CommandName)
                            {
                                case "Restar":
                                    if (!DetalleVentaNegocio.disminuirCantidadVendida(dv).ErrorFound) { CargarDetalles(obj); }
                                    else { Utils.ShowSnackbar("No es posible disminuir la cantidad vendida. ", this.Page); }
                                break;
                                case "Sumar":
                                    if (!DetalleVentaNegocio.aumentarCantidadVendida(dv).ErrorFound) { CargarDetalles(obj); }
                                    else { Utils.ShowSnackbar("No es posible aumentar la cantidad vendida. ", this.Page); }
                                break;
                            }
                        }
                        else { Utils.ShowSnackbar("No es posible obtener el registro del detalle de la venta. ", this.Page); }
                    }
                    else { Utils.ShowSnackbar("No es posible obtener el detalle de la venta. ", this.Page); }
                }, err => { 
                    Utils.ShowSnackbar("Tu token caducó, volvé a iniciar sesión. ", this.Page); 
                });
            } 
            else { Utils.ShowSnackbar("No tenés permiso para realizar esta acción. ", this.Page); }
        }
    }
}