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
namespace Vista.Animales
{
    public partial class AltaAnimal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NegocioAnimales a = new NegocioAnimales();
            Response resultado = a.GetAnimalesBaja();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Animal.DataSource = dt;
            GV_Animal.DataBind();

        }

        protected void BT_Filtrar_Click(object sender, EventArgs e)
        {
            Animal aa = new Animal();
            aa.Codigo = TB_Animal.Text;
            NegocioAnimales a = new NegocioAnimales();
            Response resultado = a.ObtenerPorCodBaja(aa);
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Animal.DataSource = dt;
            GV_Animal.DataBind();
        }

        protected void GV_Animal_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void GV_Animal_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Animal aa = new Animal();
            aa.Codigo = ((Label)GV_Animal.Rows[e.NewSelectedIndex].FindControl("Lv_Cod")).Text;
            NegocioAnimales a = new NegocioAnimales();
            Response resultado = a.AltaAnimal(aa);
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Animal.DataSource = dt;
            GV_Animal.DataBind();
        }
    }
}