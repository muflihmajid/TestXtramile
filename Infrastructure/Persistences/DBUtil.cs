using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SceletonAPI.Infrastructure.Persistences
{
    public class DBUtil
    {
        private static readonly string PasswordSalt = "SceletonAPIPasswordSalt!?1qaz2wsx";
        private static readonly string AuthTokenKey = "SceletonAPIAuthTokenKeys";

        public static string PasswordHash(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(PasswordSalt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public static string GenerateAuthToken()
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: DateTime.Now.ToString("yyyyMMddHHmmssffff"), 
                salt: Encoding.ASCII.GetBytes(AuthTokenKey), 
                prf: KeyDerivationPrf.HMACSHA1, 
                iterationCount: 10000, 
                numBytesRequested: 256 / 8));
        }
        public static string RandomString(int size)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, size)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GeneratePin()
        {
            Random generator = new Random();
            string random = generator.Next(0, 999999).ToString("D6");
            return random;
        }
    }
}
