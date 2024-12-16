using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Dtos; // API DTOs for Auction and Bid
using WebApp.BusinessLogicLayer;

namespace WebApp.ServiceLayer
{
    public class BidService : IBidService
    {
        private readonly HttpClient _httpClient;

        public BidService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch auction details by ID
        public async Task<AuctionDto> GetAuctionByIdAsync(int auctionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Auctions/{auctionId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AuctionDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                // Log error (e.g., to a file or monitoring system)
                Console.WriteLine($"Error fetching auction: {ex.Message}");
            }

            return null; // Return null if the auction is not found or an error occurs
        }

        // Submit a bid
        public async Task<BidResult> PlaceBidAsync(BidDto bidDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(bidDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Bids", content);
                if (response.IsSuccessStatusCode)
                {
                    return new BidResult { IsSuccessful = true };
                }

                // Extract error message from API response, if available
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new BidResult { IsSuccessful = false, ErrorMessage = errorMessage };
            }
            catch (Exception ex)
            {
                // Log error (e.g., to a file or monitoring system)
                Console.WriteLine($"Error placing bid: {ex.Message}");
                return new BidResult { IsSuccessful = false, ErrorMessage = "An unexpected error occurred." };
            }
        }
    }
}
