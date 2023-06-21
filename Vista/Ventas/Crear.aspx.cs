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
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                Utils.CargarSesion(this, true, "Primero tenés que iniciar sesión. ");
                DateTime fechaHora = DateTime.Now;
                string fecha = fechaHora.ToString("yyyy-MM-dd");
                string hora = fechaHora.ToString("HH:mm");
                txtFecha.Text = fecha;
                txtMedio.Focus();
                txtHora.Text = hora;
                var em = Session[Utils.actualUser] as Empleado;
                adLabel.Text = em.Nombre + " " + em.Apellido + " es el gestor.";

            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e) {
            SesionNegocio.Autenticar((data) => {
                // Enviar los datos y recibir el ID y el AFFECTEDROWS
                DateTime fn = DateTime.ParseExact(txtFecha.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var emp = Session[Utils.actualUser] as Empleado;
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