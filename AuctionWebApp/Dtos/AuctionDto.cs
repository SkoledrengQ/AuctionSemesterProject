namespace API.Dtos;

public class AuctionDto
{
    public AuctionDto(int auctionId, decimal startPrice, decimal minBid, decimal? endingBid, decimal? currentHighestBid, decimal? buyNowPrice, int? noOfBids, string? timeExtension, int employeeId, int itemId)
    {
        AuctionID = auctionId;
        StartPrice = startPrice;
        MinBid = minBid;
        EndingBid = endingBid;
        CurrentHighestBid = currentHighestBid;
        BuyNowPrice = buyNowPrice;
        NoOfBids = noOfBids;
        TimeExtension = timeExtension;
        EmployeeID = employeeId;
        ItemID = itemId;
    }

    public int AuctionID { get; init; }

    public decimal StartPrice { get; init; }

    public decimal MinBid { get; init; }

    public decimal? EndingBid { get; init; }

    public decimal? CurrentHighestBid { get; init; }

    public decimal? BuyNowPrice { get; init; }

    public int? NoOfBids { get; init; }

    public string? TimeExtension { get; init; }

    public int EmployeeID { get; init; }

    public int ItemID { get; init; }
}
