using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Services
{
    public class BidService
    {
        private readonly BidDAO _bidDAO;
        private readonly AuctionDAO _auctionDAO;

        public BidService(BidDAO bidDAO, AuctionDAO auctionDAO)
        {
            _bidDAO = bidDAO;
            _auctionDAO = auctionDAO;
        }

        public async Task<(bool Success, bool Conflict, string? ErrorMessage)> PlaceBidAsync(int auctionId, int memberId, decimal amount)
        {
            var auction = await _auctionDAO.GetAuctionByIdAsync(auctionId);
            if (auction == null)
            {
                return (false, false, "Auction does not exist.");
            }

            // Check if the bid is higher than the current highest bid
            if (auction.CurrentHighestBid.HasValue && amount <= auction.CurrentHighestBid)
            {
                return (false, false, "Bid must be higher than the current highest bid.");
            }

            // Check if the bid meets the minimum increment requirement
            if (amount < (auction.CurrentHighestBid ?? auction.StartPrice) + auction.MinBid)
            {
                return (false, false, "Bid must meet the minimum increment.");
            }

            // Set the new bid amount
            auction.CurrentHighestBid = amount;

            // Perform the auction update with concurrency check
            var updateSuccess = await _auctionDAO.UpdateAuctionWithConcurrencyCheckAsync(auction);
            if (!updateSuccess)
            {
                return (false, true, null); // Conflict occurred
            }

            // Update the LastUpdated timestamp after the auction is updated
            auction.LastUpdated = DateTime.UtcNow;

            // Create the bid entry with the MemberID and other details
            var bid = new Bid
            {
                AuctionID_FK = auctionId,
                MemberID_FK = memberId,  // Pass the MemberID
                Amount = amount
            };

            await _bidDAO.CreateBidAsync(bid); // Save the new bid
            return (true, false, null); // Bid successfully placed
        }

        public async Task<List<Bid>> GetAllBidsAsync()
        {
            return await _bidDAO.GetAllBidsAsync();
        }

        public async Task<Bid?> GetBidByIdAsync(int auctionId, int memberId)
        {
            return await _bidDAO.GetBidByIdAsync(auctionId, memberId);
        }
    }
}
