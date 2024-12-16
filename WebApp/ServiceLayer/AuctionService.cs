using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AuctionSemesterProject.AuctionModels;

namespace WebApp.ServiceLayer
{
    public class AuctionService : IAuctionService
    {
        private readonly HttpClient _httpClient;

        public AuctionService()
        {
            _httpClient = new HttpClient(); // In a real app, consider injecting this via DI
        }

        public async Task<IEnumerable<AuctionItem>> GetAllAuctionItemsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7005/api/AuctionItem");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<AuctionItem>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return new List<AuctionItem>();
        }

        public async Task<Auction> GetAuctionByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7005/api/Auction/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Auction>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return null;
        }

        public async Task<AuctionItem> GetAuctionItemByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7005/api/AuctionItem/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AuctionItem>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return null;
        }
    }
}
