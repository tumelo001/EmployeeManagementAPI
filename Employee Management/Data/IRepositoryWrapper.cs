namespace Employee_Management.Data
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employee { get; }
        IPositionRepository Position { get; }
        IDepartmentRepository Department { get; }
        Task SaveChangesAsync();
    }
}
