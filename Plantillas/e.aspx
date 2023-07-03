<div class="mdc-data-table">
    <asp:GridView ID="GvDatos" runat="server" AutoGenerateColumns="False"
        OnPageIndexChanging="GvDatos_PageIndexChanging" AllowPaging="True" OnRowCreated="GvDatos_RowCreated"
        PageSize="5" AutoGenerateSelectButton="False"
        OnSelectedIndexChanging="GvDatos_SelectedIndexChanging">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton CommandName="edit"
                        class="mdc-button mdc-card__action mdc-card__action--button">
                        <div class="mdc-button__ripple"></div>
                        <span class="mdc-button__label mcardbl-act">Editar</span>
                        <i class="material-icons mdc-button__icon" aria-hidden="true">edit</i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="delete" class="mdc-button mdc-card__action mdc-card__action--button">
                        <div class="mdc-button__ripple"></div>
                        <span class="mdc-button__label mcardbl-act">Eliminar</span>
                        <i class="material-icons mdc-button__icon" aria-hidden="true">delete</i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Código">
                <EditItemTemplate>
                    <asp:Label ID="LV_EditCod" runat="server" Text='<%# Eval(TipoProducto.Columns.Codigo) %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="LV_CodTipoDeProducto"
                        runat="server" Text='<%# Eval(TipoProducto.Columns.Codigo) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Animal">
                <EditItemTemplate>
                    <asp:DropDownList ID="DD_EditAnimal" runat="server">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="LV_CodAnimales"
                        runat="server" Text='<%# Eval(TipoProducto.Columns.CodAnimal) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Categoría">
                <EditItemTemplate>
                    <asp:DropDownList ID="DD_EditTdp" runat="server">
                        <asp:ListItem>Comida</asp:ListItem>
                        <asp:ListItem>Accesorios</asp:ListItem>
                        <asp:ListItem>Ropa</asp:ListItem>
                        <asp:ListItem>Higiene</asp:ListItem>
                        <asp:ListItem>Salud</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="LV_TipoDeProducto"
                        runat="server" Text='<%# Eval(TipoProducto.Columns.TipoDeProducto) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Descripción">
                <EditItemTemplate>
                    <asp:TextBox ID="TB_EditDesc" runat="server" Text='<%# Eval(TipoProducto.Columns.Descripcion) %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="LV_Descripcion"
                        runat="server" Text='<%# Eval(TipoProducto.Columns.Descripcion) %>'>
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