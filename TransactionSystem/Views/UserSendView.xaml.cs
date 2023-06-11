using System;
using System.Configuration;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TransactionSystem.ViewModels;
using static TransactionSystem.ViewModels.UserSendViewModel;

namespace TransactionSystem.Views
{
    /// <summary>
    /// Interaction logic for UserSendView.xaml
    /// </summary>
    public partial class UserSendView : UserControl
    {
        public int userId;
        private string connectionString;
        public UserSendView(int userId)
        {   
            this.userId = userId;
            UserSendViewModel userSendViewModel = new UserSendViewModel();
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            userSendViewModel.LoadDataGrid(connectionString, Trans,userId);
            userSendViewModel.LoadComboBox(connectionString, ToUser, "Username", "Users");
        }

        private void SendMony_Click(object sender, RoutedEventArgs e)
        {
            UserSendViewModel userSendViewModel = new UserSendViewModel();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    decimal feeprice = 30;
                    string ReciverName = ToUser.Text;
                    int receiverAccountId = userSendViewModel.GetAccountIdByName(connectionString, ReciverName);
                    string sendamount = Amount.Text;
                    decimal amount = decimal.Parse(sendamount);
                    decimal discount = userSendViewModel.GetDiscount(connectionString, userId);
                    decimal fee = feeprice - discount;
                    userSendViewModel.ExecuteTransaction(connectionString, userId, receiverAccountId, amount, feeprice);
                    userSendViewModel.InsertTrans(connectionString, userId, receiverAccountId, amount, fee);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                userSendViewModel.LoadDataGrid(connectionString, Trans,userId);
            }
        }
    }
}
