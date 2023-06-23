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
                var settings = new Utils.Authorization() {
                    AccessType = Utils.Authorization.AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
                    RejectNonMatches = true,
                    Message = "Iniciá sesión para registrar una venta. "
                };

                Session[Utils.AUTH] = settings.ValidateSession(this);

                var auth = Session[Utils.AUTH] as Utils.SessionData;
                var UsuarioActual = auth.User;


                Utils.CargarSesion(this, true, "Primero tenés que iniciar sesión. ");
                DateTime fechaHora = DateTime.Now;
                string fecha = fechaHora.ToString("yyyy-MM-dd");
                txtFecha.Text = fecha;
                txtMedio.Focus();
                var em = Session[Utils.actualUser] as Empleado;
                adLabel.Text = em.Nombre + " " + em.Apellido + " es el gestor.";

            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e) {
            SesionNegocio.Autenticar((data) => {
                // Enviar los datos y recibir el ID y el AFFECTEDROWS
                DateTime fn = DateTime.ParseExact(txtFecha.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var auth = Session[Utils.AUTH] as Utils.SessionData;
                var emp = auth.User;
                Venta obj = new Venta() {
                    EmpleadoGestor = emp,
                    TipoPago = txtMedio.Text,
                    Fecha = fn.ToString("yyyy-MM-dd"),
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