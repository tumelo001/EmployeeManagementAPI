using System.Linq.Expressions;

namespace Employee_Management.Data
{
    public interface IRepositoryBase<T> where T : class
    {
        Task CreateAsync(T entity);
        void Update(T entity); 
        void Delete(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync(); 
    }
}
