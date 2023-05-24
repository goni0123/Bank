using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Procedures
{
    internal class SaveCommits
    {
        public bool HasSufficientFunds(string connectionString, int senderAccountId, decimal amount, decimal fee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Balance FROM Accounts WHERE AccountId = @AccountId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountId", senderAccountId);

                    decimal balance = (decimal)command.ExecuteScalar();

                    return balance >= (amount + fee);
                }
            }
        }

        public void ExecuteTransaction(string connectionString, int senderAccountId, int receiverAccountId, decimal amount, decimal fee)
        {
            // Check if the sender has sufficient funds
            if (!HasSufficientFunds(connectionString, senderAccountId, amount, fee))
            {
                throw new Exception("Insufficient funds in the sender's account.");
            }

            decimal discount = GetDiscount(connectionString, senderAccountId);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Deduct the amount from the sender account
                        DeductAmountFromAccount(connection, transaction, senderAccountId, amount, fee, discount);

                        // Add the amount to the receiver account
                        AddAmountToAccount(connection, transaction, receiverAccountId, amount);

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // An error occurred, rollback the transaction
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        private void DeductAmountFromAccount(SqlConnection connection, SqlTransaction transaction, int accountId, decimal amount,decimal fee, decimal discount)
        {
            if (discount == 0)
            {
                string updateQuery = "UPDATE Accounts SET Balance = Balance - (@Amount+@Fee) WHERE AccountId = @AccountId";

                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                {
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@AccountId", accountId);
                    command.Parameters.AddWithValue("@Fee", fee);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                string updateQuery = "UPDATE Accounts SET Balance = Balance - (@Amount + (@Fee-(@Fee * @Discount))) WHERE AccountId = @AccountId";

                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                {
                    decimal discountPercentage = discount / 100;
                    command.Parameters.AddWithValue("@Fee", fee);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Discount", discountPercentage);
                    command.Parameters.AddWithValue("@AccountId", accountId);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void AddAmountToAccount(SqlConnection connection, SqlTransaction transaction, int accountId, decimal amount)
        {
            string updateQuery = "UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountId = @AccountId";

            using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@AccountId", accountId);

                command.ExecuteNonQuery();
            }
        }
        public decimal GetDiscount(string connectionString, int accountId)
        {
            decimal discount = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT Discount FROM Accounts WHERE AccountId = @AccountId", con))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        decimal.TryParse(result.ToString(), out discount);
                    }
                }
                con.Close();
            }

            return discount;
        }

    }
}
