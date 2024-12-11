using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;  // Add this for async/await support

namespace AuctionSemesterProject.DataAccess
{
    public class BidDAO
    {
        private readonly string _connectionString;

        public BidDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Bid>> GetAllBidsAsync()
        {
            List<Bid> bids = new List<Bid>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();  // Async open connection
                SqlCommand command = new SqlCommand("SELECT * FROM Bid", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();  // Async execute

                while (await reader.ReadAsync())  // Async read
                {
                    bids.Add(new Bid
                    {
                        Amount = reader.GetDecimal(0),
                        MemberID_FK = reader.GetInt32(1),
                        AuctionID_FK = reader.GetInt32(2)
                    });
                }
            }

            return bids;
        }

        public async Task<Bid?> GetBidByIdAsync(int id)
        {
            Bid? bid = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();  // Async open connection
                SqlCommand command = new SqlCommand("SELECT * FROM Bid WHERE auctionID_FK = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();  // Async execute

                if (await reader.ReadAsync())  // Async read
                {
                    bid = new Bid
                    {
                        Amount = reader.GetDecimal(0),
                        MemberID_FK = reader.GetInt32(1),
                        AuctionID_FK = reader.GetInt32(2)
                    };
                }
            }

            return bid;
        }

        public async Task CreateBidAsync(decimal newBid, int auctionID, int memberID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();  // Async open connection
                SqlCommand command = new SqlCommand("PlaceBid", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@newBid", newBid);
                command.Parameters.AddWithValue("@auctionID", auctionID);
                command.Parameters.AddWithValue("@memberID", memberID);

                await command.ExecuteNonQueryAsync();  // Async execute
            }
        }

        public async Task UpdateBidAsync(int id, Bid bid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();  // Async open connection
                SqlCommand command = new SqlCommand("UPDATE Bid SET amount = @amount, memberID_FK = @memberID_FK WHERE auctionID_FK = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@amount", bid.Amount);
                command.Parameters.AddWithValue("@memberID_FK", bid.MemberID_FK);

                await command.ExecuteNonQueryAsync();  // Async execute
            }
        }

        public async Task DeleteBidAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();  // Async open connection
                SqlCommand command = new SqlCommand("DELETE FROM Bid WHERE auctionID_FK = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();  // Async execute
            }
        }
    }
}
