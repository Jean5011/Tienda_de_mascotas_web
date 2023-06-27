using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using Negocio;
using System.Web.UI.WebControls;
using System.Web;

namespace Vista.Animales {
    public partial class Administrar : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);



                CargarDatos();
            }
        }

        protected void GV_Datos_SelectedIndexChanged(object sender, EventArgs e) {

        }

        public void CargarDatos(bool restartEditIndex = true) {
            string tbuscar = txtBuscar.Text;
            bool cargarTodo = tbuscar == "";
            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = cargarTodo ? nt.GetAnimales() : nt.ObtenerPorCod(tbuscar);
            if (!resultado.ErrorFound) {
                DataSet dt = resultado.ObjectReturned as DataSet;
                if(restartEditIndex) GV_Datos.EditIndex = -1;
                GV_Datos.DataSource = dt;
                GV_Datos.DataBind();
            } else {
                Utils.ShowSnackbar("Error cargando los registros. ", this, GetType());
            }
        }

        public void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();

            
        }


        protected void GV_Datos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(res => {
                    Animal a = new Animal();
                    a.Codigo = ((Label)GV_Datos.Rows[e.RowIndex].FindControl("LV_Cod_Animal")).Text;
                    NegocioAnimales nt = new NegocioAnimales();
                    nt.EliminarAnimal(a);
                    CargarDatos();
                }, err => {
                    Utils.ShowSnackbar("El Token caducó. Iniciá sesión de nuevo.", this, GetType());
                });

            } else {
                Utils.ShowSnackbar("No disponés de los permisos suficientes para realizar esta acción. ", this, GetType());
            }
        }

        protected void GV_Datos_RowEditing(object sender, GridViewEditEventArgs e) {
            GV_Datos.EditIndex = e.NewEditIndex;
            CargarDatos(false);
        }

        protected void GV_Datos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            CargarDatos();
        }

        protected void GV_Datos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            var auth = Session[Utils.AUTH] as SessionData;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(res => {
                    Animal a = new Animal();
                    a.Codigo = ((Label)GV_Datos.Rows[e.RowIndex].FindControl("LV_EditCod")).Text;
                    a.Nombre = ((TextBox)GV_Datos.Rows[e.RowIndex].FindControl("TB_EditNombre")).Text;
                    a.Raza = ((TextBox)GV_Datos.Rows[e.RowIndex].FindControl("TB_EditRaza")).Text;
                    ////////////////////////////////////////////////////////////////////////////////
                    NegocioAnimales nt = new NegocioAnimales();
                    nt.ActualizarAnimal(a);
                    CargarDatos();
                }, err => {
                    Utils.ShowSnackbar("El token caducó. Deberás iniciar sesión de nuevo. ", this, GetType());
                });
            } else {
                Utils.ShowSnackbar("No disponés de los permisos suficientes para realizar esta acción. ", this, GetType());
            }
        }
    }
}