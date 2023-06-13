<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Empleados.Administrar" %>


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
</head>

<body>
    <form  id="form1" runat="server" class="contents">
        <header class="mdc-top-app-bar mdc-top-app-bar--fixed">
    <div class="mdc-top-app-bar__row">
      <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-start">
        <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
          aria-label="Open navigation menu">menu</button>
        <span class="mdc-top-app-bar__title">Pet Shop</span>
      </section>
      <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">
        <button class="mdc-button mdc-top-app-bar__action-item">
          <span class="mdc-button__ripple"></span>
          <span class="mdc-button__label">Proveedores</span>
        </button>
        <button class="mdc-button mdc-top-app-bar__action-item">
          <span class="mdc-button__ripple"></span>
          <span class="mdc-button__label">Clientes</span>
        </button>
        <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Search">search</button>
        <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button"
          aria-label="Options">more_vert</button>
      </section>
    </div>
  </header>
  <main class="mdc-top-app-bar--fixed-adjust obj--main">
    <h2>Empleados</h2>
    <div class="searchbox">
      <span class="text">Buscar</span>
      <span class="material-icons">search</span>
    </div>
   <asp:GridView ID="GridView2" runat="server" CssClass="mdc-data-table" AutoGenerateColumns="False" aria-label="Dessert calories">
    <Columns>
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <button class="material-icons mdc-icon-button btn-edit">edit</button>
                <button class="material-icons mdc-icon-button btn-delete">delete</button>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DNI" HeaderText="DNI" ItemStyle-CssClass="mdc-data-table__cell" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-CssClass="mdc-data-table__cell" />
        <asp:BoundField DataField="Apellido" HeaderText="Apellido" ItemStyle-CssClass="mdc-data-table__cell" />
        <asp:BoundField DataField="Sexo" HeaderText="Sexo" ItemStyle-CssClass="mdc-data-table__cell" />
        <asp:BoundField DataField="Sueldo" HeaderText="Sueldo" ItemStyle-CssClass="mdc-data-table__cell" />
        <asp:BoundField DataField="Dirección" HeaderText="Dirección" ItemStyle-CssClass="mdc-data-table__cell" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-CssClass="mdc-data-table__cell" />
    </Columns>
</asp:GridView>

  </main>
    </form>
  
</body>

</html>