using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionModels;

namespace DataAccess.Interfaces
{
	public interface IBidAccess
	{
		Task<List<Bid>> GetAllBidsAsync();
		Task<Bid?> GetBidByIdAsync(int bidId); // Updated to use BidID
		Task<int> CreateBidAsync(Bid bid, decimal oldBid); // Returns BidID after creation
		Task UpdateBidAsync(Bid bid);
		Task<bool> DeleteBidAsync(int bidId); // Updated to use BidID
	}
}
