namespace API.Dtos;

public class MemberDto
{
    public MemberDto(int memberId, string? firstName, string? lastName, DateTime? birthday, string? phoneNo, string? email, int addressId)
    {
        MemberID = memberId;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
        PhoneNo = phoneNo;
        Email = email;
        AddressID = addressId;
    }

    public int MemberID { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public DateTime? Birthday { get; init; }

    public string? PhoneNo { get; init; }

    public string? Email { get; init; }

    public int AddressID { get; init; }
}
