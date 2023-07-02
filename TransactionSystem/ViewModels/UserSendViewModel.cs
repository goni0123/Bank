using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TransactionSystem.ViewModels
{
    internal class UserSendViewModel
    {
        public void LoadDataGrid(string connectionString, DataGrid dataGrid,int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT t.TransactionId, u.Username AS Sender, u2.Username AS Receiver, t.Amount, t.Fee,t.TransactionDate " +
                                   "FROM Transactions t " +
                                   "JOIN Users u ON t.Sender = u.Id " +
                                   "JOIN Users u2 ON t.Receiver = u2.Id " +
                                   "WHERE t.Sender = @UserId OR t.Receiver = @UserId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while loading the DataGrid:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void LoadComboBox(string connectionString, ComboBox comboBox, string columnName, string tableName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand($"SELECT {columnName} FROM {tableName}", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader[columnName].ToString());
                        }
                        reader.Close();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while loading the ComboBox:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public int GetAccountIdByName(string connectionString, string name)
        {
            int AccountId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id FROM Users WHERE Username = @Name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        AccountId = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }
            return AccountId;
        }
        public void InsertTrans(string connectionString, int senderAccountId, int receiverAccountId, decimal amount, decimal fee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("InsertTransaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Sender", senderAccountId);
                    command.Parameters.AddWithValue("@Receiver", receiverAccountId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Fee", fee);
                    command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool HasSufficientFunds(string connectionString, int senderAccountId, decimal amount, decimal fee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Balance FROM Users WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", senderAccountId);

                    decimal balance = (decimal)command.ExecuteScalar();

                    return balance >= (amount + fee);
                }
            }
        }

        public void ExecuteTransaction(string connectionString, int senderAccountId, int receiverAccountId, decimal amount, decimal fee)
        {
            if (!HasSufficientFunds(connectionString, senderAccountId, amount, fee))
            {
                throw new Exception("Insufficient funds in the sender's account.");
            }
            else if (senderAccountId == receiverAccountId)
            {
                throw new Exception("Funds Cannot be send to the same account.");
            }

            decimal discount = GetDiscount(connectionString, senderAccountId);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DeductAmountFromAccount(connection, transaction, senderAccountId, amount, fee, discount);
                        AddAmountToAccount(connection, transaction, receiverAccountId, amount);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        private void DeductAmountFromAccount(SqlConnection connection, SqlTransaction transaction, int accountId, decimal amount, decimal fee, decimal discount)
        {
            if (discount == 0)
            {
                string updateQuery = "UPDATE Users SET Balance = Balance - (@Amount+@Fee) WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                {
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Id", accountId);
                    command.Parameters.AddWithValue("@Fee", fee);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                string updateQuery = "UPDATE Users SET Balance = Balance - (@Amount + (@Fee-(@Fee * @Discount))) WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                {
                    decimal discountPercentage = discount / 100;
                    command.Parameters.AddWithValue("@Fee", fee);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Discount", discountPercentage);
                    command.Parameters.AddWithValue("@Id", accountId);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void AddAmountToAccount(SqlConnection connection, SqlTransaction transaction, int accountId, decimal amount)
        {
            string updateQuery = "UPDATE Users SET Balance = Balance + @Amount WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Id", accountId);

                command.ExecuteNonQuery();
            }
        }
        public decimal GetDiscount(string connectionString, int accountId)
        {
            decimal discount = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT Discount FROM Users WHERE Id = @Id", con))
                {
                    command.Parameters.AddWithValue("@Id", accountId);
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


