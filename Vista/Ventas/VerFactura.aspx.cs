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
                } else {
                    int idFactura = Convert.ToInt32(IDFactura);
                    var res = VentaNegocio.BuscarVentaPorID(idFactura);
                    if(!res.ErrorFound) {
                        Venta obj = res.ObjectReturned as Venta;
                        Session[VK] = obj;
                        CargarCabecera(obj);
                        CargarDetalles(obj);
                    } else {
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


        protected void BtnAgregar_Click(object sender, EventArgs e) {
            if(TieneDerechosNecesarios()) {
                if (Session[VK] != null) {
                    SesionNegocio.Autenticar(op => {
                        Venta obj = Session[VK] as Venta;
                        string idProducto = txtIDProducto.Text;
                        int cantidad = Convert.ToInt32(txtCantidad.Text);
                        var res = ProductoNegocio.ObtenerPorCodigo(idProducto);
                        if (!res.ErrorFound) {
                            // Producto exists
                            Producto p = res.ObjectReturned as Producto;
                            DetalleVenta dv = new DetalleVenta() {
                                Id = obj,
                                Producto = p,
                                Proveedor = p.Proveedor,
                                Cantidad = cantidad,
                                PrecioUnitario = p.Precio,
                                PrecioTotal = cantidad * p.Precio,
                                Estado = true
                            };
                            var uploadres = DetalleVentaNegocio.AgregarDetalleVenta(dv);
                            if (!uploadres.ErrorFound) {
                                Utils.ShowSnackbar($"El producto #{dv.Producto.Codigo} se agregó correctamente. ", this.Page, GetType());
                                CargarDetalles(obj);
                                CargarCabecera(obj);
                            }
                            else {
                                Utils.ShowSnackbar($"Problema al registrar detalle. {uploadres.Details}. ", this.Page, GetType());
                            }

                        }
                        else {
                            Utils.ShowSnackbar("El producto no está disponible. ", this.Page, GetType());
                        }

                    }, err => {
                        Utils.ShowSnackbar("El token caducó, volvé a iniciar sesión. ", this.Page, GetType());
                    });
                }
            }
            else {
                Utils.ShowSnackbar("No tenés permiso para realizar esta acción. ", this.Page, GetType());
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

        protected void GvDetalles_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }



    }
}