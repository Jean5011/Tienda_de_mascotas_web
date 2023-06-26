using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vista {
    public static class Utils {
        public static readonly string actualUser = "Usuario_Actual";
        public static readonly string AUTH = "__auth";
      

        public static void ShowSnackbar(string message, Control control, Type type) {
            const string key = "MostrarMensaje";
            string script = $"MostrarMensaje('{message}');";
            ScriptManager.RegisterStartupScript(
                control: control,
                type: type,
                key: key,
                script: script,
                addScriptTags: true
            );
        }
        public static void MostrarMensaje(string mensaje, Control control, Type type) {
            string script = "MostrarMensaje('" + mensaje + "');";
            ScriptManager.RegisterStartupScript(control, type, "MostrarMensaje", script, true);
        }
        public static void EsperarSegundos(double cantSeg)
        {
            // Creo la cadena para convertir en TimeSpan:
            string s = "0.00:00:" + cantSeg.ToString().Replace(",", ".");
            TimeSpan ts = TimeSpan.Parse(s);

            // Le añado la diferencia a la hora actual:
            DateTime t1 = DateTime.Now.Add(ts);

            // Guardo la fecha y hora actual en una variable DateTime:
            DateTime t2 = DateTime.Now;

            // Mientras no haya pasado el tiempo indicado:
            while (t2 < t1)
            {
                // Asignar la hora actual:
                t2 = DateTime.Now;
            }
        }

        public static void SetTimeout(Action funcionAEjecutar, double segundos) {
            EsperarSegundos(segundos);
            funcionAEjecutar.Invoke();
        }
    }
}