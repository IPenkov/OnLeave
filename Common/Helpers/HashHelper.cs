namespace Common.Helpers
{
    #region Using Directives

    using System;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// Hash Helper class
    /// </summary>
    public class HashHelper
    {
        public static byte[] CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[8];
            rng.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Hashes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The hash</returns>
        public static string Hash(params object[] args)
        {
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(String.Concat(args));
            byte[] bytHash = hashAlg.ComputeHash(bytValue);
            return Convert.ToBase64String(bytHash);
        }
    }
}
