using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Datos {
    public class Connection {
        public static class Database {
            public static string Neptuno { get { return "Neptuno"; } }
            public static string BDSucursales { get { return "BDSucursales"; } }
            public static string Libreria { get { return "Libreria"; } }
            public static string Viajes { get { return "Viajes"; } }
        }
        public string ServerName { get { return "localhost\\SQLEXPRESS"; } }
        public bool IntegratedSecurity { get { return true; } }
        public string DatabaseName { get; set; }
        public Response Response = new Response() { };
        public Connection(string DatabaseName) {
            this.DatabaseName = DatabaseName;
        }
        /// <summary>
        /// Devuelve una conexión SQL.
        /// </summary>
        /// <param name="DatabaseName">Nombre de la base de datos a la que querés conectar. Para Neptuno, hay una variable: Database.Neptuno.</param>
        /// <returns></returns>
        public SqlConnection OpenConnection(string DatabaseName) {
            SqlConnection con;
            try {
                string r = $"Data Source={ServerName};Initial Catalog={DatabaseName};Integrated Security={IntegratedSecurity}";
                con = new SqlConnection(r);

            }
            catch (SqlException err) {
                this.Response.ErrorFound = true;
                this.Response.Message = "Error al conectar al servidor";
                this.Response.Details = err.Message;
                this.Response.Exception = err;
                return null;
            }
            return con;
        }
        /// <summary>
        /// Realiza una consulta en la base de datos y recolecta datos obtenidos. 
        /// Sólo para consultas que devuelvan tablas con datos.
        /// </summary>
        /// <param name="query">La consulta a ejecutar</param>
        /// <param name="parameters">Parámetros de la consulta.</param>
        /// <returns>Objeto Response con el resultado de la operación y los datos obtenidos.</returns>
        public Response FetchData(string query, Dictionary<string, object> parameters = null) {
            DataSet dataSet = new DataSet();
            try {
                using (SqlConnection con = OpenConnection(this.DatabaseName)) {
                    using (SqlCommand command = new SqlCommand(query, con)) {
                        if (parameters != null) {
                            foreach (KeyValuePair<string, object> parameter in parameters) {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command)) {
                            dataAdapter.Fill(dataSet, "root");
                        }
                    }
                }
            }
            catch (Exception ex) {
                return new Response() {
                    ErrorFound = true,
                    Message = "Error al obtener datos de la base de datos. ",
                    Details = ex.ToString(),
                    Exception = ex
                };
            }
            return new Response() {
                ObjectReturned = dataSet
            };
        }
        /// <summary>
        /// Realiza una consulta en la base de datos. 
        /// Sólo consultas que actualizan, eliminan o agregan registros.
        /// </summary>
        /// <param name="query">La consulta a ejecutar.</param>
        /// <param name="parameters">Parámetros de la consulta.</param>
        /// <returns>Objeto Response con el resultado de la transacción.</returns>
        public Response RunTransaction(string query, Dictionary<string, object> parameters = null) {
            try {
                using (SqlConnection con = OpenConnection(this.DatabaseName)) {
                    using (SqlCommand command = new SqlCommand(query, con)) {
                        if (parameters != null) {
                            foreach (KeyValuePair<string, object> parameter in parameters) {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        con.Open();
                        int filasAfectadas = command.ExecuteNonQuery();
                        return new Response() {
                            AffectedRows = filasAfectadas
                        };
                    }
                }
            }
            catch (Exception ex) {
                return new Response() {
                    ErrorFound = true,
                    Message = "Error al realizar la transacción en la base de datos. ",
                    Details = ex.ToString(),
                    Exception = ex,
                    AffectedRows = 0
                };
            }
        }

    }
}
