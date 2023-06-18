using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using TransactionSystem.Models;
using TransactionSystem.ViewModels;

namespace TransactionSystem.Views
{
    public partial class UserBillView : UserControl
    {
        private int userId;
        private string connectionString;
        private UserBillViewModel userBillViewModel;

        public UserBillView(int userId)
        {
            this.userId = userId;
            connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            this.userBillViewModel = new UserBillViewModel(connectionString);

            InitializeComponent();
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            try
            {
                userBillViewModel.LoadBillsByUserId(userId);
                Trans.ItemsSource = userBillViewModel.Bills;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while loading the DataGrid:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PayBill_Click(object sender, RoutedEventArgs e)
        {
            if (Trans.SelectedItem is Bill selectedBill)
            {
                if (selectedBill.Status)
                {
                    MessageBox.Show("The bill is already paid.", "Payment Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        decimal fee = 10.0m; // Set the fee here
                        bool paymentSuccess = userBillViewModel.PayBill(selectedBill.BillId, fee,userId);

                        if (paymentSuccess)
                        {
                            selectedBill.Status = true;
                            MessageBox.Show("Bill paid successfully.", "Payment Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            LoadDataGrid();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the bill status.", "Payment Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred while updating the bill status:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a bill to pay.", "Payment Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
