using AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess
{
	public class AuctionDBAccess(string connectionString) : IAuctionAccess
	{
		private readonly string _connectionString = connectionString;

		// Fetch all auctions
		public async Task<List<Auction>> GetAllAuctionsAsync()
		{
			var auctions = new List<Auction>();

			const string query = @"
                SELECT 
                    A.AuctionID, A.StartPrice, A.MinBid, A.EndingBid, A.CurrentHighestBid,
                    A.BuyNowPrice, A.NoOfBids, A.TimeExtension, A.EmployeeID_FK, A.ItemID_FK
                FROM Auction A";

			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			using var command = new SqlCommand(query, connection);
			using var reader = await command.ExecuteReaderAsync();

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
					EmployeeID_FK = reader.GetInt32(8),
					ItemID_FK = reader.GetInt32(9)
				});
			}

			return auctions;
		}

		// Fetch auction by ID
		public async Task<Auction?> GetAuctionByIdAsync(int id)
		{
			const string query = @"
                SELECT 
                    A.AuctionID, A.StartPrice, A.MinBid, A.EndingBid, A.CurrentHighestBid,
                    A.BuyNowPrice, A.NoOfBids, A.TimeExtension, A.EmployeeID_FK, A.ItemID_FK
                FROM Auction A
                WHERE A.AuctionID = @AuctionID";

			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@AuctionID", id);

			using var reader = await command.ExecuteReaderAsync();

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
					EmployeeID_FK = reader.GetInt32(8),
					ItemID_FK = reader.GetInt32(9)
				};
			}

			return null;
		}

		// Create a new auction
		public async Task CreateAuctionAsync(Auction auction)
		{
			const string query = @"
                INSERT INTO Auction (StartPrice, MinBid, EndingBid, CurrentHighestBid, BuyNowPrice, 
                                     NoOfBids, TimeExtension, EmployeeID_FK, ItemID_FK)
                VALUES (@StartPrice, @MinBid, @EndingBid, @CurrentHighestBid, @BuyNowPrice, 
                        @NoOfBids, @TimeExtension, @EmployeeID_FK, @ItemID_FK)";

			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			using var command = new SqlCommand(query, connection);
			AddAuctionParameters(command, auction);
			await command.ExecuteNonQueryAsync();
		}

		// Update auction details
		public async Task<bool> UpdateAuctionAsync(Auction auction)
		{
			const string query = @"
                UPDATE Auction
                SET CurrentHighestBid = @CurrentHighestBid
                WHERE AuctionID = @AuctionID";

			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@CurrentHighestBid", auction.CurrentHighestBid ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@AuctionID", auction.AuctionID);

			var rowsAffected = await command.ExecuteNonQueryAsync();
			return rowsAffected > 0;
		}

		// Delete auction by ID
		public async Task<bool> DeleteAuctionAsync(int id)
		{
			const string query = "DELETE FROM Auction WHERE AuctionID = @AuctionID";

			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@AuctionID", id);

			var rowsAffected = await command.ExecuteNonQueryAsync();
			return rowsAffected > 0;
		}

		// Add parameters to SQL command
		private static void AddAuctionParameters(SqlCommand command, Auction auction)
		{
			command.Parameters.AddWithValue("@StartPrice", auction.StartPrice);
			command.Parameters.AddWithValue("@MinBid", auction.MinBid);
			command.Parameters.AddWithValue("@EndingBid", auction.EndingBid ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@CurrentHighestBid", auction.CurrentHighestBid ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@BuyNowPrice", auction.BuyNowPrice ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@NoOfBids", auction.NoOfBids);
			command.Parameters.AddWithValue("@TimeExtension", auction.TimeExtension ?? (object)DBNull.Value);
			command.Parameters.AddWithValue("@EmployeeID_FK", auction.EmployeeID_FK);
			command.Parameters.AddWithValue("@ItemID_FK", auction.ItemID_FK);
		}
	}
}
