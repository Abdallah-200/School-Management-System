using Microsoft.EntityFrameworkCore;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SchoolContext schoolContext;

        public Repository(SchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }
        public async Task AddAsync(T entity)
        {
            await schoolContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
             schoolContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await schoolContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await schoolContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
             schoolContext.Set<T>().Update(entity);
        }
    }
}
