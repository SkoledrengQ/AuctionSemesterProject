using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            List<Address> addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }

        // GET: api/Address/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
                return NotFound();

            return Ok(address);
        }

        // POST: api/Address
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            await _addressService.CreateAddressAsync(address);
            return CreatedAtAction(nameof(GetAddressById), new { id = address.AddressID }, address);
        }

        // PUT: api/Address/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] Address address)
        {
            await _addressService.UpdateAddressAsync(id, address);
            return NoContent();
        }

        // DELETE: api/Address/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            await _addressService.DeleteAddressAsync(id);
            return NoContent();
        }
    }
}
