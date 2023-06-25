<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="VerFactura.aspx.cs" Inherits="Vista.Ventas.VerFactura" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

</asp:Content>
