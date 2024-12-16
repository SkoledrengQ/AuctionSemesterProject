namespace API.Dtos;

public class EmployeeDto
{
    public EmployeeDto(int employeeId, string? firstName, string? lastName, string? phoneNo, string? email)
    {
        EmployeeID = employeeId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNo = phoneNo;
        Email = email;
    }

    public int EmployeeID { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? PhoneNo { get; init; }

    public string? Email { get; init; }
}
