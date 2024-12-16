using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.AuctionModels;

namespace AuctionSemesterProject.DataAccess.Interfaces
{
    public interface IAddressAccess
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<Address?> GetAddressByIdAsync(int id);
        Task CreateAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task<bool> DeleteAddressAsync(int id); // Changed to return Task<bool>
    }
}
