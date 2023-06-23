<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="IgresarTipoDePrductos.aspx.cs" Inherits="Vista.Tipos.VerTipoDePrductos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <h1>Ingresar tipos de productos</h1>
            </td>
        </tr>
    </table>
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Igrese codigo:</td>
            <td class="auto-style3">
                <asp:TextBox ID="TB_cod" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Eliga tipo de animal:</td>
            <td class="auto-style3">
                <asp:DropDownList ID="DD_Animal" runat="server">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Ingrese tipo de producto:</td>
            <td class="auto-style3">
                <asp:DropDownList ID="DD_Tpd" runat="server">
                    <asp:ListItem>Comida</asp:ListItem>
                    <asp:ListItem>Accesorios</asp:ListItem>
                    <asp:ListItem>Ropa</asp:ListItem>
                    <asp:ListItem>Higiene</asp:ListItem>
                    <asp:ListItem>Salud</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Ingrese descripcion:</td>
            <td class="auto-style3">
                <asp:TextBox ID="TB_Descripcion" runat="server" Height="45px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">
                <asp:Button ID="BT_Guardar" runat="server" OnClick="BT_Guardar_Click" Text="Guardar" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
