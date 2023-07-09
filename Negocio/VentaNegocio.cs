using System;
using System.Data;
using System.Globalization;
using Datos;
using Entidades;

namespace Negocio {
    public class VentaNegocio {
        public static Response ExtractPreliminarFromDataSet(DataSet resultDataSet) {
            if (resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0) {
                DataRow primerRegistro = resultDataSet.Tables[0].Rows[0];
                Venta.Preliminar obj = new Venta.Preliminar() {
                    Id = Convert.ToInt32(primerRegistro["ID"].ToString()),
                    AffectedRows = Convert.ToInt32(primerRegistro["AFFECTEDROWS"].ToString())
                };
                return new Response() {
                    ErrorFound = false,
                    ObjectReturned = obj
                };
            }
            else {
                return new Response() {
                    ErrorFound = true,
                    Message = SesionNegocio.ErrorCode.NO_ROWS,
                    ObjectReturned = null
                };
            }

        }
        public static Response ExtractDataFromDataSet(DataSet resultDataSet) {
            if (resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0) {
                DataRow primerRegistro = resultDataSet.Tables[0].Rows[0];
                Venta obj = new Venta() {
                    Id = Convert.ToInt32(primerRegistro[Venta.Columns.Id].ToString()),
                    EmpleadoGestor = new Empleado() { DNI = primerRegistro[Venta.Columns.DNI].ToString() },
                    TipoPago = primerRegistro[Venta.Columns.TipoPago].ToString(),
                    Fecha = primerRegistro[Venta.Columns.Fecha].ToString(),
                    Total = Convert.ToDouble(primerRegistro[Venta.Columns.Total].ToString())
                };
                return new Response() {
                    ErrorFound = false,
                    ObjectReturned = obj
                };
            }
            else {
                return new Response() {
                    ErrorFound = true,
                    Message = SesionNegocio.ErrorCode.NO_ROWS,
                    ObjectReturned = null
                };
            }

        }

        public static Response BuscarVentaPorID(int id) {
            var res = VentaDatos.GetVentaByID(id);
            if (!res.ErrorFound) {
                var res2 = ExtractDataFromDataSet(res.ObjectReturned as DataSet);
                return res2;
            }
            else return res;
        }

        public static Response Buscar(string key) {
            return VentaDatos.Buscar(key);
        }


        public static Response AgregarProducto(SessionData auth, Venta venta, Producto producto, int cantidad) {
            var respuesta = Response.ErrorDesconocido;
            bool tieneDerechoDeEdicion = auth.User.Rol == Empleado.Roles.ADMIN || auth.User.DNI == venta.EmpleadoGestor.DNI;
            if (tieneDerechoDeEdicion) {
                SesionNegocio.Autenticar(op => {
                    var res = ProductoNegocio.ObtenerPorCodigoYCuit(producto.Codigo,producto.Proveedor.CUIT);
                    if (!res.ErrorFound) {
                        // Producto exists
                        Producto p = res.ObjectReturned as Producto;
                        // Verificar el stock del producto:
                        if (cantidad <= p.Stock) // Si la cantidad indicada es menor o igual que el stock del producto, se realiza la venta del producto.
                        {
                            DetalleVenta dv = new DetalleVenta() {
                                Id = venta,
                                Producto = p,
                                Proveedor = p.Proveedor,
                                Cantidad = cantidad,
                                PrecioUnitario = p.Precio,
                                PrecioTotal = cantidad * p.Precio,
                                Estado = true
                            };
                            var uploadres = DetalleVentaNegocio.AgregarDetalleVenta(dv);
                            respuesta = new Response {
                                ErrorFound = uploadres.ErrorFound,
                                Message = !uploadres.ErrorFound
                                    ? $"El producto #{dv.Producto.Codigo} se agregó correctamente. "
                                    : $"Problema al registrar detalle. {uploadres.Details}. "
                            };
                        }
                        else {
                            respuesta = new Response {
                                ErrorFound = true,
                                Message = "No hay stock."
                            };
                        }
                    }
                    else {
                        respuesta = new Response {
                            ErrorFound = true,
                            Message = "El producto ingresado no está disponible. "
                        };
                    }

                }, err => {
                    respuesta = Response.TokenCaducado;
                });
                return respuesta;
            }
            else {
                respuesta = Response.PermisosInsuficientes;
            }
            return respuesta;
        }

