using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionWebApp.Models
{
    public class Products
    {
        [Key]
        public int ID { get; set; } // Primary key

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Author { get; set; }

        [MaxLength(20)]
        public string ISBN { get; set; }

        [MaxLength(30)]
        public string Genre { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Type { get; set; }
        public decimal CurrentHighestBid { get; set; }
        public DateTime? AuctionStartDate { get; set; }
        public DateTime? AuctionEndDate { get; set; }

        // Navigation for related bids
        public List<Bid> Bids { get; set; } = new List<Bid>();
    }
}
