using Addresses.DataContext.EntityFramework;
using Addresses.Services.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;

namespace Addresses.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AddressesDbContext _context;
        protected DbSet<T> _entities;

        public Repository(AddressesDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _entities.FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found.");
            }

            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            if (entity == null)
                throw new Exception($"Entity with id {id} not found.");

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
