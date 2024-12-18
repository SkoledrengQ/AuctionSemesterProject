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

	[HttpGet("{bidId}")]
	public async Task<IActionResult> Get(int bidId)
	{
		var bid = await _bidLogic.GetBidByIdAsync(bidId);
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

		var bidId = await _bidLogic.CreateBidAsync(bid, bidDto.OldBid);

		if (bidId == 0) // Conflict
			return Conflict("Bid rejected: Another user has already placed a higher bid.");

		// Return the created bid with the new BidID
		return CreatedAtAction(nameof(Get), new { bidId = bidId }, bidDto);
	}

	[HttpPut("{bidId}")]
	public async Task<IActionResult> Update(int bidId, [FromBody] BidDto bidDto)
	{
		var success = await _bidLogic.UpdateBidAsync(bidId, bidDto);
		if (!success) return NotFound();
		return NoContent();
	}

	[HttpDelete("{bidId}")]
	public async Task<IActionResult> Delete(int bidId)
	{
		var success = await _bidLogic.DeleteBidAsync(bidId);
		if (!success) return NotFound();
		return NoContent();
	}
}
