<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="Vista.Empleados.IniciarSesion" MasterPageFile="/Root.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2>Iniciá sesión para continuar</h2>
        <br>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="dni-label">DNI</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtDNI" runat="server" CssClass="mdc-text-field__input" aria-labelledby="dni-label"></asp:TextBox>
        </label>
        <br>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="clave-label">Clave</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtClave" runat="server" CssClass="mdc-text-field__input" aria-labelledby="clave-label" TextMode="Password"></asp:TextBox>
        </label>
        <br />
        <br>
        <asp:LinkButton ID="btnIniciarSesion" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="btnIniciarSesion_Click">
        <span class="mdc-button__label">Iniciar sesión</span>
        </asp:LinkButton>

        <br />
        <br />
        <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>