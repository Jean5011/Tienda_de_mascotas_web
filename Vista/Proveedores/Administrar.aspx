<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs"
    Inherits="Vista.Proveedores.Administrar" %>

<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Proveedores</h2>

    <div class="searchbox">
        <asp:TextBox ID="txtBuscar" placeholder="Buscá por número de CUIT" CssClass="search" runat="server" ValidationGroup="GrupoBuscar"></asp:TextBox>
        <asp:Button ID="btnBuscar" CssClass="material-icons mdc-icon-button btn-search" runat="server" Text="search"
            OnClick="filtrarProveedor_Click" ValidationGroup="GrupoBuscar" />
    </div>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="no es un cuit"
        ValidationExpression="^[0-9]{10,}$" ValidationGroup="GrupoBuscar" ControlToValidate="txtBuscar">CUIT
                incorrecto</asp:RegularExpressionValidator>
    <div class="mdc-data-table">
        <asp:GridView ID="GvDatos" runat="server" AutoGenerateColumns="False"
            OnPageIndexChanging="GvDatos_PageIndexChanging" AllowPaging="True" OnRowCreated="GvDatos_RowCreated"
            PageSize="5" AutoGenerateSelectButton="False">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="/Proveedores/Editar.aspx?CUIT=<%# Eval(Proveedor.Columns.CUIT) %>"
                            class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Editar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">edit</i>
                        </a>
                        <asp:LinkButton runat="server" Visible="<%# Convert.ToBoolean(Eval(Proveedor.Columns.Estado)) %>" OnClientClick="return confirm('¿Estás seguro de deshabilitar este registro?');" OnCommand="Lb_Command" CommandName="EliminarProveedor" CommandArgument="<%# Eval(Proveedor.Columns.CUIT) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Deshabilitar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">delete</i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" Visible="<%# !Convert.ToBoolean(Eval(Proveedor.Columns.Estado)) %>" OnClientClick="return confirm('¿Estás seguro de habilitar este registro?');" OnCommand="Lb_Command" CommandName="Habilitar" CommandArgument="<%# Eval(Proveedor.Columns.CUIT) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Habilitar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">unarchive</i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CUIT">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="GvDatosItemTemplate__CUIT"
                            runat="server" Text='<%# Eval(Proveedor.Columns.CUIT) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Razón Social">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="GvDatosItemTemplate__RS" runat="server"
                            Text='<%# Eval(Proveedor.Columns.RazonSocial) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Contacto">
                    <ItemTemplate>
                        <span class="mdc-chip-set" role="grid">
                            <span class="mdc-chip-set__chips" role="presentation">

                                <span class="mdc-chip" role="row" id="chip--nc">
                                    <span class="mdc-chip__cell mdc-chip__cell--primary" role="gridcell">
                                        <span
                                            class="mdc-chip__action mdc-chip__action--primary" type="button"
                                            tabindex="-1">
                                            <span class="mdc-chip__ripple mdc-chip__ripple--primary"></span>
                                            <span class="mdc-chip__icon mdc-chip__icon--leading material-icons">person</span>
                                            <span class="mdc-chip__text-label">
                                                <%# Eval(Proveedor.Columns.NombreContacto) %>
                                            </span>
                                        </span>
                                    </span>
                                </span>

                                <a class="mdc-chip" href="mailto:<%# Eval(Proveedor.Columns.CorreoElectronico) %>" role="row" id="chip--ce">
                                    <span class="mdc-chip__cell mdc-chip__cell--primary" role="gridcell">
                                        <span
                                            class="mdc-chip__action mdc-chip__action--primary" type="button"
                                            tabindex="-1">
                                            <span class="mdc-chip__ripple mdc-chip__ripple--primary"></span>
                                            <span class="mdc-chip__icon mdc-chip__icon--leading material-icons">alternate_email</span>
                                            <span class="mdc-chip__text-label">
                                                <%# Eval(Proveedor.Columns.CorreoElectronico) %>
                                            </span>
                                        </span>
                                    </span>
                                </a>

                                <a class="mdc-chip" href="tel://<%# Eval(Proveedor.Columns.Telefono) %>" role="row" id="chip--tf">
                                    <span class="mdc-chip__cell mdc-chip__cell--primary" role="gridcell">
                                        <span
                                            class="mdc-chip__action mdc-chip__action--primary" type="button"
                                            tabindex="-1">
                                            <span class="mdc-chip__ripple mdc-chip__ripple--primary"></span>
                                            <span class="mdc-chip__icon mdc-chip__icon--leading material-icons">call</span>
                                            <span class="mdc-chip__text-label">
                                                <%# Eval(Proveedor.Columns.Telefono) %>
                                            </span>
                                        </span>
                                    </span>
                                </a>
                                <a title="<%# string.Format("{0}, {1}, {2}, {3}", Eval(Proveedor.Columns.Direccion), Eval(Proveedor.Columns.Localidad), Eval(Proveedor.Columns.Provincia), Eval(Proveedor.Columns.Pais)) %>" class="mdc-chip" href="https://www.google.com/maps/search/<%# string.Format("{0}, {1}, {2}, {3}", Eval(Proveedor.Columns.Direccion), Eval(Proveedor.Columns.Localidad), Eval(Proveedor.Columns.Provincia), Eval(Proveedor.Columns.Pais)) %>" role="row" id="chip--direccion">
                                    <span class="mdc-chip__cell mdc-chip__cell--primary" role="gridcell">
                                        <span
                                            class="mdc-chip__action mdc-chip__action--primary" type="button"
                                            tabindex="-1">
                                            <span class="mdc-chip__ripple mdc-chip__ripple--primary"></span>
                                            <span class="mdc-chip__icon mdc-chip__icon--leading material-icons">location_on</span>
                                            <span class="mdc-chip__text-label">
                                                <%# Eval(Proveedor.Columns.Direccion) %>
                                            </span>
                                        </span>
                                    </span>
                                </a>
                            </span>
                        </span>
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

                            <asp:ListItem  Selected="True">5</asp:ListItem>
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
    <a href="/Proveedores/Agregar.aspx" class="mdc-fab" id="fab" aria-label="Agregar">
      <div class="mdc-fab__ripple"></div>
      <span class="mdc-fab__icon material-icons">add_business</span>
    </a>
</asp:Content>
