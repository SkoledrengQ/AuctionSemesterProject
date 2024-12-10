using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.DataAccess
{
    public class EmployeeDAO
    {
        private readonly string _connectionString;

        public EmployeeDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Employee", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

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
            Employee? employee = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Employee WHERE employeeID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    employee = new Employee
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNo = reader.GetString(3),
                        Email = reader.GetString(4)
                    };
                }
            }

            return employee;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Employee (firstName, lastName, phoneNo, email) VALUES (@firstName, @lastName, @phoneNo, @email)",
                    connection
                );
                command.Parameters.AddWithValue("@firstName", employee.FirstName);
                command.Parameters.AddWithValue("@lastName", employee.LastName);
                command.Parameters.AddWithValue("@phoneNo", employee.PhoneNo);
                command.Parameters.AddWithValue("@email", employee.Email);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "UPDATE Employee SET firstName = @firstName, lastName = @lastName, phoneNo = @phoneNo, email = @email WHERE employeeID = @id",
                    connection
                );
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstName", employee.FirstName);
                command.Parameters.AddWithValue("@lastName", employee.LastName);
                command.Parameters.AddWithValue("@phoneNo", employee.PhoneNo);
                command.Parameters.AddWithValue("@email", employee.Email);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("DELETE FROM Employee WHERE employeeID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
