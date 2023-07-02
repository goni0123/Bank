using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using TransactionSystem.ViewModels;

namespace TransactionSystem.Views
{
    public partial class AdminBillView : UserControl
    {
        private string connectionString;

        public AdminBillView()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            InitializeComponent();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT B.BillId, B.Name, B.Amount, B.Status, U.Username " +
                                   "FROM Bills AS B " +
                                   "INNER JOIN Users AS U ON B.UserId = U.Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable("BillsD");
                        adapter.Fill(dataTable);
                        BillsD.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddBill_Click(object sender, RoutedEventArgs e)
        {
            AdminBillViewModel adminBillView = new AdminBillViewModel();
            string name = BillName.Text;
            decimal amount = Convert.ToDecimal(Amount.Text);
            bool status = false;
            string user = User.Text;
            int userId =adminBillView.GetUserIdFromUsername(user); 

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Bills (Name, Amount, Status, UserId) VALUES (@Name, @Amount, @Status, @UserId)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@UserId", userId);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                DataTable billsTable = adminBillView.GetBillsByUsername(user);
                BillsD.ItemsSource = billsTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateBill_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)BillsD.SelectedItem;
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a bill to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int billId = Convert.ToInt32(selectedRow["BillId"]);
            AdminBillViewModel adminBillViewModel = new AdminBillViewModel();
            string name = BillName.Text;
            decimal amount = Convert.ToDecimal(Amount.Text);
            string user = User.Text;
            int userId = adminBillViewModel.GetUserIdFromUsername(user);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Bills SET Name = @Name, Amount = @Amount,UserId = @UserId WHERE BillId = @BillId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@BillId", billId);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                DataTable billsTable = adminBillViewModel.GetBillsByUsername(user);
                BillsD.ItemsSource = billsTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBill_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)BillsD.SelectedItem;
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a bill to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int billId = Convert.ToInt32(selectedRow["BillId"]);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Bills WHERE BillId = @BillId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BillId", billId);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                string user = User.Text;
                AdminBillViewModel adminBillViewModel = new AdminBillViewModel();
                DataTable billsTable = adminBillViewModel.GetBillsByUsername(user);
                BillsD.ItemsSource = billsTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            AdminBillViewModel adminBillView = new AdminBillViewModel();
            string user = User.Text; 
            DataTable billsTable = adminBillView.GetBillsByUsername(user);
            BillsD.ItemsSource = billsTable.DefaultView;

        }
    }
}