using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly BidService _bidService;

        public BidController(BidService bidService)
        {
            _bidService = bidService;
        }

        [HttpPost("PlaceBid")]
        public async Task<IActionResult> PlaceBid([FromForm] int auctionId, [FromForm] int memberId, [FromForm] decimal amount)
        {
            var result = await _bidService.PlaceBidAsync(auctionId, memberId, amount);

            if (!result.Success)
            {
                if (result.Conflict)
                {
                    return Conflict("The auction was updated by another user. Please refresh and try again.");
                }

                return BadRequest(result.ErrorMessage);
            }

            return Ok("Your bid has been placed successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBids()
        {
            var bids = await _bidService.GetAllBidsAsync();
            return Ok(bids);
        }

        [HttpGet("{auctionId}/{memberId}")]
        public async Task<IActionResult> GetBid(int auctionId, int memberId)
        {
            var bid = await _bidService.GetBidByIdAsync(auctionId, memberId);
            if (bid == null) return NotFound();
            return Ok(bid);
        }
    }
}
