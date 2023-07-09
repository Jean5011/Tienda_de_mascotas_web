<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Productos.Editar" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Productos</h2>
    <div class="searchbox">
        <asp:TextBox ID="txtBuscar" CssClass="search" placeholder="Buscar por código" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" CssClass="material-icons mdc-icon-button btn-search" OnClick="BtnBuscar_Click" runat="server" Text="search" />
    </div>
    <br />
    <div>
        Mostrar productos inactivos
        <asp:CheckBox ID="CheckBox1" runat="server" />
    </div>
    <div>
        <asp:DropDownList ID="ddlFiltro" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <div class="mdc-data-table">
        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False"
            OnPageIndexChanging="GvDatos_PageIndexChanging" AllowPaging="True" OnRowCreated="GvDatos_RowCreated"
            PageSize="5" AutoGenerateSelectButton="False" OnSelectedIndexChanging="GvDatos_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="/Productos/Editar.aspx?ID=<%# Eval(Producto.Columns.Codigo_Prod) %>"
                            class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Editar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">open_in_new</i>
                        </a>
                        <asp:LinkButton runat="server" Visible="<%# Convert.ToBoolean(Eval(Producto.Columns.Estado)) %>" OnClientClick="return confirm('¿Estás seguro de deshabilitar este registro?');" OnCommand="Lb_Command" CommandName="Deshabilitar" CommandArgument="<%# Eval(Producto.Columns.Codigo_Prod) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Deshabilitar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">delete</i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" Visible="<%# !Convert.ToBoolean(Eval(Producto.Columns.Estado)) %>" OnClientClick="return confirm('¿Estás seguro de habilitar este registro?');" OnCommand="Lb_Command" CommandName="Habilitar" CommandArgument="<%# Eval(Producto.Columns.Codigo_Prod) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Habilitar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">unarchive</i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Codigo" runat="server"
                            Text='<%# Eval(Producto.Columns.Codigo_Prod) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proveedor">
                    <ItemTemplate>
                        <a ID="gvDatosItemTemplate__Proveedor" class="mdc-typography--body2" href="/Proveedores/?ID=<%# Eval(Producto.Columns.CUITProv) %>">
                            <%# Eval(Proveedor.Columns.RazonSocial) %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Categoría">
                    <ItemTemplate>
                        <a ID="gvDatosItemTemplate__TP" class="mdc-typography--body2" href="/Tipos/?ID=<%# Eval(Producto.Columns.CodTipoProducto) %>">
                            <%# Eval(TipoProducto.Columns.TipoDeProducto) %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Nombre" runat="server"
                            Text='<%# Eval(Producto.Columns.Nombre) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Marca">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Marca" runat="server"
                            Text='<%# Eval(Producto.Columns.Marca) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Descripcion" runat="server"
                            Text='<%# Eval(Producto.Columns.Descripcion) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stock">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Stock" runat="server"
                            Text='<%# Eval(Producto.Columns.Stock) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Precio">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Precio" runat="server"
                            Text='<%# Eval(Producto.Columns.Precio) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                <div class="pager">
                    <span class="mdc-typography--body2" style="white-space: nowrap;">Filas por página: </span>
                    <label class="mdc-text-field mdc-text-field--outlined mdc-text-field--no-label page-roll">
                        <span class="mdc-notched-outline">
                            <span class="mdc-notched-outline__leading"></span>
                            <span class="mdc-notched-outline__trailing"></span>
                        </span>
                        <asp:DropDownList ID="ddlFilasPorPaginaPagerTemplate" CssClass="mdc-text-field__input"
                            AutoPostBack="true" runat="server"
                            OnSelectedIndexChanged="DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged">

                            <asp:ListItem Selected="True">5</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>

                        </asp:DropDownList>
                    </label>
                    <div class="pager-space"></div>
                    <asp:LinkButton ID="gvDatosPagerFirst" runat="server" CommandName="Page" CommandArgument="First"
                        CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">first_page</i>
                    <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="gvDatosPagerPrev" runat="server" CommandName="Page" CommandArgument="Prev"
                        CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_left</i>
                    <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <label class="mdc-text-field mdc-text-field--outlined mdc-text-field--no-label page-roll">
                        <span class="mdc-notched-outline">
                            <span class="mdc-notched-outline__leading"></span>
                            <span class="mdc-notched-outline__trailing"></span>
                        </span>
                        <asp:TextBox type="number" CssClass="mdc-text-field__input" ID="gvDatosPagerPageTxtBox"
                            runat="server" OnTextChanged="GvDatosPagerPageTxtBox_TextChanged" AutoPostBack="true">
                        </asp:TextBox>
                    </label>
                    <asp:LinkButton ID="gvDatosPagerNext" runat="server" CommandName="Page" CommandArgument="Next"
                        CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_right</i>
                    <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="gvDatosPagerLast" runat="server" CommandName="Page" CommandArgument="Last"
                        CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">last_page</i>
                    <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                </div>
            </PagerTemplate>
        </asp:GridView>

    </div>
    <br />
    <br />
    <asp:Label ID="lbl_mensaje_error" runat="server"></asp:Label>
    <a href="/Productos/Agregar.aspx" class="mdc-fab" id="fab" aria-label="Agregar">
      <div class="mdc-fab__ripple"></div>
      <span class="mdc-fab__icon material-icons">add</span>
    </a>
</asp:Content>
