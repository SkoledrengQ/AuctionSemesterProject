using API.Dtos; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.ServiceLayer
{
    public interface IAuctionService
    {
        Task<IEnumerable<AuctionDetailsDto>> GetAllAuctionsAsync(); 
        Task<AuctionDetailsDto?> GetAuctionDetailsAsync(int id); 
    }
}
