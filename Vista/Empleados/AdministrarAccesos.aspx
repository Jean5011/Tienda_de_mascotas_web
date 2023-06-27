<%@ Page Language="C#" MasterPageFile="/Root.Master" AutoEventWireup="true" CodeBehind="AdministrarAccesos.aspx.cs" Inherits="Vista.Empleados.AdministrarAccesos" %>

<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Sesiones abiertas</h2>
    <br />
    <br />
    <div class="mdc-data-table">
        <asp:GridView ID="gvAdmin" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GvAdmin_PageIndexChanging"
            AllowPaging="True" OnRowCreated="GvAdmin_RowCreated" PageSize="10" AutoGenerateSelectButton="False"
            OnSelectedIndexChanging="GvAdmin_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Panel runat="server" Visible='<%# Convert.ToBoolean(Eval(Sesion.Columns.Estado)) %>'>
                            <!--Estado = true -->
                            <asp:LinkButton ID="lnkDisable" runat="server" CssClass="mdc-button mdc-card__action mdc-card__action--button"
                                CommandName="DISABLE" OnCommand="SwitchStatusCommand" CommandArgument='<%# Eval(Sesion.Columns.Codigo) %>'>
                                <div class="mdc-button__ripple"></div>
                                <span class="mdc-button__label mcardbl-act">Revocar</span>
                                <i class="material-icons mdc-button__icon danger-color" aria-hidden="true">close</i>
                            </asp:LinkButton>
                        </asp:Panel>
                        <asp:Panel runat="server" Visible='<%# !Convert.ToBoolean(Eval(Sesion.Columns.Estado)) %>'>
                            <!--Estado = false -->
                            <asp:LinkButton ID="lnkEnable" runat="server" CssClass="mdc-button mdc-card__action mdc-card__action--button"
                                CommandName="ENABLE" OnCommand="SwitchStatusCommand" CommandArgument='<%# Eval(Sesion.Columns.Codigo) %>'>
                                <div class="mdc-button__ripple"></div>
                                <span class="mdc-button__label mcardbl-act">Autorizar</span>
                                <i class="material-icons mdc-button__icon" aria-hidden="true">lock_open</i>
                            </asp:LinkButton>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvAdminItemTemplate__COD" runat="server"
                            Text='<%# Eval(Sesion.Columns.Codigo) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cuenta">
                    <ItemTemplate>
                        <a href="/Empleados/Perfil.aspx?DNI=<%# Eval(Sesion.Columns.DNI) %>" class="mdc-button mdc-card__action mdc-card__action--button">
                            <div class="mdc-button__ripple"></div>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">person</i>
                            <span class="mdc-button__label mcardbl-act"><%# Eval(Empleado.Columns.Nombre) %> <%# Eval(Empleado.Columns.Apellido) %></span>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Últimos dígitos del token">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2 __token_lb" ID="gvAdminItemTemplate__TK" runat="server"
                            Text='<%# Eval(Sesion.Columns.Token) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Fecha de alta">
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvAdminItemTemplate__Sexo" runat="server"
                            Text='<%# Eval(Sesion.Columns.FechaAlta) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <asp:Panel runat="server" Visible='<%# Convert.ToBoolean(Eval(Sesion.Columns.Estado)) %>'>
                            <!-- Control para cuando el estado es true -->
                            <div class="_token_status">
                                <span class="material-icons _token_ok">check_circle</span>
                                <span class="text">Autorizado</span>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" Visible='<%# !Convert.ToBoolean(Eval(Sesion.Columns.Estado)) %>'>
                            <!-- Control para cuando el estado es false -->
                            <div class="_token_status">
                                <span class="material-icons _token_bad">cancel</span>
                                <span class="text">Revocado</span>
                            </div>
                        </asp:Panel>
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
    <br />
    <br />
    <div class="mdc-card">
        <div class="mdc-card__content">
            <div class="row _title mdc-typography--headline6">
                Más opciones
            </div>

        </div>
        <div class="mdc-card__actions mdc-card__actions--full-bleed">
            <asp:LinkButton ID="lnkCambiarClave" OnClick="CambiarClaveClick" runat="server" CssClass="mdc-button mdc-card__action mdc-card__action--button">
                <div class="mdc-button__ripple"></div>
                <span class="mdc-button__label mcardbl-act">Cambiar clave</span>
                <i class="material-icons mdc-button__icon" aria-hidden="true">password</i>
            </asp:LinkButton>
            <asp:LinkButton ID="lnkDisableAll" OnClick="RevocarTodo" runat="server" CssClass="mdc-button mdc-card__action mdc-card__action--button">
                <div class="mdc-button__ripple"></div>
                <span class="mdc-button__label mcardbl-act">Revocar todas las sesiones</span>
                <i class="material-icons mdc-button__icon danger-color" aria-hidden="true">close</i>
            </asp:LinkButton>
        </div>
    </div>
    <br />
    <br />
    <br />

</asp:Content>
