using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using Trans.Procedures;

namespace Trans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            Filler filler = new Filler();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            filler.LoadDataGrid(connectionString, Transactions);
            filler.LoadComboBox(connectionString, senderName, "AccountName", "Accounts");
            filler.LoadComboBox(connectionString, reciverName, "AccountName", "Accounts");
        }

        private void SendMoney_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    InsertTransaction insertTransaction = new InsertTransaction();
                    FindID findId = new FindID();
                    SaveCommits saveCommits = new SaveCommits();

                    decimal feeprice = 30;
                    string SenderName = senderName.Text;
                    string ReciverName = reciverName.Text;
                    int senderAccountId = findId.GetAccountIdByName(connectionString, SenderName);
                    int receiverAccountId = findId.GetAccountIdByName(connectionString, ReciverName);
                    string sendamount = sendAmount.Text;
                    decimal amount = decimal.Parse(sendamount);
                    decimal discount = saveCommits.GetDiscount(connectionString, senderAccountId);
                    decimal fee = feeprice - discount;

                    saveCommits.ExecuteTransaction(connectionString, senderAccountId, receiverAccountId, amount, feeprice);
                    insertTransaction.InsertTrans(connectionString, senderAccountId, receiverAccountId, amount, fee);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Filler filler = new Filler();
            filler.LoadDataGrid(connectionString, Transactions);

        }
    }
}
