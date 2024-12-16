namespace API.Dtos;

public class AddressDto
{
    public AddressDto(int addressId, string? streetName, string? city, string? zipCode)
    {
        AddressID = addressId;
        StreetName = streetName;
        City = city;
        ZipCode = zipCode;
    }

    public int AddressID { get; init; }

    public string? StreetName { get; init; }

    public string? City { get; init; }

    public string? ZipCode { get; init; }
}
