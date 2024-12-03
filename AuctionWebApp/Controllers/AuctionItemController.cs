using AuctionWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AuctionWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionItemController : ControllerBase
    {
        private readonly string _connectionString;

        public AuctionItemController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/AuctionItem
        [HttpGet]
        public IActionResult GetAllItems()
        {
            List<AuctionItem> items = new List<AuctionItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AuctionItem", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(new AuctionItem
                    {
                        ItemID = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        ReleaseDate = reader.GetDateTime(2),
                        Author = reader.GetString(3),
                        Genre = reader.GetString(4),
                        Description = reader.GetString(5),
                        ItemType = reader.GetString(6)
                    });
                }
            }

            return Ok(items);
        }

        // GET: api/AuctionItem/{id}
        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            AuctionItem item = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AuctionItem WHERE itemID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    item = new AuctionItem
                    {
                        ItemID = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        ReleaseDate = reader.GetDateTime(2),
                        Author = reader.GetString(3),
                        Genre = reader.GetString(4),
                        Description = reader.GetString(5),
                        ItemType = reader.GetString(6)
                    };
                }
            }

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/AuctionItem
        [HttpPost]
        public IActionResult CreateItem([FromBody] AuctionItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AuctionItem (title, releaseDate, author, genre, description, itemType) VALUES (@title, @releaseDate, @author, @genre, @description, @itemType)", connection);
                command.Parameters.AddWithValue("@title", item.Title);
                command.Parameters.AddWithValue("@releaseDate", item.ReleaseDate);
                command.Parameters.AddWithValue("@author", item.Author);
                command.Parameters.AddWithValue("@genre", item.Genre);
                command.Parameters.AddWithValue("@description", item.Description);
                command.Parameters.AddWithValue("@itemType", item.ItemType);

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetItemById), new { id = item.ItemID }, item);
        }

        // PUT: api/AuctionItem/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] AuctionItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE AuctionItem SET title = @title, releaseDate = @releaseDate, author = @author, genre = @genre, description = @description, itemType = @itemType WHERE itemID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@title", item.Title);
                command.Parameters.AddWithValue("@releaseDate", item.ReleaseDate);
                command.Parameters.AddWithValue("@author", item.Author);
                command.Parameters.AddWithValue("@genre", item.Genre);
                command.Parameters.AddWithValue("@description", item.Description);
                command.Parameters.AddWithValue("@itemType", item.ItemType);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        // DELETE: api/AuctionItem/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM AuctionItem WHERE itemID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
