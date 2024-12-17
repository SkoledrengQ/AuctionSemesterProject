namespace WebApp.Models
{
    public class AuctionDetailsViewModel
    {
        // Auction Information
        public int AuctionID { get; set; }
        public decimal StartPrice { get; set; }
        public decimal MinBid { get; set; }
        public decimal? EndingBid { get; set; }
        public decimal? CurrentHighestBid { get; set; }
        public decimal? BuyNowPrice { get; set; }
        public int? NoOfBids { get; set; }
        public string? TimeExtension { get; set; }
        public int EmployeeID { get; set; }

        // Auction Item Information
        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
    }
}
