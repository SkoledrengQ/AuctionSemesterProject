using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Services
{
    public class MemberService
    {
        private readonly MemberDAO _memberDAO;

        public MemberService(MemberDAO memberDAO)
        {
            _memberDAO = memberDAO;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            return await _memberDAO.GetAllMembersAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _memberDAO.GetMemberByIdAsync(id);
        }

        public async Task CreateMemberAsync(Member member)
        {
            await _memberDAO.CreateMemberAsync(member);
        }

        public async Task UpdateMemberAsync(int id, Member member)
        {
            await _memberDAO.UpdateMemberAsync(member);
        }

        public async Task DeleteMemberAsync(int id)
        {
            await _memberDAO.DeleteMemberAsync(id);
        }
    }
}
