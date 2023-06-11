using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionSystem.ViewModels
{
    internal class UserBalanceViewModel
    {
        public decimal Balance(string connectionString, int senderAccountId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Balance FROM Users WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", senderAccountId);

                    decimal balance = (decimal)command.ExecuteScalar();
                    return balance;
                }
            }
        }
    }
}
