using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Globalization;

namespace Vista.Ventas {
    public partial class Crear : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                SessionData auth = Session[Utils.AUTH] as SessionData;
                Empleado em = auth.User;
                DateTime fechaHora = DateTime.Now;
                string fecha = fechaHora.ToString("yyyy-MM-dd");
                string hora = fechaHora.ToString("HH:mm");
                txtFecha.Text = fecha;
                txtHora.Text = hora;
                txtMedio.Focus();
                adLabel.Text = em.Nombre + " " + em.Apellido + " es el gestor.";
                CargarDDL();
            }
        }

        protected void CargarDDL()
        {   
            ddlMedioPago.Items.Insert(0, new ListItem("<Selecciona Tipo>", "0"));
            ddlMedioPago.Items.Add(new ListItem("Tarjeta de Credito", "Credit_Card"));
            ddlMedioPago.Items.Add(new ListItem("Tarjeta de Debito", "Debit_Card"));
            ddlMedioPago.Items.Add(new ListItem("BitCoin", "BTC"));
            ddlMedioPago.Items.Add(new ListItem("Ethereum", "ETH"));
            ddlMedioPago.Items.Add(new ListItem("Mercado Pago", "Mercado_Pago"));
            ddlMedioPago.Items.Add(new ListItem("Efectivo", "Efectivo"));
        }


        protected void BtnGuardarCambios_Click(object sender, EventArgs e) {
            SesionNegocio.Autenticar((data) => {
                // Enviar los datos y recibir el ID y el AFFECTEDROWS
                string ff = $"{txtFecha.Text} {txtHora.Text}";
                DateTime fn = DateTime.ParseExact(ff, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                var auth = Session[Utils.AUTH] as SessionData;
                var emp = auth.User;
                if (ddlMedioPago.SelectedIndex == 0)
                {
                    Utils.ShowSnackbar("Seleccione un Metodo de pago valido. ", this);
                    return;
                }
                Venta obj = new Venta() {
                    EmpleadoGestor = emp,
                    TipoPago = ddlMedioPago.SelectedValue,
                    Fecha = fn.ToString("yyyy-MM-dd HH:mm"),
                    Total = 0
                };
                var res = VentaNegocio.IniciarVenta(obj);
                if(!res.ErrorFound) {
                    var vp = res.ObjectReturned as Venta.Preliminar;
                    Utils.MostrarMensaje("Código de venta asignado: #" + vp.Id, this.Page, GetType());
                    Response.Redirect($"/Ventas/VerFactura.aspx?ID={vp.Id}");
                    /// Redirigir a Administrar Venta #x
                } else {
                    Utils.MostrarMensaje("Error: " + res.Details, this.Page, GetType());
                }
            }, (err) => {
                Utils.MostrarMensaje("El token caducó. Volvé a iniciar sesión para continuar. ", this.Page, GetType());
            });        
        }

    }
}