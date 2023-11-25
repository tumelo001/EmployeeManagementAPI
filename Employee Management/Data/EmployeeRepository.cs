using Employee_Management.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Employee_Management.Data
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<Employee> GetFullEmployeeDetailsByIdAsync(int id)
        {
            return await _appDbContext.Employees
                                        .Include(e => e.Position)
                                        .Include(e => e.Department)
                                        .FirstOrDefaultAsync(emp => emp.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetFullEmployeesDetailsAsync(int page, int take, string position, string department)
        {
            if (department != null || position != null)
            {
                IEnumerable<Employee> employees = await _appDbContext.Employees
                         .Include(p => p.Position)
                         .Include(d => d.Department)
                         .ToListAsync();

                if (department != null)
                    employees = employees.Where(e => String.Compare(department, e.Department.Name, StringComparison.OrdinalIgnoreCase) == 0);
                if (position != null)
                    employees = employees.Where(e => String.Compare(position, e.Position.Name, StringComparison.OrdinalIgnoreCase) == 0);

                return employees
                          .Skip((page - 1) * take)
                          .Take(take);
            }
            
            return await _appDbContext.Employees
                          .Skip((page - 1) * take)
                          .Take(take)
                          .Include(p => p.Position)
                          .Include(d => d.Department).ToListAsync();
        }
    }
}
