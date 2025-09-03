using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace TechritizeAuthAPI.Helpers
{
    public static class PasswordHasher
    {
        // Hash a plain password
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verify if plain password matches hashed one
        public static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
