﻿namespace API.BusinessLogicLayer;

using API.Dtos;
using AuctionModels;
using DataAccess.Interfaces;

public class AuctionItemLogic(IAuctionItemAccess auctionItemAccess)
{
    private readonly IAuctionItemAccess _auctionItemAccess = auctionItemAccess;

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

    public async Task UpdateAuctionItemAsync(int id, AuctionItemDto auctionItemDto)
    {
        var item = await _auctionItemAccess.GetAuctionItemByIdAsync(id);
        

        item.Title = auctionItemDto.Title;
        item.ReleaseDate = auctionItemDto.ReleaseDate;
        item.Author = auctionItemDto.Author;
        item.Genre = auctionItemDto.Genre;
        item.Description = auctionItemDto.Description;

        await _auctionItemAccess.UpdateAuctionItemAsync(item);
    }

    public async Task<bool> DeleteAuctionItemAsync(int id)
    {
        return await _auctionItemAccess.DeleteAuctionItemAsync(id);
    }
}
