using BCrypt.Net;

namespace Arclight.Shared.Cryptography
{
    public static class BCryptProvider
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA512);
        }

        public static bool Verify(string password, string digest)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, digest, HashType.SHA512);
        }
    }
}
