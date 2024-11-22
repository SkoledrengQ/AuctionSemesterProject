using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Models
{
    public class Products
    {
        public int ID { get; set; }

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
    }

}
