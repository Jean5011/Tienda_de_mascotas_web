﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crear.aspx.cs" Inherits="Vista.Ventas.Crear" %>

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
  <script src="./index.js"></script>
</head>

<body>
    <form id="form1" runat="server" class="contents">

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
          <span class="mdc-button__label">Productos</span>
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
  <main class="mdc-top-app-bar--fixed-adjust obj--main main-grid">
    <div class="col perfil">
      <ul class="mdc-card mdc-card--outlined mdc-list mdc-list--two-line">
        <li class="mdc-list-item" tabindex="0">
          <span class="mdc-list-item__ripple"></span>
          <span class="mdc-list-item__text">
            <span class="mdc-list-item__primary-text"><b>$ 1,861.58</b></span>
            <span class="mdc-list-item__secondary-text">Total de la compra</span>
          </span>
        </li>
      </ul>
      <br>
      <label class="mdc-text-field mdc-text-field--outlined">
        <span class="mdc-notched-outline">
          <span class="mdc-notched-outline__leading"></span>
          <span class="mdc-notched-outline__notch">
            <span class="mdc-floating-label" id="my-label-id">Empleado Gestor</span>
          </span>
          <span class="mdc-notched-outline__trailing"></span>
        </span>
        <input type="text" value="Héctor Da Silva" class="mdc-text-field__input" aria-labelledby="my-label-id">
      </label>
      <br>
      <label class="mdc-text-field mdc-text-field--outlined">
        <span class="mdc-notched-outline">
          <span class="mdc-notched-outline__leading"></span>
          <span class="mdc-notched-outline__notch">
            <span class="mdc-floating-label" id="my-label-id">Método de Pago</span>
          </span>
          <span class="mdc-notched-outline__trailing"></span>
        </span>
        <input type="text" value="Efectivo" class="mdc-text-field__input" aria-labelledby="my-label-id">
      </label>
      <br>
      <label class="mdc-text-field mdc-text-field--outlined">
        <span class="mdc-notched-outline">
          <span class="mdc-notched-outline__leading"></span>
          <span class="mdc-notched-outline__notch">
            <span class="mdc-floating-label" id="my-label-id">Fecha</span>
          </span>
          <span class="mdc-notched-outline__trailing"></span>
        </span>
        <input type="date" value="12/11/2021" class="mdc-text-field__input" aria-labelledby="my-label-id">
      </label>
      <br>
      <label class="mdc-text-field mdc-text-field--outlined">
        <span class="mdc-notched-outline">
          <span class="mdc-notched-outline__leading"></span>
          <span class="mdc-notched-outline__notch">
            <span class="mdc-floating-label" id="my-label-id">Hora</span>
          </span>
          <span class="mdc-notched-outline__trailing"></span>
        </span>
        <input type="time" value="13:54" class="mdc-text-field__input" aria-labelledby="my-label-id">
      </label>
      <br><br>
      <button class="mdc-button mdc-button--raised">
        <span class="mdc-button__label">Guardar Factura</span>
      </button>
    </div>
    <div class="col">
      <h2>Registrar venta</h2>
      <div class="mdc-data-table">
        <div class="mdc-data-table__table-container">
          <table class="mdc-data-table__table" aria-label="Dessert calories">
            <thead>
              <tr class="mdc-data-table__header-row">
                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Acciones</th>
                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Producto</th>
                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Proveedor</th>
                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Cantidad</th>
                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Precio Unitario</th>
                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Subtotal</th>
              </tr>
            </thead>
            <tbody class="mdc-data-table__content">
              <tr class="mdc-data-table__row">
                <td class="mdc-data-table__cell"><button
                    class="material-icons mdc-icon-button btn-edit">edit</button><button
                    class="material-icons mdc-icon-button btn-erase">delete</button></td>
                <td class="mdc-data-table__cell">Whiskas Sabor Pollo 530gr</td>
                <td class="mdc-data-table__cell">WHISKAS ARG.</td>
                <td class="mdc-data-table__cell">
                  <div class="group table-group"><button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
                    aria-label="Open navigation menu">remove</button>
                            3
                            <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
                    aria-label="Open navigation menu">add</button></div>
                </td>
                <td class="mdc-data-table__cell">$ 300.00</td>
                <td class="mdc-data-table__cell">$ 900.00</td>
              </tr>
              <tr class="mdc-data-table__row">
                <td class="mdc-data-table__cell"><button
                    class="material-icons mdc-icon-button btn-edit">edit</button><button
                    class="material-icons mdc-icon-button btn-erase">delete</button></td>
                <td class="mdc-data-table__cell">Dogui Sabor Primavera 200gr</td>
                <td class="mdc-data-table__cell">DOGUI ARG.</td>
                <td class="mdc-data-table__cell">
                  <div class="group table-group"><button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
                    aria-label="Open navigation menu">remove</button>
                            1
                            <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
                    aria-label="Open navigation menu">add</button></div>
                </td>
                <td class="mdc-data-table__cell">$ 100.00</td>
                <td class="mdc-data-table__cell">$ 100.00</td>
              </tr>
              <tr class="mdc-data-table__row">
                <td class="mdc-data-table__cell"><button
                    class="material-icons mdc-icon-button btn-edit">edit</button><button
                    class="material-icons mdc-icon-button btn-erase">delete</button></td>
                <td class="mdc-data-table__cell">Sabrositos Sabor Pescado 400gr</td>
                <td class="mdc-data-table__cell">SABROSITOS CHL.</td>
                <td class="mdc-data-table__cell">
                  <div class="group table-group"><button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
                    aria-label="Open navigation menu">remove</button>
                            2
                            <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
                    aria-label="Open navigation menu">add</button></div>
                </td>
                <td class="mdc-data-table__cell">$ 430.79</td>
                <td class="mdc-data-table__cell">$ 861.58</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <br><br>
      <button class="mdc-fab mdc-fab--extended">
        <div class="mdc-fab__ripple"></div>
        <span class="material-icons mdc-fab__icon">add</span>
        <span class="mdc-fab__label">Agregar Producto</span>
      </button>
    </div>
    
  </main>
    </form>
</body>

</html>