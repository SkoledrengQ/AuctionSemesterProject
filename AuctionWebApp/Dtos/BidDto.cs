namespace API.Dtos;

public class BidDto(decimal amount, int memberId, int auctionId, decimal oldBid)
{
	public decimal Amount { get; init; } = amount;
	public int MemberID { get; init; } = memberId;
	public int AuctionID { get; init; } = auctionId;
	public decimal OldBid { get; init; } = oldBid; 
}
