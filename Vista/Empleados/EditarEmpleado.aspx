<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="EditarEmpleado.aspx.cs" Inherits="Vista.Empleados.EditarEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2>Editar detalles</h2>
        <br>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="dni-label">DNI *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtDNI" minlength="8" runat="server" CssClass="mdc-text-field__input" aria-labelledby="dni-label"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtDNI" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un DNI."></asp:RequiredFieldValidator>
        </div>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Nombre *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtNombre" minlength="3" ValidationGroup="A" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtNombre" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un nombre."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Apellido *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtApellido" minlength="3" ValidationGroup="A" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtApellido" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un apellido."></asp:RequiredFieldValidator>
        </div>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Sexo *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:DropDownList ValidationGroup="A" ID="ddlGenero" CssClass="mdc-text-field__input" runat="server">
                <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
            </asp:DropDownList>
        </label>
        <br>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Fecha de Nacimiento *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtFechaNacimiento" type="date" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>
        <label class="mdc-text-field mdc-text-field--outlined" style="margin-left: 12px">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Fecha de Contrato *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtFechaContrato" type="date" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>
        <br>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Sueldo *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtSueldo" runat="server" type="number" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtSueldo" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Dirección *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtDireccion" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtDireccion" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Localidad *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtLocalidad" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtLocalidad" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Provincia *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtProvincia" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>

        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtProvincia" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nlabel">Nacionalidad *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtNacionalidad" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtNacionalidad" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br>
        <br>
        <asp:CheckBox ValidationGroup="A" ID="chkEstado" runat="server" Text="Habilitado" />
        <br>
        <br>
        <hr />
        <br />
        <asp:CheckBox ValidationGroup="A" ID="chkAdmin" runat="server" Text="Conceder permisos de administrador" />
        <br />
        <br />
        <asp:LinkButton ValidationGroup="A" ID="btnGuardarCambios" runat="server" OnClick="btnGuardarCambios_Click" CssClass="mdc-button mdc-button--raised">
                    <span class="mdc-button__label">Guardar cambios</span>
        </asp:LinkButton>
    </div>

</asp:Content>
