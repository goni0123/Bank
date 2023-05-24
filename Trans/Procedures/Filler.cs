using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Trans.Procedures
{
    internal class Filler
    {
        public void LoadDataGrid(string connectionString,DataGrid dataGrid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Transactions ORDER BY TransactionId DESC", con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable("Nalog");
                        sda.Fill(dt);
                        dataGrid.ItemsSource = dt.DefaultView;
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while loading the DataGrid:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadComboBox(string connectionString, ComboBox comboBox, string columnName, string tableName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand($"SELECT {columnName} FROM {tableName}", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader[columnName].ToString());
                        }
                        reader.Close();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while loading the ComboBox:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
