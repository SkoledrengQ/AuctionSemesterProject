using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Services
{
    public class AuctionItemService
    {
        private readonly AuctionItemDAO _auctionItemDAO;

        public AuctionItemService(AuctionItemDAO auctionItemDAO)
        {
            _auctionItemDAO = auctionItemDAO;
        }

        public async Task<List<AuctionItem>> GetAllAuctionItemsAsync()
        {
            return await _auctionItemDAO.GetAllAuctionItemsAsync();
        }

        public async Task<AuctionItem?> GetAuctionItemByIdAsync(int id)
        {
            return await _auctionItemDAO.GetAuctionItemByIdAsync(id);
        }

        public async Task CreateAuctionItemAsync(AuctionItem auctionItem)
        {
            await _auctionItemDAO.CreateAuctionItemAsync(auctionItem);
        }

        public async Task UpdateAuctionItemAsync(int id, AuctionItem auctionItem)
        {
            await _auctionItemDAO.UpdateAuctionItemAsync(id, auctionItem);
        }

        public async Task DeleteAuctionItemAsync(int id)
        {
            await _auctionItemDAO.DeleteAuctionItemAsync(id);
        }
    }
}
