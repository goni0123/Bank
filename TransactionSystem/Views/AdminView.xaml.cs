using System;
using System.Collections.Generic;
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

namespace TransactionSystem.Views
{
    public partial class AdminView : UserControl
    {
        public AdminView()
        {
            InitializeComponent();
        }
        private void UserClick(object sender, RoutedEventArgs e)
        {
            AdminUserView adminUserView = new AdminUserView();
            AdminAction.Content = adminUserView;
        }

        private void TransactionClick(object sender, RoutedEventArgs e)
        {
            AdminTransView transView = new AdminTransView();
            AdminAction.Content = transView;
        }

        private void BillClick(object sender, RoutedEventArgs e)
        {
            AdminBillView billView = new AdminBillView();
            AdminAction.Content = billView;
        }

        private void BalanceClick(object sender, RoutedEventArgs e)
        {
            AdminBalanceView balanceView = new AdminBalanceView();
            AdminAction.Content = balanceView;
        }

        public event EventHandler LogoutRequested;

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }

    }
}
