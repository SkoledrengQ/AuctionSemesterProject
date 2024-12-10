using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.DataAccess
{
    public class AddressDAO
    {
        private readonly string _connectionString;

        public AddressDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            List<Address> addresses = new List<Address>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Address", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    addresses.Add(new Address
                    {
                        AddressID = reader.GetInt32(0),
                        StreetName = reader.GetString(1),
                        City = reader.GetString(2),
                        ZipCode = reader.GetString(3)
                    });
                }
            }

            return addresses;
        }

        public async Task<Address?> GetAddressByIdAsync(int id)
        {
            Address? address = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Address WHERE addressID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    address = new Address
                    {
                        AddressID = reader.GetInt32(0),
                        StreetName = reader.GetString(1),
                        City = reader.GetString(2),
                        ZipCode = reader.GetString(3)
                    };
                }
            }

            return address;
        }

        public async Task CreateAddressAsync(Address address)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Address (streetName, city, zipCode) VALUES (@streetName, @city, @zipCode)",
                    connection
                );
                command.Parameters.AddWithValue("@streetName", address.StreetName);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@zipCode", address.ZipCode);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAddressAsync(int id, Address address)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "UPDATE Address SET streetName = @streetName, city = @city, zipCode = @zipCode WHERE addressID = @id",
                    connection
                );
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@streetName", address.StreetName);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@zipCode", address.ZipCode);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAddressAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("DELETE FROM Address WHERE addressID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
