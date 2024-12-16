using AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class EmployeeDAO(string connectionString) : IEmployeeAccess
    {
        private readonly string _connectionString = connectionString;

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Employee;";

                using var command = new SqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    employees.Add(new Employee
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNo = reader.GetString(3),
                        Email = reader.GetString(4)
                    });
                }
            }

            return employees;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var query = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeID", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Employee
                {
                    EmployeeID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    PhoneNo = reader.GetString(3),
                    Email = reader.GetString(4)
                };
            }

            return null;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var query = @"
                    INSERT INTO Employee (FirstName, LastName, PhoneNo, Email) 
                    VALUES (@FirstName, @LastName, @PhoneNo, @Email);";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@LastName", employee.LastName);
            command.Parameters.AddWithValue("@PhoneNo", employee.PhoneNo);
            command.Parameters.AddWithValue("@Email", employee.Email);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var query = @"
                    UPDATE Employee 
                    SET FirstName = @FirstName, LastName = @LastName, PhoneNo = @PhoneNo, Email = @Email
                    WHERE EmployeeID = @EmployeeID;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@LastName", employee.LastName);
            command.Parameters.AddWithValue("@PhoneNo", employee.PhoneNo);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var query = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeID", id);

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}
