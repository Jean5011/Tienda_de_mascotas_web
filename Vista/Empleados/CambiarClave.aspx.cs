using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Web.UI;

namespace Vista.Empleados {
    public partial class CambiarClave : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                string password = "potato";
                byte[] salt = GenerarSalt();
                byte[] hash = GenerarHash(password, salt);

                string saltBase64 = Convert.ToBase64String(salt);
                string hashBase64 = Convert.ToBase64String(hash);

                bool isPasswordCorrect = VerificarClave("potato", hash, salt);

                Label1.Text = "Salt: " + saltBase64 + "<br />Hash: " + hashBase64 + ("<br />¿La contraseña es correcta? " + isPasswordCorrect);
            }
        }
        string ConvertToHex(byte[] txt) {
            string hex = BitConverter.ToString(txt).Replace("-", string.Empty);
            return hex;
        }

        byte[] GenerarSalt() {
            byte[] salt = new byte[16]; // 16 bytes = 128 bits
            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(salt);
            }
            return salt;
        }
        byte[] GenerarHash(string password, byte[] salt) {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];

            Array.Copy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Array.Copy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

            using (var sha256 = SHA256.Create()) {
                return sha256.ComputeHash(combinedBytes);
            }
        }
        bool VerificarClave(string password, byte[] savedHash, byte[] savedSalt) {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + savedSalt.Length];

            Array.Copy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Array.Copy(savedSalt, 0, combinedBytes, passwordBytes.Length, savedSalt.Length);

            using (var sha256 = SHA256.Create()) {
                byte[] inputHash = sha256.ComputeHash(combinedBytes);
                return savedHash.SequenceEqual(inputHash);
            }
        }
    }
}
