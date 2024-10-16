using MarkAPI.CORE.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Repositories.Interfaces
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        Task CreateAsync(T data);
        
        Task<IQueryable<T>> GetAllAsync(string userId, bool noTracking = true,params string[] includes);
        
        Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, bool noTracking = true, params string[] includes);
        
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        
        Task SaveChangesAsync();

        Task RemoveAsync(T data);

        Task RemoveAllAsync(string userId,params string[] includes);
    }
}
