using System;
using System.Windows;
using System.Windows.Controls;
using TransactionSystem.ViewModels;
using TransactionSystem.Views;

namespace TransactionSystem.Views
{
    public partial class MainView : Window
    {
        public int AuthenticatedUserId { get; private set; }
        public string AuthenticatedUserRole { get; private set; }

        public MainView()
        {
            InitializeComponent();
            LoginView loginView = new LoginView();
            loginView.Authenticated += LoginView_Authenticated;
            ActionView.Content = loginView;
        }

        private void LoginView_Authenticated(int userId, string role)
        {
            AuthenticatedUserId = userId;
            AuthenticatedUserRole = role;

            if (role == "admin")
            {
                AdminView adminView = new AdminView();
                ActionView.Content = adminView;
            }
            else if (role == "user")
            {
                UserView userView = new UserView();
                userView.UserId = userId;
                ActionView.Content = userView;
            }
        }
    }
}
