<%@ Page MasterPageFile="/Root.Master" Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Ventas.Administrar" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Ventas</h2>
    <div class="searchbox">
        <asp:TextBox ID="txtBuscar" placeholder="Buscar por código de compra" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" CssClass="material-icons mdc-icon-button" OnClick="btnBuscar_Click" runat="server" Text="search" />
    </div>
    <asp:GridView ID="gvVentas" CssClass="mdc-data-table" AutoGenerateColumns="False" runat="server">
        <Columns>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <div class="mdc-data-table__cell">
                        <a href="/Ventas/VerFactura.aspx?ID=<%# Eval(Venta.Columns.Id) %>">Ver detalles</a>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <div class="mdc-data-table__cell">
                        <asp:Label ID="lbID" runat="server" Text="<%# Eval(Venta.Columns.Id) %>"></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Empleado Gestor">
                <ItemTemplate>
                    <div class="mdc-data-table__cell">
                        <asp:Label ID="lbEMPLEADOGESTOR" runat="server" Text="<%# Eval(Venta.Columns.DNI) %>"></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Medio de pago">
                <ItemTemplate>
                    <div class="mdc-data-table__cell">
                        <asp:Label ID="lbTIPOPAGO" runat="server" Text="<%# Eval(Venta.Columns.TipoPago) %>"></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha">
                <ItemTemplate>
                    <div class="mdc-data-table__cell">
                        <asp:Label ID="lbFECHA" runat="server" Text="<%# Eval(Venta.Columns.Fecha) %>"></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total">
                <ItemTemplate>
                    <div class="mdc-data-table__cell">
                        <asp:Label ID="lbPTOTAL" runat="server" Text="<%# Eval(Venta.Columns.Total) %>"></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
