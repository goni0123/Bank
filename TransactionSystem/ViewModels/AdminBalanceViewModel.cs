using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TransactionSystem.Models;

namespace TransactionSystem.ViewModels
{
    public class AdminBalanceViewModel 
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public ObservableCollection<UserBalance> UserBalances { get; set; }
        public void LoadUserBalances( DataGrid dataGrid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Select * from Users";

                    SqlCommand command = new SqlCommand(query, connection);

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
        public void UpdateUserBalance(string username, decimal newBalance)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Balance = Balance+@NewBalance WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NewBalance", newBalance);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
