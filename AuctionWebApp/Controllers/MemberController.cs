using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberService _memberService;

        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: api/Member
        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        // GET: api/Member/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        // POST: api/Member
        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] Member member)
        {
            await _memberService.CreateMemberAsync(member);
            return CreatedAtAction(nameof(GetMemberById), new { id = member.MemberID }, member);
        }

        // PUT: api/Member/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] Member member)
        {
            await _memberService.UpdateMemberAsync(id, member);
            return NoContent();
        }

        // DELETE: api/Member/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            await _memberService.DeleteMemberAsync(id);
            return NoContent();
        }
    }
}
