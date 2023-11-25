using Employee_Management.Models;

namespace Employee_Management.Data
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Task<IEnumerable<Employee>> GetFullEmployeesDetailsAsync(int page, int take, string position, string department);
        Task<Employee> GetFullEmployeeDetailsByIdAsync(int id);
    }
}
