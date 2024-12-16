using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.AuctionModels;

namespace AuctionSemesterProject.Interfaces
{
    public interface IMemberAccess
    {
        Task<List<Member>> GetAllMembersAsync();
        Task<Member?> GetMemberByIdAsync(int id);
        Task CreateMemberAsync(Member member);
        Task UpdateMemberAsync(Member member);
        Task<bool> DeleteMemberAsync(int id); // Changed to return Task<bool>
    }
}
