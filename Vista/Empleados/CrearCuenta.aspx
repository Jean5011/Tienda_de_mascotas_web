<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearCuenta.aspx.cs" Inherits="Vista.Empleados.CrearCuenta" %>

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
                            <span class="mdc-floating-label" id="dni-label">DNI *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtDNI" minlength="8" runat="server" CssClass="mdc-text-field__input" aria-labelledby="dni-label"></asp:TextBox>
                </label>
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtDNI" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un DNI."></asp:RequiredFieldValidator>
                </div>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Nombre *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtNombre" minlength="3" ValidationGroup="A" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtNombre" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un nombre."></asp:RequiredFieldValidator>
                </div>
                <br />
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Apellido *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ID="txtApellido" minlength="3" ValidationGroup="A" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtApellido" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Ingresá un apellido."></asp:RequiredFieldValidator>
                </div>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Sexo *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:DropDownList ValidationGroup="A" ID="ddlGenero" CssClass="mdc-text-field__input" runat="server">
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
                            <span class="mdc-floating-label" id="nlabel">Fecha de Nacimiento *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" ID="txtFechaNacimiento" type="date" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <label class="mdc-text-field mdc-text-field--outlined" style="margin-left: 12px">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Fecha de Contrato *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" ID="txtFechaContrato" type="date" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <br>
                <br />
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Sueldo *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" ID="txtSueldo" runat="server" type="number" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtSueldo" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
                </div>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Dirección *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" ID="txtDireccion" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtDireccion" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
                </div>
                <br />
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Localidad *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" ID="txtLocalidad" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtLocalidad" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
                </div>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Provincia *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" ID="txtProvincia" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtProvincia" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
                </div>
                <br />
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Nacionalidad *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" ID="txtNacionalidad" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtNacionalidad" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
                </div>
                <br>
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Creá una clave *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" ID="txtClave" TextMode="Password" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                <div class="mdc-text-field-helper-line">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="mdc-text-field-helper-text" aria-hidden="true" ControlToValidate="txtNacionalidad" runat="server" ErrorMessage="" ValidationGroup="A" SetFocusOnError="True" Text="Revisá este dato."></asp:RequiredFieldValidator>
                </div>
                <br />
                <label class="mdc-text-field mdc-text-field--outlined">
                    <span class="mdc-notched-outline">
                        <span class="mdc-notched-outline__leading"></span>
                        <span class="mdc-notched-outline__notch">
                            <span class="mdc-floating-label" id="nlabel">Confirmá la clave *</span>
                        </span>
                        <span class="mdc-notched-outline__trailing"></span>
                    </span>
                    <asp:TextBox ValidationGroup="A" TextMode="Password" ID="txtConfirmarClave" runat="server" CssClass="mdc-text-field__input" aria-labelledby="nlabel"></asp:TextBox>
                </label>
                 <div class="mdc-text-field-helper-line">
                     <asp:CompareValidator ID="CompareValidator1" CssClass="mdc-text-field-helper-text"  runat="server" ErrorMessage="Las claves no coinciden" ControlToCompare="txtClave" ControlToValidate="txtConfirmarClave"></asp:CompareValidator>
                </div>
                <br />
                <br>
                <asp:CheckBox ValidationGroup="A" ID="chkEstado" runat="server" Text="Habilitado" />
                <br>
                <br>
                <hr />
                <br />
                <asp:CheckBox ValidationGroup="A" ID="chkAdmin" runat="server" Text="Conceder permisos de administrador" />
                <br />
                <br />
                <asp:LinkButton ValidationGroup="A" ID="btnGuardarCambios" runat="server" CssClass="mdc-button mdc-button--raised" OnClick="btnGuardarCambios_Click">
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

