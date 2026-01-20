using System.Security.Cryptography;
using System.Text;

namespace CommercialManagement.Utilities
{
    public static class PasswordHasher
    {
        /// <summary>
        /// Hashes a password using SHA256
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <returns>Hashed password (64 character hex string)</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Verifies a password against a hash
        /// </summary>
        /// <param name="password">Plain text password to verify</param>
        /// <param name="hash">Stored hash to compare against</param>
        /// <returns>True if password matches hash</returns>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            string hashOfInput = HashPassword(password);
            return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hash) == 0;
        }

        /// <summary>
        /// Checks if a string is a valid SHA256 hash
        /// </summary>
        /// <param name="hash">String to check</param>
        /// <returns>True if it's a valid hash format</returns>
        public static bool IsValidHashFormat(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                return false;

            // SHA256 produces 64 character hex string
            if (hash.Length != 64)
                return false;

            // Check if all characters are valid hex
            return hash.All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));
        }

        /// <summary>
        /// Generates a random secure password
        /// </summary>
        /// <param name="length">Length of password (default 12)</param>
        /// <returns>Random password</returns>
        public static string GenerateRandomPassword(int length = 12)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@#$%^&*";
            StringBuilder password = new StringBuilder();

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                foreach (byte b in randomBytes)
                {
                    password.Append(validChars[b % validChars.Length]);
                }
            }

            return password.ToString();
        }
    }
}