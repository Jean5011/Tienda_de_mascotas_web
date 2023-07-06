<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="Eliminar.aspx.cs" Inherits="Vista.Ventas.Eliminar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2 runat="server">¿Estás seguro de eliminar esta venta?</h2>
        <br>
        <p>Esta acción no se puede deshacer. <br /> Se eliminará el registro Venta y todos los productos asociados a esta Venta. </p>
        <br>
        <br />
        <br />
        <asp:LinkButton ID="btnBorrar" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="btnBorrar_Click">
            <span class="mdc-button__label">Eliminar permanentemente</span>
        </asp:LinkButton>
    </div>
</asp:Content>
