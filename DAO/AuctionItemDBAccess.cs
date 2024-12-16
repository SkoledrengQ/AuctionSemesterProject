using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.DataAccess.Interfaces;

namespace AuctionSemesterProject.DataAccess
{
    public class AuctionItemDAO : IAuctionItemAccess
    {
        private readonly string _connectionString;

        public AuctionItemDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<AuctionItem>> GetAllAuctionItemsAsync()
        {
            var items = new List<AuctionItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM AuctionItem;";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new AuctionItem
                        {
                            ItemID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            ReleaseDate = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                            Author = reader.GetString(3),
                            Genre = reader.GetString(4),
                            Description = reader.GetString(5),
                            ItemType = reader.GetString(6)
                        });
                    }
                }
            }

            return items;
        }

        public async Task<AuctionItem?> GetAuctionItemByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM AuctionItem WHERE ItemID = @ItemID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new AuctionItem
                            {
                                ItemID = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                ReleaseDate = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                                Author = reader.GetString(3),
                                Genre = reader.GetString(4),
                                Description = reader.GetString(5),
                                ItemType = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task CreateAuctionItemAsync(AuctionItem item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    INSERT INTO AuctionItem (Title, ReleaseDate, Author, Genre, Description, ItemType)
                    VALUES (@Title, @ReleaseDate, @Author, @Genre, @Description, @ItemType);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", item.Title);
                    command.Parameters.AddWithValue("@ReleaseDate", item.ReleaseDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Author", item.Author);
                    command.Parameters.AddWithValue("@Genre", item.Genre);
                    command.Parameters.AddWithValue("@Description", item.Description);
                    command.Parameters.AddWithValue("@ItemType", item.ItemType);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAuctionItemAsync(AuctionItem item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    UPDATE AuctionItem
                    SET Title = @Title, ReleaseDate = @ReleaseDate, Author = @Author, Genre = @Genre, Description = @Description, ItemType = @ItemType
                    WHERE ItemID = @ItemID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", item.Title);
                    command.Parameters.AddWithValue("@ReleaseDate", item.ReleaseDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Author", item.Author);
                    command.Parameters.AddWithValue("@Genre", item.Genre);
                    command.Parameters.AddWithValue("@Description", item.Description);
                    command.Parameters.AddWithValue("@ItemType", item.ItemType);
                    command.Parameters.AddWithValue("@ItemID", item.ItemID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAuctionItemAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM AuctionItem WHERE ItemID = @ItemID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemID", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
