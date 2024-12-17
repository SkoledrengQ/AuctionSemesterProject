using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Dtos;
using WebApp.Models;

namespace WebApp.ServiceLayer
{
    public class BidService : IBidService
    {
        private readonly HttpClient _httpClient;

        public BidService()
        {
            _httpClient = new HttpClient(); // In a real app, consider injecting this via DI
        }

        public async Task<BidResult> PlaceBidAsync(BidDto bidDto)
        {
            // Serialize the DTO to JSON
            var content = new StringContent(
                JsonSerializer.Serialize(bidDto),
                Encoding.UTF8,
                "application/json");

            // Send POST request to the API
            var response = await _httpClient.PostAsync("https://localhost:7005/api/bids", content);

            if (response.IsSuccessStatusCode)
            {
                return BidResult.Success();
            }

            // Handle errors
            var error = await response.Content.ReadAsStringAsync();
            return BidResult.Failure($"Failed to place bid: {error}");
        }
    }
}
