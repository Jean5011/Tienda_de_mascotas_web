<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Vista.Empleados.Perfil" %>
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
    <link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet">
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
                    <span class="mdc-top-app-bar__title" runat="server" id="spanPageTitle">PetShop</span>
                </section>
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">
                    <ASP:LinkButton ID="lbIniciarSesion" runat="server" OnClick="IniciarSesion" CssClass="mdc-button mdc-button--raised _header-important-btn mdc-top-app-bar__action-item">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label">Iniciar sesión</span>
                    </ASP:LinkButton>
                    <ASP:LinkButton ID="lbActualUser" OnClick="VerPerfilActual" runat="server" CssClass="mdc-button mdc-top-app-bar__action-item _header-profile-btn">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label"><b runat="server" id="lbAUNombre"></b><br>
                            <span runat="server" id="lbAURol"></span></span>
                    </ASP:LinkButton>
                </section>
            </div>
        </header>
        <main class="mdc-top-app-bar--fixed-adjust obj--main main-grid">
            <div class="col perfil">
                <h2 id="NombreEmpleadoTitulo" runat="server"></h2>
                <ul class="mdc-card mdc-card--outlined mdc-list mdc-list--two-line">
                    <asp:ListView ID="DetallesList" runat="server">
                        <ItemTemplate>
                            <div class="mdc-list-item">
                                <span class="mdc-list-item__ripple"></span>
                                <span class="mdc-list-item__text">
                                    <span class="mdc-list-item__primary-text"><%# Eval("Valor") %></span>
                                    <span class="mdc-list-item__secondary-text"><%# Eval("Propiedad") %></span>
                                </span>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    <li role="separator" class="mdc-list-divider"></li>
                    <asp:LinkButton runat="server" class="mdc-list-item" ID="BtnEditarDetalles" OnClick="BtnEditarDetalles_Click">
                        <span class="mdc-list-item__ripple"></span>
                        <span class="mdc-deprecated-list-item__graphic material-icons" aria-hidden="true">
                            edit
                        </span>
                        <span class="mdc-list-item__text">
                            <span class="mdc-list-item__primary-text">Editar información</span>
                            <span class="mdc-list-item__secondary-text">Cambiar sueldo, dirección.</span>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" class="mdc-list-item" ID="BtnDeshabilitar" OnClick="BtnDeshabilitar_Click">
                        <span class="mdc-list-item__ripple"></span>
                        <span class="mdc-deprecated-list-item__graphic material-icons" aria-hidden="true">
                            delete
                        </span>
                        <span class="mdc-list-item__text">
                            <span class="mdc-list-item__primary-text">Deshabilitar empleado</span>
                            <span class="mdc-list-item__secondary-text">En caso de despido, renuncia.</span>
                        </span>
                    </asp:LinkButton>
                </ul>
                <br>
            </div>
            <div class="col principal">
                <h2>Ventas que registró</h2>
                 <asp:GridView ID="gvVentas" CssClass="mdc-data-table" AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <a href="/Ventas/VerFactura.aspx?ID=<%# Eval(Venta.Columns.Id) %>">Ver detalles</a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbID" runat="server" Text="<%# Eval(Venta.Columns.Id) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empleado Gestor">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbEMPLEADOGESTOR" runat="server" Text="<%# Eval(Venta.Columns.DNI) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Medio de pago">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbTIPOPAGO" runat="server" Text="<%# Eval(Venta.Columns.TipoPago) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbFECHA" runat="server" Text="<%# Eval(Venta.Columns.Fecha) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbPTOTAL" runat="server" Text="<%# Eval(Venta.Columns.Total) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </main>
        <aside class="mdc-snackbar">
            <div class="mdc-snackbar__surface" role="status" aria-relevant="additions">
                <div class="mdc-snackbar__label" aria-atomic="false"></div>
            </div>
        </aside>
    </form>
</body>
</html>
