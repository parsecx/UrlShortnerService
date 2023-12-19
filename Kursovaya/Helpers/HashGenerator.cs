using System.Security.Cryptography;
using System.Text;

namespace Kursovaya.NewFolder
{
    public static class HashGenerator
    {
        public static string GenerateHash(string url)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute hash from the URL
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < Math.Min(6, hashedBytes.Length); i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
