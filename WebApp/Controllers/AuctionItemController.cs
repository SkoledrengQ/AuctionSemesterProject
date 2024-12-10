using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    public class AuctionItemController : Controller
    {
        private readonly AuctionItemService _auctionItemService;

        public AuctionItemController(AuctionItemService auctionItemService)
        {
            _auctionItemService = auctionItemService;
        }

        // GET: AuctionItem
        public async Task<IActionResult> Index()
        {
            List<AuctionItem> items = await _auctionItemService.GetAllAuctionItemsAsync();
            return View(items);
        }

        // GET: AuctionItem/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            AuctionItem? item = await _auctionItemService.GetAuctionItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // GET: AuctionItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuctionItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuctionItem auctionItem)
        {
            if (ModelState.IsValid)
            {
                await _auctionItemService.CreateAuctionItemAsync(auctionItem);
                return RedirectToAction(nameof(Index));
            }
            return View(auctionItem);
        }

        // GET: AuctionItem/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            AuctionItem? item = await _auctionItemService.GetAuctionItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: AuctionItem/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuctionItem auctionItem)
        {
            if (id != auctionItem.ItemID)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _auctionItemService.UpdateAuctionItemAsync(id, auctionItem);
                return RedirectToAction(nameof(Index));
            }

            return View(auctionItem);
        }

        // GET: AuctionItem/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            AuctionItem? item = await _auctionItemService.GetAuctionItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: AuctionItem/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _auctionItemService.DeleteAuctionItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
