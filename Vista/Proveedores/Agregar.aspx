<%@ Page MasterPageFile="/Root.Master" Language="C#" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="Vista.Proveedores.Agregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        </label>
          <label class="mdc-text-field mdc-text-field--outlined">
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
  </asp:Content>