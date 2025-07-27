using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        ValueTask<T?> GetByIdAsync(int id);
        Task<List<T>?> GetWhereIncludeAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        //Task<T?> GetThenInclude(Expression<Func<T, object>> include, Expression<Func<T, object>> thenInclude);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
