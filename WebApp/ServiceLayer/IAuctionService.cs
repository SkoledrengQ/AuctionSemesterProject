using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.AuctionModels;

namespace WebApp.ServiceLayer
{
    public interface IAuctionService
    {
        Task<IEnumerable<AuctionItem>> GetAllAuctionItemsAsync();
        Task<Auction> GetAuctionByIdAsync(int id);
        Task<AuctionItem> GetAuctionItemByIdAsync(int id);
    }
}
