using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ServiceLayer;
using API.Dtos;

namespace WebApp.BusinessLogicLayer
{
    public class BidLogic
    {
        private readonly IBidService _bidService;

        public BidLogic()
        {
            _bidService = new BidService();
        }

        public async Task<BidResult> PlaceBidAsync(int auctionId, int memberId, decimal amount)
        {
            // Validate the bid amount
            if (amount <= 0)
            {
                return BidResult.Failure("Bid amount must be greater than zero.");
            }

            // Prepare the DTO
            var bidDto = new BidDto
            {
                AuctionID = auctionId,
                MemberID = memberId,
                Amount = amount
            };

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
