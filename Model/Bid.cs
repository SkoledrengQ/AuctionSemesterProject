using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionModels
{
	public class Bid
	{
		[Key]
		public int BidID { get; set; } 

		[Required]
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Amount { get; set; }

		[Required]
		public int MemberID_FK { get; set; }

		[Required]
		public int AuctionID_FK { get; set; }

		[ForeignKey(nameof(MemberID_FK))]
		public virtual Member? Member { get; set; }

		[ForeignKey(nameof(AuctionID_FK))]
		public virtual Auction? Auction { get; set; }
	}
}
