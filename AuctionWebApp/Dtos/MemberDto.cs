namespace API.Dtos;

public class MemberDto(int memberId, string? firstName, string? lastName, DateTime? birthday, string? phoneNo, string? email, int addressId)
{
    public int MemberID { get; init; } = memberId;

    public string? FirstName { get; init; } = firstName;

    public string? LastName { get; init; } = lastName;

    public DateTime? Birthday { get; init; } = birthday;

    public string? PhoneNo { get; init; } = phoneNo;

    public string? Email { get; init; } = email;

    public int AddressID { get; init; } = addressId;
}
