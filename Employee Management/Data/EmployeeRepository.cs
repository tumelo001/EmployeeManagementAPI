using Employee_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Data
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext){}

        public async Task<Employee> GetFullEmployeeDetailsByIdAsync(int id)
        {
            return await _appDbContext.Employees
                                        .Include(e => e.Position)
                                        .Include(e => e.Department)
                                        .FirstOrDefaultAsync(emp => emp.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetFullEmployeesDetailsAsync()
        {
           return await _appDbContext.Employees
                         .Include(p=>p.Position)
                         .Include(d=>d.Department).ToListAsync();   
        }
    }
}
