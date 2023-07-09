<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Empleados.Administrar" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Empleados</h2>
    <div class="searchbox">
        <asp:TextBox ID="txtBuscar" CssClass="search" placeholder="Buscar por nombre, apellido" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" CssClass="material-icons mdc-icon-button btn-search" OnClick="BtnBuscar_Click" runat="server" Text="search" />
    </div>
    <asp:DropDownList ID="ddlRol" runat="server">
        <asp:ListItem Text="Todos los roles" Selected="true" Value="ALL"></asp:ListItem>
        <asp:ListItem Text="Administrador" Value="ADMIN"></asp:ListItem>
        <asp:ListItem Text="No Administradores" Value="NORMAL"></asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSexo" runat="server">
        <asp:ListItem Text="Todos" Selected="True" Value="ALL"></asp:ListItem>
        <asp:ListItem Text="Hombres" Value="M"></asp:ListItem>
        <asp:ListItem Text="Mujeres" Value="F"></asp:ListItem>
    </asp:DropDownList>
    <asp:CheckBox ID="chkEstado" Text="Mostrar empleados inactivos" Checked="false" runat="server" />
    
    <br />
    <div class="mdc-data-table">
        <asp:GridView ID="gvAdmin" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GvAdmin_PageIndexChanging"
            AllowPaging="True" OnRowCreated="GvAdmin_RowCreated" PageSize="10" AutoGenerateSelectButton="False"
            OnSelectedIndexChanging="GvAdmin_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="/Empleados/Perfil.aspx?DNI=<%# Eval(Empleado.Columns.DNI) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Ver</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">open_in_new</i>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DNI">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvAdminItemTemplate__DNI" runat="server"
                            Text='<%# Eval(Empleado.Columns.DNI) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvAdminItemTemplate__Nombre" runat="server"
                            Text='<%# Eval(Empleado.Columns.Nombre) + " " + Eval(Empleado.Columns.Apellido) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sexo">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvAdminItemTemplate__Sexo" runat="server"
                            Text='<%# Eval(Empleado.Columns.Sexo) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sueldo">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvAdminItemTemplate__Sueldo" runat="server"
                            Text='<%# Eval(Empleado.Columns.Sueldo) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dirección">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvAdminItemTemplate__Dirección" runat="server"
                            Text='<%# Eval(Empleado.Columns.Direccion) + ", " + Eval(Empleado.Columns.Localidad) + ", " + Eval(Empleado.Columns.Provincia) + ", " + Eval(Empleado.Columns.Nacionalidad) %>'>
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

                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem Selected="True">10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>

                        </asp:DropDownList>
                    </label>
                    <div class="pager-space"></div>
                    <asp:LinkButton ID="gvAdminPagerFirst" runat="server" CommandName="Page" CommandArgument="First"
                        CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">first_page</i>
            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="gvAdminPagerPrev" runat="server" CommandName="Page" CommandArgument="Prev"
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
                        <asp:TextBox type="number" CssClass="mdc-text-field__input" ID="gvAdminPagerPageTxtBox" runat="server"
                            OnTextChanged="GvAdminPagerPageTxtBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </label>
                    <asp:LinkButton ID="gvAdminPagerNext" runat="server" CommandName="Page" CommandArgument="Next"
                        CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_right</i>
            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="gvAdminPagerLast" runat="server" CommandName="Page" CommandArgument="Last"
                        CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">last_page</i>
            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                </div>
            </PagerTemplate>
        </asp:GridView>

    </div>

    <div class="mdc-data-table">
        <asp:GridView ID="gvEmpleado" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GvEmpleado_PageIndexChanging"
            AllowPaging="True" OnRowCreated="GvEmpleado_RowCreated" PageSize="5" AutoGenerateSelectButton="False"
            OnSelectedIndexChanging="GvEmpleado_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvEmpleadoItemTemplate__Nombre" runat="server"
                            Text='<%# Eval(Empleado.Columns.Nombre) + " " + Eval(Empleado.Columns.Apellido) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sexo">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvEmpleadoItemTemplate__Sexo" runat="server"
                            Text='<%# Eval(Empleado.Columns.Sexo) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Región">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvEmpleadoItemTemplate__Dirección" runat="server"
                            Text='<%# Eval(Empleado.Columns.Provincia) + ", " + Eval(Empleado.Columns.Nacionalidad) %>'>
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
                        <asp:DropDownList ID="gvEmpleadoddlFilasPorPaginaPagerTemplate" CssClass="mdc-text-field__input"
                            AutoPostBack="true" runat="server"
                            OnSelectedIndexChanged="GvEmpleadoddlFilasPorPaginaPagerTemplate_SelectedIndexChanged">

                            <asp:ListItem Selected="True">5</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>

                        </asp:DropDownList>
                    </label>
                    <div class="pager-space"></div>
                    <asp:LinkButton ID="gvEmpleadoPagerFirst" runat="server" CommandName="Page" CommandArgument="First"
                        CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">first_page</i>
            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="gvEmpleadoPagerPrev" runat="server" CommandName="Page" CommandArgument="Prev"
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
                        <asp:TextBox type="number" CssClass="mdc-text-field__input" ID="gvEmpleadoPagerPageTxtBox" runat="server"
                            OnTextChanged="GvEmpleadoPagerPageTxtBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </label>
                    <asp:LinkButton ID="gvEmpleadoPagerNext" runat="server" CommandName="Page" CommandArgument="Next"
                        CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_right</i>
            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="gvEmpleadoPagerLast" runat="server" CommandName="Page" CommandArgument="Last"
                        CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">last_page</i>
            <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                </div>
            </PagerTemplate>
        </asp:GridView>

    </div>
    <a href="/Empleados/CrearCuenta.aspx" class="mdc-fab" id="fab" aria-label="Agregar">
      <div class="mdc-fab__ripple"></div>
      <span class="mdc-fab__icon material-icons">person_add</span>
    </a>

</asp:Content>
