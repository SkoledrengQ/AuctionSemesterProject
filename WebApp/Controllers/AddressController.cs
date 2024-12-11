using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            List<Address> addresses = await _addressService.GetAllAddressesAsync();
            return View(addresses);
        }

        // GET: Address/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            Address? address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
                return NotFound();

            return View(address);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                await _addressService.CreateAddressAsync(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Address/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            Address? address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
                return NotFound();

            return View(address);
        }

        // POST: Address/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Address address)
        {
            if (id != address.AddressID)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _addressService.UpdateAddressAsync(id, address);
                return RedirectToAction(nameof(Index));
            }

            return View(address);
        }

        // GET: Address/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            Address? address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
                return NotFound();

            return View(address);
        }

        // POST: Address/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _addressService.DeleteAddressAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
