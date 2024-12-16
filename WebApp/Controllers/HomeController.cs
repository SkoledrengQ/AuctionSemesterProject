using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuctionLogic _auctionLogic;

        public HomeController()
        {
            _auctionLogic = new AuctionLogic();
        }

        // GET: /Home/Index
        public async Task<IActionResult> Index()
        {
            // Fetch active auctions from AuctionLogic
            var auctions = await _auctionLogic.GetActiveAuctionsAsync();
            return View(auctions);
        }

        // GET: /Home/AuctionDetails/{id}
        public async Task<IActionResult> AuctionDetails(int id)
        {
            // Fetch auction details from AuctionLogic
            var auctionDetails = await _auctionLogic.GetAuctionDetailsAsync(id);
            if (auctionDetails == null)
            {
                return NotFound();
            }

            // Pass the auction details to the view
            return View(auctionDetails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
