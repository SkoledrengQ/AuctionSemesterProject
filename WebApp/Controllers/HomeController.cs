using Microsoft.AspNetCore.Mvc;

namespace AuctionSemesterProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
