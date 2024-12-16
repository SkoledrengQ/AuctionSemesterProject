namespace AuctionSemesterProject.Controllers;

using AuctionSemesterProject.BusinessLogicLayer;
using AuctionSemesterProject.DTO;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeLogic _employeeLogic;

    public EmployeeController(EmployeeLogic employeeLogic)
    {
        _employeeLogic = employeeLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeLogic.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var employee = await _employeeLogic.GetEmployeeByIdAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
    {
        await _employeeLogic.CreateEmployeeAsync(employeeDto);
        return CreatedAtAction(nameof(Get), new { id = employeeDto.EmployeeID }, employeeDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto employeeDto)
    {
        var success = await _employeeLogic.UpdateEmployeeAsync(id, employeeDto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _employeeLogic.DeleteEmployeeAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
