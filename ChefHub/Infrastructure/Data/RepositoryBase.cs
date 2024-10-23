using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext _context;
        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();

        }
        public virtual async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task<T?> GetByIdAsync<TId>(TId id) where TId : notnull
        {
            return await _context.Set<T>().FindAsync(new object[] { id });
        }
        public virtual async Task<bool> EntityExistsAsync<TId>(TId id) where TId : notnull
        {
            var entity = await _context.Set<T>().FindAsync(new object[] { id });
            return entity != null;
        }
    }
}
