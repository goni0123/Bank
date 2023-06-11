using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TransactionSystem.Views
{
    public partial class UserView : UserControl
    {
        public int UserId { get; set; }

        public UserView(int userId)
        {
            InitializeComponent();
            UserId = userId;
        }
        public event EventHandler LogoutRequested;

        private void SignOutClick(object sender, RoutedEventArgs e)
        {
            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }
        private void SendClick(object sender, RoutedEventArgs e)
        {
            UserSendView userSendView = new UserSendView(UserId);
            ActionUser.Content = userSendView;
        }

        private void BalanceClick(object sender, RoutedEventArgs e)
        {
            UserBalanceView userBalanceView = new UserBalanceView(UserId);
            ActionUser.Content = userBalanceView;
        }

        private void BillClick(object sender, RoutedEventArgs e)
        {
            UserBillView userBillView = new UserBillView(UserId);
            ActionUser.Content = userBillView;
        }
    }
}
