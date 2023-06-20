using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Entidades;
using Negocio;

namespace Vista.Tipos
{
    public partial class VerTipoDePrductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NegocioAnimales nt = new NegocioAnimales();
            Response resultado = nt.GettAnimales();
            DataSet dt = resultado.ObjectReturned as DataSet;
            DD_Animal.DataSource = dt;
            DD_Animal.DataTextField = "Animal_A";/// NombreDeRaza_An";
            DD_Animal.DataValueField = "PK_CodAnimales_An";
            DD_Animal.DataBind();
 
        }

        protected void BT_Guardar_Click(object sender, EventArgs e)
        {
            TipoProducto t = new TipoProducto();
            t.Codigo = TB_cod.Text;
            t.tipoDeProducto = DD_Tpd.SelectedValue;
            t.CodAnimal = DD_Animal.SelectedValue;
            t.Descripcion = TB_Descripcion.Text;
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            nt.IgresarTipoDeProducto(t);
        }
    }
}