using System;
using System.Threading.Tasks;
using WebApp.ServiceLayer; // For BidService interactions
using WebApp.Models; // For AuctionDetailsViewModel
using API.Dtos; // DTOs exposed by the API

namespace WebApp.BusinessLogic
{
    public class BidLogic : IBidLogic
    {
        private readonly IBidService _bidService;

        public BidLogic(IBidService bidService)
        {
            _bidService = bidService;
        }

        // Process the bid request
        public async Task<BidResult> ProcessBidAsync(int auctionId, int memberId, decimal amount)
        {
            // Step 1: Retrieve auction details via DTO
            var auctionDto = await _bidService.GetAuctionByIdAsync(auctionId);
            if (auctionDto == null)
            {
                return new BidResult { IsSuccessful = false, ErrorMessage = "Auction not found." };
            }

            // Step 2: Validate bid amount
            var currentBid = auctionDto.EndingBid ?? auctionDto.StartPrice;
            if (amount <= currentBid)
            {
                return new BidResult
                {
                    IsSuccessful = false,
                    ErrorMessage = $"Your bid must be higher than the current bid of {currentBid:C}."
                };
            }

            if (amount < currentBid + auctionDto.MinBid)
            {
                return new BidResult
                {
                    IsSuccessful = false,
                    ErrorMessage = $"Your bid must be at least {auctionDto.MinBid:C} higher than the current bid."
                };
            }

            // Step 3: Ensure auction is active
            if (auctionDto.Status != "Open")
            {
                return new BidResult
                {
                    IsSuccessful = false,
                    ErrorMessage = "Bidding is closed for this auction."
                };
            }

            // Step 4: Create a BidDto to send to the API
            var bidDto = new BidDto
            {
                AuctionID = auctionId,
                MemberID = memberId,
                Amount = amount
            };

            // Step 5: Submit the bid via the service layer
            var serviceResult = await _bidService.PlaceBidAsync(bidDto);

            if (!serviceResult.IsSuccessful)
            {
                return new BidResult
                {
                    IsSuccessful = false,
                    ErrorMessage = serviceResult.ErrorMessage ?? "Failed to place the bid due to an unknown error."
                };
            }

            // Step 6: Return success
            return new BidResult { IsSuccessful = true };
        }
    }
}
