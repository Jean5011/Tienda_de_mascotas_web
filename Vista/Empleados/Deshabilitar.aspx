<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="Deshabilitar.aspx.cs" Inherits="Vista.Empleados.Deshabilitar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2 id="H2Titulo" runat="server"></h2>
        <br>
        <asp:Label ID="LabelDescripcion" runat="server" Text=""></asp:Label>
        <br>
        <br />
        <br />
        <asp:LinkButton ID="btnDeshabilitar" Visible="false" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="BtnDeshabilitar_Click">
            <span class="mdc-button__label">Deshabilitar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnHabilitar" Visible="false" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="BtnHabilitar_Click">
            <span class="mdc-button__label">Habilitar</span>
        </asp:LinkButton>
    </div>
</asp:Content>

