using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ServiceLayer;
using WebApp.BusinessLogicLayer;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly BidLogic _bidLogic;

        public HomeController(IAuctionService auctionService, BidLogic bidLogic)
        {
            _auctionService = auctionService;
            _bidLogic = bidLogic;
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

        public async Task<IActionResult> AuctionDetails(int id)
        {
            var auctionDetails = await _auctionService.GetAuctionDetailsAsync(id);

            if (auctionDetails == null)
                return NotFound();

            // Map the DTO to a ViewModel
            var viewModel = new AuctionDetailsViewModel
            {
                // Auction Properties
                AuctionID = auctionDetails.Auction.AuctionID,
                StartPrice = auctionDetails.Auction.StartPrice,
                MinBid = auctionDetails.Auction.MinBid,
                EndingBid = auctionDetails.Auction.EndingBid,
                CurrentHighestBid = auctionDetails.Auction.CurrentHighestBid,
                BuyNowPrice = auctionDetails.Auction.BuyNowPrice,
                NoOfBids = auctionDetails.Auction.NoOfBids,
                TimeExtension = auctionDetails.Auction.TimeExtension,
                EmployeeID = auctionDetails.Auction.EmployeeID,

                // Auction Item Properties
                Title = auctionDetails.AuctionItem.Title,
                Author = auctionDetails.AuctionItem.Author,
                Genre = auctionDetails.AuctionItem.Genre,
                Description = auctionDetails.AuctionItem.Description
            };

            // Pass the ViewModel instead of the DTO
            return View("~/Views/Auctions/AuctionDetails.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceBid(decimal Amount, int AuctionID, int MemberID)
        {
            // Retrieve the auction details to get the current highest bid
            var auctionDetails = await _auctionService.GetAuctionDetailsAsync(AuctionID);
            if (auctionDetails == null)
            {
                TempData["ErrorMessage"] = "Auction not found.";
                return RedirectToAction("Index");
            }

            var currentHighestBid = auctionDetails.Auction.CurrentHighestBid ?? auctionDetails.Auction.StartPrice;

            // Call the business logic layer with OldBid
            var result = await _bidLogic.PlaceBidAsync(Amount, AuctionID, MemberID, currentHighestBid);

            if (result.IsSuccessful)
            {
                TempData["SuccessMessage"] = "Bid placed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
            }

            return RedirectToAction("AuctionDetails", new { id = AuctionID });
        }
    }
}