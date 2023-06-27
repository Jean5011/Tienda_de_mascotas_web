<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="Editar.aspx.cs" Inherits="Vista.Productos.Editar1" %>

<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tb_editar {
            padding: 2px;
            width: 90%;
        }

        td {
            padding: 0;
        }
    </style>
    <div>
        <h1>Editar productos</h1>
        <div class="mdc-data-table">
            <div>
                <asp:GridView ID="Tabla_Productos_Gdv" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Tabla_Productos_Gdv_SelectedIndexChanged">

                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label CssClass="tb_editar" ID="Codigo_Prod_lb" runat="server"
                                    Text='<%# Eval(Producto.Columns.Codigo_Prod) %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proveedor">
                            <ItemTemplate>
                                <asp:TextBox ID="CUITProv_tb" runat="server" CssClass="tb_editar"
                                    Text='<%# Eval(Producto.Columns.CUITProv) %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Categoría">
                            <ItemTemplate>
                                <asp:TextBox ID="CodTipoProducto_tb" runat="server" CssClass="tb_editar"
                                    Text='<%#  Eval(Producto.Columns.CodTipoProducto) %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:TextBox ID="Nombre_tb" runat="server" CssClass="tb_editar"
                                    Text='<%# Eval(Producto.Columns.Nombre) %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <asp:TextBox ID="Marca_tb" runat="server" CssClass="tb_editar"
                                    Text='<%# Eval(Producto.Columns.Marca) %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripcion">
                            <ItemTemplate>
                                <asp:TextBox ID="Descripcion_tb" runat="server" CssClass="tb_editar"
                                    Text='<%# Eval(Producto.Columns.Descripcion) %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:TextBox ID="Stock_tb" runat="server" CssClass="tb_editar"
                                    Text='<%# Eval(Producto.Columns.Stock) %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio">
                            <ItemTemplate>
                                <asp:TextBox ID="Precio_tb" runat="server" CssClass="tb_editar"
                                    Text='<%# Eval(Producto.Columns.Precio) %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:Label ID="lbl_mensaje_error" runat="server"></asp:Label>

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

</asp:Content>
