<%@ Page MasterPageFile="/Root.Master" Language="C#" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="Vista.Proveedores.Agregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .textBox {
            border-radius: 4px 4px 0px 0px;
            width: 214px;
            height: 56px;
        }
    </style>
    <div class="page">
        <h2>Añadir proveedor</h2>
        <br>

        <div class="group">
            <!-- CUIT-->
            <label>
                <asp:TextBox ID="Cuit_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Cuit"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="Cuit_tb_Validator" runat="server"
                ControlToValidate="Cuit_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
            <asp:RegularExpressionValidator ID="Cuit_tb_RegexValidator" runat="server"
                ControlToValidate="Cuit_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^\d{2}-\d{8}-\d{1}$"></asp:RegularExpressionValidator>
            <!-- Razon Social-->
            <label>
                <asp:TextBox ID="RazonSocial_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Razon social"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="RazonSocial_tb_Validator" runat="server"
                ControlToValidate="RazonSocial_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"/>
             <asp:RegularExpressionValidator ID="RazonSocial_tb_RegexValidator" runat="server"
                ControlToValidate="RazonSocial_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^\d{2}-\d{8}-\d{1}$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
            <!-- Nombre de contacto-->
            <label>
                <asp:TextBox ID="NombreContacto_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Nombre contacto"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="NombreContacto_tb_Validator" runat="server"
                ControlToValidate="NombreContacto_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
               <asp:RegularExpressionValidator ID="NombreContacto_tb_RegexValidator" runat="server"
                ControlToValidate="NombreContacto_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^\d{2}-\d{8}-\d{1}$"></asp:RegularExpressionValidator>
             <!-- Codigo postal-->
            <label>
                <asp:TextBox ID="CodigoPostal_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Código postal"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="CodigoPostal_tb_Validator" runat="server"
                ControlToValidate="CodigoPostal_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
             <asp:RegularExpressionValidator ID="CodigoPostal_tb_RegexValidator" runat="server"
                ControlToValidate="CodigoPostal_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^\d{1,10}$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
             <!-- Correo electronico-->
            <label>
                <asp:TextBox ID="CorreoElectronico_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Correo Electronico"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="CorreoElectronico_tb_Validator" runat="server"
                ControlToValidate="CorreoElectronico_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
              <asp:RegularExpressionValidator ID="CorreoElectronico_tb_RegexValidator" runat="server"
                ControlToValidate="CorreoElectronico_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^\d{1,10}$"></asp:RegularExpressionValidator>
             <!-- Numero de telefono-->
            <label>
                <asp:TextBox ID="NumeroTelefono_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Numero de telefono"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="NumeroTelefono_tb_Validator" runat="server"
                ControlToValidate="NumeroTelefono_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
            <asp:RegularExpressionValidator ID="NumeroTelefono_tb_RegexValidator" runat="server"
                ControlToValidate="NumeroTelefono_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^\d{1,20}$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
             <!-- Direccion-->
            <label>
                <asp:TextBox ID="Direccion_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Direccion"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="Direccion_tb_Validator" runat="server"
                ControlToValidate="Direccion_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
            <asp:RegularExpressionValidator ID="Direccion_tb_RegexValidator" runat="server"
                ControlToValidate="Direccion_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-z0-9\s]{1,30}$"></asp:RegularExpressionValidator>
              <!-- Localidad-->
            <label>
                <asp:TextBox ID="localidad_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Localidad"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="localidad_tb_Validator" runat="server"
                ControlToValidate="localidad_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
            <asp:RegularExpressionValidator ID="localidad_tb_RegexValidator" runat="server"
                ControlToValidate="localidad_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-z\s]{1,30}$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
            <!--Provincia-->
            <label>
                <asp:TextBox ID="Provincia_tb" runat="server" CssClass="textBox" ValidationGroup="ValidationGroup" placeholder="Provincia"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="Provincia_tb_Validator" runat="server"
                ControlToValidate="Provincia_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
              <asp:RegularExpressionValidator ID="Provincia_tb_RegexValidator" runat="server"
                ControlToValidate="Provincia_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-z\s]{1,30}$"></asp:RegularExpressionValidator>
              <!--Pais-->
            <label>
                <asp:TextBox ID="Pais_tb" runat="server" CssClass="textBox" placeholder="Pais" BorderColor="Black" ValidationGroup="ValidationGroup" BorderStyle="Solid"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="Pais_tb_Validator" runat="server"
                ControlToValidate="Pais_tb" ErrorMessage="*" ValidationGroup="ValidationGroup" />
              <asp:RegularExpressionValidator ID="Pais_tb_RegexValidator" runat="server"
                ControlToValidate="Pais_tb" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-z\s]{1,20}$"></asp:RegularExpressionValidator>
        </div>

        <br>
        <asp:Button class="mdc-button mdc-button--raised" ID="Button1" ValidationGroup="ValidationGroup" runat="server" Text="Guardar" />
    </div>
</asp:Content>
