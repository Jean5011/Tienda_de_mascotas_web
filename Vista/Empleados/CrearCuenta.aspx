<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="CrearCuenta.aspx.cs" Inherits="Vista.Empleados.CrearCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2>Crear cuenta</h2>
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
                    <span class="mdc-floating-label" id="n-nombre">Nombre *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtNombre" minlength="3" ValidationGroup="A" runat="server" CssClass="mdc-text-field__input" aria-labelledby="n-nombre"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtNombre" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un nombre."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="n-apellido">Apellido *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="txtApellido" minlength="3" ValidationGroup="A" runat="server" CssClass="mdc-text-field__input" aria-labelledby="n-apellido"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtApellido" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un apellido."></asp:RequiredFieldValidator>
        </div>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="n-sexo">Sexo *</span>
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
                    <span class="mdc-floating-label" id="nfn">Fecha de Nacimiento *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtFechaNacimiento" type="date" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nfn"></asp:TextBox>
        </label>
        <label class="mdc-text-field mdc-text-field--outlined" style="margin-left: 12px">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nfc">Fecha de Contrato *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtFechaContrato" type="date" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nfc"></asp:TextBox>
        </label>
        <br>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nsueldo">Sueldo *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtSueldo" runat="server" type="number" CssClass="mdc-text-field__input" aria-labelledby="nsueldo"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtSueldo" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="ndireccion">Dirección *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtDireccion" runat="server" CssClass="mdc-text-field__input" aria-labelledby="ndireccion"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtDireccion" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nloc">Localidad *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtLocalidad" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nloc"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtLocalidad" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nprov">Provincia *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtProvincia" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nprov"></asp:TextBox>
        </label>

        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtProvincia" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nnac">Nacionalidad *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtNacionalidad" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nnac"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtNacionalidad" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="nclau">Creá una clave *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" ID="txtClave" TextMode="Password" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nclau"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtNacionalidad" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
        </div>
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="ncclau">Confirmá la clave *</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ValidationGroup="A" TextMode="Password" ID="txtConfirmarClave" runat="server" CssClass="mdc-text-field__input" aria-labelledby="ncclau"></asp:TextBox>
        </label>
        <div class="mdc-text-field-helper-line">
            <asp:CompareValidator ID="CompareValidator1" CssClass="mdc-text-field-helper-text" runat="server" ErrorMessage="Las claves no coinciden" ControlToCompare="txtClave" ControlToValidate="txtConfirmarClave"></asp:CompareValidator>
        </div>
        <br />
        <br>
        <asp:CheckBox ValidationGroup="A" ID="chkEstado" runat="server" Text="Habilitado" />
        <br>
        <br>
        <hr />
        <br />
        <asp:CheckBox ValidationGroup="A" ID="chkAdmin" runat="server" Text="Conceder permisos de administrador" />
        <br />
        <br />
        <asp:LinkButton ValidationGroup="A" ID="btnGuardarCambios" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="BtnGuardarCambios_Click">
                    <span class="mdc-button__label">Guardar cambios</span>
        </asp:LinkButton>
    </div>

</asp:Content>
