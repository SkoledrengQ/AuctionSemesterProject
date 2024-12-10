using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    public class BidController : Controller
    {
        private readonly BidService _bidService;

        public BidController(BidService bidService)
        {
            _bidService = bidService;
        }

        // GET: Bid
        public async Task<IActionResult> Index()
        {
            var bids = await _bidService.GetAllBidsAsync();
            return View(bids);
        }

        // GET: Bid/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            Bid? bid = await _bidService.GetBidByIdAsync(id);
            if (bid == null)
                return NotFound();

            return View(bid);
        }

        // POST: Bid/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal newBid, int auctionID, int memberID)
        {
            if (ModelState.IsValid)
            {
                await _bidService.CreateBidAsync(newBid, auctionID, memberID);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Bid/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            Bid? bid = await _bidService.GetBidByIdAsync(id);
            if (bid == null)
                return NotFound();

            return View(bid);
        }
        
        // GET: Bid/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            Bid? bid = await _bidService.GetBidByIdAsync(id);
            if (bid == null)
                return NotFound();

            return View(bid);
        }

        // POST: Bid/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bidService.DeleteBidAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
