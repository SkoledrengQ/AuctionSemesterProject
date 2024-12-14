using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Services
{
    public class AuctionService
    {
        private readonly AuctionDAO _auctionDAO;

        public AuctionService(AuctionDAO auctionDAO)
        {
            _auctionDAO = auctionDAO;
        }

        public async Task<List<Auction>> GetAllAuctionsAsync()
        {
            return await _auctionDAO.GetAllAuctionsAsync();
        }

        public async Task<Auction?> GetAuctionByIdAsync(int id)
        {
            return await _auctionDAO.GetAuctionByIdAsync(id);
        }

        public async Task<bool> UpdateAuctionAsync(int id, Auction updatedAuction)
        {
            var existingAuction = await _auctionDAO.GetAuctionByIdAsync(id);
            if (existingAuction == null)
            {
                return false; // Auction does not exist
            }

            // Update the LastUpdated field directly here
            updatedAuction.LastUpdated = DateTime.UtcNow;

            // Validate and update the auction, now without the originalLastUpdated parameter
            return await _auctionDAO.UpdateAuctionWithConcurrencyCheckAsync(updatedAuction);
        }
    }
}
