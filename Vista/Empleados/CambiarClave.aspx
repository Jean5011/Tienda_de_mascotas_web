<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarClave.aspx.cs" Inherits="Vista.Empleados.CambiarClave" %>

<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Plantilla</title>
    <link href="https://unpkg.com/material-components-web@latest/dist/material-components-web.min.css" rel="stylesheet">
    <script src="https://unpkg.com/material-components-web@latest/dist/material-components-web.min.js"></script>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="/index.css" />
    <script src="/index.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="contents">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <header class="mdc-top-app-bar mdc-top-app-bar--fixed">
            <div class="mdc-top-app-bar__row">
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-start">
                    <span class="mdc-top-app-bar__title" runat="server" id="spanPageTitle">PetShop</span>
                </section>
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">
                    <ASP:LinkButton ID="lbIniciarSesion" runat="server" OnClick="IniciarSesion" CssClass="mdc-button mdc-button--raised _header-important-btn mdc-top-app-bar__action-item">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label">Iniciar sesión</span>
                    </ASP:LinkButton>
                    <ASP:LinkButton ID="lbActualUser" OnClick="VerPerfilActual" runat="server" CssClass="mdc-button mdc-top-app-bar__action-item _header-profile-btn">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label"><b runat="server" id="lbAUNombre"></b><br>
                            <span runat="server" id="lbAURol"></span></span>
                    </ASP:LinkButton>
                </section>
            </div>
        </header>
        <main class="mdc-top-app-bar--fixed-adjust obj--main">
            <div class="page">
                <h2>Cambiar clave</h2>
                <br>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="clave-label">Clave</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtClave" runat="server" ValidationGroup="A"  CssClass="mdc-text-field__input" aria-labelledby="clave-label" TextMode="Password"></asp:TextBox>
                </label>
                
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="A"  CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtClave" runat="server" ErrorMessage="" SetFocusOnError="True" Text="Ingresá una clave."></asp:RequiredFieldValidator>
                </div>
                <br />
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="clave-label">Volvé a escribir tu clave</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtConfirmar" ValidationGroup="A"  runat="server" CssClass="mdc-text-field__input" aria-labelledby="clave-label" TextMode="Password"></asp:TextBox>
                </label>
                
                 <div class="mdc-text-field-helper-line">
                     <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator1" ValidationGroup="A" CssClass="mdc-text-field-helper-text"  runat="server" ErrorMessage="Las claves no coinciden" ControlToCompare="txtClave" ControlToValidate="txtConfirmar"></asp:CompareValidator>
                </div>
                <br />
                <br>
                <asp:LinkButton ID="btnGuardarCambios" ValidationGroup="A"  runat="server" CssClass="mdc-button mdc-button--raised" OnClick="btnGuardarCambios_Click">
        <span class="mdc-button__label">Guardar cambios</span>
                </asp:LinkButton>
            </div>

        </main>
        <aside class="mdc-snackbar">
            <div class="mdc-snackbar__surface" role="status" aria-relevant="additions">
                <div class="mdc-snackbar__label" aria-atomic="false"></div>
            </div>
        </aside>

    </form>
</body>
</html>