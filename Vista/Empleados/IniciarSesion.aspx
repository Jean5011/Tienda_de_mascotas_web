<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="Vista.Empleados.IniciarSesion" %>

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
                    <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button" aria-label="Open navigation menu">menu</button>
                    <span class="mdc-top-app-bar__title">Iniciar sesión</span>
                </section>
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">
                    <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Search">search</button>
                    <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Options">more_vert</button>
                </section>
            </div>
        </header>
        <main class="mdc-top-app-bar--fixed-adjust obj--main">
            <div class="page">
                <h2>Iniciá sesión para continuar</h2>
                <br>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="dni-label">DNI</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="mdc-text-field__input" aria-labelledby="dni-label"></asp:TextBox>
                </label>
                <br>
                <br />
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="clave-label">Clave</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtClave" runat="server" CssClass="mdc-text-field__input" aria-labelledby="clave-label" TextMode="Password"></asp:TextBox>
                </label>
                <br />
                <br>
                <asp:LinkButton ID="btnIniciarSesion" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="btnIniciarSesion_Click">
        <span class="mdc-button__label">Iniciar sesión</span>
                </asp:LinkButton>
                <br>
                <br />
                <asp:LinkButton ID="btnOlvidasteClave" runat="server" CssClass="mdc-button">
        <span class="mdc-button__ripple"></span>
        <span class="mdc-button__label">¿Olvidaste la clave?</span>
                </asp:LinkButton>
                <br />
                <asp:Label ID="Label1" runat="server">Resultado aparecer aquí</asp:Label>
                <br />
                <asp:LinkButton ID="btnCrearCuenta" runat="server" CssClass="mdc-button">
        <span class="mdc-button__ripple"></span>
        <span class="mdc-button__label">Crear cuenta</span>
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
