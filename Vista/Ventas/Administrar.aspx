<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Ventas.Administrar" %>

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
    <link rel="stylesheet" href="./index.css" />
    <script src="./index.js"></script>
</head>

<body>
    <form id="form1" runat="server" class="contents">
        <header class="mdc-top-app-bar mdc-top-app-bar--fixed">
            <div class="mdc-top-app-bar__row">
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-start">
                    <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button"
                        aria-label="Open navigation menu">
                        menu</button>
                    <span class="mdc-top-app-bar__title">Pet Shop</span>
                </section>
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">

                    <button class="mdc-button mdc-top-app-bar__action-item">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__label">ADMIN</span>
                    </button>
                    <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Search">search</button>
                    <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button"
                        aria-label="Options">
                        more_vert</button>
                </section>
            </div>
        </header>
        <main class="mdc-top-app-bar--fixed-adjust obj--main">
            <h2>Ventas</h2>
            <div class="searchbox">
                <span class="text">Buscar</span>
                <span class="material-icons">search</span>
            </div>
            <div class="mdc-data-table">
                <div class="mdc-data-table__table-container">
                    <table class="mdc-data-table__table" aria-label="Dessert calories">
                        <thead>
                            <tr class="mdc-data-table__header-row">
                                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Acciones</th>
                                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Código</th>
                                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Empleado</th>
                                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Medio de pago</th>
                                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Fecha</th>
                                <th class="mdc-data-table__header-cell" role="columnheader" scope="col">Total</th>
                            </tr>
                        </thead>
                        <tbody class="mdc-data-table__content">
                            <tr class="mdc-data-table__row">
                                <td class="mdc-data-table__cell">
                                    <button class="material-icons mdc-icon-button btn-open_in_new">open_in_new</button>
                                    <button class="material-icons mdc-icon-button btn-erase">delete</button></td>
                                <td class="mdc-data-table__cell">000</td>
                                <td class="mdc-data-table__cell"><a href='#'>Antonio León</a></td>
                                <td class="mdc-data-table__cell">Tarjeta de Crédito</td>
                                <td class="mdc-data-table__cell">15 de junio de 2023 12:15</td>
                                <td class="mdc-data-table__cell">$ 258.60</td>
                            </tr>
                            <tr class="mdc-data-table__row">
                                <td class="mdc-data-table__cell">
                                    <button class="material-icons mdc-icon-button btn-open_in_new">open_in_new</button>
                                    <button class="material-icons mdc-icon-button btn-erase">delete</button></td>
                                <td class="mdc-data-table__cell">001</td>
                                <td class="mdc-data-table__cell"><a href='#'>Héctor Da Silva</a></td>
                                <td class="mdc-data-table__cell">Efectivo</td>
                                <td class="mdc-data-table__cell">21 de agosto de 2022 15:15</td>
                                <td class="mdc-data-table__cell">$ 1,861.58</td>
                            </tr>
                            <tr class="mdc-data-table__row">
                                <td class="mdc-data-table__cell">
                                    <button class="material-icons mdc-icon-button btn-open_in_new">open_in_new</button>
                                    <button class="material-icons mdc-icon-button btn-erase">delete</button></td>
                                <td class="mdc-data-table__cell">002</td>
                                <td class="mdc-data-table__cell"><a href='#'>Ana María González</a></td>
                                <td class="mdc-data-table__cell">Transferencia bancaria</td>
                                <td class="mdc-data-table__cell">15 de septiembre de 2021 20:50</td>
                                <td class="mdc-data-table__cell">$ 2,568.08</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </main>
    </form>
</body>

</html>
