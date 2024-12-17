using API.Dtos; // Ensure this matches your namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.ServiceLayer
{
    public interface IAuctionService
    {
        Task<IEnumerable<AuctionDetailsDto>> GetAllAuctionsAsync(); // Correct method name
        Task<AuctionDetailsDto?> GetAuctionDetailsAsync(int id); // Correct method
    }
}
