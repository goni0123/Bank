using System;
using System.Windows;
using System.Windows.Controls;
using TransactionSystem.ViewModels;

namespace TransactionSystem.Views
{
    public partial class LoginView : UserControl
    {
        public event Action<int, string> Authenticated;

        public LoginView()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string username = UserName.Text;
            string password = Password.Password;

            LoginViewModel loginVM = new LoginViewModel();
            (string role, int userId) = loginVM.AuthenticateUser(username, password);

            if (!string.IsNullOrEmpty(role))
            {
                Authenticated?.Invoke(userId, role);
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignupView signupView = new SignupView();
            MainView mainView = Window.GetWindow(this) as MainView;
            mainView.ActionView.Content = signupView;
        }
    }
}
