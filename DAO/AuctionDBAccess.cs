using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.DataAccess.Interfaces;

namespace AuctionSemesterProject.DataAccess
{
    public class AuctionDAO : IAuctionAccess
    {
        private readonly string _connectionString;

        public AuctionDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Auction>> GetAllAuctionsAsync()
        {
            var auctions = new List<Auction>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    SELECT 
                        A.AuctionID, A.StartPrice, A.MinBid, A.EndingBid, A.CurrentHighestBid,
                        A.BuyNowPrice, A.NoOfBids, A.TimeExtension, A.LastUpdated,
                        E.EmployeeID, E.FirstName, E.LastName,
                        I.ItemID, I.Title, I.Author
                    FROM Auction A
                    LEFT JOIN Employee E ON A.EmployeeID_FK = E.EmployeeID
                    LEFT JOIN AuctionItem I ON A.ItemID_FK = I.ItemID;";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        auctions.Add(new Auction
                        {
                            AuctionID = reader.GetInt32(0),
                            StartPrice = reader.GetDecimal(1),
                            MinBid = reader.GetDecimal(2),
                            EndingBid = reader.IsDBNull(3) ? null : reader.GetDecimal(3),
                            CurrentHighestBid = reader.IsDBNull(4) ? null : reader.GetDecimal(4),
                            BuyNowPrice = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                            NoOfBids = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                            TimeExtension = reader.IsDBNull(7) ? null : reader.GetString(7),
                            LastUpdated = reader.GetDateTime(8),
                            Employee = new Employee
                            {
                                EmployeeID = reader.GetInt32(9),
                                FirstName = reader.GetString(10),
                                LastName = reader.GetString(11),
                            },
                            AuctionItem = new AuctionItem
                            {
                                ItemID = reader.GetInt32(12),
                                Title = reader.GetString(13),
                                Author = reader.GetString(14),
                            }
                        });
                    }
                }
            }

            return auctions;
        }

        public async Task<Auction?> GetAuctionByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    SELECT 
                        A.AuctionID, A.StartPrice, A.MinBid, A.EndingBid, A.CurrentHighestBid,
                        A.BuyNowPrice, A.NoOfBids, A.TimeExtension, A.LastUpdated,
                        E.EmployeeID, E.FirstName, E.LastName,
                        I.ItemID, I.Title, I.Author
                    FROM Auction A
                    LEFT JOIN Employee E ON A.EmployeeID_FK = E.EmployeeID
                    LEFT JOIN AuctionItem I ON A.ItemID_FK = I.ItemID
                    WHERE A.AuctionID = @AuctionID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuctionID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Auction
                            {
                                AuctionID = reader.GetInt32(0),
                                StartPrice = reader.GetDecimal(1),
                                MinBid = reader.GetDecimal(2),
                                EndingBid = reader.IsDBNull(3) ? null : reader.GetDecimal(3),
                                CurrentHighestBid = reader.IsDBNull(4) ? null : reader.GetDecimal(4),
                                BuyNowPrice = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                                NoOfBids = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                                TimeExtension = reader.IsDBNull(7) ? null : reader.GetString(7),
                                LastUpdated = reader.GetDateTime(8),
                                Employee = new Employee
                                {
                                    EmployeeID = reader.GetInt32(9),
                                    FirstName = reader.GetString(10),
                                    LastName = reader.GetString(11),
                                },
                                AuctionItem = new AuctionItem
                                {
                                    ItemID = reader.GetInt32(12),
                                    Title = reader.GetString(13),
                                    Author = reader.GetString(14),
                                }
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task CreateAuctionAsync(Auction auction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    INSERT INTO Auction (StartPrice, MinBid, EndingBid, CurrentHighestBid, BuyNowPrice, NoOfBids, TimeExtension, EmployeeID_FK, ItemID_FK, LastUpdated)
                    VALUES (@StartPrice, @MinBid, @EndingBid, @CurrentHighestBid, @BuyNowPrice, @NoOfBids, @TimeExtension, @EmployeeID_FK, @ItemID_FK, @LastUpdated);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartPrice", auction.StartPrice);
                    command.Parameters.AddWithValue("@MinBid", auction.MinBid);
                    command.Parameters.AddWithValue("@EndingBid", auction.EndingBid ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CurrentHighestBid", auction.CurrentHighestBid ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BuyNowPrice", auction.BuyNowPrice ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NoOfBids", auction.NoOfBids);
                    command.Parameters.AddWithValue("@TimeExtension", auction.TimeExtension ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@EmployeeID_FK", auction.Employee.EmployeeID);
                    command.Parameters.AddWithValue("@ItemID_FK", auction.AuctionItem.ItemID);
                    command.Parameters.AddWithValue("@LastUpdated", auction.LastUpdated);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> UpdateAuctionWithConcurrencyCheckAsync(Auction auction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    UPDATE Auction
                    SET CurrentHighestBid = @CurrentHighestBid, LastUpdated = @LastUpdated
                    WHERE AuctionID = @AuctionID AND LastUpdated = @OriginalLastUpdated;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CurrentHighestBid", auction.CurrentHighestBid ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastUpdated", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@AuctionID", auction.AuctionID);
                    command.Parameters.AddWithValue("@OriginalLastUpdated", auction.LastUpdated);

                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> DeleteAuctionAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Auction WHERE AuctionID = @AuctionID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuctionID", id);

                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
