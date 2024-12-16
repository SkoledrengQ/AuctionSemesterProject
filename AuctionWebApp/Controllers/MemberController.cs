namespace AuctionSemesterProject.Controllers;

using API.Dtos;
using AuctionSemesterProject.BusinessLogicLayer;
using AuctionSemesterProject.DTO;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MemberController : ControllerBase
{
    private readonly MemberLogic _memberLogic;

    public MemberController(MemberLogic memberLogic)
    {
        _memberLogic = memberLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var members = await _memberLogic.GetAllMembersAsync();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var member = await _memberLogic.GetMemberByIdAsync(id);
        if (member == null) return NotFound();
        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MemberDto memberDto)
    {
        await _memberLogic.CreateMemberAsync(memberDto);
        return CreatedAtAction(nameof(Get), new { id = memberDto.MemberID }, memberDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MemberDto memberDto)
    {
        var success = await _memberLogic.UpdateMemberAsync(id, memberDto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _memberLogic.DeleteMemberAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
