<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Vista.Empleados.Administrar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form class="container-fluid flex-column align-items-center" id="form1" runat="server">
        <h1>Administrar Empleados</h1>
        <br />
        <br />
        <table class="table">
            <thead>
                <tr>
                    <th></th>
                    <th scope="col">DNI</th>
                    <th scope="col">Usuario</th>
                    <th scope="col">Nombre</th>
                    <th scope="col">Apellido</th>
                    <th scope="col">Sexo</th>
                    <th scope="col">Dirección completa</th>
                    <th scope="col">Estado</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <button class="btn btn-primary">Editar</button>
                        <button class="btn btn-danger">Eliminar</button>
                    </td>
                    <th scope="row">43 113 145</th>
                    <td>
                        @maria_dolores
                    </td>
                    <td>Ana María De Los Dolores</td>
                    <td>Buscaroli de Musicardi</td>
                    <td>F</td>
                    <td>Monti Verni 444, Vincente López (b1455), Buenos Aires Provincia, Argentina</td>
                    <td>Activo</td>
                </tr>
                
                <tr>
                    <td>
                        <button class="btn btn-primary">Editar</button>
                        <button class="btn btn-danger">Eliminar</button>
                    </td>
                    <th scope="row">43 113 145</th>
                    <td>
                        @julian_chase
                    </td>
                    <td>Julián Carlos</td>
                    <td>Chase</td>
                    <td>M</td>
                    <td>Montevideo 1371, Tigre, Buenos Aires, Argentina</td>
                    <td>Activo</td>
                </tr>
                <tr>
                    <td>
                        <button class="btn btn-primary">Editar</button>
                        <button class="btn btn-danger">Eliminar</button>
                    </td>
                    <th scope="row">43 113 145</th>
                    <td>
                        @diego_agustin
                    </td>
                    <td>Diego Augusto</td>
                    <td>González</td>
                    <td>M</td>
                    <td>Montevideo 1300, Tigre, Buenos Aires, Argentina</td>
                    <td>Activo</td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
