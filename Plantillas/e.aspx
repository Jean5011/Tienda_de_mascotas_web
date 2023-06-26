<div class="mdc-data-table">
    <asp:GridView ID="gvDetalles" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvDetalles_PageIndexChanging"
        AllowPaging="True" OnRowCreated="gvDetalles_RowCreated" PageSize="10" AutoGenerateSelectButton="False"
        OnSelectedIndexChanging="gvDetalles_SelectedIndexChanging">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton CssClass="mdc-button mdc-card__action mdc-card__action--button" runat="server" ID="GVDETALLESBTNELIMINAR" OnCommand="GVDETALLESBTNELIMINAR_Command" CommandName="ELIMINAR" Text="Eliminar" CommandArgument="<%# Eval(DetalleVenta.Columns.CodProducto_Dv) %>">
                        <div class="mdc-button__ripple"></div>
                        <span class="mdc-button__label mcardbl-act">Eliminar</span>
                        <i class="material-icons mdc-button__icon danger-color" aria-hidden="true">delete</i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Producto">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDetallesItemTemplate__Producto" runat="server"
                        Text='<%# Eval(DetalleVenta.Columns.CodProducto_Dv) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Proveedor">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDetallesItemTemplate__Proveedor" runat="server"
                        Text='<%# Eval(DetalleVenta.Columns.CUITProv) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cantidad">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvDetallesItemTemplate__Cantidad" runat="server"
                        Text='<%# Eval(DetalleVenta.Columns.Cantidad_Dv) %>'>
                    </asp:Label>
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
                        OnSelectedIndexChanged="ddlFilasPorPaginaPagerTemplate_SelectedIndexChanged">

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
                    <asp:TextBox type="number" CssClass="mdc-text-field__input" ID="gvDetallesPagerPageTxtBox" runat="server"
                        OnTextChanged="gvDetallesPagerPageTxtBox_TextChanged" AutoPostBack="true"></asp:TextBox>
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