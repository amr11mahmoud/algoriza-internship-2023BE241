using System.Linq.Expressions;
using static Vezeeta.Core.Consts.AppConsts;

namespace Vezeeta.Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Add(T entity);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int page, int pageSize, string[]? includes);
        Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>>? match, 
            int page, 
            int pageSize, 
            Expression<Func<T, object>> orderBy, 
            string orderByDirection = OrderBy.Ascending);
        Task<T?> FindAsync(Expression<Func<T, bool>> match, string[]? includes = null);
        Task<int> CountAsync(Expression<Func<T, bool>> match);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
        Task<int> SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> match);
        T Update(T entity);
    }
}
