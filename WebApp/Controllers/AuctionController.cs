using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    public class AuctionController : Controller
    {
        private readonly AuctionService _auctionService;

        public AuctionController(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: Auction
        public async Task<IActionResult> Index()
        {
            List<Auction> auctions = await _auctionService.GetAllAuctionsAsync();
            return View(auctions);
        }

        // GET: Auction/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            Auction? auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null)
                return NotFound();

            return View(auction);
        }

        // GET: Auction/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Auction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Auction auction)
        {
            if (ModelState.IsValid)
            {
                await _auctionService.CreateAuctionAsync(auction);
                return RedirectToAction(nameof(Index));
            }
            return View(auction);
        }

        // GET: Auction/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            Auction? auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null)
                return NotFound();

            return View(auction);
        }

        // POST: Auction/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Auction auction)
        {
            if (id != auction.AuctionID)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _auctionService.UpdateAuctionAsync(id, auction);
                return RedirectToAction(nameof(Index));
            }

            return View(auction);
        }

        // GET: Auction/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            Auction? auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null)
                return NotFound();

            return View(auction);
        }

        // POST: Auction/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _auctionService.DeleteAuctionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
