using WinFormsApp;

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
            if (txtUsername.Text == "Employee" && txtPassword.Text == "Password")
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
    }
}
