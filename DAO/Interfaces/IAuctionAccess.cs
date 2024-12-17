using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionModels;

namespace DataAccess.Interfaces
{
    public interface IAuctionAccess
    {
        Task<List<Auction>> GetAllAuctionsAsync();
        Task<Auction?> GetAuctionByIdAsync(int id);
        Task CreateAuctionAsync(Auction auction);
        Task<bool> UpdateAuctionAsync(Auction auction);
        Task<bool> DeleteAuctionAsync(int id);
    }
}
