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
    public partial class Administrar : System.Web.UI.Page {

        public void CargarDatos(Response data = null) {
            Empleado emp = new Empleado { DNI = ddlEmpleados.SelectedValue };
            var filtros = new Venta.Busqueda
            {
                Texto = txtBuscar.Text,
                EmpleadoGestor = emp,
                TipoPago = ddlTipoDePago.SelectedValue
            };

            data = VentaNegocio.cargarRegistros(filtros);
            if (!data.ErrorFound)
            {
                DataSet ds = data.ObjectReturned as DataSet;
                gvDatos.DataSource = ds;
                gvDatos.DataBind();
            }
            else
            {
                Utils.ShowSnackbar("Error cargando ventas. ", this.Page);
            }
        }
        protected void cargarDDLEmpleados()
        {
            Response emp = EmpleadoNegocio.CargarDuo();
            if(!emp.ErrorFound) {
                ddlEmpleados.DataSource = emp.ObjectReturned as DataSet;
                ddlEmpleados.DataTextField = Empleado.Columns.Nombre;
                ddlEmpleados.DataValueField = Empleado.Columns.DNI;
                ddlEmpleados.DataBind();
                ddlEmpleados.Items.Insert(0, new ListItem("Todos los empleados", "ALL"));
            }
        }
        protected void cargarDLLTipoDePago()
        {
            ddlTipoDePago.Items.Insert(0, new ListItem("Todos los medios de pago", "ALL"));
            ddlTipoDePago.Items.Add(new ListItem("Tarjeta de Credito", "Credit_Card"));
            ddlTipoDePago.Items.Add(new ListItem("Tarjeta de Debito", "Debit_Card"));
            ddlTipoDePago.Items.Add(new ListItem("BitCoin", "BTC"));
            ddlTipoDePago.Items.Add(new ListItem("Ethereum", "ETH"));
            ddlTipoDePago.Items.Add(new ListItem("Mercado Pago", "Mercado_Pago"));
            ddlTipoDePago.Items.Add(new ListItem("Efectivo", "Efectivo"));
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                CargarDatos();
                cargarDDLEmpleados();
                cargarDLLTipoDePago();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }

        protected void GvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            int newPageIndex = e.NewPageIndex;
            if (newPageIndex >= 0 && newPageIndex < gvDatos.PageCount) {
                gvDatos.PageIndex = newPageIndex;
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
    }
}