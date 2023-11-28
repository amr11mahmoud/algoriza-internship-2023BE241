using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(string id);

        Task<bool> AddAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
