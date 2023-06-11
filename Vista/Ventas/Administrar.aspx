<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Ventas.Administrar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form class="container-fluid flex-column align-items-center" id="form1" runat="server">
        <h1>Ventas</h1>
        <br />
        <br />
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">Número de Factura</th>
                    <th scope="col">Empleado</th>
                    <th scope="col">Fecha</th>
                    <th scope="col">Coste total</th>
                    <th scope="col">Acciones</th>
                </tr>
            </thead>
            <tbody>
               <tr>
                   <td>001</td>
                   <td>Dante Dabrowski</td>
                   <td>12/04/2021 13:15</td>
                   <td>$1.565,80</td>
                   <td>
                       <button class="btn btn-link">Ver Detalles</button>
                       <button class="btn btn-link">Editar</button>
                   </td>
               </tr>
            </tbody>
        </table>
    </form>
</body>
</html>

