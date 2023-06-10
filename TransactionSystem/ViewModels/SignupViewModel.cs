﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TransactionSystem.ViewModels
{
    internal class SignupViewModel
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

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

        public void CreateUser(string username, string password, string role)
        {
            byte[] salt = GenerateSalt();
            byte[] hashedPassword = HashPassword(password, salt);

            string query = "INSERT INTO Users (Username, Password, Salt, Role) VALUES (@Username, @Password, @Salt, @Role)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", Convert.ToBase64String(hashedPassword));
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@Role", role);
                    command.ExecuteNonQuery();
                }
            }
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
