using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.DataAccess
{
    public interface IAddressAccess
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<Address?> GetAddressByIdAsync(int id);
        Task CreateAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task<bool> DeleteAddressAsync(int id); // Updated to return Task<bool>
    }

    public class AddressDAO : IAddressAccess
    {
        private readonly string _connectionString;

        public AddressDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            var addresses = new List<Address>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Address;";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
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
            }
            return addresses;
        }

        public async Task<Address?> GetAddressByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Address WHERE AddressID = @AddressID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AddressID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Address
                            {
                                AddressID = reader.GetInt32(0),
                                StreetName = reader.GetString(1),
                                City = reader.GetString(2),
                                ZipCode = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task CreateAddressAsync(Address address)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO Address (StreetName, City, ZipCode) VALUES (@StreetName, @City, @ZipCode);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StreetName", address.StreetName);
                    command.Parameters.AddWithValue("@City", address.City);
                    command.Parameters.AddWithValue("@ZipCode", address.ZipCode);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAddressAsync(Address address)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Address SET StreetName = @StreetName, City = @City, ZipCode = @ZipCode WHERE AddressID = @AddressID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StreetName", address.StreetName);
                    command.Parameters.AddWithValue("@City", address.City);
                    command.Parameters.AddWithValue("@ZipCode", address.ZipCode);
                    command.Parameters.AddWithValue("@AddressID", address.AddressID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAddressAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Address WHERE AddressID = @AddressID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AddressID", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0; // Return true if rows were deleted
                }
            }
        }
    }
}
