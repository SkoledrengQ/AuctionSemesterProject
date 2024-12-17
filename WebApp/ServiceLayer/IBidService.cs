using System.Threading.Tasks;
using API.Dtos;
using WebApp.Models;

namespace WebApp.ServiceLayer
{
    public interface IBidService
    {
        Task<BidResult> PlaceBidAsync(BidDto bidDto);
    }
}
