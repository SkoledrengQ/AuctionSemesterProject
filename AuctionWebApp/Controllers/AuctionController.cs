namespace API.Controllers;

using API.Dtos;
using API.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuctionController(AuctionLogic auctionLogic) : ControllerBase
{
    private readonly AuctionLogic _auctionLogic = auctionLogic;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var auctions = await _auctionLogic.GetAllAuctionsAsync();
        return Ok(auctions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var auction = await _auctionLogic.GetAuctionByIdAsync(id);
        if (auction == null) return NotFound();
        return Ok(auction);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuctionDto auctionDto)
    {
        await _auctionLogic.CreateAuctionAsync(auctionDto);
        return CreatedAtAction(nameof(Get), new { id = auctionDto.AuctionID }, auctionDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AuctionDto auctionDto)
    {
        var success = await _auctionLogic.UpdateAuctionAsync(id, auctionDto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _auctionLogic.DeleteAuctionAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
