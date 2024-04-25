using System.Security.Cryptography;
using System.Text;

namespace SubUrl.Service
{
    public class HashWorker
    {
        public static string GenerateSHA512HashInLengthWithSalt(string password, int length, string salt = "@salt@")
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hash = SHA512.HashData(bytes);

            return Convert.ToBase64String(hash).Substring(0, length);
        }
    }
}