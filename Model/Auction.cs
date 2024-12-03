using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Auction
{
    [Key]
    public int AuctionID { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal StartPrice { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal MinBid { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? EndingBid { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CurrentHighestBid { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? BuyNowPrice { get; set; }

    public int? NoOfBids { get; set; }

    [StringLength(50)]
    public string? TimeExtension { get; set; }

    [Required]
    public int EmployeeID_FK { get; set; }

    [Required]
    public int ItemID_FK { get; set; }

    [ForeignKey(nameof(EmployeeID_FK))]
    public virtual Employee? Employee { get; set; }

    [ForeignKey(nameof(ItemID_FK))]
    public virtual AuctionItem? AuctionItem { get; set; }
}
