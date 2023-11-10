using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Employee_Management.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;

        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task CreateAsync(T entity)
        {
            await _appDbContext.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _appDbContext.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _appDbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _appDbContext.Update(entity);    
        }
    }
}
