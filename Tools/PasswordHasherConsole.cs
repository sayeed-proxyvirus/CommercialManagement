using System;
using System.Security.Cryptography;
using System.Text;

namespace CommercialManagement.Tools
{
    /// <summary>
    /// Console application to hash passwords for testing
    /// Usage: Run this to generate hashed passwords for your database
    /// </summary>
    class PasswordHasherConsole
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("  Password Hasher - Commercial Management");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Options:");
                Console.WriteLine("1. Hash a password");
                Console.WriteLine("2. Verify a password");
                Console.WriteLine("3. Generate sample SQL UPDATE statements");
                Console.WriteLine("4. Exit");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HashPassword();
                        break;
                    case "2":
                        VerifyPassword();
                        break;
                    case "3":
                        GenerateSQLStatements();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void HashPassword()
        {
            Console.Write("Enter password to hash: ");
            string password = Console.ReadLine();

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty!");
                return;
            }

            string hash = ComputeHash(password);

            Console.WriteLine($"\nOriginal Password: {password}");
            Console.WriteLine($"Hashed Password:   {hash}");
            Console.WriteLine($"Hash Length:       {hash.Length} characters");
        }

        static void VerifyPassword()
        {
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.Write("Enter hash to verify against: ");
            string hash = Console.ReadLine();

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
            {
                Console.WriteLine("Both password and hash are required!");
                return;
            }

            string computedHash = ComputeHash(password);
            bool matches = string.Equals(computedHash, hash, StringComparison.OrdinalIgnoreCase);

            Console.WriteLine($"\nPassword:       {password}");
            Console.WriteLine($"Expected Hash:  {hash}");
            Console.WriteLine($"Computed Hash:  {computedHash}");
            Console.WriteLine($"Match:          {(matches ? "YES ✓" : "NO ✗")}");
        }

        static void GenerateSQLStatements()
        {
            Console.WriteLine("\nGenerating SQL UPDATE statements...\n");

            string[] users = { "Sayeed", "Hasina", "Atika", "Babu" };
            string[] passwords = { "sayeed123", "hasina123", "atika123", "babu123" };

            Console.WriteLine("-- SQL UPDATE Statements to Hash Passwords");
            Console.WriteLine("-- Copy and run these in your SQL Server Management Studio\n");

            for (int i = 0; i < users.Length; i++)
            {
                string hash = ComputeHash(passwords[i]);
                Console.WriteLine($"UPDATE Users SET Upassword = '{hash}' WHERE UserName = '{users[i]}';");
                Console.WriteLine($"-- Password for {users[i]}: {passwords[i]}\n");
            }

            Console.WriteLine("\n-- Verify all passwords are hashed:");
            Console.WriteLine("SELECT UserName, LEN(Upassword) AS HashLength, ");
            Console.WriteLine("       CASE WHEN LEN(Upassword) = 64 THEN 'Hashed' ELSE 'Not Hashed' END AS Status");
            Console.WriteLine("FROM Users;");
        }

        static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}