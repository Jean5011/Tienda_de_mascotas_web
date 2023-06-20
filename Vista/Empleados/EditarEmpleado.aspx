<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarEmpleado.aspx.cs" Inherits="Vista.Empleados.EditarEmpleado" %>

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
    <form id="form1" runat="server" class="contents">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <header class="mdc-top-app-bar mdc-top-app-bar--fixed">
            <div class="mdc-top-app-bar__row">
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-start">
                    <button class="material-icons mdc-top-app-bar__navigation-icon mdc-icon-button" aria-label="Open navigation menu">menu</button>
                    <span class="mdc-top-app-bar__title">Iniciar sesión</span>
                </section>
                <section class="mdc-top-app-bar__section mdc-top-app-bar__section--align-end" role="toolbar">
                    <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Search">search</button>
                    <button class="material-icons mdc-top-app-bar__action-item mdc-icon-button" aria-label="Options">more_vert</button>
                </section>
            </div>
        </header>
        <main class="mdc-top-app-bar--fixed-adjust obj--main">
            <div class="page">
                <h2>Editar detalles</h2>
                <br>
                <br>
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
                <div class="mdc-text-field-helper-line">
                    <span class="mdc-text-field-helper-text"></span>
                    <asp:RequiredFieldValidator ID="rfvDNI" aria-hidden="true" ControlToValidate="txtDNI" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Tenés que ingresar un DNI válido."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="txtDNI" ValidationExpression="^\d+$"
                        ErrorMessage="Ingresá únicamente números" CssClass="error-message"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="customValidator" runat="server" ControlToValidate="txtDNI"
                        OnServerValidate="customValidator_ServerValidate" ErrorMessage="El DNI ingresado ya está en uso. Intente con otro."
                        ></asp:CustomValidator>
                </div>
                <br>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Nombre</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Apellido</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <br>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Sexo</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:DropDownList ID="ddlGenero" CssClass="mdc-text-field__input" runat="server">
                        <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </label>
                <br>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Fecha de Nacimiento</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Fecha de Contrato</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtFechaContrato" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <br>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Sueldo</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtSueldo" runat="server" type="number" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <br>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Dirección</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Localidad</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <br>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Provincia</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtProvincia" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Nacionalidad</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <br>
                <br>
                <asp:CheckBox ID="chkEstado" runat="server" Text="Habilitado" />
                <br>
                <br>
                <hr />
                <br />
                <asp:CheckBox ID="chkAdmin" runat="server" Text="Conceder permisos de administrador" />
                <br />
                <br />
                <asp:LinkButton ID="btnGuardarCambios" runat="server" OnClick="btnGuardarCambios_Click" CssClass="mdc-button mdc-button--raised">
                    <span class="mdc-button__label">Guardar cambios</span>
                </asp:LinkButton>
            </div>

        </main>
        <aside class="mdc-snackbar">
            <div class="mdc-snackbar__surface" role="status" aria-relevant="additions">
                <div class="mdc-snackbar__label" aria-atomic="false"></div>
            </div>
        </aside>

    </form>
</body>
</html>
