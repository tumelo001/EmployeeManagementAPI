using Employee_Management.Models;

namespace Employee_Management.Data
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
