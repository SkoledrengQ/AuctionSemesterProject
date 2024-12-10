using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        // GET: api/Auction
        [HttpGet]
        public async Task<IActionResult> GetAllAuctions()
        {
            List<Auction> auctions = await _auctionService.GetAllAuctionsAsync();
            return Ok(auctions);
        }

        // GET: api/Auction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionById(int id)
        {
            Auction? auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null)
                return NotFound();

            return Ok(auction);
        }

        // POST: api/Auction
        [HttpPost]
        public async Task<IActionResult> CreateAuction([FromBody] Auction auction)
        {
            await _auctionService.CreateAuctionAsync(auction);
            return CreatedAtAction(nameof(GetAuctionById), new { id = auction.AuctionID }, auction);
        }

        // PUT: api/Auction/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuction(int id, [FromBody] Auction auction)
        {
            await _auctionService.UpdateAuctionAsync(id, auction);
            return NoContent();
        }

        // DELETE: api/Auction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            await _auctionService.DeleteAuctionAsync(id);
            return NoContent();
        }
    }
}
