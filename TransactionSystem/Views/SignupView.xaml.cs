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
using TransactionSystem.ViewModels;

namespace TransactionSystem.Views
{
    public partial class SignupView : UserControl
    {
        public SignupView()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignupViewModel signupView = new SignupViewModel();
            string username = UserName.Text;
            string password = Password.Password;
            string role = Role.Text;

            bool isUserCreated = signupView.CreateUser(username, password, role);

            if (isUserCreated)
            {
                MainView mainView = Window.GetWindow(this) as MainView;
                LoginView loginView = new LoginView();
                mainView.ActionView.Content = loginView;
            }
            else
            {
                MessageBox.Show("Username is already taken");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainView mainView = Window.GetWindow(this) as MainView;
            LoginView loginView = new LoginView();
            mainView.ActionView.Content = loginView;
        }
    }
}
