using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Empleados {
    public partial class AdministrarAccesos : System.Web.UI.Page {
        private readonly string editingUser = "Usuario_Perfil";
        protected bool CargarPerfil() {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            string dni_empleado = Request.QueryString["DNI"];
            if (string.IsNullOrEmpty(dni_empleado)) {
                if (!string.IsNullOrEmpty(UsuarioActual.DNI)) {
                    dni_empleado = UsuarioActual.DNI;
                }
                else return false;
            }
            Response res_b = EmpleadoNegocio.BuscarEmpleadoPorDNI(dni_empleado);
            if (res_b.ErrorFound) {
                return false;
            }
            Session[editingUser] = res_b.ErrorFound ? null : res_b.ObjectReturned as Empleado;
            return true;
        }
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                // Página accesible para empleados y administradores.
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                var auth = Session[Utils.AUTH] as SessionData;

                // Verificamos que el empleado esté revisando sus propias sesiones, o que sea administrador.
                bool cargoPerfil = CargarPerfil();
                if (auth.Granted && cargoPerfil) {
                    var UsuarioActual = auth.User;
                    var UsuarioPerfil = Session[editingUser] as Empleado;
                    if (UsuarioActual.Rol == Empleado.Roles.ADMIN || UsuarioActual.DNI == UsuarioPerfil.DNI) {
                        CargarDatos();
                    }
                    else {
                        AuthorizationVista.GoLogin(this, new Authorization() {
                            Message = "No podés acceder al historial de sesiones de otra persona. "
                        });
                    }
                }
            }
        }

        public void CargarDatos() {
            var UsuarioPerfil = Session[editingUser] as Empleado;
            string DNI = UsuarioPerfil.DNI;
            var res = SesionNegocio.ObtenerSesionesAbiertasDeEmpleado(DNI);
            if(!res.ErrorFound) {
                DataSet dt = res.ObjectReturned as DataSet;
                gvAdmin.DataSource = dt;
                gvAdmin.DataBind();
            }
        }
        public void CambiarClaveClick(object sender, EventArgs e) {
            var UsuarioPerfil = Session[editingUser] as Empleado;
            string DNI = UsuarioPerfil.DNI;
            Response.Redirect($"/Empleados/CambiarClave.aspx?DNI={DNI}");

        }
        public void RevocarTodo(object sender, EventArgs e) {
            var UsuarioPerfil = Session[editingUser] as Empleado;
            Sesion obj = new Sesion() {
                Empleado = UsuarioPerfil
            };
            var respuesta = SesionNegocio.RevocarTodasLasSesiones(obj);
            if(!respuesta.ErrorFound) {
                AuthorizationVista.GoLogin(this, new Authorization() {
                    Message = "Se revocaron todos los tokens y se cerró tu sesión. "
                });
            }
            Utils.ShowSnackbar(respuesta.Message, this);
            
        }

        public void Disable(int id) {
            Sesion obj = new Sesion() {
                Codigo = id
            };
            var respuesta = SesionNegocio.RevocarSesion(obj);
            Utils.ShowSnackbar(respuesta.Message, this);
        }
        public void Enable(int id) {
            Sesion obj = new Sesion() {
                Codigo = id
            };
            var respuesta = SesionNegocio.ReabrirSesion(obj);
            Utils.ShowSnackbar(respuesta.Message, this);
        }

        protected void SwitchStatusCommand(object sender, CommandEventArgs e) {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            switch(e.CommandName) {
                case "DISABLE":
                    Disable(id);
                    break;
                case "ENABLE":
                    Enable(id);
                    break;
                default:
                    Utils.ShowSnackbar("Comando no admitido. ", this, GetType());
                    break;
            }
            CargarDatos();
        }


        protected void GvAdmin_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            int newPageIndex = e.NewPageIndex;
            if (newPageIndex >= 0 && newPageIndex < gvAdmin.PageCount) {
                gvAdmin.PageIndex = newPageIndex;
                CargarDatos();
            }
        }
        protected void GvAdmin_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Pager) {
                if (e.Row.FindControl("gvAdminPagerPageTxtBox") is TextBox txtPagerTextBox) {
                    txtPagerTextBox.Text = (gvAdmin.PageIndex + 1) + "";
                }
                if (e.Row.FindControl("ddlFilasPorPaginaPagerTemplate") is DropDownList ddlPager) {
                    ddlPager.SelectedValue = gvAdmin.PageSize + "";
                }
            }
        }
        protected void GvAdminPagerPageTxtBox_TextChanged(object sender, EventArgs e) {
            int intendedPage = int.Parse(((TextBox)sender).Text) - 1;
            if (intendedPage <= gvAdmin.PageCount - 1) {
                gvAdmin.PageIndex = intendedPage;
                CargarDatos();
            }
            else {
                ((TextBox)sender).Text = gvAdmin.PageIndex + "";
            }
        }
        protected void DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            int filasPorPaginaN = int.Parse(((DropDownList)sender).SelectedValue);
            if (filasPorPaginaN > 0) {
                gvAdmin.PageSize = filasPorPaginaN;
                CargarDatos();
            }
        }
        protected void GvAdmin_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {

        }


    }
}