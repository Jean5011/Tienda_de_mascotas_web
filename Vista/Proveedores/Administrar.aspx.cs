﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
namespace Vista.Proveedores {
    public partial class Administrar : System.Web.UI.Page {
        protected void CargarTabla(Response res) {
            DataSet myDataSet = res.ObjectReturned as DataSet;
            GridView1.DataSource = myDataSet.Tables["root"];
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e) {

            if (IsPostBack != true) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);
                Response res = ProveedorNegocio.ObtenerListaDeProveedores();
                if (!res.ErrorFound) {
                    CargarTabla(res);
                }
            }

        }

        protected void filtrarProveedor_Click(object sender, EventArgs e) {
            string cuit = txtBuscar.Text;
            Response resObtProveedorByCUIT = ProveedorNegocio.ObtenerProveedorByCUIT(cuit);
            if ((!string.IsNullOrEmpty(txtBuscar.Text)) && (!resObtProveedorByCUIT.ErrorFound)) {
                CargarTabla(resObtProveedorByCUIT);
            }
            else {
                Response resObtListDeProv = ProveedorNegocio.ObtenerListaDeProveedores();
                CargarTabla(resObtListDeProv);
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e) {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            string cuit = ((Label)GridView1.Rows[e.RowIndex].FindControl("CUIT_Prov_lb")).Text;
            var respProveedorActualizado = ProveedorNegocio.EliminadoLogicoProveedor(cuit);
            if (!respProveedorActualizado.ErrorFound)
            {
                Utils.ShowSnackbar("El proveedor ha sido eliminado correctamente!.", this, GetType());
            }
            else
            {
                Utils.ShowSnackbar("Hubo un error al eliminar el proveedor.", this, GetType());
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GridView1.EditIndex = -1;
            Response res = ProveedorNegocio.ObtenerListaDeProveedores();
            if (!res.ErrorFound) {
                CargarTabla(res);
            }
        }


        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Proveedor proveedor = new Proveedor {
                CUIT = ((Label)GridView1.Rows[e.RowIndex].FindControl("cuitEditar_lb")).Text,
                RazonSocial = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("RazonSocial_Prov_tb")).Text,
                NombreContacto = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("NombreDeContacto_Prov_tb")).Text,
                CorreoElectronico = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("CorreoElectronico_Prov_tb")).Text,
                Telefono = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Telefono_Prov_tb")).Text,
                Direccion = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Direccion_Prov_tb")).Text,
                Provincia = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Provincia_Prov_tb")).Text,
                Localidad = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Localidad_Prov_tb")).Text,
                Pais = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("Pais_Prov_tb")).Text,
                CodigoPostal = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("CodigoPostal_Prov_tb")).Text
            };

            Response res = ProveedorNegocio.ActualizarProveedor(proveedor);
            Response resMain = ProveedorNegocio.ObtenerListaDeProveedores();
           
            if (!res.ErrorFound)
             {
                GridView1.EditIndex = -1;
                Utils.ShowSnackbar("Proveedor ha sido actualizado correctamente!.", this, GetType());
                if (!resMain.ErrorFound) 
                { 
                    CargarTabla(resMain);
                }
                else Utils.ShowSnackbar("Algo salio mal.", this, GetType());

            }
            else
            {
                Utils.ShowSnackbar("No se pudo actualizar el proveedor.", this, GetType());
            }
        }
    }
}