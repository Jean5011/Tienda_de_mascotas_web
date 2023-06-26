using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio {
    public class SessionData {
        public static class StatusCode {
            public const int ERR_SQL = 0;
            public const int OK = 1;
            public const int AUTHENTICATION_FAILED = 2;
            public const int NO_SESSION_FOUND = 3;
            public const int TOKEN_EXPIRED = 4;
            public const int UNSPECIFIED_ERROR = 5;
            public const int UNAUTHORIZED = 6;
        }
        public bool Granted { get; set; }
        public Empleado User { get; set; }
        public int Status { get; set; }

    }
}
