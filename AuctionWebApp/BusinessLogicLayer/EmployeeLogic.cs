namespace AuctionSemesterProject.BusinessLogicLayer;
using AuctionSemesterProject.DTO;
using AuctionSemesterProject.AuctionModels;
using AuctionSemesterProject.DataAccess;
using AuctionSemesterProject.Interfaces;

public class EmployeeLogic
{
    private readonly IEmployeeAccess _employeeAccess;

    public EmployeeLogic(IEmployeeAccess employeeAccess)
    {
        _employeeAccess = employeeAccess;
    }

    public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeeAccess.GetAllEmployeesAsync();
        return employees.Select(e => new EmployeeDto(
            e.EmployeeID,
            e.FirstName,
            e.LastName,
            e.PhoneNo,
            e.Email
        )).ToList();
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _employeeAccess.GetEmployeeByIdAsync(id);
        if (employee == null) return null;

        return new EmployeeDto(
            employee.EmployeeID,
            employee.FirstName,
            employee.LastName,
            employee.PhoneNo,
            employee.Email
        );
    }

    public async Task CreateEmployeeAsync(EmployeeDto employeeDto)
    {
        var employee = new Employee
        {
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            PhoneNo = employeeDto.PhoneNo,
            Email = employeeDto.Email
        };

        await _employeeAccess.CreateEmployeeAsync(employee);
    }

    public async Task<bool> UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
    {
        var employee = await _employeeAccess.GetEmployeeByIdAsync(id);
        if (employee == null) return false;

        employee.FirstName = employeeDto.FirstName;
        employee.LastName = employeeDto.LastName;
        employee.PhoneNo = employeeDto.PhoneNo;
        employee.Email = employeeDto.Email;

        await _employeeAccess.UpdateEmployeeAsync(employee);
        return true;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        return await _employeeAccess.DeleteEmployeeAsync(id);
    }
}
