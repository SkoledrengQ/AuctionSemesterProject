using WebApp.ServiceLayer;
using API.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApp.BusinessLogicLayer
{
    public class AuctionLogicWeb
    {
        private readonly IAuctionService _auctionService;

        public AuctionLogicWeb(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public async Task<IEnumerable<AuctionDetailsDto>> GetActiveAuctionsAsync()
        {
            return await _auctionService.GetAllAuctionsAsync();
        }

        public async Task<AuctionDetailsDto?> GetAuctionDetailsAsync(int id)
        {
            return await _auctionService.GetAuctionDetailsAsync(id);
        }
    }
}
