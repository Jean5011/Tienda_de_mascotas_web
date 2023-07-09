<%@ Page MasterPageFile="/Root.Master" Language="C#" AutoEventWireup="true" CodeBehind="VerFactura.aspx.cs" Inherits="Vista.Ventas.VerFactura" %>

<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mdc-card col perfil">
        <br />
        <br />
        <br />
        <ul class="mdc-card mdc-list mdc-list--two-line">
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
                    <asp:Label runat="server" ID="lblNroVenta" class="mdc-list-item__primary-text"></asp:Label>
                    <span class="mdc-list-item__secondary-text">Número de Venta</span>
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

        <asp:LinkButton OnClick="BtnBorrar_Click" runat="server" ID="BtnBorrar" CssClass="mdc-button mdc-card__action mdc-card__action--button">
                    <div class="mdc-button__ripple"></div>
                    <span class="mdc-button__label mcardbl-act">Eliminar permanentemente</span>
                    <i class="material-icons mdc-button__icon danger-color" aria-hidden="true">delete</i>
        </asp:LinkButton>
    </div>
    <div class="col col-no-perfil">
        <h2 id="h2Factura" runat="server"></h2>
        <br />
        <div class="mdc-card banner-agregar-producto" style="padding: 12px;">
            <div class="row _p">
                <div class="_side" style="flex-direction: row; gap: 12px; justify-content: flex-start">
                    <div>
                    <label class="mdc-text-field mdc-text-field--filled">
                        <span class="mdc-floating-label" id="ddlrol-d">Producto</span>
                        <span class="mdc-line-ripple"></span>
                        <span class="mdc-text-field__ripple"></span>
                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="ddl mdc-text-field__input" AutoPostBack="True" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged"></asp:DropDownList>
                    </label>                                               
                        <div class="mdc-text-field-helper-line">
                            <asp:RequiredFieldValidator CssClass="mdc-text-field-helper-text mdc-text-field-helper-text--persistent" ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProducto" InitialValue="0" ErrorMessage="Seleccioná un producto"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div>
                    <label class="mdc-text-field mdc-text-field--filled">
                        <span class="mdc-floating-label" id="ddlrol-d">Proveedor</span>
                        <span class="mdc-line-ripple"></span>
                        <span class="mdc-text-field__ripple"></span>
                        <asp:DropDownList CssClass="ddl mdc-text-field__input" ID="ddlProveedor" runat="server"></asp:DropDownList>
                    </label>                        
                        <div class="mdc-text-field-helper-line">
                            <asp:RequiredFieldValidator CssClass="mdc-text-field-helper-text mdc-text-field-helper-text--persistent" ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProveedor" InitialValue="0" ErrorMessage="Seleccioná un proveedor"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div>
                        <label class="mdc-text-field mdc-text-field--filled">
                            <span class="mdc-floating-label" id="ddlrol-d">Cantidad</span>
                            <span class="mdc-line-ripple"></span>
                            <span class="mdc-text-field__ripple"></span>
                            <asp:TextBox  ValidationGroup="X" CausesValidation="True" CssClass="mdc-text-field__input" ID="txtCantidad" type="number" runat="server"></asp:TextBox>
                        </label>

                        <div class="mdc-text-field-helper-line">
                            <asp:RegularExpressionValidator CssClass="mdc-text-field-helper-text mdc-text-field-helper-text--persistent" ID="CantValidator" runat="server" ControlToValidate="txtCantidad"
                                ErrorMessage="Ingrese un número mayor a 0" ValidationGroup="X" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <asp:LinkButton ID="btnAgregar" ValidationGroup="X" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="BtnAgregar_Click">
            <span class="mdc-button__label">Agregar producto</span>
                    </asp:LinkButton>

                </div>
            </div>
        </div>
        <br />
        <br />
        <h2>Productos</h2>
        <div class="auto-style1">
            <asp:GridView ID="gvDetalles" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton CssClass="mdc-button mdc-card__action mdc-card__action--button" runat="server"
                                ID="GVDETALLESBTNELIMINAR" OnCommand="GVDETALLESBTNELIMINAR_Command" CommandName="ELIMINAR"
                                Text="Eliminar" CommandArgument="<%# Eval(DetalleVenta.Columns.CodProducto_Dv) %>">
                        <div class="mdc-button__ripple"></div>
                        <span class="mdc-button__label mcardbl-act">Eliminar</span>
                        <i class="material-icons mdc-button__icon danger-color" aria-hidden="true">clear</i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Producto">
                        <ItemTemplate>
                            <a class="mdc-typography--body2" id="gvDetallesItemTemplate__Producto" href="/Productos/?CODIGO=<%# Eval(DetalleVenta.Columns.CodProducto_Dv) %>">
                                <%# Eval(Producto.Columns.Nombre) %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proveedor">
                        <ItemTemplate>
                            <a class="mdc-typography--body2" id="gvDetallesItemTemplate__Proveedor" href="/Proveedores/?ID=<%# Eval(DetalleVenta.Columns.CUITProv) %>">
                                <%# Eval(Proveedor.Columns.RazonSocial) %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad">
                        <ItemTemplate>
                            <div class="preu_container">
                                <asp:LinkButton class="mdc-icon-button material-icons"  ID="btnRestar" runat="server" CommandName="Restar" CommandArgument="<%#Eval(DetalleVenta.Columns.CodProducto_Dv)%>" OnCommand="modificarCantidadVendida_Command">
                              <div class="mdc-icon-button__ripple"></div>
                              remove
                            </asp:LinkButton>
                            <asp:Label CssClass="mdc-typography--body2" ID="gvDetallesItemTemplate__Cantidad" runat="server"
                                Text='<%# Eval(DetalleVenta.Columns.Cantidad_Dv) %>'>
                            </asp:Label>
                            <asp:LinkButton class="mdc-icon-button material-icons" ID="btnSumar" runat="server" Text="+" CommandName="Sumar" CommandArgument="<%#Eval(DetalleVenta.Columns.CodProducto_Dv)%>" OnCommand="modificarCantidadVendida_Command" >
                              <div class="mdc-icon-button__ripple"></div>
                              add
                            </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio Unitario">
                        <ItemTemplate>
                            <asp:Label CssClass="mdc-typography--body2" ID="gvDetallesItemTemplate__PU" runat="server"
                                Text='<%# Eval(DetalleVenta.Columns.PrecioUnitario_Dv) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio Total">
                        <ItemTemplate>
                            <asp:Label CssClass="mdc-typography--body2" ID="gvDetallesItemTemplate__PT" runat="server"
                                Text='<%# Eval(DetalleVenta.Columns.PrecioTotal_Dv) %>'>
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
                        <asp:LinkButton ID="gvDetallesPagerFirst" runat="server" CommandName="Page" CommandArgument="First"
                            CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">first_page</i>
                    <!--span class="mdc-button__label"></span-->
                        </asp:LinkButton>
                        <asp:LinkButton ID="gvDetallesPagerPrev" runat="server" CommandName="Page" CommandArgument="Prev"
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
                            <asp:TextBox type="number" CssClass="mdc-text-field__input" ID="gvDetallesPagerPageTxtBox"
                                runat="server" OnTextChanged="GvDetallesPagerPageTxtBox_TextChanged" AutoPostBack="true">
                            </asp:TextBox>
                        </label>
                        <asp:LinkButton ID="gvDetallesPagerNext" runat="server" CommandName="Page" CommandArgument="Next"
                            CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_right</i>
                    <!--span class="mdc-button__label"></span-->
                        </asp:LinkButton>
                        <asp:LinkButton ID="gvDetallesPagerLast" runat="server" CommandName="Page" CommandArgument="Last"
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
            <br />
            <br />
            <br />
            <br />
    </div>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            background-color: #fff;
            background-color: var(--mdc-theme-surface, #fff);
            border-radius: 4px;
            border-radius: var(--mdc-shape-medium, 4px);
            border-width: 1px;
            border-style: solid;
            -webkit-overflow-scrolling: touch;
            display: inline-flex;
            flex-direction: column;
            box-sizing: border-box;
            position: relative;
            left: 7px;
            top: 1px;
        }
    </style>
</asp:Content>

