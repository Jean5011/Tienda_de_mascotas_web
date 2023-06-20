<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerTipoDeProducto.aspx.cs" Inherits="Vista.Tipos.VerTipoDeProducto" %>

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
            width: 137px;
        }
        .auto-style3 {
            width: 173px;
        }
        .auto-style4 {
            width: 185px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Ver tipos de productos</td>
                </tr>
            </table>
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Filtrar por cod</td>
                <td class="auto-style3">
                    <asp:TextBox ID="TB_Filtrar" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:Button ID="BT_Filtrar" runat="server" OnClick="BT_Filtrar_Click" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="BT_Todo" runat="server" Text="Todos" OnClick="BT_Todo_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table class="auto-style1">
            <tr>
                <td>
                    <asp:GridView ID="GV_Datos" runat="server">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
