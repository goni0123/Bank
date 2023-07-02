using System;
using System.Windows;
using System.Windows.Controls;
using TransactionSystem.ViewModels;

namespace TransactionSystem.Views
{
    public partial class MainView : Window
    {
        public int AuthenticatedUserId { get;  set; }
        public string AuthenticatedUserRole { get;  set; }

        public MainView()
        {
            InitializeComponent();
            ShowLoginView();
        }

        public void ShowLoginView()
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

        private void ShowUserView()
        {
            UserView userView = new UserView(AuthenticatedUserId);
            userView.LogoutRequested += UserView_LogoutRequested;
            ActionView.Content = userView;
        }

        private void UserView_LogoutRequested(object sender, EventArgs e)
        {
            AuthenticatedUserId = 0;
            AuthenticatedUserRole = null;
            ShowLoginView();
        }
        private void ShowAdminView()
        {
            AdminView adminView = new AdminView();
            adminView.LogoutRequested += AdminView_LogoutRequested;
            ActionView.Content = adminView;
        }

        private void AdminView_LogoutRequested(object sender, EventArgs e)
        {
            // Perform logout actions
            AuthenticatedUserId = 0;
            AuthenticatedUserRole = null;
            ShowLoginView();
        }

    }
}
