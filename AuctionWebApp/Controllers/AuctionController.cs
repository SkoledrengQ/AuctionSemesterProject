using AuctionSemesterProject.Services;
using AuctionSemesterProject.AuctionModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly AuctionService _auctionService;

        public AuctionController(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var auctions = await _auctionService.GetAllAuctionsAsync();
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null) return NotFound();
            return Ok(auction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Auction auction)
        {
            var success = await _auctionService.UpdateAuctionAsync(id, auction);
            if (!success)
            {
                return Conflict("The auction was updated by another user. Please refresh and try again.");
            }

            return Ok();
        }
    }
}
