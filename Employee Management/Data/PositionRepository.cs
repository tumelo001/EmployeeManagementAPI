using Employee_Management.Models;

namespace Employee_Management.Data
{
    public class PositionRepository : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
