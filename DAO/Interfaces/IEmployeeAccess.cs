using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.AuctionModels;

namespace AuctionSemesterProject.Interfaces
{
    public interface IEmployeeAccess
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id); // Changed to return Task<bool>
    }
}
