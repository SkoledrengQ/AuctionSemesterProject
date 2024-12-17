using AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess
{
	public class BidDBAccess(string connectionString) : IBidAccess
	{
		private readonly string _connectionString = connectionString;

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

				using var command = new SqlCommand(query, connection);
				using var reader = await command.ExecuteReaderAsync();
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

			return bids;
		}

		public async Task<Bid?> GetBidByIdAsync(int auctionId, int memberId)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();
			var query = @"
                    SELECT 
                        B.Amount, B.MemberID_FK, M.FirstName, M.LastName, M.PhoneNo, M.Email,
                        B.AuctionID_FK, A.StartPrice, A.CurrentHighestBid
                    FROM Bid B
                    LEFT JOIN Member M ON B.MemberID_FK = M.MemberID
                    LEFT JOIN Auction A ON B.AuctionID_FK = A.AuctionID
                    WHERE B.AuctionID_FK = @AuctionID_FK AND B.MemberID_FK = @MemberID_FK;";

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@AuctionID_FK", auctionId);
			command.Parameters.AddWithValue("@MemberID_FK", memberId);

			using var reader = await command.ExecuteReaderAsync();
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

			return null;
		}

		public async Task<bool> CreateBidAsync(Bid bid, decimal oldBid)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			using var transaction = await connection.BeginTransactionAsync();

			try
			{
				// Verify currentHighestBid matches the OldBid value
				var checkBidQuery = @"
            SELECT currentHighestBid 
            FROM Auction
            WHERE AuctionID = @AuctionID";

				using var checkCommand = new SqlCommand(checkBidQuery, connection, (SqlTransaction)transaction);
				checkCommand.Parameters.AddWithValue("@AuctionID", bid.AuctionID_FK);

				var currentBidInDb = (decimal?)await checkCommand.ExecuteScalarAsync();

				if (currentBidInDb == null)
					throw new InvalidOperationException("Auction not found.");

				if (currentBidInDb != oldBid)
				{
					// The currentHighestBid has changed, concurrency conflict
					transaction.Rollback();
					return false; // Signal the bid was rejected due to concurrency
				}

				// Insert the new bid
				var insertBidQuery = @"
            INSERT INTO Bid (Amount, MemberID_FK, AuctionID_FK)
            VALUES (@Amount, @MemberID_FK, @AuctionID_FK);";

				using var insertCommand = new SqlCommand(insertBidQuery, connection, (SqlTransaction)transaction);
				insertCommand.Parameters.AddWithValue("@Amount", bid.Amount);
				insertCommand.Parameters.AddWithValue("@MemberID_FK", bid.MemberID_FK);
				insertCommand.Parameters.AddWithValue("@AuctionID_FK", bid.AuctionID_FK);
				await insertCommand.ExecuteNonQueryAsync();

				// Update the Auction table
				var updateAuctionQuery = @"
            UPDATE Auction
            SET currentHighestBid = @NewHighestBid,
                noOfBids = ISNULL(noOfBids, 0) + 1,
                LastUpdated = GETDATE()
            WHERE AuctionID = @AuctionID";

				using var updateCommand = new SqlCommand(updateAuctionQuery, connection, (SqlTransaction)transaction);
				updateCommand.Parameters.AddWithValue("@NewHighestBid", bid.Amount);
				updateCommand.Parameters.AddWithValue("@AuctionID", bid.AuctionID_FK);
				await updateCommand.ExecuteNonQueryAsync();

				// Commit transaction
				await transaction.CommitAsync();
				return true; // Signal the bid was successful
			}
			catch
			{
				await transaction.RollbackAsync();
				throw; // Rethrow the exception for higher-level handling
			}
		}


		public async Task UpdateBidAsync(Bid bid)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();
			var query = "UPDATE Bid SET Amount = @Amount WHERE MemberID_FK = @MemberID_FK AND AuctionID_FK = @AuctionID_FK;";

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@Amount", bid.Amount);
			command.Parameters.AddWithValue("@MemberID_FK", bid.MemberID_FK);
			command.Parameters.AddWithValue("@AuctionID_FK", bid.AuctionID_FK);


			await command.ExecuteNonQueryAsync();
		}

		public async Task<bool> DeleteBidAsync(int auctionId, int memberId)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();
			var query = "DELETE FROM Bid WHERE AuctionID_FK = @AuctionID_FK AND MemberID_FK = @MemberID_FK;";

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@AuctionID_FK", auctionId);
			command.Parameters.AddWithValue("@MemberID_FK", memberId);

			int rowsAffected = await command.ExecuteNonQueryAsync();
			return rowsAffected > 0;
		}
	}
}
