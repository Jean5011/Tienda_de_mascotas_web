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

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response existe = ProveedorNegocio.VerificarExiste(Cuit_tb.Text);
            int cantidad;
            if (!existe.ErrorFound)
            {
                DataSet dt = existe.ObjectReturned as DataSet;
                cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["CUIT"]);

                if (cantidad > 0)
                {   //si cantidad es mayor a cero es pq ya existe
                    Utils.MostrarMensaje($"El CUIT de proveedor ingresado ya existe. ", this.Page, GetType());
                }
                else
                {   //si es igual a cero es pq no existe, se crea el proveedor
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

                    void vaciarTextBoxs()
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
                    Response resInsertarProveedor = ProveedorNegocio.InsertarProveedor(proveedor);
                    if (!resInsertarProveedor.ErrorFound)
                    {
                        vaciarTextBoxs();
                        Utils.ShowSnackbar("Proveedor ha sido agregado correctamente!.", this, GetType());

                    }
                    else
                    {
                        Utils.ShowSnackbar("Hubo un error, no se pudo agregar el proveedor.", this, GetType());
                    }
                    //Utils.MostrarMensaje($"El proveedor se agregaria correctamente en este caso. ", this.Page, GetType());//ELIMINAR ESTO, ES UN MENSAJE DE TESTEO AL VERIFICAR
                }
            }

        }
    }
}