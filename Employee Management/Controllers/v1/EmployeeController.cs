using Employee_Management.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Employee_Management.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Annotations;
using Employee_Management.Models.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace Employee_Management.Controllers.v1
{
    /*[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]*/
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class EmployeesController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public EmployeesController(IRepositoryWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "Get all employees")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _repo.Employee.GetFullEmployeesDetailsAsync();
            if (employees == null) { return NotFound(); }
            return Ok(employees.Select(_mapper.Map<EmployeeGetDto>));
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Get employee by Id")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var employee = await _repo.Employee.GetFullEmployeeDetailsByIdAsync(Id);
            if (employee == null) { return NotFound(); }
            return Ok(_mapper.Map<EmployeeGetDto>(employee));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add employee")]
        public async Task<IActionResult> AddEmployee(EmployeeCreateDto employee, int positionId, int departmentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await ValidateId(positionId, departmentId))
                return BadRequest("Invalid PositionId or DepartmentId");
            try
            {
                var employeeMap = _mapper.Map<Employee>(employee);
                employeeMap.PositionId = positionId;
                employeeMap.DepartmentId = departmentId;
                await _repo.Employee.CreateAsync(employeeMap);
                await _repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong. Please try again later.");
            }
            return Created(Request.Path.Value, "Employee added successfully.");
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(Summary = "Upadate employee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdateDto request, int Id, int positionId, int departmentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await ValidateId(positionId, departmentId))
                return BadRequest("Invalid PositionId or DepartmentId");

            var employee = await _repo.Employee.GetFullEmployeeDetailsByIdAsync(Id);

            if (employee != null)
            {
                try
                {
                    var emp = _mapper.Map<Employee>(request);
                    emp.Id = Id;
                    emp.DepartmentId = departmentId;
                    emp.PositionId = positionId;
                    emp.HireDate = employee.HireDate;
                    _repo.Employee.Update(emp);
                    await _repo.SaveChangesAsync();
                    return Ok("Employee updated successfully");
                }
                catch (Exception)
                {
                    return StatusCode(500, "Something went wrong while updating. Please try again later.");
                }
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete employee by Id")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _repo.Employee.GetByIdAsync(id);
            if (employee == null)
                return NotFound();
            try
            {
                _repo.Employee.Delete(employee);
                await _repo.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong while updating. Please try again later.");
            }
        }

        private async Task<bool> ValidateId(int positionId, int departmentId)
        {
            var position = await _repo.Position.GetByIdAsync(positionId);
            var department = await _repo.Department.GetByIdAsync(departmentId);

            return !(position == null || department == null);
        }
    }
}

