namespace AuctionSemesterProject.BusinessLogicLayer;

using AuctionSemesterProject.DTO;
using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;

public class AuctionItemLogic
{
    private readonly IAuctionItemAccess _auctionItemAccess;

    public AuctionItemLogic(IAuctionItemAccess auctionItemAccess)
    {
        _auctionItemAccess = auctionItemAccess;
    }

    public async Task<List<AuctionItemDto>> GetAllAuctionItemsAsync()
    {
        var items = await _auctionItemAccess.GetAllAuctionItemsAsync();
        return items.Select(i => new AuctionItemDto(
            i.ItemID,
            i.Title,
            i.ReleaseDate,
            i.Author,
            i.Genre,
            i.Description
        )).ToList();
    }

    public async Task<AuctionItemDto?> GetAuctionItemByIdAsync(int id)
    {
        var item = await _auctionItemAccess.GetAuctionItemByIdAsync(id);
        if (item == null) return null;

        return new AuctionItemDto(
            item.ItemID,
            item.Title,
            item.ReleaseDate,
            item.Author,
            item.Genre,
            item.Description
        );
    }

    public async Task CreateAuctionItemAsync(AuctionItemDto auctionItemDto)
    {
        var item = new AuctionItem
        {
            Title = auctionItemDto.Title,
            ReleaseDate = auctionItemDto.ReleaseDate,
            Author = auctionItemDto.Author,
            Genre = auctionItemDto.Genre,
            Description = auctionItemDto.Description

        };

        await _auctionItemAccess.CreateAuctionItemAsync(item);
    }

    public async Task<bool> UpdateAuctionItemAsync(int id, AuctionItemDto auctionItemDto)
    {
        var item = await _auctionItemAccess.GetAuctionItemByIdAsync(id);
        if (item == null) return false;

        item.Title = auctionItemDto.Title;
        item.ReleaseDate = auctionItemDto.ReleaseDate;
        item.Author = auctionItemDto.Author;
        item.Genre = auctionItemDto.Genre;
        item.Description = auctionItemDto.Description;

        return await _auctionItemAccess.UpdateAuctionItemAsync(item);
    }

    public async Task<bool> DeleteAuctionItemAsync(int id)
    {
        return await _auctionItemAccess.DeleteAuctionItemAsync(id);
    }
}
