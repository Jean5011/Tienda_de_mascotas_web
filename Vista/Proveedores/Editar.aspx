<%@ Page MasterPageFile="/Root.Master" Language="C#" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="Vista.Proveedores.Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .textBox {
            border-radius: 4px 4px 0px 0px;
            width: 214px;
            height: 56px;
        }
        .error{
            color:red;
        }
    </style>
    <div class="page">
        <h2>Editar proveedor</h2>
        <br>

        <div class="group">
            <!-- CUIT-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">CUIT</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="Cuit_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="Cuit_tb_Validator" runat="server"
                ControlToValidate="Cuit_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
            <asp:RegularExpressionValidator ID="Cuit_tb_RegexValidator" runat="server"
                ControlToValidate="Cuit_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[0-9]{10,}$"></asp:RegularExpressionValidator>
            <!-- Razon Social-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Razón Social</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="RazonSocial_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="RazonSocial_tb_Validator" runat="server"
                ControlToValidate="RazonSocial_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
             <asp:RegularExpressionValidator ID="RazonSocial_tb_RegexValidator" runat="server"
                ControlToValidate="RazonSocial_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,50}$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
            <!-- Nombre de contacto-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Nombre de contacto</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="NombreContacto_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="NombreContacto_tb_Validator" runat="server"
                ControlToValidate="NombreContacto_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error"/>
               <asp:RegularExpressionValidator ID="NombreContacto_tb_RegexValidator" runat="server"
                ControlToValidate="NombreContacto_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,30}$"></asp:RegularExpressionValidator>
             <!-- Codigo postal-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Código Postal</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="CodigoPostal_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="CodigoPostal_tb_Validator" runat="server"
                ControlToValidate="CodigoPostal_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error"/>
             <asp:RegularExpressionValidator ID="CodigoPostal_tb_RegexValidator" runat="server"
                ControlToValidate="CodigoPostal_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^\d{1,10}$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
             <!-- Correo electronico-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Correo electrónico</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="CorreoElectronico_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="CorreoElectronico_tb_Validator" runat="server"
                ControlToValidate="CorreoElectronico_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
              <asp:RegularExpressionValidator ID="CorreoElectronico_tb_RegexValidator" runat="server"
                ControlToValidate="CorreoElectronico_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"></asp:RegularExpressionValidator>
             <!-- Numero de telefono-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Número de teléfono</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="NumeroTelefono_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="NumeroTelefono_tb_Validator" runat="server"
                ControlToValidate="NumeroTelefono_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error"/>
            <asp:RegularExpressionValidator ID="NumeroTelefono_tb_RegexValidator" runat="server"
                ControlToValidate="NumeroTelefono_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[0-9]{1,20}$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
             <!-- Direccion-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Dirección</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="Direccion_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="Direccion_tb_Validator" runat="server"
                ControlToValidate="Direccion_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
            <asp:RegularExpressionValidator ID="Direccion_tb_RegexValidator" runat="server"
                ControlToValidate="Direccion_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,30}$"></asp:RegularExpressionValidator>
              <!-- Localidad-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Localidad</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="localidad_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="localidad_tb_Validator" runat="server"
                ControlToValidate="localidad_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
            <asp:RegularExpressionValidator ID="localidad_tb_RegexValidator" runat="server"
                ControlToValidate="localidad_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,30}$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
            <!--Provincia-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Provincia</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="Provincia_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="Provincia_tb_Validator" runat="server"
                ControlToValidate="Provincia_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error"/>
              <asp:RegularExpressionValidator ID="Provincia_tb_RegexValidator" runat="server"
                ControlToValidate="Provincia_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,30}$"></asp:RegularExpressionValidator>
              <!--Pais-->
             <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">País</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="textBox mdc-text-field__input" ID="Pais_tb"  ValidationGroup="ValidationGroup"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="Pais_tb_Validator" runat="server"
                ControlToValidate="Pais_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
              <asp:RegularExpressionValidator ID="Pais_tb_RegexValidator" runat="server"
                ControlToValidate="Pais_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,20}$"></asp:RegularExpressionValidator>
        </div>

        <br>
        <br />
         <div class="botones">
            <asp:Button runat="server" ID="btnVolverAtras" CssClass="mdc-button mdc-button--raised" Text="Volver" OnClick="btnVolverAtras_Click" />
             <asp:Button class="mdc-button mdc-button--raised" ID="Button1" ValidationGroup="ValidationGroup" runat="server" Text="Guardar" OnClick="Button1_Click" />
        </div>
    </div>
</asp:Content>