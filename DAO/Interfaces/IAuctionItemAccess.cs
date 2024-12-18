﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionModels;

namespace DataAccess.Interfaces
{
    public interface IAuctionItemAccess
    {
        Task<List<AuctionItem>> GetAllAuctionItemsAsync();
        Task<AuctionItem?> GetAuctionItemByIdAsync(int id);
        Task CreateAuctionItemAsync(AuctionItem item);
        Task<bool> UpdateAuctionItemAsync(AuctionItem item);
        Task<bool> DeleteAuctionItemAsync(int id);
    }
}
