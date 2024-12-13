using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.Services
{
    public class EmployeeService
    {
        private readonly EmployeeDAO _employeeDAO;

        public EmployeeService(EmployeeDAO employeeDAO)
        {
            _employeeDAO = employeeDAO;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeDAO.GetAllEmployeesAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _employeeDAO.GetEmployeeByIdAsync(id);
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _employeeDAO.CreateEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            await _employeeDAO.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeDAO.DeleteEmployeeAsync(id);
        }
    }
}
