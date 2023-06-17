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
    }
}
