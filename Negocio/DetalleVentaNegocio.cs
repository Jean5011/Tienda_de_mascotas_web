using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace Negocio
{
    public class DetalleVentaNegocio
    {
       
        public static Response ObtenerDetalleVenta(int Cod)
        {
            return DaoDetalleVentas.ObtenerDetalleVenta(Cod);
        }

        public static Response AgregarDetalleVenta(DetalleVenta v)
        {
            return DaoDetalleVentas.AgregarRegistro(v);
        }

        public static Response EliminarDetalle(int cv, string cp) {
            return DaoDetalleVentas.EliminarDetalle(cv, cp);
        }

        /// @Deprecated
        public static Response DarDeBaja(int Cod) 
        {
            return DaoDetalleVentas.DarDeBajaRegistro(Cod);
        }

        public static Response aumentarCantidadVendida(DetalleVenta dv)
        {
            return DaoDetalleVentas.aumentarCantidadVendida(dv);
        }

        public static Response disminuirCantidadVendida(DetalleVenta dv)
        {
            return DaoDetalleVentas.disminuirCantidadVendida(dv);
        }

        private static Proveedor obtenerCUITProvDesdeDataSet(DataSet dsProducto, Proveedor proveedor)
        {
            foreach (DataRow fila in dsProducto.Tables[0].Rows) // recorro el DataSet dsProducto.
            {
                proveedor.CUIT = fila[Producto.Columns.CUITProv].ToString(); // guardo el CUIT en la propiedad CUIT del proveedor. Se guarda correctamente.
            }
            return proveedor;
        }

        private static Proveedor instanciarObjetoProveedor(DataSet dsProveedor, Proveedor proveedor)
        {
            foreach (DataRow fila in dsProveedor.Tables[0].Rows) // recorro el DataSet dsProveedor, y establezco las demás propiedades del objeto proveedor.
            {
                proveedor.RazonSocial = fila[Proveedor.Columns.RazonSocial].ToString();
                proveedor.NombreContacto = fila[Proveedor.Columns.NombreContacto].ToString();
                proveedor.CorreoElectronico = fila[Proveedor.Columns.CorreoElectronico].ToString();
                proveedor.Telefono = fila[Proveedor.Columns.Telefono].ToString();
                proveedor.Direccion = fila[Proveedor.Columns.Direccion].ToString();
                proveedor.Localidad = fila[Proveedor.Columns.Localidad].ToString();
                proveedor.Provincia = fila[Proveedor.Columns.Provincia].ToString();
                proveedor.Pais = fila[Proveedor.Columns.Pais].ToString();
                proveedor.CodigoPostal = fila[Proveedor.Columns.CodigoPostal].ToString();
                proveedor.Estado = Convert.ToBoolean(fila[Proveedor.Columns.Estado]);
            }
            return proveedor;
        }

        private static DetalleVenta instanciarObjetoDetalleVenta(DataSet dsDetalleVenta, Producto producto, Venta venta)
        {
            DetalleVenta dv = new DetalleVenta() // Guardo en las propiedades de DetalleVenta que tengan como tipo un objeto, los objetos correspondientes.
            {
                Id = venta,
                Producto = producto,
                Proveedor = producto.Proveedor
            };
            foreach (DataRow fila in dsDetalleVenta.Tables[0].Rows)
            {
                dv.Cantidad = Convert.ToInt32(fila[DetalleVenta.Columns.Cantidad_Dv]);
                dv.PrecioUnitario = Convert.ToDouble(fila[DetalleVenta.Columns.PrecioUnitario_Dv]);
                dv.PrecioTotal = Convert.ToDouble(fila[DetalleVenta.Columns.PrecioTotal_Dv]);
                dv.Estado = true;
                return dv; // si todo se asignó correctamente, devuelve el objeto DetalleVenta.
            }
            return null; // si ocurrió un error al buscar la info del detalle de venta, la función devuelve null.
        }

        public static DetalleVenta obtenerRegistro(DataSet dsDetalleVenta, Producto producto, Venta venta) // optimizar esta función (dividir por funciones).
        {
            Proveedor proveedor = new Proveedor(); // DECLARO una nueva instancia de Proveedor.

            var res = ProductoNegocio.BuscarPorCodigo(producto.Codigo); // busco el registro del código de producto enviado por parámetro.
            if (!res.ErrorFound)
            {
                DataSet dsProducto = res.ObjectReturned as DataSet; // guardo en el DataSet dsProducto el objeto devuelto.

                proveedor = obtenerCUITProvDesdeDataSet(dsProducto, proveedor); // en esta función se recorre el dsProducto para obtener el CUIT del proveedor asociado a ese producto.

                res = ProveedorNegocio.ObtenerProveedorByCUIT(proveedor.CUIT); // busco el proveedor por su CUIT.
                if (!res.ErrorFound)
                {
                    DataSet dsProveedor = res.ObjectReturned as DataSet; // guardo en el DataSet dsProveedor el objeto devuelto.

                    proveedor = instanciarObjetoProveedor(dsProveedor, proveedor); // para INICIALIZAR la instancia del objeto proveedor.
                }
                else { return null; } // si ocurrió un error al buscar la info del proveedor, la función devuelve null.

                producto.Proveedor = proveedor; // le asigno el objeto proveedor a la propiedad Proveedor del objeto prod.

                return instanciarObjetoDetalleVenta(dsDetalleVenta, producto, venta); // si se instanció correctamente, devuelve un objeto DetalleVenta, sino devuelve null.
            }
            else { return null; } // si ocurrió un error al buscar la info del producto, la función devuelve null.
        }
    }
}
