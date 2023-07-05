using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    public class Response {
        public bool ErrorFound { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public object ObjectReturned { get; set; }
        public int AffectedRows { get; set; }
        public Exception Exception { get; set; }
        public Response() {
            this.ErrorFound = false;
            this.Message = "";
            this.Details = "";
            this.ObjectReturned = null;
            this.AffectedRows = 0;
            this.Exception = null;
        }

        public static Response TokenCaducado = new Response {
            ErrorFound = true,
            Message = "El token caducó, iniciá sesión para continuar. "
        };
        public static Response ErrorDesconocido = new Response {
            ErrorFound = true,
            Message = "Error desconocido. "
        };
        public static Response PermisosInsuficientes = new Response {
            ErrorFound = true,
            Message = "No disponés de los permisos suficientes para realizar esta acción. "
        };
    }
}
