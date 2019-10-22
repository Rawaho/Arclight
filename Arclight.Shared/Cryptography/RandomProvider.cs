using System;
using System.Security.Cryptography;

namespace Arclight.Shared.Cryptography
{
    public static class RandomProvider
    {
        /// <summary>
        /// Generate a new secure random session key.
        /// </summary>
        public static ulong GenerateSessionKey()
        {
            byte[] GenerateBytes(uint amount)
            {
                using var provider = new RNGCryptoServiceProvider();
                var bytes = new byte[amount];
                provider.GetBytes(bytes);
                return bytes;
            }

            return BitConverter.ToUInt64(GenerateBytes(sizeof(ulong)));
        }

        /// <summary>
        /// Generate a new random number between supplied min and max.
        /// </summary>
        public static uint RandomInteger(uint min, uint max)
        {
            var random = new Random();
            return (uint)random.Next((int)min, (int)max);
        }
    }
}
