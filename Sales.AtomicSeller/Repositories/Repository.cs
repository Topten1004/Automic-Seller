using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sales.AtomicSeller.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public readonly ApplicationDbContext Context;
        public Repository(ApplicationDbContext context)
        {
            this.Context = context;
        }

        #region Public Methods

        public virtual async ValueTask<T> Get(object id)
        {
            return await Context.Set<T>().FindAsync(id);
        }
        public virtual async ValueTask<T> GetSingle()
        {
            return await Context.Set<T>().FirstOrDefaultAsync();
        }
        public virtual async ValueTask<T> FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] Includes)
        {
            //return await Context.Set<T>().Where(predicate).ToListAsync();
            var set = Context.Set<T>().AsQueryable();
            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    set = set.Include(include);
                }
            }

            return await set.FirstOrDefaultAsync(predicate);
        }
        //public virtual async ValueTask<T> Get(object id, params Expression<Func<T, object>>[] Includes)
        //{
        //    return await Context.Set<T>().FindAsync(id);

        //    //var set = Context.Set<T>().AsQueryable();
        //    //if (Includes != null)
        //    //{
        //    //    foreach (var include in Includes)
        //    //    {
        //    //        set = set.Include(include);
        //    //    }
        //    //}

        //    //return await set.Find(i => i.Id == id);
        //}

        public virtual async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public virtual async Task Update(T entity, params Expression<Func<T, dynamic>>[] excludeProperties)
        {
            // In case AsNoTracking is used
            Context.Entry(entity).State = EntityState.Modified;
            //Context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
            //Context.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
            foreach (var item in excludeProperties)
            {
                Context.Entry(entity).Property(item).IsModified = false;
            }
        }
        public virtual async Task Remove(object id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            Context.Set<T>().Remove(entity);
        }
        public virtual async Task Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] Includes)
        {
            //return await Context.Set<T>().Where(predicate).ToListAsync();
            var set = Context.Set<T>().AsQueryable();
            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    set = set.Include(include);
                }
            }

            return await set.Where(predicate).ToListAsync();
        }

        public virtual async Task<int> Count()
        {
            return await Context.Set<T>().CountAsync();
        }

        public virtual async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().CountAsync(predicate);
        }

        public virtual async Task<int> SaveChange()
        {
            return await Context.SaveChangesAsync();
        }

        #endregion
    }
}
