<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerAnimales.aspx.cs" Inherits="Vista.Animales.VerAnimales" %>

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
            width: 151px;
        }
        .auto-style3 {
            width: 177px;
        }
        .auto-style4 {
            width: 294px;
        }
    </style>
</head>
<body>
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><h1>Ver animales</h1></td>
        </tr>
    </table>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">Filtrar por codigo:</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TB_Filtrar" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4">
                        <asp:Button ID="BT_Filtrar" runat="server" Text="Filtrar" />
                    </td>
                    <td>
                        <asp:Button ID="BT_Todos" runat="server" Text="Todos" OnClick="BT_Todos_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table class="auto-style1">
            <tr>
                <td>
                    <asp:GridView ID="GV_Datos" runat="server" OnSelectedIndexChanged="GV_Datos_SelectedIndexChanged">
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
