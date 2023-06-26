
<div class="mdc-data-table">
    <asp:GridView ID="gvEmpleado" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvEmpleado_PageIndexChanging"
        AllowPaging="True" OnRowCreated="gvEmpleado_RowCreated" PageSize="15" AutoGenerateSelectButton="False"
        OnSelectedIndexChanging="gvEmpleado_SelectedIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvEmpleadoItemTemplate__Nombre" runat="server"
                        Text='<%# Eval(Empleado.Columns.Nombre) + " " + Eval(Empleado.Columns.Apellido) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Sexo">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvEmpleadoItemTemplate__Sexo" runat="server"
                        Text='<%# Eval(Empleado.Columns.Sexo) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Región">
                <ItemTemplate>
                    <asp:Label CssClass="mdc-typography--body2" ID="gvEmpleadoItemTemplate__Dirección" runat="server"
                        Text='<%# Eval(Empleado.Columns.Provincia) + ", " + Eval(Empleado.Columns.Nacionalidad) %>'>
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
                <asp:LinkButton ID="gvEmpleadoPagerFirst" runat="server" CommandName="Page" CommandArgument="First"
                    CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">first_page</i>
            <!--span class="mdc-button__label"></span-->
                </asp:LinkButton>
                <asp:LinkButton ID="gvEmpleadoPagerPrev" runat="server" CommandName="Page" CommandArgument="Prev"
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
                    <asp:TextBox type="number" CssClass="mdc-text-field__input" ID="gvEmpleadoPagerPageTxtBox" runat="server"
                        OnTextChanged="gvEmpleadoPagerPageTxtBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                </label>
                <asp:LinkButton ID="gvEmpleadoPagerNext" runat="server" CommandName="Page" CommandArgument="Next"
                    CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">chevron_right</i>
            <!--span class="mdc-button__label"></span-->
                </asp:LinkButton>
                <asp:LinkButton ID="gvEmpleadoPagerLast" runat="server" CommandName="Page" CommandArgument="Last"
                    CssClass="mdc-icon-button mdc-button--primary">
            <span class="mdc-icon-button__ripple"></span>
            <i class="material-icons mdc-button__icon" aria-hidden="true">last_page</i>
            <!--span class="mdc-button__label"></span-->
                </asp:LinkButton>
            </div>
        </PagerTemplate>
    </asp:GridView>

</div>
