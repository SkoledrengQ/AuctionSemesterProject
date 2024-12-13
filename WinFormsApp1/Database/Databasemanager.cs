using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Database
{
    public class Databasemanager
    {
        private string _connectionString;

        public Databasemanager()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DMA-CSD-V23_10461224"].ConnectionString;
        }

        // Method to retriever user details for validation
        public DataRow GetUserByFirstName(string firstName)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT FirstName, PasswordHash, Salt FROM Employee WHERE FirstName = @firstName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);

                    DataTable userTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(userTable);

                    if (userTable.Rows.Count == 1)
                    {
                        return userTable.Rows[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}