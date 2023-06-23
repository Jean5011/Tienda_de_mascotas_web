<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" MasterPageFile="/Root.Master" Inherits="Vista.Empleados.Perfil" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col perfil">
        <h2 id="NombreEmpleadoTitulo" runat="server"></h2>
        <ul class="mdc-card mdc-card--outlined mdc-list mdc-list--two-line">
            <asp:ListView ID="DetallesList" runat="server">
                <itemtemplate>
                    <div class="mdc-list-item">
                        <span class="mdc-list-item__ripple"></span>
                        <span class="mdc-list-item__text">
                            <span class="mdc-list-item__primary-text"><%# Eval("Valor") %></span>
                            <span class="mdc-list-item__secondary-text"><%# Eval("Propiedad") %></span>
                        </span>
                    </div>
                </itemtemplate>
            </asp:ListView>
            <li role="separator" class="mdc-list-divider"></li>
            <asp:LinkButton runat="server" class="mdc-list-item" ID="BtnEditarDetalles" OnClick="BtnEditarDetalles_Click">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-deprecated-list-item__graphic material-icons" aria-hidden="true">edit
                </span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">Editar información</span>
                    <span class="mdc-list-item__secondary-text">Cambiar sueldo, dirección.</span>
                </span>
            </asp:LinkButton>
            <asp:LinkButton runat="server" class="mdc-list-item" ID="BtnDeshabilitar" OnClick="BtnDeshabilitar_Click">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-deprecated-list-item__graphic material-icons" aria-hidden="true">delete
                </span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">Deshabilitar empleado</span>
                    <span class="mdc-list-item__secondary-text">En caso de despido, renuncia.</span>
                </span>
            </asp:LinkButton>
        </ul>
        <br>
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
