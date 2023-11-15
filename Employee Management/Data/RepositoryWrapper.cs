using Employee_Management.Models;

namespace Employee_Management.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _appDbContext;
        private IEmployeeRepository _employee;
        private IPositionRepository _position;
        private IDepartmentRepository _department;
        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee is null)
                    _employee = new EmployeeRepository(_appDbContext);
                return _employee;
            }
        }
        public IPositionRepository Position
        {
            get
            {
                if (_position is null)
                    _position = new PositionRepository(_appDbContext);
                return _position;
            }
        }
        public IDepartmentRepository Department
        {
            get
            {
                if (_department is null)
                    _department = new DepartmentRepository(_appDbContext);
                return _department;
            }
        }
        public async Task SaveChangesAsync()
        {
           await _appDbContext.SaveChangesAsync();
        }
    }
}
