using AuctionSemesterProject.AuctionModels;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSemesterProject.Controllers
{
    public class MemberController : Controller
    {
        // GET: /Member/Register
        [HttpGet]
        public IActionResult Register()
        {
            // Pass a new Member object to the view to prevent 'Model is null'
            var member = new Member();
            return View(member);
        }

        // POST: /Member/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Member member)
        {
            if (ModelState.IsValid)
            {
                // Handle the registration logic (e.g., saving the member to the database)
                // You can add code here to save the member to your database or other logic.

                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Index", "Home");  // Redirect to home or login page after successful registration.
            }

            // If model is invalid, return the same model to the view to display errors.
            return View(member);
        }
    }
}


