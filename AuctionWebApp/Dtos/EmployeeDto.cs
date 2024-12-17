namespace API.Dtos;

public class EmployeeDto(int employeeId, string? firstName, string? lastName, string? phoneNo, string? email)
{
    public int EmployeeID { get; init; } = employeeId;

    public string? FirstName { get; init; } = firstName;

    public string? LastName { get; init; } = lastName;

    public string? PhoneNo { get; init; } = phoneNo;

    public string? Email { get; init; } = email;
}
