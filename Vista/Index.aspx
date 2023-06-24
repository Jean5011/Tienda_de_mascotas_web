<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Vista.Index" MasterPageFile="/Root.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
        <h2 id="H2Titulo" runat="server">PetShop</h2>
        <ul>
            <li>
                Total vendido último día: <asp:Label ID="lblTotalVendidoUltimoDia" runat="server" Text="Label"></asp:Label>
            </li>
            <li>
                Total vendido última semana: <asp:Label ID="lblTotalVendidoUltimaSemana" runat="server" Text="Label"></asp:Label>
            </li>
            Productos:
            <li>
                Producto más vendido última semana: <asp:Label runat="server" ID="lblProductoMasVendidoUltimaSemana" Text="Label"></asp:Label>
            </li>
        </ul>
        <ul>
            <b>Empleados</b>
            <li><a href="/Empleados/Administrar.aspx">Ver todos los empleados</a></li>
            <b>Ventas</b>
            <li><a href="/Ventas/Administrar.aspx">Ver todas las ventas</a></li>
            <li><a href="/Ventas/Crear.aspx">Agregar venta</a></li>
            <b>Productos</b>
            <li><a href="/Productos/Administrar.aspx">Ver todos los productos</a></li>
            <li><a href="/Productos/Agregar.aspx">Agregar producto (Sólo admins)</a></li>
            <b>Proveedores</b>
            <li><a href="/Proveedores/Administrar.aspx">Ver todos los proveedores</a></li>
            <li><a href="/Proveedores/Agregar.aspx">Agregar proveedor (Sólo admins)</a></li>
            <b>Animales</b>
            <li><a href="/Animales/VerAnimales.aspx">Ver todos los animales</a></li>
            <li><a href="/Animales/AgregarAnimal.aspx">Agregar animal (Sólo admins)</a></li>
            <b>Tipos</b>
            <li><a href="/Animales/VerTipoDeProducto.aspx">Ver todos los tipos</a></li>
            <li><a href="/Animales/IgresarTipoDePrductos.aspx">Agregar tipos (Sólo admins)</a></li>
        </ul>
    </div>
</asp:Content>
