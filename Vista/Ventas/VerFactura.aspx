<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerFactura.aspx.cs" Inherits="Vista.Ventas.VerFactura" %>
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
    <form runat="server" id="form1" class="contents">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <header class="mdc-top-app-bar mdc-top-app-bar--fixed">
            <div class="mdc-top-app-bar__row">
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-start">
                    <span class="mdc-top-app-bar__title" runat="server" id="spanPageTitle">PetShop</span>
                </section>
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">
                    <asp:LinkButton ID="lbIniciarSesion" runat="server" OnClick="IniciarSesion" CssClass="mdc-button mdc-button--raised _header-important-btn mdc-top-app-bar__action-item">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label">Iniciar sesión</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbActualUser" OnClick="VerPerfilActual" runat="server" CssClass="mdc-button mdc-top-app-bar__action-item _header-profile-btn">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label"><b runat="server" id="lbAUNombre"></b>
                            <br>
                            <span runat="server" id="lbAURol"></span></span>
                    </asp:LinkButton>
                </section>
            </div>
        </header>
        <main class="mdc-top-app-bar--fixed-adjust obj--main main-grid">
            <div class="col perfil">
                <ul class="mdc-card mdc-card--outlined mdc-list mdc-list--two-line">
                    <li class="mdc-list-item" tabindex="0">
                        <span class="mdc-list-item__ripple"></span>
                        <span class="mdc-list-item__text">
                            <span class="mdc-list-item__primary-text"><b>
                                <asp:Label ID="lblTotalCalculado" runat="server" Text="Label"></asp:Label></b></span>
                            <span class="mdc-list-item__secondary-text">Total de la compra</span>
                        </span>
                    </li>
                    <li class="mdc-list-item" tabindex="0">
                        <span class="mdc-list-item__ripple"></span>
                        <span class="mdc-list-item__text">
                            <asp:Label runat="server" ID="lblEmpleadoGestor" class="mdc-list-item__primary-text"></asp:Label>
                            <span class="mdc-list-item__secondary-text">Empleado gestor</span>
                        </span>
                    </li>
                    <li class="mdc-list-item">
                        <span class="mdc-list-item__ripple"></span>
                        <span class="mdc-list-item__text">
                            <asp:Label runat="server" ID="lblMedioPago" class="mdc-list-item__primary-text"></asp:Label>
                            <span class="mdc-list-item__secondary-text">Medio de pago utilizado</span>
                        </span>
                    </li>
                    <li class="mdc-list-item">
                        <span class="mdc-list-item__ripple"></span>
                        <span class="mdc-list-item__text">
                            <asp:Label runat="server" ID="lblFechaRegistro" class="mdc-list-item__primary-text"></asp:Label>
                            <span class="mdc-list-item__secondary-text">Fecha de registro</span>
                        </span>
                    </li>
                </ul>
            </div>
            <div class="col">
                <h2 id="h2Factura" runat="server"></h2>
                <asp:TextBox ID="txtIDProducto" placeholder="ID Producto" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtCantidad" placeholder="Cantidad" type="number" runat="server"></asp:TextBox>
                <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" Text="Agregar Producto" />
                <br />
                <br />
                <h2>Productos</h2>
                <asp:GridView ID="gvDetalles" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Button runat="server" ID="GVDETALLESBTNELIMINAR" OnCommand="GVDETALLESBTNELIMINAR_Command" CommandName="ELIMINAR" Text="Eliminar" CommandArgument="<%# Eval(DetalleVenta.Columns.CodProducto_Dv) %>" />
                                    
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producto">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbCODIGOPRODUCTO" runat="server" Text="<%# Eval(DetalleVenta.Columns.CodProducto_Dv) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proveedor">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbCUITPROVEEDOR" runat="server" Text="<%# Eval(DetalleVenta.Columns.CUITProv) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbCANTIDAD" runat="server" Text="<%# Eval(DetalleVenta.Columns.Cantidad_Dv) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio Unitario">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbPU" runat="server" Text="<%# Eval(DetalleVenta.Columns.PrecioUnitario_Dv) %>"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio Total">
                            <ItemTemplate>
                                <div class="mdc-data-table__cell">
                                    <asp:Label ID="lbPTOTAL" runat="server" Text="<%# Eval(DetalleVenta.Columns.PrecioTotal_Dv) %>"></asp:Label>
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
