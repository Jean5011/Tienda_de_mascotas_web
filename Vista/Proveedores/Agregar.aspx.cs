using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Datos;
namespace Vista.Proveedores {
    public partial class Agregar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            /*Connection connection = new Connection("Pets");
            Response response = connection.FetchData("SELECT * FROM Proveedores");

            if (!response.ErrorFound)
            {
                DataSet dataSet = response.ObjectReturned as DataSet;

                // Asigna el DataSet al DataSource del GridView
                GridView1.DataSource = dataSet.Tables["root"];

                // Enlaza los datos al GridView
                GridView1.DataBind();
            }
            else
            {
                Label1.Text = response.Message+ "/"+response.Details;
                // Maneja el error en caso de que ocurra
                Console.WriteLine("Error al obtener datos de la base de datos: " + response.Message);
            }
            */
        }
    }
}