using Microsoft.AspNetCore.Mvc;

namespace AuctionSemesterProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new { Status = "API is running", Timestamp = DateTime.UtcNow });
        }
    }
}
