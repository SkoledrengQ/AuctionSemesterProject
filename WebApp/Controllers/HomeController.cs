using AuctionSemesterProject.AuctionModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Displays the homepage with all auction items
        public async Task<IActionResult> Index()
        {
            var apiUrl = "https://localhost:7005/api/AuctionItem";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var auctionItems = JsonSerializer.Deserialize<List<AuctionItem>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(auctionItems);
            }

            ViewBag.ErrorMessage = "Unable to load auction items.";
            return View(new List<AuctionItem>());
        }

        // Displays details for a specific auction and its associated item
        public async Task<IActionResult> AuctionDetails(int id)
        {
            var auctionApiUrl = $"https://localhost:7005/api/Auction/{id}";
            var auctionItemApiUrl = $"https://localhost:7005/api/AuctionItem/{id}";

            var auctionResponse = await _httpClient.GetAsync(auctionApiUrl);
            var auctionItemResponse = await _httpClient.GetAsync(auctionItemApiUrl);

            if (auctionResponse.IsSuccessStatusCode && auctionItemResponse.IsSuccessStatusCode)
            {
                var auctionJson = await auctionResponse.Content.ReadAsStringAsync();
                var auctionItemJson = await auctionItemResponse.Content.ReadAsStringAsync();

                var auction = JsonSerializer.Deserialize<Auction>(auctionJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var auctionItem = JsonSerializer.Deserialize<AuctionItem>(auctionItemJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var viewModel = new AuctionDetailsViewModel
                {
                    Auction = auction,
                    AuctionItem = auctionItem
                };

                return View("~/Views/Auctions/AuctionDetails.cshtml", viewModel);
            }

            ViewBag.ErrorMessage = "Unable to load auction details.";
            return RedirectToAction("Index");
        }

        // Privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
