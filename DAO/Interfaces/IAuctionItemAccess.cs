using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.AuctionModels;

namespace AuctionSemesterProject.Interfaces
{
    public interface IAuctionItemAccess
    {
        Task<List<AuctionItem>> GetAllAuctionItemsAsync();
        Task<AuctionItem?> GetAuctionItemByIdAsync(int id);
        Task CreateAuctionItemAsync(AuctionItem item);
        Task UpdateAuctionItemAsync(AuctionItem item);
        Task<bool> DeleteAuctionItemAsync(int id); // Changed to return Task<bool>
    }
}
