using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionModels;

namespace DataAccess.Interfaces
{
	public interface IBidAccess
	{
		Task<List<Bid>> GetAllBidsAsync();
		Task<Bid?> GetBidByIdAsync(int auctionId, int memberId);
		Task<bool> CreateBidAsync(Bid bid, decimal oldBid); // Updated to include OldBid for concurrency
		Task UpdateBidAsync(Bid bid);
		Task<bool> DeleteBidAsync(int auctionId, int memberId); // Returns Task<bool> for success indication
	}
}
