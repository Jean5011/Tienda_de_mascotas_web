<%@ Page MasterPageFile="/Root.Master" Language="C#" AutoEventWireup="true" CodeBehind="Crear.aspx.cs" Inherits="Vista.Ventas.Crear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="page">
    <h2>Crear Venta</h2>
    <br />
    <br />
    <label class="mdc-text-field mdc-text-field--outlined">
        <span class="mdc-notched-outline">
            <span class="mdc-notched-outline__leading"></span>
            <span class="mdc-notched-outline__notch">
                <span class="mdc-floating-label" id="dni-label">Medio de pago *</span>
            </span>
            <span class="mdc-notched-outline__trailing"></span>
        </span>
        <asp:TextBox ID="txtMedio" minlength="8" runat="server" CssClass="mdc-text-field__input" aria-labelledby="dni-label"></asp:TextBox>
    </label>
    <div class="mdc-text-field-helper-line">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtMedio" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un medio de pago."></asp:RequiredFieldValidator>
    </div>
    <br>
    <label class="mdc-text-field mdc-text-field--outlined">
        <span class="mdc-notched-outline">
            <span class="mdc-notched-outline__leading"></span>
            <span class="mdc-notched-outline__notch">
                <span class="mdc-floating-label" id="nfn">Fecha *</span>
            </span>
            <span class="mdc-notched-outline__trailing"></span>
        </span>
        <asp:TextBox ValidationGroup="A" ID="txtFecha" type="date" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nfn"></asp:TextBox>
    </label>
    <div class="mdc-text-field-helper-line">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtFecha" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá una fecha."></asp:RequiredFieldValidator>
    </div>
    <br>
    <label class="mdc-text-field mdc-text-field--outlined">
        <span class="mdc-notched-outline">
            <span class="mdc-notched-outline__leading"></span>
            <span class="mdc-notched-outline__notch">
                <span class="mdc-floating-label" id="nh">Hora *</span>
            </span>
            <span class="mdc-notched-outline__trailing"></span>
        </span>
        <asp:TextBox ValidationGroup="A" ID="txtHora" type="time" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nh"></asp:TextBox>
    </label>
    <div class="mdc-text-field-helper-line">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtHora" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá una hora."></asp:RequiredFieldValidator>
    </div>
    <br>
    <asp:LinkButton ValidationGroup="A" ID="btnGuardarCambios" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="BtnGuardarCambios_Click">
                    <span class="mdc-button__label">Siguiente</span>
    </asp:LinkButton>
    <br />
    <br />
    <asp:Label ID="adLabel" runat="server" Text=""></asp:Label>
</div>


    </asp:Content>