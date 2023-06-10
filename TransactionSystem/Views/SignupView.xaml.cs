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
    /// <summary>
    /// Interaction logic for SignupView.xaml
    /// </summary>
    public partial class SignupView : UserControl
    {
        public SignupView()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MainView mainView = new MainView();
            SignupViewModel signupView = new SignupViewModel();
            string username = UserName.Text;
            string password = Password.Password;
            string role = Role.Text;

            signupView.CreateUser(username, password, role);

            LoginView loginView = new LoginView();
            mainView.ActionView.Content = loginView;
        }

    }
}
