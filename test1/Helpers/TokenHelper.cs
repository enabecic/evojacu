using System;
using System.Security.Cryptography;
using System.Text;

namespace evojacu.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[32];
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
