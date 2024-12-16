using WinFormsApp.Database;
using WinFormsApp.Helpers;
using WinFormsApp1.Helpers;

namespace WinFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            UserManager userManager = new UserManager();

            // Salt and hash the passwords for existing employees
            userManager.AddSaltAndHashToExistingEmployees();

            Console.WriteLine("Passwords have been salted and hashed");

            // Launch the application at the same time
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        private static void TestDatabasePasswordVerification(UserManager userManager)
        {
            string testFirstName = "Morten";
            string testPassword = "KanLoggeInd";

            bool isVerified = userManager.VerifyUserPassword(testFirstName, testPassword);

            string message = isVerified
                ? "Password verification succeeded."
                : "Password verification failed.";

            MessageBox.Show(message, "Database password verification test");

        }
    }
}