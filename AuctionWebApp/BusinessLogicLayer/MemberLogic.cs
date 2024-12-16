namespace AuctionSemesterProject.BusinessLogicLayer;

using AuctionSemesterProject.DTO;
using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using API.Dtos;

public class MemberLogic
{
    private readonly IMemberAccess _memberAccess;

    public MemberLogic(IMemberAccess memberAccess)
    {
        _memberAccess = memberAccess;
    }

    public async Task<List<MemberDto>> GetAllMembersAsync()
    {
        var members = await _memberAccess.GetAllMembersAsync();
        return members.Select(m => new MemberDto(
            m.MemberID,
            m.FirstName,
            m.LastName,
            m.Birthday,
            m.PhoneNo,
            m.Email,
            m.AddressID_FK
        )).ToList();
    }

    public async Task<MemberDto?> GetMemberByIdAsync(int id)
    {
        var member = await _memberAccess.GetMemberByIdAsync(id);
        if (member == null) return null;

        return new MemberDto(
            member.MemberID,
            member.FirstName,
            member.LastName,
            member.Birthday,
            member.PhoneNo,
            member.Email,
            member.AddressID_FK
        );
    }

    public async Task CreateMemberAsync(MemberDto memberDto)
    {
        var member = new Member
        {
            FirstName = memberDto.FirstName,
            LastName = memberDto.LastName,
            Birthday = memberDto.Birthday,
            PhoneNo = memberDto.PhoneNo,
            Email = memberDto.Email,
            AddressID_FK = memberDto.AddressID
        };

        await _memberAccess.CreateMemberAsync(member);
    }

    public async Task<bool> UpdateMemberAsync(int id, MemberDto memberDto)
    {
        var member = await _memberAccess.GetMemberByIdAsync(id);
        if (member == null) return false;

        member.FirstName = memberDto.FirstName;
        member.LastName = memberDto.LastName;
        member.Birthday = memberDto.Birthday;
        member.PhoneNo = memberDto.PhoneNo;
        member.Email = memberDto.Email;
        member.AddressID_FK = memberDto.AddressID;

        await _memberAccess.UpdateMemberAsync(member);
        return true;
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        return await _memberAccess.DeleteMemberAsync(id);
    }
}
