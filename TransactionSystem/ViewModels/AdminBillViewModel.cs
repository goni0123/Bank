using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace TransactionSystem.ViewModels
{
    public class AdminBillViewModel
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public  DataTable GetBillsByUsername(string username)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT B.BillId, B.Name, B.Amount, B.Status, U.Username " +
                                   "FROM Bills AS B " +
                                   "INNER JOIN Users AS U ON B.UserId = U.Id " +
                                   "WHERE U.Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return dataTable;
        }
        public  int GetUserIdFromUsername(string username)
        {
            int userId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id FROM Users WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            userId = reader.GetInt32(0);
                        }

                        reader.Close();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return userId;
        }
    }
}
