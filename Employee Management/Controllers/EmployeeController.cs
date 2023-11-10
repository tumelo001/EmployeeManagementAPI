using Employee_Management.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Employee_Management.Dtos;
using Employee_Management.Models;

namespace Employee_Management.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public EmployeeController(IRepositoryWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _repo.Employee.GetFullEmployeesDetailsAsync();
            if(employees == null) { return BadRequest(); }
            return Ok(employees.Select(_mapper.Map<EmployeeGetDto>));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeById(int Id) 
        {
            var employee = await _repo.Employee.GetFullEmployeeDetailsByIdAsync(Id);
            if(employee == null) { return BadRequest(); }
            return Ok(_mapper.Map<EmployeeGetDto>(employee));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeCreateDto employee) 
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var position = await _repo.Position.GetByIdAsync(employee.PositionId);
            var department = await _repo.Department.GetByIdAsync(employee.DepartmentId);

            if (position == null || department == null)
                return BadRequest("Invalid PositionId or DepartmentId"); 
            try
            {
                var employeeMap = _mapper.Map<Employee>(employee);
                await _repo.Employee.CreateAsync(employeeMap);
                _repo.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong. Please try again later.");
            }
           return Ok("Employee added successfully.");
        }
    }
}
