using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TransactionSystem.ViewModels;

namespace TransactionSystem.Views
{
    public partial class AdminUserView : UserControl
    {
        private string connectionString;
        public AdminUserView()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            InitializeComponent();
            FillUsersDataGrid();
        }
        private void FillUsersDataGrid()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id, Username, Balance, Transaction_count, Discount, Role FROM Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        UsersD.ItemsSource = dataTable.DefaultView;
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            SignupViewModel signupView = new SignupViewModel();
            string username = User.Text;
            string password = Password.Text;
            string role = Role.Text;

            bool isUserCreated = signupView.CreateUser(username, password, role);

            if (isUserCreated)
            {
                MessageBox.Show("Username is created");
            }
            else
            {
                MessageBox.Show("Username is already taken");
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersD.SelectedItem != null && UsersD.SelectedItem is DataRowView selectedUserRow)
            {
                int userId = Convert.ToInt32(selectedUserRow["Id"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "DELETE FROM Users WHERE Id = @UserId";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserId", userId);
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                    FillUsersDataGrid();
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }

                            connection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PasswordChange_Click(object sender, RoutedEventArgs e)
        {
            string username = User.Text;
            string newPassword = Password.Text;

            SignupViewModel signupViewModel = new SignupViewModel();

            bool userExists = signupViewModel.CheckUserExists(username);
            if (!userExists)
            {
                MessageBox.Show("User does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool passwordChanged = UpdatePassword(username, newPassword);
            if (passwordChanged)
            {
                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                FillUsersDataGrid();
            }
            else
            {
                MessageBox.Show("Failed to change password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool UpdatePassword(string username, string newPassword)
        {
            SignupViewModel vm = new SignupViewModel();
            byte[] salt = vm.GenerateSalt();
            byte[] hashedPassword = vm.HashPassword(newPassword, salt);

            string query = "UPDATE Users SET Password = @Password, Salt = @Salt WHERE Username = @Username";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Password", Convert.ToBase64String(hashedPassword));
                        command.Parameters.AddWithValue("@Salt", salt);
                        command.Parameters.AddWithValue("@Username", username);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
