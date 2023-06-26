<div class="mdc-data-table">
    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False"
        OnPageIndexChanging="GvDatos_PageIndexChanging" AllowPaging="True" OnRowCreated="GvDatos_RowCreated"
        PageSize="15" AutoGenerateSelectButton="False" OnSelectedIndexChanging="GvDatos_SelectedIndexChanging">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="/Producto/Ver?ID=<%# Eval(Venta.Columns.Id) %>"
                        class="mdc-button mdc-card__action mdc-card__action--button">
                        <div class="mdc-button__ripple"></div>
                        <span class="mdc-button__label mcardbl-act">Abrir</span>
                        <i class="material-icons mdc-button__icon" aria-hidden="true">open_in_new</i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Codigo" runat="server"
                        Text='<%# Eval(Producto.Columns.Codigo_Prod) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Proveedor">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Proveedor" runat="server"
                        Text='<%# Eval(Producto.Columns.CUITProv) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Categoría">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__TP" runat="server"
                        Text='<%# Eval(Producto.Columns.CodTipoProducto) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Nombre" runat="server"
                        Text='<%# Eval(Producto.Columns.Nombre) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Marca">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Marca" runat="server"
                        Text='<%# Eval(Producto.Columns.Marca) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Descripcion">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Descripcion" runat="server"
                        Text='<%# Eval(Producto.Columns.Descripcion) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Stock">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Stock" runat="server"
                        Text='<%# Eval(Producto.Columns.Stock) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Precio">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDatosItemTemplate__Precio" runat="server"
                        Text='<%# Eval(Producto.Columns.Precio) %>'>
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
                        OnSelectedIndexChanged="ddlFilasPorPaginaPagerTemplate_SelectedIndexChanged">

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