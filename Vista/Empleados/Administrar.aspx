<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Empleados.Administrar" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Empleados</h2>
    <div class="searchbox">
        <asp:TextBox ID="txtBuscar" placeholder="Buscar por nombre, apellido" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" CssClass="material-icons mdc-icon-button" OnClick="btnBuscar_Click" runat="server" Text="search" />
    </div>
    <asp:CheckBox ID="chkEstado" Text="Mostrar empleados inactivos" Checked="false" runat="server" />
    <br />
    <asp:GridView ID="gvAdmin" runat="server" CssClass="mdc-data-table" AutoGenerateColumns="False" aria-label="Dessert calories">
        <columns>
            <asp:TemplateField HeaderText="Acciones">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <a href="Perfil.aspx?DNI=<%# Eval(Empleado.Columns.DNI) %>">Ver perfil</a>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DNI">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <span><%# Eval(Empleado.Columns.DNI) %></span>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <span><%# Eval(Empleado.Columns.Nombre) %> <%# Eval(Empleado.Columns.Apellido) %></span>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sexo">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <span><%# Eval(Empleado.Columns.Sexo) %></span>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sueldo">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <span><%# Eval(Empleado.Columns.Sueldo) %></span>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Dirección">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <span><%# Eval(Empleado.Columns.Direccion) %>, <%# Eval(Empleado.Columns.Localidad) %>, <%# Eval(Empleado.Columns.Provincia) %>, <%# Eval(Empleado.Columns.Nacionalidad) %></span>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
        </columns>
    </asp:GridView>

    <asp:GridView ID="gvEmpleado" runat="server" CssClass="mdc-data-table" AutoGenerateColumns="False" aria-label="Dessert calories">
        <columns>
            <asp:TemplateField HeaderText="Nombre">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <span><%# Eval(Empleado.Columns.Nombre) %> <%# Eval(Empleado.Columns.Apellido) %></span>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sexo">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <span><%# Eval(Empleado.Columns.Sexo) %></span>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Región">
                <itemtemplate>
                    <div class="mdc-data-table__cell">
                        <span><%# Eval(Empleado.Columns.Provincia) %>, <%# Eval(Empleado.Columns.Nacionalidad) %></span>
                    </div>
                </itemtemplate>
            </asp:TemplateField>
        </columns>
    </asp:GridView>
</asp:Content>
