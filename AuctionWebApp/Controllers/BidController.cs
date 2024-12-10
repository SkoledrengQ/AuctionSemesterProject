using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> GetAllBids()
        {
            List<Bid> bids = await _bidService.GetAllBidsAsync();
            return Ok(bids);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBidById(int id)
        {
            Bid? bid = await _bidService.GetBidByIdAsync(id);
            if (bid == null)
                return NotFound();

            return Ok(bid);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBid(decimal newBid, int auctionID, int memberID)
        {
            await _bidService.CreateBidAsync(newBid, auctionID, memberID);
            return Ok(new { Message = "Bid placed successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] Bid bid)
        {
            await _bidService.UpdateBidAsync(id, bid);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            await _bidService.DeleteBidAsync(id);
            return NoContent();
        }
    }
}
