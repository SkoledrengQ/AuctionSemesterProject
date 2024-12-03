using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Key]
    public int EmployeeID { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(20)]
    public string? PhoneNo { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }
}
