namespace API.Dtos;

public class BidDto(decimal amount, int memberId, int auctionId)
{
    public decimal Amount { get; init; } = amount;

    public int MemberID { get; init; } = memberId;

    public int AuctionID { get; init; } = auctionId;
}
