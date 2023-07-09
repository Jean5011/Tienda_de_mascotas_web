<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Vista.Index" MasterPageFile="/Root.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="card-viewer">
                <div class="big-col">
                    <div class="col-a"><!-- Categorías -->
                        <div class="mdc-card widget">
                            <div class="mdc-card__content">
                                <div class="row _title mdc-typography--headline6">
                                    Categorías
                                </div>
                            </div>
                            <div class="_space"></div>
                            <div class="mdc-card__actions mdc-card__actions--full-bleed">
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Tipos/Agregar.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Añadir</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">add</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Tipos/">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Ver todo</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">arrow_forward</i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="col-a"><!-- Ventas & Proveedores -->
                        <div class="mdc-card widget">
                            <div class="mdc-card__content">
                                <div class="row _title mdc-typography--headline6">
                                    Ventas
                                </div>
                                <br>
                                <div class="row _p">
                                    <div class="_side">
                                        <span runat="server" id="lblTotalVendidoUltimoDia" class="mdc-typography--subtitle1">N/A</span>
                                        <span class="mdc-typography--body2 __secondary-txt">Hoy</span>
                                    </div>
                                    <div class="_side">
                                        <span runat="server" id="lblTotalVendidoUltimaSemana" class="mdc-typography--subtitle1">N/A</span>
                                        <span class="mdc-typography--body2 __secondary-txt">Esta semana</span>
                                    </div>
                                </div>
                            </div>
                            <div class="_space"></div>
                            <div class="mdc-card__actions mdc-card__actions--full-bleed mcard-actions">
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Ventas/Crear.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Registrar nueva venta</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">add</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Reportes/MayoresVentas.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Ventas con mayor recaudación</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">article</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Ventas/">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Ver todas las ventas</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">arrow_forward</i>
                                </a>
                            </div>
                        </div>
                        <div class="mdc-card widget">
                            <div class="mdc-card__content">
                                <div class="row _title mdc-typography--headline6">
                                    Proveedores
                                </div>
                            </div>
                            <div class="_space"></div>
                            <div class="mdc-card__actions mdc-card__actions--full-bleed">
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Proveedores/Agregar.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Añadir</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">add</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Proveedores/">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Ver todo</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">arrow_forward</i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="big-col">
                    <div class="col-a"><!-- Productos & Animales -->
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
                                                <span runat="server" id="lblProductoMasVendidoUltimaSemana" class="mdc-list-item__primary-text">Sin información</span>
                                                <span class="mdc-list-item__secondary-text">Más vendido esta semana</span>
                                            </span>
                                        </li>
                                        <li role="separator" class="mdc-list-divider"></li>
                                    </ul>
                                </div>
                                <div class="row _p">
                                    <div class="_side">
                                        <span runat="server" id="lblProductosPorAgotarse" class="mdc-typography--subtitle1">N/A</span>
                                        <span class="mdc-typography--body2 __secondary-txt">Por agotarse</span>
                                    </div>
                                    <div class="_side">
                                        <span runat="server" id="lblProductosAgotados" class="mdc-typography--subtitle1">N/A</span>
                                        <span class="mdc-typography--body2 __secondary-txt">Sin stock</span>
                                    </div>
                                </div>
                            </div>
                            <div class="_space"></div>
                            <div class="mdc-card__actions mdc-card__actions--full-bleed mcard-actions">
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Productos/Agregar.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Añadir</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">add</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Reportes/ProductoMasVendido.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Productos más vendidos</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">article</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Productos/">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Ver el catálogo</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">arrow_forward</i>
                                </a>
                            </div>
                        </div>
                        <div class="mdc-card widget">
                            <div class="mdc-card__content">
                                <div class="row _title mdc-typography--headline6">
                                    Animales
                                </div>
                            </div>
                            <div class="_space"></div>
                            <div class="mdc-card__actions mdc-card__actions--full-bleed">
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Animales/AgregarAnimal.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Añadir</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">add</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Animales/">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Ver todo</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">arrow_forward</i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="col-a"><!-- Empleados (& ~Sesiones~ ?) -->
                        <div class="mdc-card widget">
                            <div class="mdc-card__content">
                                <div class="row _title mdc-typography--headline6">
                                    Empleados
                                </div>
                            </div>
                            <div class="_space"></div>
                            <div class="mdc-card__actions mdc-card__actions--full-bleed">
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Empleados/CrearCuenta.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Crear cuenta</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">person_add</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/IniciarSesion">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Cambiar de cuenta</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">people</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Empleados/AdministrarAccesos.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Administrar accesos</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">security</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Empleados/CambiarClave.aspx">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Cambiar clave</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">password</i>
                                </a>
                                <a class="mdc-button mdc-card__action mdc-card__action--button" href="/Empleados/">
                                    <div class="mdc-button__ripple"></div>
                                    <span class="mdc-button__label mcardbl-act">Ver todo</span>
                                    <i class="material-icons mdc-button__icon" aria-hidden="true">arrow_forward</i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
</asp:Content>
