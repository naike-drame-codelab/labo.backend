using System.Security.Cryptography;
using System.Text;

namespace Labo.Application.Utils
{
    public static class PasswordUtils
    {
        public static string HashPassword(string password, Guid salt)
        {
            return
                Encoding.UTF8.GetString(
                    SHA512.HashData(Encoding.UTF8.GetBytes(password + salt))
                );
        }

        public static string GeneratePassword(int length = 10)
        {
            string result = string.Empty;
            for (int i = 0; i < length; i++)
            {
                char letter = (char)new Random().Next(65, 91);
                result += letter;
            }
            return result;
        }
    }
}
