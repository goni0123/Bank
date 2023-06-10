using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace TransactionSystem.ViewModels
{
    internal class LoginViewModel
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public (string role, int userId) AuthenticateUser(string username, string password)
        {
            string query = "SELECT Id, Password, Salt, Role FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string encryptedPassword = reader.GetString(1);
                            byte[] salt = (byte[])reader.GetValue(2);
                            string role = reader.GetString(3);

                            if (VerifyPassword(password, encryptedPassword, salt))
                            {
                                return (role, userId);
                            }
                        }
                    }
                }
            }

            return (null, 0);
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPasswordBytes, passwordBytes.Length, salt.Length);

                return sha256.ComputeHash(saltedPasswordBytes);
            }
        }

        private bool VerifyPassword(string password, string encryptedPassword, byte[] salt)
        {
            byte[] hashedPasswordBytes = HashPassword(password, salt);
            string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);

            return string.Equals(hashedPassword, encryptedPassword);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
