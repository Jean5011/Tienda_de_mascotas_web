<%@ Page MasterPageFile="/Root.Master" Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Tipos.Administrar" %>

<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Categorías</h2>
    <div class="searchbox">
        <asp:TextBox ID="txtBuscar" CssClass="search" placeholder="Buscar por código" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" CssClass="material-icons mdc-icon-button btn-search" OnClick="btnBuscar_Click" runat="server" Text="search" />
    </div>
     <div>
        Mostrar categorias inactivas
        <asp:CheckBox ID="ChkFiltro" runat="server"  />
    </div>
    <div class="mdc-data-table">
        <asp:GridView ID="GvDatos" runat="server" AutoGenerateColumns="False"
            OnPageIndexChanging="GvDatos_PageIndexChanging" AllowPaging="True" OnRowCreated="GvDatos_RowCreated" 
            PageSize="5" AutoGenerateSelectButton="False" OnRowEditing="GvDatos_RowEditing" 
            OnSelectedIndexChanging="GvDatos_SelectedIndexChanging" OnRowCancelingEdit="GvDatos_RowCancelingEdit" OnRowUpdating="GvDatos_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:LinkButton CommandName="update" runat="server"
                            class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Guardar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">save</i>
                        </asp:LinkButton>
                        <asp:LinkButton CommandName="cancel" runat="server"
                            class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Cancelar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">cancel</i>
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <div style="display: flex; flex-direction: row; justify-content: center; align-items: center">
                        <asp:LinkButton CommandName="edit" runat="server"
                            class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Editar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">edit</i>
                        </asp:LinkButton>
                        <asp:Panel runat="server" Visible='<%# Convert.ToBoolean(Eval(TipoProducto.Columns.Estado)) %>'>
                            <!--Estado = true -->
                            <asp:LinkButton runat="server" OnClientClick="return confirm('¿Estás seguro de deshabilitar este elemento?');" OnCommand="H_command" CommandName="Deshabilitar" CommandArgument="<%# Eval(TipoProducto.Columns.Codigo) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                                <div class="mdc-button__ripple"></div>
                                <span class="mdc-button__label mcardbl-act">Deshabilitar</span>
                                <i class="material-icons mdc-button__icon" aria-hidden="true">highlight_off</i>
                            </asp:LinkButton>
                        </asp:Panel>
                        <asp:Panel runat="server" Visible='<%# !Convert.ToBoolean(Eval(TipoProducto.Columns.Estado)) %>'>
                            <!--Estado = false -->
                            <asp:LinkButton  OnClientClick="return confirm('¿Estás seguro de habilitar este elemento?');" runat="server" OnCommand="H_command" CommandName="Habilitar" CommandArgument="<%# Eval(TipoProducto.Columns.Codigo) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                                <div class="mdc-button__ripple"></div>
                                <span class="mdc-button__label mcardbl-act">Habilitar</span>
                                <i class="material-icons mdc-button__icon" aria-hidden="true">check_circle_outline</i>
                            </asp:LinkButton>
                        </asp:Panel>
                            </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Código">
                    <EditItemTemplate>
                        <asp:Label ID="LV_EditCod" runat="server" Text='<%# Eval(TipoProducto.Columns.Codigo) %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="LV_CodTipoDeProducto"
                            runat="server" Text='<%# Eval(TipoProducto.Columns.Codigo) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Animal">
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="DD_EditAnimal" DataSourceID="SqlDataSource1" DataTextField="nombre_An" DataValueField="PK_CodAnimales_An">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:PetsConnectionString %>' SelectCommand="SELECT [PK_CodAnimales_An], CONCAT([nombre_An], ' ', [NombreDeRaza_An]) AS [nombre_An] FROM [Animales] WHERE [Estado_An] = 1"></asp:SqlDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <a class="mdc-typography--body2" ID="LV_CodAnimales" href='/Animales/?ID=<%# Eval(TipoProducto.Columns.CodAnimal) %>'>
                            <%# Eval(Animal.Columns.Nombre) %> (<%# Eval(Animal.Columns.Raza) %>)
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Categoría">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DD_EditTdp" runat="server">
                            <asp:ListItem>Comida</asp:ListItem>
                            <asp:ListItem>Accesorios</asp:ListItem>
                            <asp:ListItem>Ropa</asp:ListItem>
                            <asp:ListItem>Higiene</asp:ListItem>
                            <asp:ListItem>Salud</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="LV_TipoDeProducto"
                            runat="server" Text='<%# Eval(TipoProducto.Columns.TipoDeProducto) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripción">
                    <EditItemTemplate>
                        <asp:TextBox ID="TB_EditDesc" runat="server" Text='<%# Eval(TipoProducto.Columns.Descripcion) %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="LV_Descripcion"
                            runat="server" Text='<%# Eval(TipoProducto.Columns.Descripcion) %>'>
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
                        <asp:DropDownList ID="DdlFilasPorPaginaPagerTemplate" CssClass="mdc-text-field__input"
                            AutoPostBack="true" runat="server"
                            OnSelectedIndexChanged="DdlFilasPorPaginaPagerTemplate_SelectedIndexChanged">

                            <asp:ListItem Selected="True">5</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>

                        </asp:DropDownList>
                    </label>
                    <div class="pager-space"></div>
                    <asp:LinkButton ID="GvDatosPagerFirst" runat="server" CommandName="Page"
                        CommandArgument="First" CssClass="mdc-icon-button mdc-button--primary">
                            <span class="mdc-icon-button__ripple"></span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">first_page</i>
                            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="GvDatosPagerPrev" runat="server" CommandName="Page"
                        CommandArgument="Prev" CssClass="mdc-icon-button mdc-button--primary">
                            <span class="mdc-icon-button__ripple"></span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_left</i>
                            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <label class="mdc-text-field mdc-text-field--outlined mdc-text-field--no-label page-roll">
                        <span class="mdc-notched-outline">
                            <span class="mdc-notched-outline__leading"></span>
                            <span class="mdc-notched-outline__trailing"></span>
                        </span>
                        <asp:TextBox type="number" CssClass="mdc-text-field__input" ID="GvDatosPagerPageTxtBox"
                            runat="server" OnTextChanged="GvDatosPagerPageTxtBox_TextChanged"
                            AutoPostBack="true">
                        </asp:TextBox>
                    </label>
                    <asp:LinkButton ID="GvDatosPagerNext" runat="server" CommandName="Page"
                        CommandArgument="Next" CssClass="mdc-icon-button mdc-button--primary">
                            <span class="mdc-icon-button__ripple"></span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_right</i>
                            <!--span class="mdc-button__label"></span--> 
                    </asp:LinkButton>
                    <asp:LinkButton ID="GvDatosPagerLast" runat="server" CommandName="Page"
                        CommandArgument="Last" CssClass="mdc-icon-button mdc-button--primary">
                            <span class="mdc-icon-button__ripple"></span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">last_page</i>
                            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                </div>
            </PagerTemplate>
        </asp:GridView>

    </div>
    <asp:Label ID="lbl_mensaje_error" runat="server"></asp:Label>
    <a href="/Tipos/Agregar.aspx" class="mdc-fab" id="fab" aria-label="Agregar">
      <div class="mdc-fab__ripple"></div>
      <span class="mdc-fab__icon material-icons">add</span>
    </a>
</asp:Content>
