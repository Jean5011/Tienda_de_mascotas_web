using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Vista {
    public static class Utils {

        public static void MostrarMensaje(string mensaje, Control control, Type type) {
            string script = "MostrarMensaje('" + mensaje + "');";
            ScriptManager.RegisterStartupScript(control, type, "MostrarMensaje", script, true);
        }
    }
}