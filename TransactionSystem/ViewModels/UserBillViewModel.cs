using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;

namespace TransactionSystem.ViewModels
{
    internal class UserBillViewModel
    {
        private string connectionString;

        public UserBillViewModel(string connectionString)
        {
            this.connectionString = connectionString;
            Bills = new List<Bill>();
        }

        public List<Bill> Bills { get; private set; }

        public void LoadBillsByUserId(int userId)
        {
            Bills.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT BillId, Name, Amount, Status FROM Bills WHERE UserId = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int billId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            decimal amount = reader.GetDecimal(2);
                            bool status = reader.GetBoolean(3);

                            Bill bill = new Bill(billId, name, amount, status);
                            Bills.Add(bill);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving bills: " + ex.Message);
            }
        }

        public bool PayBill(int billId, decimal fee,int userId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Enable MARS in the connection string
                        connection.ConnectionString += ";MultipleActiveResultSets=True";

                        // Get bill information
                        string billQuery = "SELECT Name, Amount, UserId FROM Bills WHERE BillId = @BillId";
                        SqlCommand billCommand = new SqlCommand(billQuery, connection);
                        billCommand.Parameters.AddWithValue("@BillId", billId);

                        connection.Open();

                        using (SqlDataReader billReader = billCommand.ExecuteReader())
                        {
                            if (billReader.Read())
                            {
                                string name = billReader.GetString(0);
                                decimal amount = billReader.GetDecimal(1);

                                // Get user balance
                                string balanceQuery = "SELECT Balance FROM Users WHERE Id = @UserId";
                                SqlCommand balanceCommand = new SqlCommand(balanceQuery, connection);
                                balanceCommand.Parameters.AddWithValue("@UserId", userId);

                                using (SqlDataReader balanceReader = balanceCommand.ExecuteReader())
                                {
                                    if (balanceReader.Read())
                                    {
                                        decimal balance = balanceReader.GetDecimal(0);

                                        // Check if the user has sufficient balance
                                        if (balance >= amount + fee)
                                        {
                                            // Update user balance
                                            decimal newBalance = balance - amount - fee;
                                            string updateBalanceQuery = "UPDATE Users SET Balance = @NewBalance WHERE Id = @UserId";
                                            SqlCommand updateBalanceCommand = new SqlCommand(updateBalanceQuery, connection);
                                            updateBalanceCommand.Parameters.AddWithValue("@NewBalance", newBalance);
                                            updateBalanceCommand.Parameters.AddWithValue("@UserId", userId);
                                            updateBalanceCommand.ExecuteNonQuery();

                                            // Update bill status
                                            string updateStatusQuery = "UPDATE Bills SET Status = 1 WHERE BillId = @BillId";
                                            SqlCommand updateStatusCommand = new SqlCommand(updateStatusQuery, connection);
                                            updateStatusCommand.Parameters.AddWithValue("@BillId", billId);
                                            updateStatusCommand.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            throw new Exception("Insufficient balance to pay the bill.");
                                        }
                                    }
                                }
                            }
                        }
                    }

                    scope.Complete(); // Commit the transaction
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating the bill status: " + ex.Message);
            }
        }
    }
}
