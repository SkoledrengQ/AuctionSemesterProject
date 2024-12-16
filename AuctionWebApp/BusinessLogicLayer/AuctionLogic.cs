namespace AuctionSemesterProject.BusinessLogicLayer;

using AuctionSemesterProject.DTO;
using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using API.Dtos;

public class AuctionLogic
{
    private readonly IAuctionAccess _auctionAccess;

    public AuctionLogic(IAuctionAccess auctionAccess)
    {
        _auctionAccess = auctionAccess;
    }

    public async Task<List<AuctionDto>> GetAllAuctionsAsync()
    {
        var auctions = await _auctionAccess.GetAllAuctionsAsync();
        return auctions.Select(a => new AuctionDto(
            a.AuctionID,
            a.StartPrice,
            a.MinBid,
            a.EndingBid,
            a.CurrentHighestBid,
            a.BuyNowPrice,
            a.NoOfBids,
            a.TimeExtension,
            a.EmployeeID_FK,
            a.ItemID_FK
        )).ToList();
    }

    public async Task<AuctionDto?> GetAuctionByIdAsync(int id)
    {
        var auction = await _auctionAccess.GetAuctionByIdAsync(id);
        if (auction == null) return null;

        return new AuctionDto(
            auction.AuctionID,
            auction.StartPrice,
            auction.MinBid,
            auction.EndingBid,
            auction.CurrentHighestBid,
            auction.BuyNowPrice,
            auction.NoOfBids,
            auction.TimeExtension,
            auction.EmployeeID_FK,
            auction.ItemID_FK
        );
    }

    public async Task CreateAuctionAsync(AuctionDto auctionDto)
    {
        var auction = new Auction
        {
            StartPrice = auctionDto.StartPrice,
            MinBid = auctionDto.MinBid,
            EndingBid = auctionDto.EndingBid,
            CurrentHighestBid = auctionDto.CurrentHighestBid,
            BuyNowPrice = auctionDto.BuyNowPrice,
            NoOfBids = auctionDto.NoOfBids,
            TimeExtension = auctionDto.TimeExtension,
            EmployeeID_FK = auctionDto.EmployeeID,
            ItemID_FK = auctionDto.ItemID
        };

        await _auctionAccess.CreateAuctionAsync(auction);
    }

    public async Task<bool> UpdateAuctionAsync(int id, AuctionDto auctionDto)
    {
        var auction = await _auctionAccess.GetAuctionByIdAsync(id);
        if (auction == null) return false;

        auction.StartPrice = auctionDto.StartPrice;
        auction.MinBid = auctionDto.MinBid;
        auction.EndingBid = auctionDto.EndingBid;
        auction.CurrentHighestBid = auctionDto.CurrentHighestBid;
        auction.BuyNowPrice = auctionDto.BuyNowPrice;
        auction.NoOfBids = auctionDto.NoOfBids;
        auction.TimeExtension = auctionDto.TimeExtension;
        auction.EmployeeID_FK = auctionDto.EmployeeID;
        auction.ItemID_FK = auctionDto.ItemID;

        return await _auctionAccess.UpdateAuctionWithConcurrencyCheckAsync(auction);
    }

    public async Task<bool> DeleteAuctionAsync(int id)
    {
        return await _auctionAccess.DeleteAuctionAsync(id);
    }
}
