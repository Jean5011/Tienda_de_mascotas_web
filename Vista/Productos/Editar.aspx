<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="Editar.aspx.cs" Inherits="Vista.Productos.Editar1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .botones{
            display:flex;
            align-content:center;
            justify-content:space-around
        }
    </style>
    <div class="page">
        <h2>Añadir producto</h2>
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
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="lbtip">Tipo de producto</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtTipoProducto"></asp:TextBox>
            </label>
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
        </div>
        <div class="group">
            <h3>Desactivar<asp:CheckBox ID="DesactivarProducto" runat="server" /></h3>
        </div>

        <br />
        <div class="botones">
            <asp:Button runat="server" ID="btnVolverAtras" CssClass="mdc-button mdc-button--raised" Text="Volver" OnClick="btnVolverAtras_Click" />
            <asp:Button runat="server" ID="btnGuardar" CssClass="mdc-button mdc-button--raised" Text="Guardar" OnClick="BtnGuardar_Click" />
        </div>
        
    </div>
</asp:Content>
