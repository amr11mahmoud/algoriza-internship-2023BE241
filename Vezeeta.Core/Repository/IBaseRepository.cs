using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);
        bool Delete(string id);
        Task<bool> DeleteAsync(string id);
        Task<int> SaveChangesAsync();
    }
}
