using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sales.AtomicSeller.Repositories
{
    public interface IRepository<T>
    {
        ValueTask<T> Get(object id);
        ValueTask<T> GetSingle();
        ValueTask<T> FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] Includes);
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] Includes);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Update(T entity, params Expression<Func<T, dynamic>>[] excludeProperties);
        Task Remove(object id);
        Task Remove(T entity);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<int> SaveChange();
    }
}
