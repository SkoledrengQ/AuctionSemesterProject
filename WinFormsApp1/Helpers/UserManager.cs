using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;
using WinFormsApp.Database;
using WinFormsApp1.Helpers;
using Microsoft.Data.SqlClient;

namespace WinFormsApp.Helpers
{
    public class UserManager
    {
        private Databasemanager _dbManager;

        public UserManager()
        {
            _dbManager = new Databasemanager();
        }

        // Method to add salt and hash to existing employees
        public void AddSaltAndHashToExistingEmployees()
        {
            // Retrieve all employees from the database
            var employees = GetAllEmployees();

            foreach (var employee in employees)
            {
                // Generate a salt and hash the password
                string salt = PasswordUtils.GenerateSalt();
                string hashedPassword = PasswordUtils.HashPassword(employee.Password, salt);

                // Update employee record in the database
                UpdateEmployeePasswordHash(employee.FirstName, salt, hashedPassword);
            }

            Console.WriteLine("All existing employees have been updated with salted and hashed passwords");
        }

        // Helper method to retrieve all employees
        private List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_dbManager.ConnectionString))
            {
                string query = "Select firstName, Password FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        FirstName = reader["FirstName"].ToString(),
                        Password = reader["Password"].ToString()
                    });
                }
            }

            return employees;
        }

        // Helper method to update an employee's password hash and salt
        private void UpdateEmployeePasswordHash(string firstName, string salt, string hashedPassword)
        {
            using (SqlConnection connection = new SqlConnection(_dbManager.ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Employee SET Salt = @Salt, HashedPassword = @PasswordHash WHERE FirstName = @FirstName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to verify a user's password during login
        public bool VerifyUserPassword(string firstName, string password)
        {
            // Retrieve the salt and hashed password from the database
            var userRow = _dbManager.GetUserByFirstName(firstName);

            if (userRow != null)
            {
                Console.WriteLine("User not found.");
                return false;
            }

            // Retrieve the stored salt and hashed password
            string storedSalt = userRow["Salt"].ToString();
            string storedHashedPassword = userRow["HashedPassword"].ToString();

            // Hash the input password with the stored salt and compare it to the stored hashed password
            string inputHashedPassword = PasswordUtils.HashPassword(password, storedSalt);
            return inputHashedPassword == storedHashedPassword;
        }
    }
}
