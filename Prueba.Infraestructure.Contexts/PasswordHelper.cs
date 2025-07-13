using System.Security.Cryptography;
using System.Text;

namespace Prueba.Infraestructure.Contexts
{
    public static class PasswordHelper
    {
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredPasswordHash = EncryptPassword(enteredPassword);
            return enteredPasswordHash.Equals(storedHash);
        }
    }
}