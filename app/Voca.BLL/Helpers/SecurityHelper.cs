using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Voca.BLL.Helpers
{
    public static class SecurityHelper
    {
        public static string GetHash(string raw)
        {
            return Encoding.ASCII.GetString(new SHA512Managed().ComputeHash(Encoding.ASCII.GetBytes(raw)));
        }

        public static string GeneratePassword()
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateApiKey()
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 40)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
