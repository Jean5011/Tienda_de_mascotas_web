using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vista.Productos {
    public partial class Agregar : System.Web.UI.Page {
        public void IniciarSesion(object sender, EventArgs e) {
            string login_url = "/Empleados/IniciarSesion.aspx";
            string next_url = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect($"{login_url}?next={next_url}");
        }
        public void VerPerfilActual(object sender, EventArgs e) {
            Response.Redirect("/Empleados/Perfil.aspx");
        }
        private Empleado UsuarioActual;
        protected void Page_Load(object sender, EventArgs e) {
            bool inicioSesion = Utils.CargarAdmin(this, true, "Iniciá sesión como administrador para agregar productos.");
            if (inicioSesion) {
                if (UsuarioActual.Rol == Empleado.Roles.ADMIN) {
                    // El usuario logueado es un ADMINISTRADOR con derecho a crear y borrar productos.
                    // Si llegó acá está todo bien.

                    /// IMPORTANTE
                    /// ANTES DE LLAMAR UNA FUNCIÓN QUE REALIZA UN CAMBIO (CUANDO SE PRESIONA EL BOTON "AGREGAR", POR EJEMPLO)
                    /// TENÉS QUE AUTENTICAR
                    /// AL USUARIO. PARA VERIFICAR QUE SIGA ACTIVO
                    /// 
                    bool auth = SesionNegocio.Autenticar(); // Si devuelve true, podés hacer acciones delicadas.
                    // Si devuelve no, lo mandás de nuevo a la página de inicio para que vuelva a iniciar sesión.


                }
                else {
                    Utils.MostrarMensaje("UNAUTHORIZED", this.Page, GetType());
                }
            } 
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            //Utils.MostrarMensaje($"EVENTO CLIIIIICKKKKKKK. ", this.Page, GetType());
            //aca casteo el precio para que se pueda cargar
            // Autenticar acá más adelante
            string numero = txtPrecioUnitario.Text;
            double Pre;
            if (double.TryParse(numero, out Pre))
            {
                string stock = txtStock.Text;
                int st;
                if(int.TryParse(stock, out st))
              {
                Producto Prod = new Producto()
                {
                    Codigo = txtID.Text,
                    Proveedor = new Proveedor() { CUIT = txtCUITProveedor.Text },
                    Categoria = new TipoProducto() { tipoDeProducto = txtTipoProducto.Text },
                    Nombre = txtNombre.Text,
                    Marca = txtMarca.Text,
                    Descripcion = txtDescripcion.Text,
                    Stock = st,
                    Imagen = txtURLImagen.Text,
                    Precio = Pre,
                    Estado = true,
                };
                Response response = ProductoNegocio.IngresarProducto(Prod);
                if (!response.ErrorFound)
                {
                    Utils.MostrarMensaje($"Producto guardado correctamente. ", this.Page, GetType());
                }
                else
                {
                    //Utils.MostrarMensaje($"Error al guardar producto. ", this.Page, GetType());
                    string error = response.Message;
                    Utils.MostrarMensaje(error, this.Page, GetType());
                }
              }
                else
                {
                    Utils.MostrarMensaje($"El stock ingresado no es valido. ", this.Page, GetType());
                }
            }
            else
            {
                Utils.MostrarMensaje($"El precio ingresado no es valido. ", this.Page, GetType());
            }
            
        }
        
    }
}