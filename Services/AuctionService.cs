// Services/AuctionService.cs
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

        public async Task CreateAuctionAsync(Auction auction)
        {
            await _auctionDAO.CreateAuctionAsync(auction);
        }

        public async Task UpdateAuctionAsync(int id, Auction auction)
        {
            await _auctionDAO.UpdateAuctionAsync(id, auction);
        }

        public async Task DeleteAuctionAsync(int id)
        {
            await _auctionDAO.DeleteAuctionAsync(id);
        }
    }
}
