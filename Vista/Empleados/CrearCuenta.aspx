<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearCuenta.aspx.cs" Inherits="Vista.Empleados.CrearCuenta" %>

<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Plantilla</title>
  <link href="https://unpkg.com/material-components-web@latest/dist/material-components-web.min.css" rel="stylesheet">
  <script src="https://unpkg.com/material-components-web@latest/dist/material-components-web.min.js"></script>
  <link rel="preconnect" href="https://fonts.googleapis.com">
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
  <link
    href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap"
    rel="stylesheet">
  <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
  <link rel="stylesheet" href="/index.css" />
  <script src="/index.js"></script>
</head>

<body>
  <form  id="form1" runat="server" class="contents">
  <header class="mdc-top-app-bar mdc-top-app-bar--fixed">
    <div class="mdc-top-app-bar__row">
      <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-start">
        <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
          aria-label="Open navigation menu">menu</button>
        <span class="mdc-top-app-bar__title">Pets Shop</span>
      </section>
      <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">
       
        <button class="mdc-button mdc-top-app-bar__action-item">
          <span class="mdc-button__ripple"></span>
          <span class="mdc-button__label">ADMIN</span>
        </button>
        <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Search">search</button>
        <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button"
          aria-label="Options">more_vert</button>
      </section>
    </div>
  </header>
<main class="mdc-top-app-bar--fixed-adjust obj--main">
    <div class="page">
        <h2>Crear cuenta de empleado</h2>
        <br>
        <br>

        <div class="group">
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="nombre-label">Nombre</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nombre-label"></asp:TextBox>
            </label>
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="apellido-label">Apellido</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="mdc-text-field__input" aria-labelledby="apellido-label"></asp:TextBox>
            </label>
        </div>

        <div class="group">
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
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="sexo-label">Sexo</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="mdc-text-field__input" aria-labelledby="sexo-label">
                    <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                </asp:DropDownList>
            </label>
        </div>

        <div class="group">
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="fecha-nacimiento-label">Fecha de nacimiento</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="mdc-text-field__input" aria-labelledby="fecha-nacimiento-label" type="date"></asp:TextBox>
            </label>
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="fecha-inicio-label">Fecha de inicio de actividades</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="mdc-text-field__input" aria-labelledby="fecha-inicio-label" type="date"></asp:TextBox>
            </label>
        </div>

        <div class="group">
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
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="repetir-clave-label">Repetí la clave</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox ID="txtRepetirClave" runat="server" CssClass="mdc-text-field__input" aria-labelledby="repetir-clave-label" TextMode="Password"></asp:TextBox>
            </label>
        </div>

        <br>
        <asp:LinkButton ID="btnCrearCuenta" runat="server" CssClass="mdc-button mdc-button--raised">
            <span class="mdc-button__label">Crear cuenta</span>
        </asp:LinkButton>
    </div>
</main>

  </form>
</body>

</html>
