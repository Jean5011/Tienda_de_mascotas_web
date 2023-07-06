using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
//using Datos;
namespace Vista.Proveedores {
    public partial class Agregar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
            }
        }
        protected void VaciarTextBoxs()
        {
            Cuit_tb.Text = "";
            RazonSocial_tb.Text = "";
            NombreContacto_tb.Text = "";
            CorreoElectronico_tb.Text = "";
            NumeroTelefono_tb.Text = "";
            Direccion_tb.Text = "";
            Provincia_tb.Text = "";
            localidad_tb.Text = "";
            Pais_tb.Text = "";
            CodigoPostal_tb.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Proveedor proveedor = new Proveedor
            {
                CUIT = Cuit_tb.Text,
                RazonSocial = RazonSocial_tb.Text,
                NombreContacto = NombreContacto_tb.Text,
                CorreoElectronico = CorreoElectronico_tb.Text,
                Telefono = NumeroTelefono_tb.Text,
                Direccion = Direccion_tb.Text,
                Provincia = Provincia_tb.Text,
                Localidad = localidad_tb.Text,
                Pais = Pais_tb.Text,
                CodigoPostal = CodigoPostal_tb.Text
            };

            Response resInsertarProveedor = ProveedorNegocio.InsertarProveedor(proveedor);
            if (!resInsertarProveedor.ErrorFound)
            {
                VaciarTextBoxs();
                Utils.ShowSnackbar(resInsertarProveedor.Message, this, GetType());

            }
            else
            {
                Utils.ShowSnackbar(resInsertarProveedor.Message, this, GetType());
            }
           
        }

        protected void BtnVolverAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Proveedores/Administrar.aspx");
        }
    }
}