// Services/BidService.cs
using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;  // For async/await

namespace AuctionSemesterProject.Services
{
    public class BidService
    {
        private readonly BidDAO _bidDAO;

        public BidService(BidDAO bidDAO)
        {
            _bidDAO = bidDAO;
        }

        public async Task<List<Bid>> GetAllBidsAsync()
        {
            return await _bidDAO.GetAllBidsAsync();
        }

        public async Task<Bid?> GetBidByIdAsync(int id)
        {
            return await _bidDAO.GetBidByIdAsync(id);
        }

        public async Task CreateBidAsync(decimal newBid, int auctionID, int memberID)
        {
            await _bidDAO.CreateBidAsync(newBid, auctionID, memberID);
        }

        public async Task UpdateBidAsync(int id, Bid bid)
        {
            await _bidDAO.UpdateBidAsync(id, bid);
        }

        public async Task DeleteBidAsync(int id)
        {
            await _bidDAO.DeleteBidAsync(id);
        }
    }
}
