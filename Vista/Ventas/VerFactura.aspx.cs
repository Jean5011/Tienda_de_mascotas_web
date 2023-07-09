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
        public void CargarCabecera() {
            string IDFactura = Request.QueryString["ID"];
            if(!string.IsNullOrEmpty(IDFactura)) {
                int idFactura = Convert.ToInt32(IDFactura);
                var res = VentaNegocio.BuscarVentaPorID(idFactura);
                if (!res.ErrorFound && res.ObjectReturned != null) {
                    Venta obj = res.ObjectReturned as Venta;
                    Session[VK] = obj;
                    lblEmpleadoGestor.Text = obj.EmpleadoGestor.DNI;
                    lblFechaRegistro.Text = obj.Fecha;
                    lblMedioPago.Text = obj.TipoPago;
                    lblTotalCalculado.Text = $"${obj.Total}";
                }
            }
                
        }
        public void CargarDetalles(Venta obj) {
            int id = obj.Id;
            var res = DetalleVentaNegocio.ObtenerDetalleVenta(id);
            if (!res.ErrorFound) {
                DataSet data = res.ObjectReturned as DataSet;
                gvDetalles.DataSource = data;
                gvDetalles.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                string IDFactura = Request.QueryString["ID"];
                if (string.IsNullOrEmpty(IDFactura)) {
                    Utils.ShowSnackbar("No hay código de factura", this.Page, GetType());
                    BtnBorrar.Visible = false;
                    BtnBorrar.Enabled = false;
                }
                else {
                    int idFactura = Convert.ToInt32(IDFactura);
                    var res = VentaNegocio.BuscarVentaPorID(idFactura);
                    if (!res.ErrorFound && res.ObjectReturned != null) {
                        Venta obj = res.ObjectReturned as Venta;
                        Session[VK] = obj;
                        CargarCabecera();
                        CargarDetalles(obj);
                        CargarDDL();
                    }
                    else {
                        BtnBorrar.Visible = false;
                        BtnBorrar.Enabled = false;
                        Utils.ShowSnackbar("Error." + res.Details + ". " + res.Message, this.Page, GetType());
                    }
                }
            }
        }


        protected void CargarDDL()
        {
            Response codigos = ProductoNegocio.ListarActivosSinRepetir();
            if (!codigos.ErrorFound)
            {
                DataSet ds = codigos.ObjectReturned as DataSet;
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string codigo = row["CodProducto_Prod"].ToString();
                    string nombre = row["Nombre_Prod"].ToString();
                 
                    ddlProducto.Items.Add(new ListItem(nombre, codigo));
                }
                ddlProducto.Items.Insert(0, new ListItem("<Seleccione un producto>", "0"));
            }
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {   //enviamos al negocio producto el codigo del producto seleccionado ya que este devolvera los cuits de los proveedores que tenga disponible
                Response codigos = ProductoNegocio.BuscarPorCodigo(ddlProducto.SelectedValue);
                if (!codigos.ErrorFound)
                {
                    ddlProveedor.Items.Clear();
                    var cuits = codigos.ObjectReturned as DataSet;
                    if (cuits.Tables.Count > 0)
                    {   //asigno los cuits al datatable
                        DataTable dt = cuits.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {   //por cada fila se carga el DDL
                            string CUIT = row["CUITProveedor_Prod"].ToString();
                            Response response = ProveedorNegocio.ObtenerProveedorByCUIT(CUIT);

                            if (!response.ErrorFound)
                            {
                                var provs = response.ObjectReturned as DataSet;
                                DataTable dtProvs = provs.Tables[0];
                                foreach (DataRow row2 in dtProvs.Rows)
                                {
                                    string nombre = row2["RazonSocial_Prov"].ToString();
                                    string cuit = row2["CUIT_Prov"].ToString();

                                    ddlProveedor.Items.Add(new ListItem(nombre, cuit));
                                }
                            }
                        }
                    }

                }
         
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
            var prod = new Producto { Codigo = ddlProducto.SelectedValue,
                                      Proveedor = new Proveedor { CUIT=ddlProveedor.SelectedValue}
                                    };
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            if (Session[VK] != null) {
                var respuesta = VentaNegocio.AgregarProducto(auth, obj, prod, cantidad);
                Utils.ShowSnackbar(respuesta.Message, this);
                if (!respuesta.ErrorFound) {
                    CargarCabecera();
                    CargarDetalles(obj);
                }
            }



        }

        protected void GVDETALLESBTNELIMINAR_Command(object sender, CommandEventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            Venta obj = Session[VK] as Venta;
            var det = new DetalleVenta {
                Id = obj,
                Producto = new Producto { Codigo = e.CommandArgument.ToString() }
            };
            if (e.CommandName == "ELIMINAR") {
                var respuesta = DetalleVentaNegocio.EliminarDetalle(auth, det);
                Utils.ShowSnackbar(respuesta.Message, this.Page, GetType());
                CargarDetalles(obj);
                CargarCabecera();
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

        protected void modificarCantidadVendida_Command(object sender, CommandEventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            DetalleVenta detalle = new DetalleVenta { 
                Id = Session[VK] as Venta,
                Producto = new Producto() {
                    Codigo = e.CommandArgument.ToString()
                }
            };
            var respuesta = DetalleVentaNegocio.ModificarCantidad(auth, detalle, e.CommandName);
            Utils.ShowSnackbar(respuesta.Message, this);
            CargarCabecera();
            CargarDatos();
        }
    }
}