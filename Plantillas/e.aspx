<span class="mdc-chip-set" role="grid">
    <span class="mdc-chip-set__chips" role="presentation">

        <span class="mdc-chip"  role="row" id="chip--nc">
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
                    <span class="mdc-chip__text-label">
                        <%# Eval(Proveedor.Columns.Telefono) %>
                    </span>
                </span>
            </span>
        </a>


    </span>
</span>