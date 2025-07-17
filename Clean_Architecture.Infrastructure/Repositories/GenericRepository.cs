using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clean_Architecture.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // check if entity exists by id
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.FindAsync(id) is not null;
        }

        // check any
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        // get info by id
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // create a new entity
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // delete by id
        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
            }
        }

        // update by id
        public async Task UpdateByIdAsync(int id, T updatedEntity)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity is not null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            }
        }
    }
}
