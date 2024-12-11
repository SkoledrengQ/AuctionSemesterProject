using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AuctionSemesterProject.DataAccess
{
    public class AuctionDAO
    {
        private readonly string _connectionString;

        public AuctionDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Auction>> GetAllAuctionsAsync()
        {
            List<Auction> auctions = new List<Auction>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Auction", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    auctions.Add(new Auction
                    {
                        AuctionID = reader.GetInt32(0),
                        StartPrice = reader.GetDecimal(1),
                        MinBid = reader.GetDecimal(2),
                        EndingBid = reader.GetDecimal(3),
                        CurrentHighestBid = reader.GetDecimal(4),
                        BuyNowPrice = reader.GetDecimal(5),
                        NoOfBids = reader.GetInt32(6),
                        TimeExtension = reader.GetString(7),
                        EmployeeID_FK = reader.GetInt32(8),
                        ItemID_FK = reader.GetInt32(9)
                    });
                }
            }

            return auctions;
        }

        public async Task<Auction?> GetAuctionByIdAsync(int id)
        {
            Auction? auction = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Auction WHERE auctionID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    auction = new Auction
                    {
                        AuctionID = reader.GetInt32(0),
                        StartPrice = reader.GetDecimal(1),
                        MinBid = reader.GetDecimal(2),
                        EndingBid = reader.GetDecimal(3),
                        CurrentHighestBid = reader.GetDecimal(4),
                        BuyNowPrice = reader.GetDecimal(5),
                        NoOfBids = reader.GetInt32(6),
                        TimeExtension = reader.GetString(7),
                        EmployeeID_FK = reader.GetInt32(8),
                        ItemID_FK = reader.GetInt32(9)
                    };
                }
            }

            return auction;
        }

        public async Task CreateAuctionAsync(Auction auction)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Auction (startPrice, minBid, endingBid, currentHighestBid, buyNowPrice, noOfBids, timeExtension, employeeID_FK, itemID_FK) " +
                    "VALUES (@startPrice, @minBid, @endingBid, @currentHighestBid, @buyNowPrice, @noOfBids, @timeExtension, @employeeID_FK, @itemID_FK)",
                    connection
                );

                command.Parameters.AddWithValue("@startPrice", auction.StartPrice);
                command.Parameters.AddWithValue("@minBid", auction.MinBid);
                command.Parameters.AddWithValue("@endingBid", auction.EndingBid);
                command.Parameters.AddWithValue("@currentHighestBid", auction.CurrentHighestBid);
                command.Parameters.AddWithValue("@buyNowPrice", auction.BuyNowPrice);
                command.Parameters.AddWithValue("@noOfBids", auction.NoOfBids);
                command.Parameters.AddWithValue("@timeExtension", auction.TimeExtension);
                command.Parameters.AddWithValue("@employeeID_FK", auction.EmployeeID_FK);
                command.Parameters.AddWithValue("@itemID_FK", auction.ItemID_FK);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAuctionAsync(int id, Auction auction)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "UPDATE Auction SET startPrice = @startPrice, minBid = @minBid, endingBid = @endingBid, currentHighestBid = @currentHighestBid, " +
                    "buyNowPrice = @buyNowPrice, noOfBids = @noOfBids, timeExtension = @timeExtension, employeeID_FK = @employeeID_FK, itemID_FK = @itemID_FK " +
                    "WHERE auctionID = @id",
                    connection
                );

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@startPrice", auction.StartPrice);
                command.Parameters.AddWithValue("@minBid", auction.MinBid);
                command.Parameters.AddWithValue("@endingBid", auction.EndingBid);
                command.Parameters.AddWithValue("@currentHighestBid", auction.CurrentHighestBid);
                command.Parameters.AddWithValue("@buyNowPrice", auction.BuyNowPrice);
                command.Parameters.AddWithValue("@noOfBids", auction.NoOfBids);
                command.Parameters.AddWithValue("@timeExtension", auction.TimeExtension);
                command.Parameters.AddWithValue("@employeeID_FK", auction.EmployeeID_FK);
                command.Parameters.AddWithValue("@itemID_FK", auction.ItemID_FK);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAuctionAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("DELETE FROM Auction WHERE auctionID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
