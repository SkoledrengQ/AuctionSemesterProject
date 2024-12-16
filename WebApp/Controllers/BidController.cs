using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models; // ViewModel for Auction Details
using WebApp.BusinessLogicLayer; // BidLogic for business rules

namespace WebApp.Controllers
{
    public class BidController : Controller
    {
        private readonly IBidLogic _bidLogic;

        public BidController(IBidLogic bidLogic)
        {
            _bidLogic = bidLogic;
        }

        // POST: /Bid/PlaceBid
        [HttpPost]
        public async Task<IActionResult> PlaceBid(AuctionDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, redisplay the AuctionDetails page
                TempData["Error"] = "Invalid input. Please ensure all fields are correctly filled.";
                return View("AuctionDetails", model);
            }

            try
            {
                // Extract the required information
                var auctionId = model.Auction.AuctionID;
                var bidAmount = model.BidAmount;
                var memberId = model.MemberID;

                // Call business logic to process the bid
                var result = await _bidLogic.ProcessBidAsync(auctionId, memberId, bidAmount);

                if (result.IsSuccessful)
                {
                    TempData["Success"] = "Your bid was successfully placed!";
                    return RedirectToAction("Index", "Auction");
                }
                else
                {
                    TempData["Error"] = result.ErrorMessage;
                    return View("AuctionDetails", model);
                }
            }
            catch
            {
                // Log error and display a generic message
                TempData["Error"] = "An unexpected error occurred while placing your bid.";
                return View("AuctionDetails", model);
            }
        }
    }
}
