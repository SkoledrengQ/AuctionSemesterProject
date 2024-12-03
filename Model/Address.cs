using System.ComponentModel.DataAnnotations;
namespace AuctionSemesterProject.AuctionModels;
public class Address
{
    [Key]
    public int AddressID { get; set; }

    [StringLength(100)]
    public string? StreetName { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(10)]
    public string? ZipCode { get; set; }
}
