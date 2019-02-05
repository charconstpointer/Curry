using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Curry.Helpers
{
    public static class Crypto
    {
        public static (string password, byte[] salt) Hash(string value, byte[] salt = null)
        {
            if (salt == null)
            {
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                value,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));
            return (password: hashed, salt: salt);
        }
    }
}
