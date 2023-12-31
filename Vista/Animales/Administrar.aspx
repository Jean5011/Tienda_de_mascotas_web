﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="/Root.Master" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Animales.Administrar" %>

<%@ Import Namespace="Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Animales</h2>
    <div class="searchbox">
        <asp:TextBox ID="txtBuscar" CssClass="search" placeholder="Buscá por código, nombre o raza." runat="server"></asp:TextBox>
        <asp:Button ID="BtnBuscar" CssClass="material-icons mdc-icon-button btn-search" OnClick="BtnBuscar_Click" runat="server" Text="search" />
    </div>
    <div>
        Mostrar animales inactivos
        <asp:CheckBox ID="CheckBox1" runat="server" />
    </div>

    <div class="mdc-data-table">
        <asp:GridView ID="GvDatos"
            runat="server"
            AutoGenerateColumns="False"
            AllowPaging="True"
            PageSize="15"
            AutoGenerateSelectButton="False"
            AutoGenerateEditButton="False"
            AutoGenerateDeleteButton="False"

            OnRowCreated="GvDatos_RowCreated"
            OnPageIndexChanging="GvDatos_PageIndexChanging"
            OnSelectedIndexChanging="GvDatos_SelectedIndexChanging"

            OnSelectedIndexChanged="GvDatos_SelectedIndexChanged"
            OnRowDeleting="GvDatos_RowDeleting"
            OnRowCancelingEdit="GvDatos_RowCancelingEdit"
            OnRowEditing="GvDatos_RowEditing"
            OnRowUpdating="GvDatos_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="mdc-button mdc-card__action mdc-card__action--button"
                            CommandName="Update">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Guardar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">save</i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CssClass="mdc-button mdc-card__action mdc-card__action--button"
                            CommandName="Cancel">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Cancelar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">cancel</i>
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="mdc-button mdc-card__action mdc-card__action--button"
                            CommandName="Edit">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Editar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">edit</i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" Visible="<%# Convert.ToBoolean(Eval(Animal.Columns.Estado)) %>" OnClientClick="return confirm('¿Seguro de eliminar este registro?');" CssClass="mdc-button mdc-card__action mdc-card__action--button"
                            CommandName="Delete">
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Deshabilitar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">highlight_off</i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1" runat="server" Visible="<%# !Convert.ToBoolean(Eval(Animal.Columns.Estado)) %>" OnClientClick="return confirm('¿Seguro de habilitar este registro?');" CssClass="mdc-button mdc-card__action mdc-card__action--button"
                            CommandName="Habilitar" CommandArgument="<%# Eval(Animal.Columns.Codigo) %>" OnCommand="SwitchStatus_Command" >
                            <div class="mdc-button__ripple"></div>
                            <span class="mdc-button__label mcardbl-act">Habilitar</span>
                            <i class="material-icons mdc-button__icon" aria-hidden="true">check_circle_outline</i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <EditItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="LV_EditCod" runat="server"
                            Text='<%# Eval(Animal.Columns.Codigo) %>'>
                        </asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="LV_Cod_Animal" runat="server"
                            Text='<%# Eval(Animal.Columns.Codigo) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre">
                    <EditItemTemplate>
                        <asp:TextBox ID="TB_EditNombre" runat="server" Text='<%# Eval(Animal.Columns.Nombre) %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Nombre" runat="server"
                            Text='<%# Eval(Animal.Columns.Nombre) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Raza">
                    <EditItemTemplate>
                        <asp:TextBox ID="TB_EditRaza" runat="server" Text='<%# Eval(Animal.Columns.Raza) %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Raza" runat="server"
                            Text='<%# Eval(Animal.Columns.Raza) %>'>
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
                    <asp:LinkButton ID="gvDatosPagerFirst" runat="server" CommandName="Page" CommandArgument="First"
                        CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">first_page</i>
                    <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="gvDatosPagerPrev" runat="server" CommandName="Page" CommandArgument="Prev"
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
                        <asp:TextBox type="number" CssClass="mdc-text-field__input" ID="gvDatosPagerPageTxtBox"
                            runat="server" OnTextChanged="GvDatosPagerPageTxtBox_TextChanged" AutoPostBack="true">
                        </asp:TextBox>
                    </label>
                    <asp:LinkButton ID="gvDatosPagerNext" runat="server" CommandName="Page" CommandArgument="Next"
                        CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_right</i>
                    <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                    <asp:LinkButton ID="gvDatosPagerLast" runat="server" CommandName="Page" CommandArgument="Last"
                        CssClass="mdc-icon-button mdc-button--primary">
                    <span class="mdc-icon-button__ripple"></span>
                    <i class="material-icons mdc-button__icon" aria-hidden="true">last_page</i>
                    <!--span class="mdc-button__label"></span-->
                    </asp:LinkButton>
                </div>
            </PagerTemplate>
        </asp:GridView>

    </div>
    <a href="/Animales/Agregar.aspx" class="mdc-fab" id="fab" aria-label="Agregar">
      <div class="mdc-fab__ripple"></div>
      <span class="mdc-fab__icon material-icons">add</span>
    </a>
</asp:Content>
