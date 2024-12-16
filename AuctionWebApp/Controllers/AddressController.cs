namespace API.Controllers;

using API.BusinessLogicLayer;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly AddressLogic _addressLogic;

    public AddressController(AddressLogic addressLogic)
    {
        _addressLogic = addressLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var addresses = await _addressLogic.GetAllAddressesAsync();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var address = await _addressLogic.GetAddressByIdAsync(id);
        if (address == null) return NotFound();
        return Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddressDto addressDto)
    {
        await _addressLogic.CreateAddressAsync(addressDto);
        return CreatedAtAction(nameof(Get), new { id = addressDto.AddressID }, addressDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AddressDto addressDto)
    {
        await _addressLogic.UpdateAddressAsync(id, addressDto);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _addressLogic.DeleteAddressAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
