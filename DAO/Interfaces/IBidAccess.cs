using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionModels;

namespace DataAccess.Interfaces
{
	public interface IBidAccess
	{
		Task<List<Bid>> GetAllBidsAsync();
		Task<Bid?> GetBidByIdAsync(int bidId);
		Task<int> CreateBidAsync(Bid bid, decimal oldBid);
		Task UpdateBidAsync(Bid bid);
		Task<bool> DeleteBidAsync(int bidId);
	}
}
