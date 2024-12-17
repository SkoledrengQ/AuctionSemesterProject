using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ServiceLayer;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuctionService _auctionService;

        public HomeController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // Default action for Home/Index
        public async Task<IActionResult> Index()
        {
            var auctionDetails = await _auctionService.GetAllAuctionsAsync();

            var viewModels = auctionDetails.Select(a => new AuctionDetailsViewModel
            {
                // Auction Information
                AuctionID = a.Auction.AuctionID,
                StartPrice = a.Auction.StartPrice,
                MinBid = a.Auction.MinBid,
                EndingBid = a.Auction.EndingBid,
                CurrentHighestBid = a.Auction.CurrentHighestBid,
                BuyNowPrice = a.Auction.BuyNowPrice,
                NoOfBids = a.Auction.NoOfBids,
                TimeExtension = a.Auction.TimeExtension,
                EmployeeID = a.Auction.EmployeeID,

                // Auction Item Information
                Title = a.AuctionItem.Title,
                Author = a.AuctionItem.Author,
                Genre = a.AuctionItem.Genre,
                Description = a.AuctionItem.Description
            }).ToList();

            return View(viewModels);
        }

        // Auction details action
        public async Task<IActionResult> AuctionDetails(int id)
        {
            var auctionDetails = await _auctionService.GetAuctionDetailsAsync(id);

            if (auctionDetails == null)
                return NotFound();

            var viewModel = new AuctionDetailsViewModel
            {
                // Auction Information
                AuctionID = auctionDetails.Auction.AuctionID,
                StartPrice = auctionDetails.Auction.StartPrice,
                MinBid = auctionDetails.Auction.MinBid,
                EndingBid = auctionDetails.Auction.EndingBid,
                CurrentHighestBid = auctionDetails.Auction.CurrentHighestBid,
                BuyNowPrice = auctionDetails.Auction.BuyNowPrice,
                NoOfBids = auctionDetails.Auction.NoOfBids,
                TimeExtension = auctionDetails.Auction.TimeExtension,
                EmployeeID = auctionDetails.Auction.EmployeeID,

                // Auction Item Information
                Title = auctionDetails.AuctionItem.Title,
                Author = auctionDetails.AuctionItem.Author,
                Genre = auctionDetails.AuctionItem.Genre,
                Description = auctionDetails.AuctionItem.Description
            };

            return View(viewModel);
        }
    }
}