        public static Response EliminarPermanentementeVentaPorID(SessionData auth, Venta venta) {
            var res = Response.ErrorDesconocido;
            if (auth.User.Rol == Empleado.Roles.ADMIN) {
                SesionNegocio.Autenticar(ok => {
                    // Verificamos que exista la venta en cuestión:
                    var existe = BuscarVentaPorID(venta.Id);
                    if (!existe.ErrorFound) {
                        // La Venta sí existe, procedemos, ahora sí, a eliminar.
                        var operacion = VentaDatos.EliminarVentaYDetalles(venta);
                        res = new Response {
                            ErrorFound = operacion.ErrorFound,
                            Message = !operacion.ErrorFound
                                ? "La venta se ha eliminado correctamente. "
                                : "Hubo un error al intentar eliminar la venta y/o los detalles. "
                        };
                    }
                    else {
                        res = new Response {
                            ErrorFound = true,
                            Message = "No existe un registro Venta con ese ID."
                        };
                    }
                }, err => {
                    res = Response.TokenCaducado;
                });
                return res;
            }
            return Response.PermisosInsuficientes;
        }

        public static Response GetVentas() {
            var res = VentaDatos.GetVentas();
            return res;
        }
        public static Response GetVentaPorID(int id) {
            return VentaDatos.GetVentaByID(id);
        }

        public static Response IniciarVenta(Venta obj) {
            var res = Response.ErrorDesconocido;
            SesionNegocio.Autenticar((data) => {
                var op = VentaDatos.IniciarVenta(obj);
                if (!op.ErrorFound) {
                    var vp = ExtractPreliminarFromDataSet(op.ObjectReturned as DataSet);
                    if(!vp.ErrorFound) {
                        var venta = vp.ObjectReturned as Venta.Preliminar;
                        res = new Response {
                            Message = "Código de venta asignado: #" + venta.Id,
                            ErrorFound = false,
                            ObjectReturned = venta
                        };
                    } else {
                        res = vp;
                    }
                }
                else {
                    res = op;
                }
            }, (err) => {
                res = Response.TokenCaducado;
            });
            return res;
        }

        public static Response Reporte_VentasOrdenadasPorTotales(string fechaInicio, string fechaFin) {
            //DateTime fi = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (!DateTime.TryParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fi)
                || !DateTime.TryParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fn)) {
                return new Response {
                    ErrorFound = true,
                    Message = "La fecha ingresada es incorrecta. "
                };
            }
            return VentaDatos.Reporte_VentasOrdenadasPorTotales(fi.ToString("yyyy-MM-dd"), fn.ToString("yyyy-MM-dd"));
        }

        public static Response VentasPorEmp(string dni) {
            return VentaDatos.GetVentaByDNI(dni);
        }

        public static class Widgets {
            public static Response TotalDeVentasUltimoDia() {
                return VentaDatos.Widgets.TotalDeVentasUltimoDia();
            }
            public static Response TotalDeVentasUltimaSemana() {
                return VentaDatos.Widgets.TotalDeVentasUltimaSemana();
            }
            public static Response ProductoMasVendidoUltimaSemana(out int cantidad) {
                cantidad = 0;
                var res = VentaDatos.Widgets.ProductoMasVendidoUltimaSemana();
                if (!res.ErrorFound) {
                    var dt = res.ObjectReturned as DataSet;
                    Response obj = ProductoNegocio.ExtractDataFromDataSet(dt);
                    if (!obj.ErrorFound) {
                        Producto p = obj.ObjectReturned as Producto;
                        cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["Cantidad"]);
                        return new Response() {
                            ErrorFound = false,
                            ObjectReturned = p
                        };
                    }
                    return new Response() {
                        ErrorFound = true,
                        Message = "Error al procesar DataSet"
                    };
                }
                return new Response() {
                    ErrorFound = true,
                    Message = "Error al obtener datos"
                };
            }

            public static Response CantidadDeProductosPorAgotarse(out int cantidad) {
                cantidad = 0;
                var res = VentaDatos.Widgets.CantidadDeProductosPorAgotarse();
                if (!res.ErrorFound) {
                    var dt = res.ObjectReturned as DataSet;
                    cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["Cantidad"]);
                    return new Response() {
                        ErrorFound = false,
                        ObjectReturned = dt
                    };
                }
                return res;
            }
            public static Response CantidadDeProductosAgotados(out int cantidad) {
                cantidad = 0;
                var res = VentaDatos.Widgets.CantidadDeProductosAgotados();
                if (!res.ErrorFound) {
                    var dt = res.ObjectReturned as DataSet;
                    cantidad = Convert.ToInt32(dt.Tables[0].Rows[0]["Cantidad"]);
                    return new Response() {
                        ErrorFound = false,
                        ObjectReturned = dt
                    };
                }
                return res;
            }

        }
    }
}
