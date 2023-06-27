<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="AltaAnimal.aspx.cs" Inherits="Vista.Animales.AltaAnimal" %>
<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Codigo Animal:</td>
            <td class="auto-style3">
                <asp:TextBox ID="TB_Animal" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="BT_Filtrar" runat="server" OnClick="BT_Filtrar_Click" Text="Filtrar" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="GV_Animal" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_Animal_SelectedIndexChanged" OnSelectedIndexChanging="GV_Animal_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Codigo">
                    <ItemTemplate>
                        <asp:Label ID="Lv_Cod" runat="server" Text='<%# Eval(Animal.Columns.Codigo) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label ID="Lv_Nombre" runat="server" Text='<%# Eval(Animal.Columns.Nombre) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Raza">
                    <ItemTemplate>
                        <asp:Label ID="Lv_Raza" runat="server" Text='<%# Eval(Animal.Columns.Raza) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
