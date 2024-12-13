using System.ComponentModel.DataAnnotations;
namespace AuctionSemesterProject.AuctionModels;
public class AuctionItem
{
    [Key]
    public int ItemID { get; set; }

    [StringLength(100)]
    public string? Title { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ReleaseDate { get; set; }

    [StringLength(100)]
    public string? Author { get; set; }

    [StringLength(50)]
    public string? Genre { get; set; }

    public string? Description { get; set; }

    [StringLength(50)]
    public string? ItemType { get; set; }
}
