using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio {
    public partial class Authorization {
        public static class AccessLevel {
            public const int ANY = 0;
            public const int ONLY_LOGGED_IN_EMPLOYEE = 1;
            public const int ONLY_LOGGED_IN_ADMIN = 2;

        }
        public int AccessType { get; set; }
        public bool RequestAuthentication { get; set; }
        public bool RejectNonMatches { get; set; }
        public string Message { get; set; }

        public Authorization() {
            AccessType = AccessLevel.ANY;
            RejectNonMatches = false;
            Message = "";
        }
        public static Authorization ONLY_EMPLOYEES_STRICT = new Authorization() {
            AccessType = AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
            RejectNonMatches = true,
            Message = "Iniciá sesión para continuar"
        };
        public static Authorization ONLY_ADMINS_STRICT = new Authorization() {
            AccessType = AccessLevel.ONLY_LOGGED_IN_ADMIN,
            RejectNonMatches = true,
            Message = "Ingresá como administrador para continuar"
        };
        public static Authorization ONLY_EMPLOYEES = new Authorization() {
            AccessType = AccessLevel.ONLY_LOGGED_IN_EMPLOYEE,
            RejectNonMatches = false
        };
        public static Authorization ONLY_ADMINS = new Authorization() {
            AccessType = AccessLevel.ONLY_LOGGED_IN_ADMIN,
            RejectNonMatches = false
        };
        public static Authorization NO_RESTRICTIONS = new Authorization() {
            AccessType = AccessLevel.ANY,
            RejectNonMatches = false
        };

        public SessionData Check() {
            Entidades.Response res_b = SesionNegocio.ObtenerDatosEmpleadoActual();

            bool onlyAdminsAllowed = AccessType == AccessLevel.ONLY_LOGGED_IN_ADMIN;
            bool onlyEmployeesAllowed = AccessType == AccessLevel.ONLY_LOGGED_IN_EMPLOYEE;
            bool anybodyAllowed = AccessType == AccessLevel.ANY;

            if (res_b.ErrorFound) {

                bool noSessionFound = res_b == SesionNegocio.AuthenticationResult.NoSessionFound;
                bool tokenExpired = res_b == SesionNegocio.AuthenticationResult.ExpiredToken;

                int status = SessionData.StatusCode.UNSPECIFIED_ERROR;

                if (noSessionFound || tokenExpired) {
                    if (noSessionFound) status = SessionData.StatusCode.NO_SESSION_FOUND;
                    if (tokenExpired) status = SessionData.StatusCode.TOKEN_EXPIRED;

                    if (AccessType == AccessLevel.ANY) {
                        // No hay sesión pero no importa
                    }
                    else {
                        // No hay sesión pero se solicita que mínimo haya una sesión activa. Se redirige a la página de login.
                    }
                }

                return new SessionData() {
                    Granted = false,
                    User = null,
                    Status = status
                };
            }
            else {
                if (res_b.ObjectReturned != null) {

                    Empleado obj = res_b.ObjectReturned as Empleado;

                    bool currentUserIsAdmin = obj.Rol == Empleado.Roles.ADMIN;
                    bool currentUserIsEmployee = obj.Rol == Empleado.Roles.NORMAL;


                    if (onlyAdminsAllowed) { // Si el acceso permitido es únicamente para administradores...
                        return new SessionData() {
                            Granted = currentUserIsAdmin,
                            User = obj,
                            Status = currentUserIsAdmin ? SessionData.StatusCode.OK : SessionData.StatusCode.UNAUTHORIZED
                        };
                    }

                    if (onlyEmployeesAllowed) { // Si el acceso permitido es para empleados únicamente...
                        bool approved = currentUserIsEmployee || currentUserIsAdmin;
                        return new SessionData() {
                            Granted = approved,
                            User = obj,
                            Status = approved ? SessionData.StatusCode.OK : SessionData.StatusCode.UNAUTHORIZED
                        };
                    }

                    if (anybodyAllowed) {
                        return new SessionData() {
                            Granted = true,
                            User = obj,
                            Status = SessionData.StatusCode.OK
                        };
                    }

                    return new SessionData() {
                        Granted = false,
                        User = obj,
                        Status = SessionData.StatusCode.UNSPECIFIED_ERROR
                    };

                }
                else {
                    return new SessionData() {
                        Granted = (AccessType == AccessLevel.ANY),
                        User = null,
                        Status = SessionData.StatusCode.UNSPECIFIED_ERROR
                    };
                }
            }
        }


        

    }

}
