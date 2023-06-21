<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Empleados.Administrar" %>

<%@ Import Namespace="Entidades" %>

<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Plantilla</title>
    <link href="https://unpkg.com/material-components-web@latest/dist/material-components-web.min.css" rel="stylesheet">
    <script src="https://unpkg.com/material-components-web@latest/dist/material-components-web.min.js"></script>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link
        href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap"
        rel="stylesheet">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="/index.css" />
    <script src="/index.js"></script>
</head>

<body>
    <form id="form1" runat="server" class="contents">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <header class="mdc-top-app-bar mdc-top-app-bar--fixed">
            <div class="mdc-top-app-bar__row">
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-start">
                    <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
                        aria-label="Open navigation menu">
                        menu</button>
                    <span class="mdc-top-app-bar__title">Pet Shop</span>
                </section>
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">
                    <button class="mdc-button mdc-top-app-bar__action-item">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label">Proveedores</span>
                    </button>
                    <button class="mdc-button mdc-top-app-bar__action-item">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label">Clientes</span>
                    </button>
                    <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Search">search</button>
                    <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button"
                        aria-label="Options">
                        more_vert</button>
                </section>
            </div>
        </header>
        <main class="mdc-top-app-bar--fixed-adjust obj--main">
            <h2>Empleados</h2>
            <div class="searchbox">
                <span class="text">Buscar</span>
                <span class="material-icons">search</span>
            </div>
            <asp:TextBox ID="txtBuscar" runat="server"></asp:TextBox>
            <asp:CheckBox ID="chkEstado" Text="Mostrar empleados inactivos" Checked="false" runat="server" />
            <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" Text="Button" />
            
            <asp:GridView ID="GridView2" runat="server" CssClass="mdc-data-table" AutoGenerateColumns="False" aria-label="Dessert calories">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="mdc-data-table__cell">
                                <a href="Perfil.aspx?DNI=<%# Eval(Empleado.Columns.DNI) %>">Ver perfil</a>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DNI">
                        <ItemTemplate>
                            <div class="mdc-data-table__cell">
                                <span><%# Eval(Empleado.Columns.DNI) %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <div class="mdc-data-table__cell">
                                <span><%# Eval(Empleado.Columns.Nombre) %> <%# Eval(Empleado.Columns.Apellido) %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sexo">
                        <ItemTemplate>
                            <div class="mdc-data-table__cell">
                                <span><%# Eval(Empleado.Columns.Sexo) %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sueldo">
                        <ItemTemplate>
                            <div class="mdc-data-table__cell">
                                <span><%# Eval(Empleado.Columns.Sueldo) %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sueldo">
                        <ItemTemplate>
                            <div class="mdc-data-table__cell">
                                <span><%# Eval(Empleado.Columns.Direccion) %>, <%# Eval(Empleado.Columns.Localidad) %>, <%# Eval(Empleado.Columns.Provincia) %>, <%# Eval(Empleado.Columns.Nacionalidad) %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </main>
        <aside class="mdc-snackbar">
            <div class="mdc-snackbar__surface" role="status" aria-relevant="additions">
                <div class="mdc-snackbar__label" aria-atomic="false"></div>
            </div>
        </aside>
    </form>

</body>

</html>
