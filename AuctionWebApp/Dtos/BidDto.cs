namespace API.Dtos;

public class BidDto
{
    public BidDto(decimal amount, int memberId, int auctionId)
    {
        Amount = amount;
        MemberID = memberId;
        AuctionID = auctionId;
    }

    public decimal Amount { get; init; }

    public int MemberID { get; init; }

    public int AuctionID { get; init; }
}
