using API.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.ServiceLayer
{
    public class AuctionService : IAuctionService
    {
        private readonly HttpClient _httpClient;

        public AuctionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all auctions - returns AuctionDetailsDto for richer data
        public async Task<IEnumerable<AuctionDetailsDto>> GetAllAuctionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<AuctionDetailsDto>>("api/Auction");
        }

        // Fetch auction details by ID
        public async Task<AuctionDetailsDto?> GetAuctionDetailsAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AuctionDetailsDto?>($"api/Auction/{id}");
        }
    }
}
