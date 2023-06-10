using System;
using System.Windows;
using System.Windows.Controls;
using TransactionSystem.ViewModels;

namespace TransactionSystem.Views
{
    public partial class MainView : Window
    {
        public int AuthenticatedUserId { get; private set; }
        public string AuthenticatedUserRole { get; private set; }

        public MainView()
        {
            InitializeComponent();

            // Show the LoginView initially
            ShowLoginView();
        }

        private void ShowLoginView()
        {
            LoginView loginView = new LoginView();
            loginView.Authenticated += LoginView_Authenticated;
            ActionView.Content = loginView;
        }

        private void LoginView_Authenticated(int userId, string role)
        {
            AuthenticatedUserId = userId;
            AuthenticatedUserRole = role;

            if (role == "Admin")
            {
                ShowAdminView();
            }
            else if (role == "User")
            {
                ShowUserView();
            }
            else
            {
                MessageBox.Show("Invalid role!");
            }
        }

        private void ShowAdminView()
        {
            AdminView adminView = new AdminView();
            ActionView.Content = adminView;
        }
        private void ShowUserView()
        {
            UserView userView = new UserView(AuthenticatedUserId);
            ActionView.Content = userView;
        }
    }
}
