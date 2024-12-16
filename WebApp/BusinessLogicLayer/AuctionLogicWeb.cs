using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionModels;
using WebApp.Models;
using WebApp.ServiceLayer;

namespace WebApp.BusinessLogicLayer
{
    public class AuctionLogic(IAuctionService auctionService)
    {
        private readonly IAuctionService _auctionService = auctionService;

        public async Task<IEnumerable<AuctionItem>> GetActiveAuctionsAsync()
        {
            try
            {
                // Call the service layer to fetch active auctions
                return await _auctionService.GetAllAuctionItemsAsync();
            }
            catch
            {
                // Return an empty list in case of an error
                return [];
            }
        }

        public async Task<AuctionDetailsViewModel?> GetAuctionDetailsAsync(int auctionId)
        {
            try
            {
                // Retrieve auction and auction item details
                var auction = await _auctionService.GetAuctionByIdAsync(auctionId);
                var auctionItem = await _auctionService.GetAuctionItemByIdAsync(auctionId);

                // Check if both auction and auction item are valid
                if (auction == null || auctionItem == null)
                {
                    return null;
                }

                // Construct and return the view model
                return new AuctionDetailsViewModel
                {
                    Auction = auction,
                    AuctionItem = auctionItem
                };
            }
            catch
            {
                // Return null in case of an error
                return null;
            }
        }
    }
}
