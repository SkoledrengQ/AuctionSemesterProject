using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Member
{
    [Key]
    public int MemberID { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Birthday { get; set; }

    [StringLength(20)]
    public string? PhoneNo { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [Required]
    public int AddressID_FK { get; set; }

    [ForeignKey(nameof(AddressID_FK))]
    public virtual Address? Address { get; set; }
}
