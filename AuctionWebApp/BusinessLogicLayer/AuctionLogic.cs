namespace API.BusinessLogicLayer;

using API.Dtos;
using AuctionModels;
using DataAccess.Interfaces;

public class AuctionLogic(IAuctionAccess auctionAccess, IAuctionItemAccess auctionItemAccess)
{
    private readonly IAuctionAccess _auctionAccess = auctionAccess;
    private readonly IAuctionItemAccess _auctionItemAccess = auctionItemAccess;

    // Fetch all auctions with their item details
    public async Task<List<AuctionDetailsDto>> GetAllAuctionDetailsAsync()
    {
        var auctions = await _auctionAccess.GetAllAuctionsAsync();

        var auctionDetailsList = new List<AuctionDetailsDto>();

        foreach (var auction in auctions)
        {
            var auctionItem = await _auctionItemAccess.GetAuctionItemByIdAsync(auction.ItemID_FK);

            auctionDetailsList.Add(new AuctionDetailsDto
            {
                Auction = new AuctionDto(
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
                ),
                AuctionItem = new AuctionItemDto(
                    auctionItem.ItemID,
                    auctionItem.Title,
                    auctionItem.ReleaseDate,
                    auctionItem.Author,
                    auctionItem.Genre,
                    auctionItem.Description
                )
            });
        }

        return auctionDetailsList;
    }

    public async Task<AuctionDetailsDto?> GetAuctionDetailsByIdAsync(int id)
    {
        var auction = await _auctionAccess.GetAuctionByIdAsync(id);
        if (auction == null) return null;

        var auctionItem = await _auctionItemAccess.GetAuctionItemByIdAsync(auction.ItemID_FK);

        return new AuctionDetailsDto
        {
            Auction = new AuctionDto(
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
            ),
            AuctionItem = new AuctionItemDto(
                auctionItem.ItemID,
                auctionItem.Title,
                auctionItem.ReleaseDate,
                auctionItem.Author,
                auctionItem.Genre,
                auctionItem.Description
            )
        };
    }

    public async Task CreateAuctionAsync(AuctionDetailsDto auctionDetailsDto)
    {
        var auction = new Auction
        {
            StartPrice = auctionDetailsDto.Auction.StartPrice,
            MinBid = auctionDetailsDto.Auction.MinBid,
            EndingBid = auctionDetailsDto.Auction.EndingBid,
            CurrentHighestBid = auctionDetailsDto.Auction.CurrentHighestBid,
            BuyNowPrice = auctionDetailsDto.Auction.BuyNowPrice,
            NoOfBids = auctionDetailsDto.Auction.NoOfBids,
            TimeExtension = auctionDetailsDto.Auction.TimeExtension,
            EmployeeID_FK = auctionDetailsDto.Auction.EmployeeID,
            ItemID_FK = auctionDetailsDto.Auction.ItemID
        };

        await _auctionAccess.CreateAuctionAsync(auction);
    }

    // Update an existing auction
    public async Task<bool> UpdateAuctionAsync(int id, AuctionDetailsDto auctionDetailsDto)
    {
        var auction = await _auctionAccess.GetAuctionByIdAsync(id);
        if (auction == null) return false;

        auction.StartPrice = auctionDetailsDto.Auction.StartPrice;
        auction.MinBid = auctionDetailsDto.Auction.MinBid;
        auction.EndingBid = auctionDetailsDto.Auction.EndingBid;
        auction.CurrentHighestBid = auctionDetailsDto.Auction.CurrentHighestBid;
        auction.BuyNowPrice = auctionDetailsDto.Auction.BuyNowPrice;
        auction.NoOfBids = auctionDetailsDto.Auction.NoOfBids;
        auction.TimeExtension = auctionDetailsDto.Auction.TimeExtension;
        auction.EmployeeID_FK = auctionDetailsDto.Auction.EmployeeID;
        auction.ItemID_FK = auctionDetailsDto.Auction.ItemID;

        return await _auctionAccess.UpdateAuctionAsync(auction);
    }

    // Delete an auction
    public async Task<bool> DeleteAuctionAsync(int id)
    {
        return await _auctionAccess.DeleteAuctionAsync(id);
    }
}
