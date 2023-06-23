<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarAnimal.aspx.cs" MasterPageFile="/Root.Master" Inherits="Vista.Animales.AgregarAnimal" %>
<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2>Agregar Animal</h2>
        <br>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="clave-label">Código</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="TB_Cod" runat="server" ValidationGroup="A" CssClass="mdc-text-field__input" aria-labelledby="clave-label"></asp:TextBox>
        </label>

        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="A" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="TB_Cod" runat="server" ErrorMessage="" SetFocusOnError="True" Text="Ingresá un código."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="clave-label">Nombre</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="TB_Nombre" runat="server" ValidationGroup="A" CssClass="mdc-text-field__input" aria-labelledby="clave-label"></asp:TextBox>
        </label>

        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="TB_Nombre" runat="server" ErrorMessage="" SetFocusOnError="True" Text="Ingresá un nombre."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="clave-label">Raza</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="TB_Raza" runat="server" ValidationGroup="A" CssClass="mdc-text-field__input" aria-labelledby="clave-label"></asp:TextBox>
        </label>

        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="TB_Raza" runat="server" ErrorMessage="" SetFocusOnError="True" Text="Ingresá una raza."></asp:RequiredFieldValidator>
        </div>
        <br />
        <br>
        <asp:LinkButton ID="btnGuardarCambios" ValidationGroup="A" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="BT_Datos_Click">
            <span class="mdc-button__label">Guardar cambios</span>
        </asp:LinkButton>
    </div>

</asp:Content>
