<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Vista.Index" MasterPageFile="/Root.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-viewer">
                <div class="mdc-card widget">
                    <div class="mdc-card__content">
                        <div class="row _title mdc-typography--headline6">
                            Productos
                        </div>
                        <div class="row">
                            <ul class="mdc-list mdc-list--two-line">
                                <li class="mdc-list-item" tabindex="0">
                                    <span class="mdc-list-item__ripple"></span>
                                    <span class="mdc-list-item__text">
                                    <asp:Label CssClass="mdc-list-item__primary-text" id="lblProductoMasVendidoUltimaSemana" runat="server" Text="Label"></asp:Label>
                                      <span class="mdc-list-item__secondary-text">Más vendido esta semana</span>
                                    </span>
                                  </li>
                                <li role="separator" class="mdc-list-divider"></li>
                            </ul>
                        </div>
                        <div class="row _p">
                            <div class="_side">
                                <asp:Label CssClass="mdc-typography--subtitle1" id="lblProductosPorAgotarse" runat="server" Text=""></asp:Label>
                                <span class="mdc-typography--body2 __secondary-txt">Por agotarse</span>
                            </div>
                            <div class="_side">
                                <asp:Label CssClass="mdc-typography--subtitle1" id="lblProductosAgotados" runat="server" Text=""></asp:Label>
                                <span class="mdc-typography--body2 __secondary-txt">Sin stock</span>
                            </div>
                        </div>
                    </div>
                    <div class="mdc-card__actions mdc-card__actions">
                        <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Productos/Administrar.aspx">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label">Ver el catálogo</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">arrow_forward</i>
                        </a>
                    </div>
                </div>
                <div class="mdc-card widget">
                    <div class="mdc-card__content">
                        <div class="row _title mdc-typography--headline6">
                            Inicio
                        </div>
                        <div class="row">
                            <ul class="mdc-list mdc-list--two-line">
                                <li class="mdc-list-item" tabindex="0">
                                  <span class="mdc-list-item__ripple"></span>
                                  <span class="mdc-list-item__text">
                                      <span class="mdc-list-item__primary-text">(No hay)</span>
                                    <span class="mdc-list-item__secondary-text">Más vendido esta semana</span>
                                  </span>
                                </li>
                                <li role="separator" class="mdc-list-divider"></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="mdc-card widget">
                    <div class="mdc-card__content">
                        <div class="row _title mdc-typography--headline6">
                            Ventas
                        </div>
                        <asp:Label CssClass="row _ltr mdc-typography--headline5" id="lblTotalVendidoUltimoDia" runat="server" Text=""></asp:Label>
                        <div class="row _ltr mdc-typography--body2 __secondary-txt">Hoy</div>
                        <br>
                        <asp:Label CssClass="row _ltr mdc-typography--headline5" id="lblTotalVendidoUltimaSemana" runat="server" Text=""></asp:Label>
                        <div class="row _ltr mdc-typography--body2 __secondary-txt">Esta semana</div>
                    </div>
                    <div class="mdc-card__actions mdc-card__actions">
                        <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Ventas/Administrar.aspx">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label">Ver todo</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">arrow_forward</i>
                        </a>
                    </div>
                </div>
            </div>

       
</asp:Content>
