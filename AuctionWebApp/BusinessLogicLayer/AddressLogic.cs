namespace AuctionSemesterProject.BusinessLogicLayer;
using AuctionSemesterProject.Interfaces;
using AuctionSemesterProject.DTO;
using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;

public class AddressLogic
{
    private readonly IAddressAccess _addressAccess;

    public AddressLogic(IAddressAccess addressAccess)
    {
        _addressAccess = addressAccess;
    }

    public async Task<List<AddressDto>> GetAllAddressesAsync()
    {
        var addresses = await _addressAccess.GetAllAddressesAsync();
        return addresses.Select(a => new AddressDto(
            a.AddressID,
            a.StreetName,
            a.City,
            a.ZipCode
        )).ToList();
    }

    public async Task<AddressDto?> GetAddressByIdAsync(int id)
    {
        var address = await _addressAccess.GetAddressByIdAsync(id);
        if (address == null) return null;

        return new AddressDto(
            address.AddressID,
            address.StreetName,
            address.City,
            address.ZipCode
        );
    }

    public async Task CreateAddressAsync(AddressDto addressDto)
    {
        var address = new Address
        {
            StreetName = addressDto.StreetName,
            City = addressDto.City,
            ZipCode = addressDto.ZipCode
        };

        await _addressAccess.CreateAddressAsync(address);
    }

    public async Task UpdateAddressAsync(int id, AddressDto addressDto)
    {
        var address = await _addressAccess.GetAddressByIdAsync(id);

        address.StreetName = addressDto.StreetName;
        address.City = addressDto.City;
        address.ZipCode = addressDto.ZipCode;

        await _addressAccess.UpdateAddressAsync(address);
    }

    public async Task<bool> DeleteAddressAsync(int id)
    {
        return await _addressAccess.DeleteAddressAsync(id);
    }
}
