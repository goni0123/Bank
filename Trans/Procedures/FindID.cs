using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Procedures
{
    internal class FindID
    {
        public int GetAccountIdByName(string connectionString, string name)
        {
            int AccountId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AccountId FROM Accounts WHERE AccountName = @Name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        AccountId = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            return AccountId;
        }

    }
}
