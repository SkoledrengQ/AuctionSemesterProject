using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionModels;

namespace DataAccess.Interfaces
{
    public interface IBidAccess
    {
        Task<List<Bid>> GetAllBidsAsync();
        Task<Bid?> GetBidByIdAsync(int auctionId, int memberId);
        Task CreateBidAsync(Bid bid);
        Task UpdateBidAsync(Bid bid);
        Task<bool> DeleteBidAsync(int auctionId, int memberId); // Changed to return Task<bool>
    }
}
