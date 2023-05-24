using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Procedures
{
    internal class InsertTransaction
    {
        public void InsertTrans(string connectionString,int senderAccountId, int receiverAccountId, decimal amount, decimal fee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("InsertTransaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Sender", senderAccountId);
                    command.Parameters.AddWithValue("@Receiver", receiverAccountId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Fee", fee);
                    command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
