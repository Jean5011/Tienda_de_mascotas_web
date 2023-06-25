<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" MasterPageFile="/Root.Master" Inherits="Vista.Empleados.Perfil" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        document.querySelector("main").classList.add("menu-main");
    </script>
    <div class="mdc-card col perfil">
        <h2 id="NombreEmpleadoTitulo" runat="server"></h2>
        <ul class="mdc-list mdc-list--two-line">
            <asp:ListView ID="DetallesList" runat="server">
                <itemtemplate>
                    <div class="mdc-list-item">
                        <span class="mdc-list-item__ripple"></span>
                        <span class="mdc-deprecated-list-item__graphic material-icons" aria-hidden="true"><%# Eval("Icon") %></span>
                        <span class="mdc-list-item__text">
                            <span class="mdc-list-item__primary-text"><%# Eval("Valor") %></span>
                            <span class="mdc-list-item__secondary-text"><%# Eval("Propiedad") %></span>
                        </span>
                    </div>
                </itemtemplate>
            </asp:ListView>
        </ul><br>
            <div class="mdc-card__actions mdc-card__actions--full-bleed mcard-actions">
              <asp:LinkButton runat="server" ID="BtnEditarDetalles" OnClick="BtnEditarDetalles_Click" CssClass="mdc-button mdc-card__action mdc-card__action--button">
                  <div class="mdc-button__ripple"></div>
                  <span class="mdc-button__label mcardbl-act">Editar</span>
                  <i class="material-icons mdc-button__icon" aria-hidden="true">edit</i>
              </asp:LinkButton>
              <asp:LinkButton runat="server" ID="BtnCambiarClave" OnClick="BtnCambiarClave_Click" CssClass="mdc-button mdc-card__action mdc-card__action--button" >
                  <div class="mdc-button__ripple"></div>
                  <span class="mdc-button__label mcardbl-act">Cambiar clave</span>
                  <i class="material-icons mdc-button__icon" aria-hidden="true">security</i>
              </asp:LinkButton>
                <asp:LinkButton runat="server" ID="BtnDeshabilitar" OnClick="BtnDeshabilitar_Click" CssClass="mdc-button mdc-card__action mdc-card__action--button" >
                    <div class="mdc-button__ripple"></div>
                    <span class="mdc-button__label mcardbl-act">Deshabilitar</span>
                    <i class="material-icons mdc-button__icon danger-color" aria-hidden="true">remove_circle</i>
                </asp:LinkButton>
        <br>
    </div>
        </div>
    <div class="col principal">
        <h2>Ventas que registró</h2>
        <asp:GridView ID="gvVentas" CssClass="mdc-data-table" AutoGenerateColumns="False" runat="server">
            <columns>
                <asp:TemplateField HeaderText="Acciones">
                    <itemtemplate>
                        <div class="mdc-data-table__cell">
                            <a href="/Ventas/VerFactura.aspx?ID=<%# Eval(Venta.Columns.Id) %>">Ver detalles</a>
                        </div>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <itemtemplate>
                        <div class="mdc-data-table__cell">
                            <asp:Label ID="lbID" runat="server" Text="<%# Eval(Venta.Columns.Id) %>"></asp:Label>
                        </div>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Empleado Gestor">
                    <itemtemplate>
                        <div class="mdc-data-table__cell">
                            <asp:Label ID="lbEMPLEADOGESTOR" runat="server" Text="<%# Eval(Venta.Columns.DNI) %>"></asp:Label>
                        </div>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Medio de pago">
                    <itemtemplate>
                        <div class="mdc-data-table__cell">
                            <asp:Label ID="lbTIPOPAGO" runat="server" Text="<%# Eval(Venta.Columns.TipoPago) %>"></asp:Label>
                        </div>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha">
                    <itemtemplate>
                        <div class="mdc-data-table__cell">
                            <asp:Label ID="lbFECHA" runat="server" Text="<%# Eval(Venta.Columns.Fecha) %>"></asp:Label>
                        </div>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total">
                    <itemtemplate>
                        <div class="mdc-data-table__cell">
                            <asp:Label ID="lbPTOTAL" runat="server" Text="<%# Eval(Venta.Columns.Total) %>"></asp:Label>
                        </div>
                    </itemtemplate>
                </asp:TemplateField>
            </columns>
        </asp:GridView>

    </div>
</asp:Content>
