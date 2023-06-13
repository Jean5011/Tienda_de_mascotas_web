﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="Vista.Proveedores.Agregar" %>

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
  <div class="contents">
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
        <h2>Añadir proveedor</h2>
      <br>
  
      <div class="group">
  
        <label class="mdc-text-field mdc-text-field--outlined">
          <span class="mdc-notched-outline">
            <span class="mdc-notched-outline__leading"></span>
            <span class="mdc-notched-outline__notch">
              <span class="mdc-floating-label" id="my-label-id">CUIT</span>
            </span>
            <span class="mdc-notched-outline__trailing"></span>
          </span>
          <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
        </label><label class="mdc-text-field mdc-text-field--outlined">
          <span class="mdc-notched-outline">
            <span class="mdc-notched-outline__leading"></span>
            <span class="mdc-notched-outline__notch">
              <span class="mdc-floating-label" id="my-label-id">Razón Social</span>
            </span>
            <span class="mdc-notched-outline__trailing"></span>
          </span>
          <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
        </label>
  </div>
  <div class="group">
    <label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">Nombre de contacto</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
    </label>
    <label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">Código Postal</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
    </label>
  </div>
  <div class="group">
    <label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">Correo electrónico</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
    </label>
    <label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">Número de teléfono</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
    </label>
  </div>
  <div class="group">
    <label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">Dirección</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
    </label>
    <label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">Localidad</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
    </label>
  </div>
  <div class="group">
    <label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">Provincia</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <select name="" class="mdc-text-field__input" id="sexo_select"></select>
    </label><label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">País</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <select name="" class="mdc-text-field__input" id="sexo_select"></select>
    </label>
  </div>
     
      <br>
      <button class="mdc-button mdc-button--raised">
        <span class="mdc-button__label">Guardar</span>
      </button>
      </div>
    </main>
  </div>
</body>

</html>
