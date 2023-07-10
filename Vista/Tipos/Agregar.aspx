<%@ Page MasterPageFile="/Root.Master" Language="C#" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="Vista.Tipos.Agregar" %>

<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2>Añadir tipo de producto</h2>
        <br>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="my-label-id">Código</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="TB_cod" runat="server" CssClass="mdc-text-field__input"></asp:TextBox>
        </label>
        <br />
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="my-label-id">Animal</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:DropDownList ID="DD_Animal" CssClass="mdc-text-field__input" runat="server" DataSourceID="SqlDataSource1" DataTextField="nombre_An" DataValueField="PK_CodAnimales_An">
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:PetsConnectionString %>' SelectCommand="SELECT [PK_CodAnimales_An], CONCAT([nombre_An], ' ', [NombreDeRaza_An]) AS [nombre_An] FROM [Animales] WHERE [Estado_An] = 1"></asp:SqlDataSource>
                    
        </label>
        <br />
        <br />
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="my-label-id">Categoría</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:DropDownList ID="DD_Tpd" CssClass="mdc-text-field__input" runat="server">
                <asp:ListItem>Comida</asp:ListItem>
                <asp:ListItem>Accesorios</asp:ListItem>
                <asp:ListItem>Ropa</asp:ListItem>
                <asp:ListItem>Higiene</asp:ListItem>
                <asp:ListItem>Salud</asp:ListItem>
            </asp:DropDownList>
        </label>
        <br>
        <br>
        <label class="mdc-text-field mdc-text-field--outlined">
            <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="my-label-id">Descripción</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
            </span>
            <asp:TextBox ID="TB_Descripcion" runat="server" CssClass="mdc-text-field__input"></asp:TextBox>
        </label>
        <br>
        <br>
        <asp:LinkButton CssClass="mdc-button mdc-button--raised" ID="BT_Guardar" runat="server" OnClick="BT_Guardar_Click" Text="Guardar">
            <span class="mdc-button__label">Guardar</span>
        </asp:LinkButton>
        <asp:Label ID="Lv_Verificacion" runat="server" Text="Label"></asp:Label>
    </div>
</asp:Content>
