using Employee_Management.Models;

namespace Employee_Management.Data
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Task<IEnumerable<Employee>> GetFullEmployeesDetailsAsync();
        Task<Employee> GetFullEmployeeDetailsByIdAsync(int id);
    }
}
