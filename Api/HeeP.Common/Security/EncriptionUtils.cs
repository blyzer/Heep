using System.Security.Cryptography;
using System.Text;

namespace HeeP.Common.Security
{
    public static class EncriptionUtils
    {
        public static byte[] HashValue(string value, string salt)
        {
            value = string.Concat(value, salt);
            byte[] arrbyte = new byte[value.Length];

            using (var hash = SHA1.Create())
            {
                return hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        public static string HashValueToString(string value, string salt)
            => Encoding.UTF8.GetString(HashValue(value, salt));
    }
}