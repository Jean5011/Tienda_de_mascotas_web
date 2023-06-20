using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Data;
using Negocio;

namespace Vista.Animales
{
    public partial class AgregarAnimal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BT_Datos_Click(object sender, EventArgs e)
        {
            Animal a = new Animal();
            a.Codigo = TB_Cod.Text;
            a.Nombre = TB_Nombre.Text;
            if(TB_Raza.Text !=" ")
            {
                a.Raza = TB_Raza.Text;
            }
            else
            {
                a.Raza = "---";
            }
            NegocioAnimales na = new NegocioAnimales();
            na.IgresarAnimal(a);
        }
    }
}