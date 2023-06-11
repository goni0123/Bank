using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for UserBalanceView.xaml
    /// </summary>
    public partial class UserBalanceView : UserControl
    {
        private int userId;
        private string connectionString;

        public UserBalanceView(int userId)
        {
            this.userId = userId;
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            UserBalanceViewModel userBalanceViewModel = new UserBalanceViewModel();
            decimal balance=userBalanceViewModel.Balance(connectionString, userId);
            Balance.Text = balance.ToString();
        }

    }
}
