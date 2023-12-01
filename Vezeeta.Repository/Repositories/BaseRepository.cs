using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Base;
using Vezeeta.Core.Repository;

namespace Vezeeta.Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public bool Delete(string id)
        {
            T? entity = _context.Set<T>().Find(id);

            if (entity == null) return false;

            _context.Set<T>().Remove(entity);

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);

            if (entity == null) return false;

            _context.Set<T>().Remove(entity);

            return true;
        }

        //public IEnumerable<T> GetAll()
        //{
        //    IEnumerable<T> entities = await _context.Set<T>().Where();
        //}

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
