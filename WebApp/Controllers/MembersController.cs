using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    public class MembersController : Controller
    {
        private readonly MemberService _memberService;

        public MembersController(MemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: Members/Index
        public async Task<IActionResult> Index()
        {
            // Check if the user is logged in by verifying the session or authentication claims
            int memberId = GetLoggedInMemberId();  // Replace with actual logic to fetch the logged-in user's MemberID

            if (memberId == 0) // If the member is not logged in
            {
                return RedirectToAction("Index", "Home"); // Redirect to the Home/Index page
            }

            // Fetch member details from the service using the memberId
            var member = await _memberService.GetMemberByIdAsync(memberId);

            if (member == null)
            {
                return RedirectToAction("Index", "Home"); // If no member data is found, redirect to Home/Index
            }

            // Return the member data to the view (Members/Index.cshtml)
            return View(member);
        }

        // Helper method to get the logged-in user's MemberID
        private int GetLoggedInMemberId()
        {
            // For demonstration purposes, we will just return 1, which should be replaced with actual login logic.
            // If using session, cookie, or claims-based authentication, fetch the logged-in user's memberId here.
            // Example: return HttpContext.Session.GetInt32("MemberID") ?? 0;
            return 0;  // Returning 0 to simulate "not logged in". Replace with real logic.
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View(); // This will look for Views/Members/Create.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                // Call your service to add the new member (this logic depends on your implementation)
                await _memberService.CreateMemberAsync(member); // This should be implemented in the MemberService

                // Redirect to the newly created member's profile page
                return RedirectToAction("Index", "Members", new { id = member.MemberID });
            }
            return View(member); // If validation fails, return the Create view with the model to show errors
        }


        // GET: Members/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }

        // POST: Members/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member)
        {
            if (id != member.MemberID)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _memberService.UpdateMemberAsync(id, member);
                return RedirectToAction(nameof(Index)); // Redirect back to their own details page
            }

            return View(member);
        }

        // GET: Members/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }

        // POST: Members/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _memberService.DeleteMemberAsync(id);
            return RedirectToAction(nameof(Index)); // Redirect to their own details page
        }
    }
}
