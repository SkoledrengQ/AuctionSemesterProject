using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionItemController : ControllerBase
    {
        private readonly AuctionItemService _auctionItemService;

        public AuctionItemController(AuctionItemService auctionItemService)
        {
            _auctionItemService = auctionItemService;
        }

        // GET: api/AuctionItem
        [HttpGet]
        public async Task<IActionResult> GetAllAuctionItems()
        {
            List<AuctionItem> items = await _auctionItemService.GetAllAuctionItemsAsync();
            return Ok(items);
        }

        // GET: api/AuctionItem/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionItemById(int id)
        {
            AuctionItem? item = await _auctionItemService.GetAuctionItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/AuctionItem
        [HttpPost]
        public async Task<IActionResult> CreateAuctionItem([FromBody] AuctionItem auctionItem)
        {
            await _auctionItemService.CreateAuctionItemAsync(auctionItem);
            return CreatedAtAction(nameof(GetAuctionItemById), new { id = auctionItem.ItemID }, auctionItem);
        }

        // PUT: api/AuctionItem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuctionItem(int id, [FromBody] AuctionItem auctionItem)
        {
            await _auctionItemService.UpdateAuctionItemAsync(id, auctionItem);
            return NoContent();
        }

        // DELETE: api/AuctionItem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuctionItem(int id)
        {
            await _auctionItemService.DeleteAuctionItemAsync(id);
            return NoContent();
        }
    }
}
