using Microsoft.EntityFrameworkCore;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Persistence.Context;

namespace Ripley.Api.Provider.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ProviderDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public GenericRepository(ProviderDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }

            return false;
        }
    }
}
