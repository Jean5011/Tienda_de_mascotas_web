using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vista.Empleados {
    public partial class Administrar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataTable dt = GetDataTable(); 
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
        }

        private DataTable GetDataTable() {
            DataTable dt = new DataTable();
            dt.Columns.Add("DNI");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido");
            dt.Columns.Add("Sexo");
            dt.Columns.Add("Sueldo");
            dt.Columns.Add("Dirección");
            dt.Columns.Add("Estado");
            dt.Rows.Add("45 000 001", "Axel Tomás", "Barrientos", "M", "$ 345.000", "Alberdi 638, Tigre, Buenos Aires, Argentina (B1648)", "En servicio");
            dt.Rows.Add("45 000 002", "Ana María de los Dolores", "Buscaroli de Musicardi", "F", "$ 405.000", "Chile 1300, Tigre, Buenos Aires, Argentina (B1648)", "En servicio");
            dt.Rows.Add("45 000 003", "Héctor", "Da Silva", "M", "$ 3.645.000", "Lisboa 5069, Tigre, Buenos Aires, Argentina (B1648)", "No disponible");
            dt.Rows.Add("45 000 001", "Horacio", "Suárez", "M", "$ 345.000", "Lavalle 1200, Tigre, Buenos Aires, Argentina (B1648)", "En servicio");
            dt.Rows.Add("45 000 002", "Juan Alberto", "Pérez García", "M", "$ 270.000", "Albarellos 500, Tigre, Buenos Aires, Argentina (B1648)", "En servicio");

            return dt;
        }

    }
}