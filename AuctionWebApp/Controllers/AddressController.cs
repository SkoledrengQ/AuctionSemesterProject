using AuctionSemesterProject.Services;
using AuctionSemesterProject.AuctionModels;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null) return NotFound();
            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            await _addressService.CreateAddressAsync(address);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Address address)
        {
            await _addressService.UpdateAddressAsync(id, address);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _addressService.DeleteAddressAsync(id);
            return Ok();
        }
    }
}
