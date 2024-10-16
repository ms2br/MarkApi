using MarkAPI.CORE.Entities.Common;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Services.Interfaces
{
    public interface IGenericService<TEntity, TDto>
        where TEntity : BaseEntity
        where TDto : class
    {
        Task CreateAsync(TDto data);

        Task<IEnumerable<T>> GetAllAsync<T>(params string[] includes)
            where T : class;
        
        Task<T> GetByIdAsync<T>(string? id, params string[] includes) where T : class;
        
        Task RemoveAsync(string? id);
        
        Task SoftRemoveAsync(string? id);

        Task RemoveAllAsync(params string[] includes);

        protected Task<TEntity> CheckIdAsync(string? id, bool noTracking = true, params string[] includes);
    }
}
