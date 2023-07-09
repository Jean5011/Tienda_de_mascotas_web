<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="MayoresVentas.aspx.cs" Inherits="Vista.Reportes.MayoresVentas" %>

<%@ Import Namespace="Entidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Cinco mayores ventas</h2>
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
                        <asp:RequiredFieldValidator CssClass="mdc-text-field-helper-text mdc-text-field-helper-text--persistent" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFechaFin" ErrorMessage="Ingrese una fecha válida." Display="Dynamic" />
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
    <div class="mdc-data-table">
        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="/Ventas/VerFactura.aspx?ID=<%# Eval(Venta.Columns.Id) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Abrir</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">open_in_new</i>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__ID" runat="server"
                            Text='<%# Eval(Venta.Columns.Id) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha y hora">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Fecha" runat="server"
                            Text='<%# Eval(Venta.Columns.Fecha) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Empleado gestor">
                    <ItemTemplate>
                        <a href="/Empleados/Perfil.aspx?DNI=<%# Eval(Venta.Columns.DNI) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">person</i>
                            <span class="mdc-button__label mcardbl-act"><%# Eval(Empleado.Columns.Nombre) %> <%# Eval(Empleado.Columns.Apellido) %></span>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Medio de pago">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__TipoPago" runat="server"
                            Text='<%# Eval(Venta.Columns.TipoPago) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Total" runat="server"
                            Text='<%# Eval(Venta.Columns.Total) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
