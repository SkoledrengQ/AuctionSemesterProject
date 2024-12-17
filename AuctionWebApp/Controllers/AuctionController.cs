namespace API.Controllers;

using API.Dtos;
using API.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuctionController(AuctionLogic auctionLogic) : ControllerBase
{
    private readonly AuctionLogic _auctionLogic = auctionLogic;

    // GET: api/Auction
    // Return a list of auctions with their details
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Fetch all auctions and their details
        var auctionDetails = await _auctionLogic.GetAllAuctionDetailsAsync();
        return Ok(auctionDetails);
    }

    // GET: api/Auction/{id}
    // Return a single auction with its details
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        // Fetch auction details by ID
        var auctionDetails = await _auctionLogic.GetAuctionDetailsByIdAsync(id);
        if (auctionDetails == null)
            return NotFound();

        return Ok(auctionDetails);
    }

    // POST: api/Auction
    // Create a new auction
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuctionDetailsDto auctionDetailsDto)
    {
        // Create a new auction using details
        await _auctionLogic.CreateAuctionAsync(auctionDetailsDto);
        return CreatedAtAction(nameof(Get), new { id = auctionDetailsDto.Auction.AuctionID }, auctionDetailsDto);
    }

    // PUT: api/Auction/{id}
    // Update an existing auction
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AuctionDetailsDto auctionDetailsDto)
    {
        var success = await _auctionLogic.UpdateAuctionAsync(id, auctionDetailsDto);
        if (!success)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/Auction/{id}
    // Delete an auction by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _auctionLogic.DeleteAuctionAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
