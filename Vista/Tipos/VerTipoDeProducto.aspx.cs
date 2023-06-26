﻿using System;
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

namespace Vista.Tipos {
    public partial class VerTipoDeProducto : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session[Utils.AUTH] = AuthorizationVista.ValidateSession(this, Authorization.ONLY_EMPLOYEES_STRICT);

                var auth = Session[Utils.AUTH] as SessionData;
                var UsuarioActual = auth.User;
                CargarDatos();
            }
        }

        protected void CargarDatos() {
            if (txtBuscar.Text == "") BT_Todo_Click();
            else BT_Filtrar_Click();
        }
        protected bool EsAdmin() {
            var auth = Session[Utils.AUTH] as SessionData;
            var UsuarioActual = auth.User;
            return UsuarioActual.Rol == Empleado.Roles.ADMIN;
        }
        protected void btnBuscar_Click(object sender, EventArgs e) {
            CargarDatos();
        }
        protected void BT_Filtrar_Click() {
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            Response resultado = nt.ObtenerPorCod(txtBuscar.Text);
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }

        protected void BT_Todo_Click() {
            NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
            Response resultado = nt.GetTipoDeProducto();
            DataSet dt = resultado.ObjectReturned as DataSet;
            GV_Datos.DataSource = dt;
            GV_Datos.DataBind();
        }

        protected void GV_Datos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e) {
            if(EsAdmin()) {
                SesionNegocio.Autenticar(res => {
                    TipoProducto t = new TipoProducto();
                    string cod = ((Label)GV_Datos.Rows[e.RowIndex].FindControl("LV_CodTipoDeProducto")).Text;
                    t.Codigo = cod;
                    NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
                    nt.EliminarTipoDeProducto(t);
                    CargarDatos();
                }, err => {
                    Utils.ShowSnackbar("Caducó tu token. Volvé a iniciar sesión. ", this.Page, GetType());
                });
            } else {
                Utils.ShowSnackbar("No tenés permiso para realizar esta acción", this.Page, GetType());
            }
        }

        protected void GV_Datos_RowEditing(object sender, GridViewEditEventArgs e) {
            GV_Datos.EditIndex = e.NewEditIndex;
            CargarDatos();
        }

        protected void GV_Datos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GV_Datos.EditIndex = -1;
            CargarDatos();
        }

        protected void GV_Datos_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            if(EsAdmin()) {
                SesionNegocio.Autenticar(res => {
                    TipoProducto Tp = new TipoProducto();
                    Tp.Codigo = ((Label)GV_Datos.Rows[e.RowIndex].FindControl("LV_EditCod")).Text;
                    Tp.CodAnimal = ((DropDownList)GV_Datos.Rows[e.RowIndex].FindControl("DD_EditAnimal")).SelectedValue;
                    Tp.tipoDeProducto = ((DropDownList)GV_Datos.Rows[e.RowIndex].FindControl("DD_EditTdp")).SelectedValue;
                    Tp.Descripcion = ((TextBox)GV_Datos.Rows[e.RowIndex].FindControl("TB_EditDesc")).Text;
                    NegocioTipoDeProducto nt = new NegocioTipoDeProducto();
                    nt.ActualizarTipoDeProducto(Tp);
                }, err => {
                    Utils.ShowSnackbar("El token caducó, volvé a iniciar sesión", this.Page, GetType());
                });
            } else {
                Utils.ShowSnackbar("No tenés permiso para realizar esta acción", this.Page, GetType());
            }

            GV_Datos.EditIndex = -1;
            CargarDatos();
        }
    }
}