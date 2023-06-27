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


namespace Vista.Tipos
{
    public partial class AltaTiposdeproductosaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NegocioTipoDeProducto NT = new NegocioTipoDeProducto();
            Response resultado = NT.GetTipoDeProductoBaja();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_TDP.DataSource = dt;
            GV_TDP.DataBind();
        }

        protected void BT_Filtrar_Click(object sender, EventArgs e)
        {
            TipoProducto t = new TipoProducto();
            t.Codigo = TB_TDP.Text;
            NegocioTipoDeProducto NT = new NegocioTipoDeProducto();
            Response resultado = NT.ObtenerPorCodBaja(t.Codigo);
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_TDP.DataSource = dt;
            GV_TDP.DataBind();
        }

        protected void GV_TDP_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TipoProducto t = new TipoProducto();
            t.Codigo = ((Label)GV_TDP.Rows[e.NewSelectedIndex].FindControl("Lv_Cod")).Text;
            NegocioTipoDeProducto NT = new NegocioTipoDeProducto();
            Response resultado = NT.AltaTipoDeProducto(t);
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_TDP.DataSource = dt;
            GV_TDP.DataBind();
        }


            
    }
}