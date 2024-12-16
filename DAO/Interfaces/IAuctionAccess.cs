using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.AuctionModels;

namespace AuctionSemesterProject.Interfaces
{
    public interface IAuctionAccess
    {
        Task<List<Auction>> GetAllAuctionsAsync();
        Task<Auction?> GetAuctionByIdAsync(int id);
        Task CreateAuctionAsync(Auction auction);
        Task<bool> UpdateAuctionWithConcurrencyCheckAsync(Auction auction);
        Task<bool> DeleteAuctionAsync(int id);
    }
}
