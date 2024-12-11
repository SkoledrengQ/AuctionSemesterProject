using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index action - returns the homepage view
        public IActionResult Index()
        {
            _logger.LogInformation("Index page accessed.");
            return View();
        }


        // Privacy action - returns the privacy page view
        public IActionResult Privacy()
        {
            return View();
        }

        // Error handling - returns the error view with an error model
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Returns the CreateAuction view
        public IActionResult CreateAuction()
        {
            return View();
        }

    }
}
