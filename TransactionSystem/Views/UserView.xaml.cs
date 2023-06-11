using System;
using System.Windows;
using System.Windows.Controls;

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

        private void SignOutClick(object sender,System.Windows.RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            MainView mainView = Window.GetWindow(this) as MainView;
            mainView.ActionView.Content = loginView;
        }

        private void SendClick(object sender, System.Windows.RoutedEventArgs e)
        {
            UserSendView userSendView = new UserSendView(UserId);
            ActionUser.Content = userSendView;
        }

        private void TransClick(object sender, System.Windows.RoutedEventArgs e)
        {
            UserTransView userTransView = new UserTransView(UserId);
            ActionUser.Content = userTransView;
        }

        private void BalanceClick(object sender, System.Windows.RoutedEventArgs e)
        {
            UserBalanceView userBalanceView = new UserBalanceView(UserId);
            ActionUser.Content= userBalanceView;
        }

        private void BillClick(object sender, System.Windows.RoutedEventArgs e)
        {
            UserBillView userBillView = new UserBillView(UserId);
            ActionUser.Content = userBillView;
        }
    }
}
