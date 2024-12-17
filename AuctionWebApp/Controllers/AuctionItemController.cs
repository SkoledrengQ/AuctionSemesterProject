namespace API.Controllers;

using API.BusinessLogicLayer;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuctionItemController(AuctionItemLogic auctionItemLogic) : ControllerBase
{
    private readonly AuctionItemLogic _auctionItemLogic = auctionItemLogic;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _auctionItemLogic.GetAllAuctionItemsAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _auctionItemLogic.GetAuctionItemByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuctionItemDto auctionItemDto)
    {
        await _auctionItemLogic.CreateAuctionItemAsync(auctionItemDto);
        return CreatedAtAction(nameof(Get), new { id = auctionItemDto.ItemID }, auctionItemDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AuctionItemDto auctionItemDto)
    {
        await _auctionItemLogic.UpdateAuctionItemAsync(id, auctionItemDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _auctionItemLogic.DeleteAuctionItemAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
