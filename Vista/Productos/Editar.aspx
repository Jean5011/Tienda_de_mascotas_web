<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="Vista.Productos.Editar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            EDITOR DE PRODUCTOS<br />
            <br />
            <br />
            <br />
            <asp:GridView ID="grdProductos" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="grdProductos_RowCancelingEdit" OnRowEditing="grdProductos_RowEditing" OnRowUpdating="grdProductos_RowUpdating">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Codigo">
                        <EditItemTemplate>
                            <asp:Label ID="lbl_eit_codigo" runat="server" Text='<%# Bind("CodProducto_Prod") %>'></asp:Label>
                            <br />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_codigo" runat="server" Text='<%# Bind("CodProducto_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proveedor">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_eit_Prov" runat="server" Text='<%# Bind("CUITProveedor_Prod") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_Prov" runat="server" Text='<%# Bind("CUITProveedor_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_eit_Tipo" runat="server" Text='<%# Bind("CodTipoProducto_Prod") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_Tipo" runat="server" Text='<%# Bind("CodTipoProducto_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_eit_Nombre" runat="server" Text='<%# Bind("Nombre_Prod") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_Nombre" runat="server" Text='<%# Bind("Nombre_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Marca">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_eit_Marca" runat="server" Text='<%# Bind("Marca_Prod") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_Marca" runat="server" Text='<%# Bind("Marca_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripcion">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_eit_Desc" runat="server" Text='<%# Bind("Descripcion_Prod") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_Desc" runat="server" Text='<%# Bind("Descripcion_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stock">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_eit_Stock" runat="server" Text='<%# Bind("Stock_Prod") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_stock" runat="server" Text='<%# Bind("Stock_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Imagen">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_eit_Imagen" runat="server" Text='<%# Bind("Imagen_Prod") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_Imagen" runat="server" Text='<%# Bind("Imagen_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_eit_Precio" runat="server" Text='<%# Bind("PrecioUnitario_Prod") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_Precio" runat="server" Text='<%# Bind("PrecioUnitario_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado">
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk_eit_estado" runat="server" Checked='<%# Bind("Estado_Prod") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_it_estado" runat="server" Text='<%# Bind("Estado_Prod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <br />
            <br />
            <asp:Label ID="lbl_mensaje_error" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
