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
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        public void CargarCabecera(Venta obj) {
            lblEmpleadoGestor.Text = obj.EmpleadoGestor.DNI;
            lblFechaRegistro.Text = obj.Fecha;
            lblMedioPago.Text = obj.TipoPago;
            lblTotalCalculado.Text = "Aún sin calcular";
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
            if(!IsPostBack) {
                bool inicioSesion = Utils.CargarSesion(this, false);
                string IDFactura = Request.QueryString["ID"];
                if(string.IsNullOrEmpty(IDFactura)) {
                    Utils.MostrarMensaje("No hay código de factura", this.Page, GetType());
                } else {
                    int idFactura = Convert.ToInt32(IDFactura);
                    var res = VentaNegocio.BuscarVentaPorID(idFactura);
                    if(!res.ErrorFound) {
                        Venta obj = res.ObjectReturned as Venta;
                        Session[VK] = obj;
                        CargarCabecera(obj);
                        CargarDetalles(obj);
                    } else {
                        Utils.MostrarMensaje("Error 32. " + res.Details + ". " + res.Message, this.Page, GetType());
                    }
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e) {
            if(Session[VK] != null) {
                Venta obj = Session[VK] as Venta;
                string idProducto = txtIDProducto.Text;
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                var res = ProductoNegocio.ObtenerPorCodigo(idProducto);
                if(!res.ErrorFound) {
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
                    if(!uploadres.ErrorFound) {
                        Utils.MostrarMensaje($"El producto #{dv.Id} se agregó correctamente. ", this.Page, GetType());
                        CargarDetalles(obj);
                    } else {
                        Utils.MostrarMensaje($"Problema al registrar detalle. {uploadres.Details}. ", this.Page, GetType());
                    }

                } else {
                    Utils.MostrarMensaje("El producto no está disponible. ", this.Page, GetType());
                }
            }
        }
    }
}