using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ServiceLayer;
using API.Dtos;

namespace WebApp.BusinessLogicLayer
{
    public class BidLogic(IBidService bidService)
    {
        private readonly IBidService _bidService = bidService;

		public async Task<BidResult> PlaceBidAsync(decimal amount, int auctionId, int memberId, decimal oldBid)
		{
			if (amount <= 0)
			{
				return BidResult.Failure("Bid amount must be greater than zero.");
			}

			var bidDto = new BidDto(amount, memberId, auctionId, oldBid);


			try
			{
				return await _bidService.PlaceBidAsync(bidDto);
			}
			catch
			{
				return BidResult.Failure("An error occurred while processing the bid.");
			}
		}

	}
}
