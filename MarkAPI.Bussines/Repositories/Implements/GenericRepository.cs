using MarkAPI.Bussines.Repositories.Interfaces;
using MarkAPI.CORE.Entities.Common;
using MarkAPI.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Repositories.Implements
{
    public class GenericRepository<T> :
        IGenericRepository<T> where T : BaseEntity
    {
        protected DbSet<T> Table => _db.Set<T>();
        
        protected MarkDbContext _db { get; }

        public GenericRepository(MarkDbContext db)
        {

            _db = db;
        }
       
        public async Task CreateAsync(T data)
        => await Table.AddAsync(data);

        public async Task<IQueryable<T>> GetAllAsync(string userId, bool noTracking = true, params string[] includes)
        {
            IQueryable<T> items = MultipleIncludesAsync(Table.AsQueryable(), includes).Result.Where(x => x.UserId == userId);
            return noTracking ? items.AsNoTracking() : items;
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> expression ,bool noTracking = true, params string[] includes)
        {
            IQueryable<T> query = await MultipleIncludesAsync(Table.AsQueryable(), includes);
            query = noTracking ? query.AsNoTracking() : query;
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
            => await Table.AnyAsync(expression);

        public async Task SaveChangesAsync()
            => await _db.SaveChangesAsync();

        public async Task RemoveAsync(T data)
        => Table.Remove(data);

        public async Task RemoveAllAsync(string userID,params string[] includes)
        {
            var query = await MultipleIncludesAsync(Table.Where(x => x.UserId == userID).AsQueryable(),includes);

            await query.ExecuteDeleteAsync();
        }

        async Task<IQueryable<T>> MultipleIncludesAsync(IQueryable<T> query, params string[] includes)
        {
            if (includes.Length > 0 && includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            return query;
        }        
    }
}
