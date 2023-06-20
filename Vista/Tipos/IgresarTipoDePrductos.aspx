<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IgresarTipoDePrductos.aspx.cs" Inherits="Vista.Tipos.VerTipoDePrductos" %>

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
            width: 206px;
        }
        .auto-style3 {
            width: 238px;
        }
    </style>
</head>
<body>
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Ingresar tipos de productos</td>
        </tr>
    </table>
    <form id="form1" runat="server">
        <div>
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
        </div>
    </form>
</body>
</html>
