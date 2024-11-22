namespace AuctionWebApp.Models
{
    public class Bid
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Bidder { get; set; }
        public decimal BidAmount { get; set; }
    }
}
