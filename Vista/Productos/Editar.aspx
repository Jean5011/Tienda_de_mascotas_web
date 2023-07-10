<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="Editar.aspx.cs" Inherits="Vista.Productos.Editar1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
         .textBox {
            border-radius: 4px 4px 0px 0px;
            width: 214px;
            height: 56px;
        }
         .ddl {
            width: 214px;
            height: 56px;
        }
        .botones {
            display: flex;
            align-content: center;
            justify-content: space-around
        }

        .error {
            color: red;
        }
        .validadorDdl_error{
            color:red;
            margin-right:16.9px;
        }
    </style>
    <div class="page">
        <h2>Editar Producto</h2>
        <br />

       <div class="group">
            <!--Nombre-->
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Nombre del producto</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtNombre"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="txtNombre_Validator" runat="server"
                ControlToValidate="txtNombre" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtNombre_RegexValidator" runat="server"
                ControlToValidate="txtNombre" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9-]{1,50}$"></asp:RegularExpressionValidator>
               <!--Precio Unitario-->
           <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbpu">Precio Unitario</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtPrecioUnitario"></asp:TextBox>
            </label>
             <asp:RequiredFieldValidator ID="txtPrecioUnitario_Validator" runat="server"
                ControlToValidate="txtPrecioUnitario" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtPrecioUnitario_RegexValidator" runat="server"
                ControlToValidate="txtPrecioUnitario" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^-?\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
            <!--Tipo de producto-->
            <asp:DropDownList id="ddlTipoProducto" CssClass="ddl" runat="server" CausesValidation="True" ValidationGroup="ValidationGroup"  >
                <asp:ListItem>Seleccionar Tipo</asp:ListItem>
            </asp:DropDownList>
           <asp:RequiredFieldValidator  ID="txtTipoProducto_Validator" runat="server"
                ControlToValidate="ddlTipoProducto" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="validadorDdl_error" InitialValue="Seleccionar Tipo" />
            <!--CUIT-->
        </div>

        <div class="group">
            <!--Descripcion-->
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbdes">Descripción</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtDescripcion"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="txtDescripcion_Validator" runat="server"
                ControlToValidate="txtDescripcion" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtDescripcion_RegexValidator" runat="server"
                ControlToValidate="txtDescripcion" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,50}$"></asp:RegularExpressionValidator>
            <!--Marca-->
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbmar">Marca</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtMarca"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="txtMarca_Validator" runat="server"
                ControlToValidate="txtMarca" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtMarca_RegexValidator" runat="server"
                ControlToValidate="txtMarca" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,50}$"></asp:RegularExpressionValidator>
        </div>

        <div class="group">
            <!--Stock-->
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbstock">Stock</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtStock"></asp:TextBox>
            </label>
              <asp:RequiredFieldValidator ID="txtStock_Validator" runat="server"
                ControlToValidate="txtStock" ErrorMessage="*" ValidationGroup="ValidationGroup" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtStock_RegexValidator" runat="server"
                ControlToValidate="txtStock" ErrorMessage="*" ValidationGroup="ValidationGroup"
                ValidationExpression="^-?\d+$"></asp:RegularExpressionValidator>
        
            
        </div>

        <br />
        <div class="botones">
            <asp:Button runat="server" ID="btnVolverAtras" CssClass="mdc-button mdc-button--raised" Text="Volver" OnClick="BtnVolverAtras_Click" />
            <asp:Button runat="server" ID="btnGuardar" CssClass="mdc-button mdc-button--raised" Text="Guardar" OnClick="BtnGuardar_Click" ValidationGroup="ValidationGroup" />
        </div>

    </div>
</asp:Content>
