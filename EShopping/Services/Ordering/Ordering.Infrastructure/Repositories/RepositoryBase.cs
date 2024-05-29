using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        private readonly OrderContext orderContext;

        public RepositoryBase(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await orderContext.Set<T>().ToListAsync();
           
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
           return await orderContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await orderContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
           await orderContext.Set<T>().AddAsync(entity);
            await orderContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            orderContext.Entry(entity).State = EntityState.Modified;
            await orderContext.SaveChangesAsync();
            return entity;
           
        }

        public async Task<T> DeleteAsync(T entity)
        {
            orderContext.Set<T>().Remove(entity);
            await orderContext.SaveChangesAsync();
            return entity;
           
        }

      
    }
}
