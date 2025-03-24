using System.Linq.Expressions;

namespace Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> RemoveBoolAsync(T entity);
        Task<bool> UpdateBoolAsync(T entity); 
        Task<int> SaveChangesAsync();
        Task UpdateRangeAsync(IEnumerable<T> entities);
    }
}
