using Microsoft.AspNetCore.Mvc;

public class BidController : Controller
{
    private readonly BidService _bidService;

    public BidController(BidService bidService)
    {
        _bidService = bidService;
    }

    public async Task<IActionResult> Index(int auctionID)
    {
        var bids = await _bidService.GetBidsForAuctionAsync(auctionID);
        return View(bids);
    }
}
