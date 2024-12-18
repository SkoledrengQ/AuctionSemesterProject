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

		// Retrieve all bids
		public async Task<List<Bid>> GetAllBidsAsync()
		{
			var bids = new List<Bid>();

			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var query = @"
                SELECT 
                    B.BidID, B.Amount, B.MemberID_FK, M.FirstName, M.LastName, M.PhoneNo, M.Email,
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
					BidID = reader.GetInt32(0),
					Amount = reader.GetDecimal(1),
					Member = new Member
					{
						MemberID = reader.GetInt32(2),
						FirstName = reader.GetString(3),
						LastName = reader.GetString(4),
						PhoneNo = reader.GetString(5),
						Email = reader.GetString(6)
					},
					Auction = new Auction
					{
						AuctionID = reader.GetInt32(7),
						StartPrice = reader.GetDecimal(8),
						CurrentHighestBid = reader.IsDBNull(9) ? null : reader.GetDecimal(9)
					}
				});
			}

			return bids;
		}

		// Retrieve bid by BidID
		public async Task<Bid?> GetBidByIdAsync(int bidId)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var query = @"
                SELECT 
                    B.BidID, B.Amount, B.MemberID_FK, M.FirstName, M.LastName, M.PhoneNo, M.Email,
                    B.AuctionID_FK, A.StartPrice, A.CurrentHighestBid
                FROM Bid B
                LEFT JOIN Member M ON B.MemberID_FK = M.MemberID
                LEFT JOIN Auction A ON B.AuctionID_FK = A.AuctionID
                WHERE B.BidID = @BidID;";

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@BidID", bidId);

			using var reader = await command.ExecuteReaderAsync();
			if (await reader.ReadAsync())
			{
				return new Bid
				{
					BidID = reader.GetInt32(0),
					Amount = reader.GetDecimal(1),
					Member = new Member
					{
						MemberID = reader.GetInt32(2),
						FirstName = reader.GetString(3),
						LastName = reader.GetString(4),
						PhoneNo = reader.GetString(5),
						Email = reader.GetString(6)
					},
					Auction = new Auction
					{
						AuctionID = reader.GetInt32(7),
						StartPrice = reader.GetDecimal(8),
						CurrentHighestBid = reader.IsDBNull(9) ? null : reader.GetDecimal(9)
					}
				};
			}

			return null;
		}

		public async Task<int> CreateBidAsync(Bid bid, decimal oldBid)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			using var transaction = await connection.BeginTransactionAsync();
			try
			{
				// Update auction with the new highest bid
				var updateAuctionQuery = @"
            UPDATE Auction
            SET CurrentHighestBid = @NewHighestBid, NoOfBids = ISNULL(NoOfBids, 0) + 1
            WHERE AuctionID = @AuctionID AND CurrentHighestBid = @OldBid";

				using var updateCommand = new SqlCommand(updateAuctionQuery, connection, (SqlTransaction)transaction);
				updateCommand.Parameters.AddWithValue("@NewHighestBid", bid.Amount);
				updateCommand.Parameters.AddWithValue("@AuctionID", bid.AuctionID_FK);
				updateCommand.Parameters.AddWithValue("@OldBid", oldBid);

				var rowsAffected = await updateCommand.ExecuteNonQueryAsync();
				if (rowsAffected == 0)
				{
					await transaction.RollbackAsync();
					return 0; // Concurrency conflict detected
				}

				// Insert new bid
				var insertBidQuery = @"
            INSERT INTO Bid (Amount, MemberID_FK, AuctionID_FK)
            OUTPUT INSERTED.BidID
            VALUES (@Amount, @MemberID_FK, @AuctionID_FK)";

				using var insertCommand = new SqlCommand(insertBidQuery, connection, (SqlTransaction)transaction);
				insertCommand.Parameters.AddWithValue("@Amount", bid.Amount);
				insertCommand.Parameters.AddWithValue("@MemberID_FK", bid.MemberID_FK);
				insertCommand.Parameters.AddWithValue("@AuctionID_FK", bid.AuctionID_FK);

				var bidId = (int)await insertCommand.ExecuteScalarAsync();

				await transaction.CommitAsync();
				return bidId;
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
		}





		// Update an existing bid
		public async Task UpdateBidAsync(Bid bid)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var query = "UPDATE Bid SET Amount = @Amount WHERE BidID = @BidID;";

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@Amount", bid.Amount);
			command.Parameters.AddWithValue("@BidID", bid.BidID);

			await command.ExecuteNonQueryAsync();
		}

		// Delete a bid
		public async Task<bool> DeleteBidAsync(int bidId)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var query = "DELETE FROM Bid WHERE BidID = @BidID;";

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@BidID", bidId);

			int rowsAffected = await command.ExecuteNonQueryAsync();
			return rowsAffected > 0;
		}
	}
}
