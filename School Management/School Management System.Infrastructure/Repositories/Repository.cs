using Microsoft.EntityFrameworkCore;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;

namespace School_Management_System.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SchoolContext _context;

        public Repository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        // ✓ FIX: SaveChangesAsync was completely missing — nothing was ever saved to the DB
        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
