using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionWebApp.Models
{
    public class Bid
    {
        [Key]
        public int BidID { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; } // Foreign key to Products

        public string Bidder { get; set; }
        public decimal BidAmount { get; set; }
    }
}
