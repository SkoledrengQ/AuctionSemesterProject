namespace API.BusinessLogicLayer;

using AuctionModels;
using DataAccess.Interfaces;
using API.Dtos;

public class BidLogic(IBidAccess bidAccess)
{
	private readonly IBidAccess _bidAccess = bidAccess;

	public async Task<List<BidDto>> GetAllBidsAsync()
	{
		var bids = await _bidAccess.GetAllBidsAsync();
		return bids.Select(b => new BidDto(
		b.Amount,
		b.MemberID_FK,
		b.AuctionID_FK,
		b.Auction?.CurrentHighestBid ?? 0 // OldBid set to currentHighestBid
	)).ToList();


	}

	public async Task<BidDto?> GetBidByIdAsync(int auctionId, int memberId)
	{
		var bid = await _bidAccess.GetBidByIdAsync(auctionId, memberId);
		if (bid == null) return null;

		return new BidDto(
		bid.Amount,
		bid.MemberID_FK,
		bid.AuctionID_FK,
		bid.Auction?.CurrentHighestBid ?? 0 // OldBid set to currentHighestBid
	);


	}

	public async Task<bool> CreateBidAsync(Bid bid, decimal oldBid)
	{
		return await _bidAccess.CreateBidAsync(bid, oldBid);
	}


	public async Task<bool> UpdateBidAsync(int auctionId, int memberId, BidDto bidDto)
	{
		var bid = await _bidAccess.GetBidByIdAsync(auctionId, memberId);
		if (bid == null) return false;

		bid.Amount = bidDto.Amount;

		await _bidAccess.UpdateBidAsync(bid);
		return true;
	}

	public async Task<bool> DeleteBidAsync(int auctionId, int memberId)
	{
		return await _bidAccess.DeleteBidAsync(auctionId, memberId);
	}
}
