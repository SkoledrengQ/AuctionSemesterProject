using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer;

namespace WebApp.Controllers
{
    public class BidController(BidLogic bidLogic) : Controller
    {
        private readonly BidLogic _bidLogic = bidLogic;

        [HttpPost]
        public async Task<IActionResult> PlaceBid(decimal amount, int auctionId, int memberId)
        {
            var result = await _bidLogic.PlaceBidAsync(amount, auctionId, memberId);

            if (!result.IsSuccessful)
            {
                TempData["Error"] = result.ErrorMessage;
                return RedirectToAction("AuctionDetails", "Home", new { id = auctionId });
            }

            TempData["Success"] = "Bid placed successfully!";
            return RedirectToAction("AuctionDetails", "Home", new { id = auctionId });
        }
    }
}
