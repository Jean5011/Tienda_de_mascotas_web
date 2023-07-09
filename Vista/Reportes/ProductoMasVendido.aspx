<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="ProductoMasVendido.aspx.cs" Inherits="Vista.Reportes.ProductoMasVendido" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Producto más vendido</h2>
    <br />
    <div class="mdc-card " style="padding: 10px;">
        <div class="row _p">
            <div class="_side" style="flex-direction: row; gap: 12px; justify-content: space-between">
                <div>
                    <label class="mdc-text-field mdc-text-field--filled">
                        <span class="mdc-floating-label" id="ddlrol-d">Desde</span>
                        <span class="mdc-line-ripple"></span>
                        <span class="mdc-text-field__ripple"></span>
                        <asp:TextBox CausesValidation="True" ID="txtFechaInicio" ValidationGroup="A" CssClass="mdc-text-field__input" type="date" runat="server" placeholder="Fecha Inicio"></asp:TextBox>
                    </label>
                    <div class="mdc-text-field-helper-line">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechaInicio" CssClass="mdc-text-field-helper-text" ErrorMessage="Ingrese una fecha válida." Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFechaInicio"
                            ErrorMessage="Ingrese una fecha válida" ValidationExpression="^\d{4}-\d{2}-\d{2}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div>

                    <label class="mdc-text-field mdc-text-field--filled">
                        <span class="mdc-floating-label" id="ddlsexo-d">Hasta</span>
                        <span class="mdc-line-ripple"></span>
                        <span class="mdc-text-field__ripple"></span>

                        <asp:TextBox CausesValidation="True" ID="txtFechaFin" ValidationGroup="A" type="date" runat="server" CssClass="mdc-text-field__input" placeholder="Fecha Fin"></asp:TextBox>
                    </label>
                    <div class="mdc-text-field-helper-line">
                        <asp:RequiredFieldValidator CssClass="mdc-text-field-helper-text" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFechaFin" ErrorMessage="Ingrese una fecha válida." Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFechaFin"
                            ErrorMessage="Ingrese una fecha válida" ValidationExpression="^\d{4}-\d{2}-\d{2}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row _p">

            <asp:LinkButton ID="btnCargar" ValidationGroup="A" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="Cargar">
            <span class="mdc-button__label">Cargar</span>
            </asp:LinkButton>
        </div>
    </div>
    <br />
    <div class=" mdc-card mdc-data-table">
        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False">
            <Columns>
                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <span class="mdc-typography--body2"><%# Eval("Cantidad") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Producto">
                    <ItemTemplate>
                        <a class="mdc-typography--body2" href="/Productos/?CODIGO=<%# Eval(Producto.Columns.Codigo_Prod) %>">
                            <%# Eval(Producto.Columns.Nombre) %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proveedor">
                    <ItemTemplate>
                        <a ID="gvDatosItemTemplate__Proveedor" class="mdc-typography--body2" href="/Proveedores/?ID=<%# Eval(Producto.Columns.CUITProv) %>">
                            <%# Eval(Proveedor.Columns.RazonSocial) %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
    </asp:Content>