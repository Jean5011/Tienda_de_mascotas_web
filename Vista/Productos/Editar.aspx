<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="Editar.aspx.cs" Inherits="Vista.Productos.Editar1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .botones {
            display: flex;
            align-content: center;
            justify-content: space-around
        }

        .error {
            color: red;
        }
    </style>
    <div class="page">
        <h2>Editar Producto</h2>
        <br />

        <div class="group">

            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbnom">Nombre</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtNombre"></asp:TextBox>
            </label>
            <asp:RequiredFieldValidator ID="txtNombre_Validator" runat="server"
                ControlToValidate="txtNombre" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtNombre_RegexValidator" runat="server"
                ControlToValidate="txtNombre" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,50}$"></asp:RegularExpressionValidator>

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
                ControlToValidate="txtPrecioUnitario" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtPrecioUnitario_RegexValidator" runat="server"
                ControlToValidate="txtPrecioUnitario" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd"
                ValidationExpression="^[0-9]+(\.[0-9]+)?$"></asp:RegularExpressionValidator>
        </div>
        <div class="group">
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
                ControlToValidate="txtDescripcion" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtDescripcion_RegexValidator" runat="server"
                ControlToValidate="txtDescripcion" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,50}$"></asp:RegularExpressionValidator>

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
                ControlToValidate="txtMarca" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtMarca_RegexValidator" runat="server"
                ControlToValidate="txtMarca" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd"
                ValidationExpression="^[A-Za-zñÑáéíóúÁÉÍÓÚ\s.0-9]{1,50}$"></asp:RegularExpressionValidator>
        </div>

        <div class="group">
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
                ControlToValidate="txtStock" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd" CssClass="error" />
            <asp:RegularExpressionValidator ID="txtStock_RegexValidator" runat="server"
                ControlToValidate="txtStock" ErrorMessage="*" ValidationGroup="ValidationGroupEditarProd"
                ValidationExpression="-?\d+$"></asp:RegularExpressionValidator>

        </div>
        <div class="group">
            <h3>Desactivar<asp:CheckBox ID="DesactivarProducto" runat="server" /></h3>
        </div>

        <br />
        <div class="botones">
            <asp:Button runat="server" ID="btnVolverAtras" CssClass="mdc-button mdc-button--raised" Text="Volver" OnClick="btnVolverAtras_Click" />
            <asp:Button runat="server" ID="btnGuardar" CssClass="mdc-button mdc-button--raised" Text="Guardar" OnClick="BtnGuardar_Click" ValidationGroup="ValidationGroupEditarProd" />
        </div>

    </div>
</asp:Content>
