namespace API.Dtos;

public class AuctionDto(int auctionId, decimal startPrice, decimal minBid, decimal? endingBid, decimal? currentHighestBid, decimal? buyNowPrice, int? noOfBids, string? timeExtension, int employeeId, int itemId)
{
    public int AuctionID { get; init; } = auctionId;

    public decimal StartPrice { get; init; } = startPrice;

    public decimal MinBid { get; init; } = minBid;

    public decimal? EndingBid { get; init; } = endingBid;

    public decimal? CurrentHighestBid { get; init; } = currentHighestBid;

    public decimal? BuyNowPrice { get; init; } = buyNowPrice;

    public int? NoOfBids { get; init; } = noOfBids;

    public string? TimeExtension { get; init; } = timeExtension;

    public int EmployeeID { get; init; } = employeeId;

    public int ItemID { get; init; } = itemId;
}
