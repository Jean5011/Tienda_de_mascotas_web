<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Vista.Empleados.Perfil" %>

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
    <form  id="form1" runat="server" class="contents">
    <header class="mdc-top-app-bar mdc-top-app-bar--fixed">
        <div class="mdc-top-app-bar__row">
          <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-start">
            <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button" aria-label="Open navigation menu">menu</button>
            <span class="mdc-top-app-bar__title">Pet Shop</span>
          </section>
          <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar"> 
            <button class="mdc-button mdc-top-app-bar__action-item">
                <span class="mdc-button__ripple"></span>
                <span class="mdc-button__label">Proveedores</span>
            </button>
            <button class="mdc-button mdc-top-app-bar__action-item">
                <span class="mdc-button__ripple"></span>
                <span class="mdc-button__label">Productos</span>
            </button>
            <button class="mdc-button mdc-top-app-bar__action-item">
                <span class="mdc-button__ripple"></span>
                <span class="mdc-button__label">Clientes</span>
            </button>
            <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Search">search</button>
            <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Options">more_vert</button>
          </section>
        </div>
      </header>
      <main class="mdc-top-app-bar--fixed-adjust obj--main main-grid">
    <div class="col perfil">
        <h2>Alejandro Gutiérrez</h2>
        <ul class="mdc-card mdc-card--outlined mdc-list mdc-list--two-line">
            <li class="mdc-list-item">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">45 116 118</span>
                    <span class="mdc-list-item__secondary-text">D.N.I.</span>
                </span>
            </li>
            <li class="mdc-list-item" tabindex="0">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">27 de Febrero de 1989</span>
                    <span class="mdc-list-item__secondary-text">Fecha de nacimiento</span>
                </span>
            </li>
            <li class="mdc-list-item" tabindex="0">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">14 de Mayo de 2015</span>
                    <span class="mdc-list-item__secondary-text">Fecha de contratación</span>
                </span>
            </li>
            <li class="mdc-list-item">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">$ 450,000</span>
                    <span class="mdc-list-item__secondary-text">Salario bruto mensual</span>
                </span>
            </li>
            <li class="mdc-list-item">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">Albarellos 1560, Tigre, Bs. As.</span>
                    <span class="mdc-list-item__secondary-text">Dirección</span>
                </span>
            </li>
            <li role="separator" class="mdc-list-divider"></li>
            <li class="mdc-list-item">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-deprecated-list-item__graphic material-icons" aria-hidden="true">
                    edit
                </span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">Editar información</span>
                    <span class="mdc-list-item__secondary-text">Cambiar sueldo, dirección.</span>
                </span>
            </li>
            <li class="mdc-list-item">
                <span class="mdc-list-item__ripple"></span>
                <span class="mdc-deprecated-list-item__graphic material-icons" aria-hidden="true">
                    delete
                </span>
                <span class="mdc-list-item__text">
                    <span class="mdc-list-item__primary-text">Deshabilitar empleado</span>
                    <span class="mdc-list-item__secondary-text">En caso de despido, renuncia.</span>
                </span>
            </li>
        </ul>
        <br>
    </div>
    <div class="col principal">
        <h1>Ventas que realizó</h1>
        <br>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnOpen" runat="server" CssClass="material-icons mdc-icon-button" Text="open_in_new" CommandName="Open" />
                <asp:Button ID="btnDelete" runat="server" CssClass="material-icons mdc-icon-button" Text="delete" CommandName="Delete" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Codigo" HeaderText="Código" />
        <asp:TemplateField HeaderText="Empleado">
            <ItemTemplate>
                <a href="#"></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="MedioPago" HeaderText="Medio de pago" />
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
        <asp:BoundField DataField="Total" HeaderText="Total" />
    </Columns>
</asp:GridView>

    </div>
</main>
</form>
</body>
</html>
