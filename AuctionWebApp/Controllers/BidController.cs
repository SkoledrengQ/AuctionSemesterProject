namespace AuctionSemesterProject.Controllers;

using API.Dtos;
using AuctionSemesterProject.BusinessLogicLayer;
using AuctionSemesterProject.DTO;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BidController : ControllerBase
{
    private readonly BidLogic _bidLogic;

    public BidController(BidLogic bidLogic)
    {
        _bidLogic = bidLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bids = await _bidLogic.GetAllBidsAsync();
        return Ok(bids);
    }

    [HttpGet("{auctionId}/{memberId}")]
    public async Task<IActionResult> Get(int auctionId, int memberId)
    {
        var bid = await _bidLogic.GetBidByIdAsync(auctionId, memberId);
        if (bid == null) return NotFound();
        return Ok(bid);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BidDto bidDto)
    {
        await _bidLogic.CreateBidAsync(bidDto);
        return CreatedAtAction(nameof(Get), new { auctionId = bidDto.AuctionID, memberId = bidDto.MemberID }, bidDto);
    }

    [HttpPut("{auctionId}/{memberId}")]
    public async Task<IActionResult> Update(int auctionId, int memberId, [FromBody] BidDto bidDto)
    {
        var success = await _bidLogic.UpdateBidAsync(auctionId, memberId, bidDto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{auctionId}/{memberId}")]
    public async Task<IActionResult> Delete(int auctionId, int memberId)
    {
        var success = await _bidLogic.DeleteBidAsync(auctionId, memberId);
        if (!success) return NotFound();
        return NoContent();
    }
}
