using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ServiceLayer;
using API.Dtos;

namespace WebApp.BusinessLogicLayer
{
    public class BidLogic(IBidService bidService)
    {
        private readonly IBidService _bidService = bidService;

        public async Task<BidResult> PlaceBidAsync(decimal amount,int auctionId, int memberId)
        {
            // Validate the bid amount
            if (amount <= 0)
            {
                return BidResult.Failure("Bid amount must be greater than zero.");
            }

            // Prepare the DTO
            var bidDto = new BidDto(amount, auctionId, memberId);
          

            try
            {
                // Call the service layer
                return await _bidService.PlaceBidAsync(bidDto);
            }
            catch
            {
                // Return a failure result on error
                return BidResult.Failure("An error occurred while processing the bid.");
            }
        }
    }
}
