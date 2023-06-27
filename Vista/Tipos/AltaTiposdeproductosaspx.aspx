<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaTiposdeproductosaspx.aspx.cs" Inherits="Vista.Tipos.AltaTiposdeproductosaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 237px;
        }
        .auto-style3 {
            width: 201px;
        }
        .auto-style4 {
            width: 197px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">Alta tipos de productos</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Codigo de tipo de producto:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="TB_TDP" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:Button ID="BT_Filtrar" runat="server" OnClick="BT_Filtrar_Click" Text="Filtrar" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div>
            <asp:GridView ID="GV_TDP" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanging="GV_TDP_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Codigo">
                        <ItemTemplate>
                            <asp:Label ID="Lv_Cod" runat="server" Text='<%# Bind("PK_CodTipoProducto_TP") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cod Animal">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("CodAnimales_Tp") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo de producto">
                        <ItemTemplate>
                            <asp:Label ID="Lv_TDP" runat="server" Text='<%# Bind("TipoDeProducto_Tp") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripcion">
                        <ItemTemplate>
                            <asp:Label ID="Lv_Desc" runat="server" Text='<%# Bind("Descripcion_TP") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
