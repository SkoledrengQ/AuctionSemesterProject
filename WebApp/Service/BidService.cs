using AuctionSemesterProject.AuctionModels;

public class BidService
{
    private readonly HttpClient _httpClient;

    public BidService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> PlaceBidAsync(decimal newBid, int auctionID, int memberID)
    {
        var bidData = new { newBid, auctionID, memberID };
        var response = await _httpClient.PostAsJsonAsync("api/bid/create", bidData);

        return response.IsSuccessStatusCode;
    }

    public async Task<List<Bid>> GetBidsForAuctionAsync(int auctionID)
    {
        var response = await _httpClient.GetAsync($"api/bid/auction/{auctionID}");
        if (response.IsSuccessStatusCode)
        {
            var bids = await response.Content.ReadFromJsonAsync<List<Bid>>();
            return bids ?? new List<Bid>(); 
        }
        return new List<Bid>();
    }
}
