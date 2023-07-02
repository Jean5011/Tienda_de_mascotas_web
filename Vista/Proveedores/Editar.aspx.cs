using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;


namespace Vista.Proveedores {
    public partial class Editar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;

                //  if (!string.IsNullOrEmpty(Request.QueryString["cuit"]))//Si el QueryString no se encuentra vacio, los datos se cargan.
                //{
                // string CUIT = Request.QueryString["cuit"];
                string CUIT = "777777777777";//proveedor de testeo


                Response response = ProveedorNegocio.ObtenerProveedorCUITEditar(CUIT);
                if (!response.ErrorFound) {
                    DataSet ds = response.ObjectReturned as DataSet;
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                        DataRow proveedorRow = ds.Tables[0].Rows[0];

                        Cuit_tb.Text = proveedorRow["CUIT_Prov"].ToString();
                        RazonSocial_tb.Text = proveedorRow["RazonSocial_Prov"].ToString();
                        NombreContacto_tb.Text = proveedorRow["NombreDeContacto_Prov"].ToString();
                        CorreoElectronico_tb.Text = proveedorRow["CorreoElectronico_Prov"].ToString();
                        NumeroTelefono_tb.Text = proveedorRow["Telefono_Prov"].ToString();
                        Direccion_tb.Text = proveedorRow["Direccion_Prov"].ToString();
                        Provincia_tb.Text = proveedorRow["Provincia_Prov"].ToString();
                        localidad_tb.Text = proveedorRow["Localidad_Prov"].ToString();
                        Pais_tb.Text = proveedorRow["Pais_Prov"].ToString();
                        CodigoPostal_tb.Text = proveedorRow["CodigoPostal_Prov"].ToString();
                        //estado check box =  proveedorRow["Estado_Prov"].ToString();


                    }
                    else {

                        Utils.MostrarMensaje($"No se encontro el CUIT ", this.Page, GetType());
                    }
                }
                else {
                    Response.Redirect("Administrar.aspx");
                    Utils.MostrarMensaje($"error ", this.Page, GetType());
                }
                //}
            }
        }

        protected void Button1_Click(object sender, EventArgs e) {

            Proveedor proveedor = new Proveedor {
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


            Response resActualizarProveedor = ProveedorNegocio.ActualizarProveedor(proveedor);
            if (!resActualizarProveedor.ErrorFound) {

                Utils.ShowSnackbar("Proveedor ha sido actualizado correctamente!.", this, GetType());

            }
            else {
                Utils.ShowSnackbar("Hubo un error, no se pudo actualizar el proveedor.", this, GetType());
            }
            //Utils.MostrarMensaje($"El proveedor se agregaria correctamente en este caso. ", this.Page, GetType());//ELIMINAR ESTO, ES UN MENSAJE DE TESTEO AL VERIFICAR
        }

    }
}