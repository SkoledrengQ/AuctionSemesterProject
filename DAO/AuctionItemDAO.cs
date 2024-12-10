using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.DataAccess
{
    public class AuctionItemDAO
    {
        private readonly string _connectionString;

        public AuctionItemDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<AuctionItem>> GetAllAuctionItemsAsync()
        {
            List<AuctionItem> items = new List<AuctionItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM AuctionItem", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
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

            return items;
        }

        public async Task<AuctionItem?> GetAuctionItemByIdAsync(int id)
        {
            AuctionItem? item = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM AuctionItem WHERE itemID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
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

            return item;
        }

        public async Task CreateAuctionItemAsync(AuctionItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO AuctionItem (title, releaseDate, author, genre, description, itemType) " +
                    "VALUES (@title, @releaseDate, @author, @genre, @description, @itemType)",
                    connection
                );

                command.Parameters.AddWithValue("@title", item.Title);
                command.Parameters.AddWithValue("@releaseDate", item.ReleaseDate);
                command.Parameters.AddWithValue("@author", item.Author);
                command.Parameters.AddWithValue("@genre", item.Genre);
                command.Parameters.AddWithValue("@description", item.Description);
                command.Parameters.AddWithValue("@itemType", item.ItemType);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAuctionItemAsync(int id, AuctionItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "UPDATE AuctionItem SET title = @title, releaseDate = @releaseDate, author = @author, genre = @genre, " +
                    "description = @description, itemType = @itemType WHERE itemID = @id",
                    connection
                );

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@title", item.Title);
                command.Parameters.AddWithValue("@releaseDate", item.ReleaseDate);
                command.Parameters.AddWithValue("@author", item.Author);
                command.Parameters.AddWithValue("@genre", item.Genre);
                command.Parameters.AddWithValue("@description", item.Description);
                command.Parameters.AddWithValue("@itemType", item.ItemType);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAuctionItemAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("DELETE FROM AuctionItem WHERE itemID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
