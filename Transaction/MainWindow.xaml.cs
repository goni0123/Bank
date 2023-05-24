using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows;

namespace Transaction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string connectionString = "Data Source=.;Initial Catalog=Bank;User ID=Gori;Password=Goni_m20";
        public MainWindow()
        {
            InitializeComponent();
            string query = "SELECT AccountName FROM Accounts";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sender.Items.Add(reader["AccountName"]);
                    Reciver.Items.Add(reader["AccountName"]);
                }
            }
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string senderAccount = Sender.Text;
            string recipientAccount = Reciver.Text;
            decimal amount = decimal.Parse(Amount.Text);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Retrieve the current balance of the sender's account
                        string selectSenderBalanceQuery = "SELECT Balance FROM Accounts WHERE AccountName = @SenderAccount";
                        SqlCommand selectSenderBalanceCommand = new SqlCommand(selectSenderBalanceQuery, connection);
                        selectSenderBalanceCommand.Parameters.AddWithValue("@SenderAccount", senderAccount);
                        decimal senderBalance = (decimal)selectSenderBalanceCommand.ExecuteScalar();

                        // Verify that the sender has enough money in their account to make the transfer
                        if (senderBalance < amount)
                        {
                            throw new Exception("Insufficient funds");
                        }

                        // Update the sender's account balance
                        string updateSenderBalanceQuery = "UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountName = @SenderAccount";
                        SqlCommand updateSenderBalanceCommand = new SqlCommand(updateSenderBalanceQuery, connection);
                        updateSenderBalanceCommand.Parameters.AddWithValue("@Amount", amount);
                        updateSenderBalanceCommand.Parameters.AddWithValue("@SenderAccount", senderAccount);
                        int senderRowsUpdated = updateSenderBalanceCommand.ExecuteNonQuery();

                        // Verify that the update succeeded
                        if (senderRowsUpdated == 0)
                        {
                            throw new Exception("Update failed");
                        }

                        // Update the recipient's account balance
                        string updateRecipientBalanceQuery = "UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountName = @RecipientAccount";
                        SqlCommand updateRecipientBalanceCommand = new SqlCommand(updateRecipientBalanceQuery, connection);
                        updateRecipientBalanceCommand.Parameters.AddWithValue("@Amount", amount);
                        updateRecipientBalanceCommand.Parameters.AddWithValue("@RecipientAccount", recipientAccount);
                        int recipientRowsUpdated = updateRecipientBalanceCommand.ExecuteNonQuery();

                        // Verify that the update succeeded
                        if (recipientRowsUpdated == 0)
                        {
                            throw new Exception("Update failed");
                        }
                        string selectAccountIdQuery = "SELECT AccountId FROM Accounts WHERE AccountName = @AccountNumber";
                        SqlCommand selectAccountIdCommand = new SqlCommand(selectAccountIdQuery, connection);
                        selectAccountIdCommand.Parameters.AddWithValue("@AccountNumber", senderAccount);
                        int saccountId = (int)selectAccountIdCommand.ExecuteScalar();

                        string ReciveAccountIdQuery = "SELECT AccountID FROM Accounts WHERE AccountName = @AccountNumber";
                        SqlCommand ReciveAccountIdCommand = new SqlCommand(ReciveAccountIdQuery, connection);
                        ReciveAccountIdCommand.Parameters.AddWithValue("@AccountNumber", recipientAccount);
                        int raccountId = (int)ReciveAccountIdCommand.ExecuteScalar();


                        string insertTransactionQuery = "INSERT INTO Transactions (SenderAccountId, ReceiverAccountId, Amount, Status) VALUES (@SenderAccountNumber, @RecipientAccountNumber, @Amount, 'Completed')";
                        SqlCommand insertTransactionCommand = new SqlCommand(insertTransactionQuery, connection);
                        insertTransactionCommand.Parameters.AddWithValue("@SenderAccountNumber", saccountId);
                        insertTransactionCommand.Parameters.AddWithValue("@RecipientAccountNumber", raccountId);
                        insertTransactionCommand.Parameters.AddWithValue("@Amount", amount);
                        int transactionRowsInserted = insertTransactionCommand.ExecuteNonQuery();

                        // Verify that the insert succeeded
                        if (transactionRowsInserted == 0)
                        {
                            throw new Exception("Insert failed");
                        }
                        
                    }

                    // If we got this far, everything succeeded, so we can commit the transaction
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // If there was an exception, the transaction will automatically be rolled back
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand("SELECT SenderAccountId, ReceiverAccountId, Amount,Status FROM Transactions", con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Transactions");
                        sda.Fill(dt);
                        Trans.ItemsSource = dt.DefaultView;
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("There is no data to display\n", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
