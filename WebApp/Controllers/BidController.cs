using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer;

namespace WebApp.Controllers
{
    public class BidController : Controller
    {
        private readonly BidLogic _bidLogic;

        public BidController()
        {
            _bidLogic = new BidLogic();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceBid(int auctionId, int memberId, decimal amount)
        {
            var result = await _bidLogic.PlaceBidAsync(auctionId, memberId, amount);

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
