using AuctionSemesterProject.Services;
using AuctionSemesterProject.AuctionModels;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _auctionItemService.GetAllAuctionItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _auctionItemService.GetAuctionItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionItem item)
        {
            await _auctionItemService.CreateAuctionItemAsync(item);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AuctionItem item)
        {
            await _auctionItemService.UpdateAuctionItemAsync(id, item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _auctionItemService.DeleteAuctionItemAsync(id);
            return Ok();
        }
    }
}
