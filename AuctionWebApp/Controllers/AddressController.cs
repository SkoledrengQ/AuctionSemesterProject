using AuctionWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AuctionWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly string _connectionString;

        public AddressController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/Address
        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            List<Address> addresses = new List<Address>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Address", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
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

            return Ok(addresses);
        }

        // GET: api/Address/{id}
        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            Address address = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Address WHERE addressID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
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

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        // POST: api/Address
        [HttpPost]
        public IActionResult CreateAddress([FromBody] Address address)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Address (streetName, city, zipCode) VALUES (@streetName, @city, @zipCode)", connection);
                command.Parameters.AddWithValue("@streetName", address.StreetName);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@zipCode", address.ZipCode);

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetAddressById), new { id = address.AddressID }, address);
        }

        // PUT: api/Address/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] Address address)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Address SET streetName = @streetName, city = @city, zipCode = @zipCode WHERE addressID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@streetName", address.StreetName);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@zipCode", address.ZipCode);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        // DELETE: api/Address/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Address WHERE addressID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
