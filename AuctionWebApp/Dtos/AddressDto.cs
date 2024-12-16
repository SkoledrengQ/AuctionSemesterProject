namespace API.Dtos;

public class AddressDto(int addressId, string? streetName, string? city, string? zipCode)
{
    public int AddressID { get; init; } = addressId;

    public string? StreetName { get; init; } = streetName;

    public string? City { get; init; } = city;

    public string? ZipCode { get; init; } = zipCode;
}
