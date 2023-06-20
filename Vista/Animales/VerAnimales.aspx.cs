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

namespace Vista.Animales
{
    public partial class VerAnimales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GV_Datos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BT_Todos_Click(object sender, EventArgs e)
        {
            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = nt.GetAnimales();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }

        protected void BT_Filtrar_Click(object sender, EventArgs e)
        {
            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = nt.ObtenerPorCod(TB_Filtrar.Text);
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }

        protected void GV_Datos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            Animal a = new Animal();
            a.Codigo = ((Label)GV_Datos.Rows[e.RowIndex].FindControl("LV_Cod_Animal")).Text;
            NegocioAnimales nt = new NegocioAnimales();
            nt.EliminarAnimal(a);
            Response resultado = nt.GetAnimales();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }
    }
}