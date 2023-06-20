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

namespace Vista.Tipos
{
    public partial class VerTipoDeProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BT_Filtrar_Click(object sender, EventArgs e)
        {
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            Response resultado = nt.ObtenerPorCod(TB_Filtrar.Text);
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }

        protected void BT_Todo_Click(object sender, EventArgs e)
        {
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            Response resultado = nt.GetTipoDeProducto();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }
    }
}