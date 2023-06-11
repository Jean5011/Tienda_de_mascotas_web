<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Proveedores.Administrar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form class="container-fluid flex-column align-items-center" id="form1" runat="server">
        <h1>Administrar Proveedores</h1>
        <br />
        <br />
        <table class="table">
            <thead>
                <tr>
                    <th></th>
                    <th scope="col">CUIT</th>
                    <th scope="col">Razón Social</th>
                    <th scope="col">Contacto</th>
                    <th scope="col">Dirección</th>
                    <th scope="col">Localidad</th>
                    <th scope="col">Código Postal</th>
                    <th scope="col">Provincia / Estado</th>
                    <th scope="col">País</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <button class="btn btn-primary">Editar</button>
                        <button class="btn btn-danger">Eliminar</button>
                    </td>
                    <th scope="row">98 444 500</th>
                    <td>BURGER KING</td>
                    <td>
                        <button type="button" class="btn btn-link">Llamar</button>
                        <button type="button" class="btn btn-link">Correo</button></td>
                    <td>Av. Siempreviva 736</td>
                    <td>Springfield</td>
                    <td>I400</td>
                    <td>Illinois</td>
                    <td>Estados Unidos</td>
                </tr>

                <tr>
                    <td>
                        <button class="btn btn-primary">Editar</button>
                        <button class="btn btn-danger">Eliminar</button>
                    </td>
                    <th scope="row">98 444 501</th>
                    <td>MAROLIO</td>
                    <td>
                        <button type="button" class="btn btn-link">Llamar</button>
                        <button type="button" class="btn btn-link">Correo</button></td>
                    <td>Av. Larralde 1001</td>
                    <td>Tigre</td>
                    <td>B1648</td>
                    <td>Buenos Aires Provincia</td>
                    <td>Argentina</td>
                </tr>

                <tr>
                    <td>
                        <button class="btn btn-primary">Editar</button>
                        <button class="btn btn-danger">Eliminar</button>
                    </td>
                    <th scope="row">98 444 520</th>
                    <td>DOGUI</td>
                    <td>
                        <button type="button" class="btn btn-link">Llamar</button>
                        <button type="button" class="btn btn-link">Correo</button></td>
                    <td>Av. Farfalla 856</td>
                    <td>Santa Rosa</td>
                    <td>P4233</td>
                    <td>La Pampa</td>
                    <td>Argentina</td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
