using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.Interfaces;

namespace AuctionSemesterProject.DataAccess
{
    public class BidDAO : IBidAccess
    {
        private readonly string _connectionString;

        public BidDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Bid>> GetAllBidsAsync()
        {
            var bids = new List<Bid>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    SELECT 
                        B.Amount, B.MemberID_FK, M.FirstName, M.LastName, M.PhoneNo, M.Email,
                        B.AuctionID_FK, A.StartPrice, A.CurrentHighestBid
                    FROM Bid B
                    LEFT JOIN Member M ON B.MemberID_FK = M.MemberID
                    LEFT JOIN Auction A ON B.AuctionID_FK = A.AuctionID;";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        bids.Add(new Bid
                        {
                            Amount = reader.GetDecimal(0),
                            Member = new Member
                            {
                                MemberID = reader.GetInt32(1),
                                FirstName = reader.GetString(2),
                                LastName = reader.GetString(3),
                                PhoneNo = reader.GetString(4),
                                Email = reader.GetString(5)
                            },
                            Auction = new Auction
                            {
                                AuctionID = reader.GetInt32(6),
                                StartPrice = reader.GetDecimal(7),
                                CurrentHighestBid = reader.IsDBNull(8) ? null : reader.GetDecimal(8)
                            }
                        });
                    }
                }
            }

            return bids;
        }

        public async Task<Bid?> GetBidByIdAsync(int auctionId, int memberId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    SELECT 
                        B.Amount, B.MemberID_FK, M.FirstName, M.LastName, M.PhoneNo, M.Email,
                        B.AuctionID_FK, A.StartPrice, A.CurrentHighestBid
                    FROM Bid B
                    LEFT JOIN Member M ON B.MemberID_FK = M.MemberID
                    LEFT JOIN Auction A ON B.AuctionID_FK = A.AuctionID
                    WHERE B.AuctionID_FK = @AuctionID_FK AND B.MemberID_FK = @MemberID_FK;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuctionID_FK", auctionId);
                    command.Parameters.AddWithValue("@MemberID_FK", memberId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Bid
                            {
                                Amount = reader.GetDecimal(0),
                                Member = new Member
                                {
                                    MemberID = reader.GetInt32(1),
                                    FirstName = reader.GetString(2),
                                    LastName = reader.GetString(3),
                                    PhoneNo = reader.GetString(4),
                                    Email = reader.GetString(5)
                                },
                                Auction = new Auction
                                {
                                    AuctionID = reader.GetInt32(6),
                                    StartPrice = reader.GetDecimal(7),
                                    CurrentHighestBid = reader.IsDBNull(8) ? null : reader.GetDecimal(8)
                                }
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task CreateBidAsync(Bid bid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO Bid (Amount, MemberID_FK, AuctionID_FK) VALUES (@Amount, @MemberID_FK, @AuctionID_FK);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Amount", bid.Amount);
                    command.Parameters.AddWithValue("@MemberID_FK", bid.Member.MemberID);
                    command.Parameters.AddWithValue("@AuctionID_FK", bid.Auction.AuctionID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateBidAsync(Bid bid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Bid SET Amount = @Amount WHERE MemberID_FK = @MemberID_FK AND AuctionID_FK = @AuctionID_FK;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Amount", bid.Amount);
                    command.Parameters.AddWithValue("@MemberID_FK", bid.Member.MemberID);
                    command.Parameters.AddWithValue("@AuctionID_FK", bid.Auction.AuctionID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteBidAsync(int auctionId, int memberId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Bid WHERE AuctionID_FK = @AuctionID_FK AND MemberID_FK = @MemberID_FK;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuctionID_FK", auctionId);
                    command.Parameters.AddWithValue("@MemberID_FK", memberId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
