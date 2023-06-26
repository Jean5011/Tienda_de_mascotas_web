<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="Deshabilitar.aspx.cs" Inherits="Vista.Empleados.Deshabilitar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2 id="H2Titulo" runat="server"></h2>
        <br>
        <asp:Label ID="LabelDescripcion" runat="server" Text=""></asp:Label>
        <br>
        <br />
        <br />
        <asp:LinkButton ID="btnDeshabilitar" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="BtnDeshabilitar_Click">
                    <span class="mdc-button__label">Deshabilitar empleado</span>
        </asp:LinkButton>
    </div>
</asp:Content>

