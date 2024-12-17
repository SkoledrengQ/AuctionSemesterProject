namespace API.Controllers;

using API.Dtos;
using API.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using AuctionModels;

[Route("api/[controller]")]
[ApiController]
public class BidController(BidLogic bidLogic) : ControllerBase
{
    private readonly BidLogic _bidLogic = bidLogic;

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
	public async Task<IActionResult> CreateBidAsync([FromBody] BidDto bidDto)
	{
		var bid = new Bid
		{
			Amount = bidDto.Amount,
			MemberID_FK = bidDto.MemberID,
			AuctionID_FK = bidDto.AuctionID
		};

		var success = await _bidLogic.CreateBidAsync(bid, bidDto.OldBid);

		if (!success)
			return Conflict("Bid rejected: Another user has already placed a higher bid.");

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
