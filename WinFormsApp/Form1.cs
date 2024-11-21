using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
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
            if (txtUsername.Text == "Employee" && txtPassword.Text == "Password")
            {
                new Form2().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("The username or password is incorrect. Try again.");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }
    }
}
