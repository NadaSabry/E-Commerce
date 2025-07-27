using E_Commerce.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.EF.Repository
{
    public class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(T entity)
        {
            this.dbContext.Add(entity);
            this.dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.dbContext.Remove(entity);
            this.dbContext.SaveChanges();
        }

        public async Task<List<T>> GetAllAsync()
        {
             return await this.dbContext.Set<T>().ToListAsync();
        }
        // includes
        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async ValueTask<T?> GetByIdAsync(int id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);

        }
        public async Task<List<T>?> GetWhereIncludeAsync(Expression<Func<T, bool>> filter,params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(filter).ToListAsync();
        }

        public void Update(T entity)
        {
            this.dbContext.Update(entity);
            this.dbContext.SaveChanges();
        }
    }
}
