using WinFormsApp;
using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing.Text;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (ValidateLogin(username, password))
            {
                new Form2().Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("The username or password is incorrect, try again.");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }

        }

        private bool ValidateLogin(string username, string password)
        {
            // Get the connection string from App.config
            string connectionString = ConfigurationManager.ConnectionStrings["DMA-CSD-V23_10461224"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Query to check if the username and password exist
                    string query = "SELECT COUNT(1) FROM Employee WHERE FirstName = @firstName AND Password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@firstName", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count == 1; // If count is 1, login is valid
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
