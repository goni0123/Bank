using System.Windows;
using System.Windows.Controls;
using TransactionSystem.ViewModels;

namespace TransactionSystem.Views
{
    public partial class AdminBalanceView : UserControl
    {
        private AdminBalanceViewModel viewModel;

        public AdminBalanceView()
        {
            InitializeComponent();
            viewModel = new AdminBalanceViewModel();
            DataContext = viewModel;
            viewModel.LoadUserBalances(Trans);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string username = User.Text;
            string amountText = Amount.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(amountText))
            {
                MessageBox.Show("Please enter a username and amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Invalid amount. Please enter a valid decimal value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            viewModel.UpdateUserBalance(username, amount);
            viewModel.LoadUserBalances(Trans);
        }
    }
}
