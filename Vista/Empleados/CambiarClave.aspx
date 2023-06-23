<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="CambiarClave.aspx.cs" Inherits="Vista.Empleados.CambiarClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page">
        <h2>Cambiar clave</h2>
        <br>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="clave-label">Clave</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtClave" runat="server" ValidationGroup="A" CssClass="mdc-text-field__input" aria-labelledby="clave-label" TextMode="Password"></asp:TextBox>
        </label>

        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="A" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtClave" runat="server" ErrorMessage="" SetFocusOnError="True" Text="Ingresá una clave."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="clave-label">Volvé a escribir tu clave</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtConfirmar" ValidationGroup="A" runat="server" CssClass="mdc-text-field__input" aria-labelledby="clave-label" TextMode="Password"></asp:TextBox>
        </label>

        <div class="mdc-text-field-helper-line">
            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator1" ValidationGroup="A" CssClass="mdc-text-field-helper-text" runat="server" ErrorMessage="Las claves no coinciden" ControlToCompare="txtClave" ControlToValidate="txtConfirmar"></asp:CompareValidator>
        </div>
        <br />
        <br>
        <asp:LinkButton ID="btnGuardarCambios" ValidationGroup="A" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="btnGuardarCambios_Click">
        <span class="mdc-button__label">Guardar cambios</span>
        </asp:LinkButton>
    </div>
</asp:Content>
