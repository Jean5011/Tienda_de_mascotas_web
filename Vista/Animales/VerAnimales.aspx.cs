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
    public partial class VerAnimales : System.Web.UI.Page {
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
                bool inicioSesion = Utils.CargarSesion(this, false);
                CargarDatos();
            }
        }

        protected void GV_Datos_SelectedIndexChanged(object sender, EventArgs e) {

        }

        public void CargarDatos() {
            string tbuscar = txtBuscar.Text;
            bool cargarTodo = tbuscar == "";
            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = cargarTodo ? nt.GetAnimales() : nt.ObtenerPorCod(tbuscar);
            if (!resultado.ErrorFound) {
                DataSet dt = resultado.ObjectReturned as DataSet;
                GV_Datos.DataSource = dt;
                GV_Datos.DataBind();
            } else {
                Utils.MostrarMensaje("Error cargando los registros. ", this.Page, GetType());
            }
        }

        public void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();

            
        }


        protected void GV_Datos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e) {
            Animal a = new Animal();
            a.Codigo = ((Label)GV_Datos.Rows[e.RowIndex].FindControl("LV_Cod_Animal")).Text;
            NegocioAnimales nt = new NegocioAnimales();
            nt.EliminarAnimal(a);
            Response resultado = nt.GetAnimales();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }

        protected void GV_Datos_RowEditing(object sender, GridViewEditEventArgs e) {
            GV_Datos.EditIndex = e.NewEditIndex;
            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = nt.GetAnimales();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }

        protected void GV_Datos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GV_Datos.EditIndex = -1;
            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = nt.GetAnimales();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }

        protected void GV_Datos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            Animal a = new Animal();
            a.Codigo = ((Label)GV_Datos.Rows[e.RowIndex].FindControl("LV_EditCod")).Text;
            a.Nombre = ((TextBox)GV_Datos.Rows[e.RowIndex].FindControl("TB_EditNombre")).Text;
            a.Raza = ((TextBox)GV_Datos.Rows[e.RowIndex].FindControl("TB_EditRaza")).Text;
            ////////////////////////////////////////////////////////////////////////////////
            NegocioAnimales nt = new NegocioAnimales();
            nt.ActualizarAnimal(a);
            Response resultado = nt.GetAnimales();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }
    }
}