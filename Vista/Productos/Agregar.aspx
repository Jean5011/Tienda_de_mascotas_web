﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="Vista.Productos.Agregar" %>
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
    <form ></form>
    <!--
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
        <h2>Añadir producto</h2>
        <br />

        <div class="group">
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">Nombre</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtNombre"></asp:TextBox>
            </label>
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">ID</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtID"></asp:TextBox>
            </label>
        </div>

        <div class="group">
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">Tipo de producto</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtTipoProducto"></asp:TextBox>
            </label>
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">CUIT del Proveedor</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtCUITProveedor"></asp:TextBox>
            </label>
        </div>

        <div class="group">
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">Descripción</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtDescripcion"></asp:TextBox>
            </label>
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">Marca</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtMarca"></asp:TextBox>
            </label>
        </div>

        <div class="group">
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">Stock</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtStock"></asp:TextBox>
            </label>
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">URL Imagen</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:TextBox runat="server" CssClass="mdc-text-field__input" ID="txtURLImagen"></asp:TextBox>
            </label>
        </div>

        <div class="group">
            <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                    <span class="mdc-notched-outline__leading"></span>
                    <span class="mdc-notched-outline__notch">
                        <span class="mdc-floating-label" id="my-label-id">Precio Unitario</span>
                    </span>
                    <span class="mdc-notched-outline__trailing"></span>
                </span>
                <asp:DropDownList runat="server" CssClass="mdc-text-field__input" ID="ddlPrecioUnitario"></asp:DropDownList>
            </label>
        </div>

        <br />
        <asp:Button runat="server" CssClass="mdc-button mdc-button--raised" Text="Guardar" OnClick="Guardar_Click" />
    </div>
</main>

  </div>
    -->
</body>

</html>