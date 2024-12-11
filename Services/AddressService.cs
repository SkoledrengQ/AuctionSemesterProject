using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Services
{
    public class AddressService
    {
        private readonly AddressDAO _addressDAO;

        public AddressService(AddressDAO addressDAO)
        {
            _addressDAO = addressDAO;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await _addressDAO.GetAllAddressesAsync();
        }

        public async Task<Address?> GetAddressByIdAsync(int id)
        {
            return await _addressDAO.GetAddressByIdAsync(id);
        }

        public async Task CreateAddressAsync(Address address)
        {
            await _addressDAO.CreateAddressAsync(address);
        }

        public async Task UpdateAddressAsync(int id, Address address)
        {
            await _addressDAO.UpdateAddressAsync(id, address);
        }

        public async Task DeleteAddressAsync(int id)
        {
            await _addressDAO.DeleteAddressAsync(id);
        }
    }
}
