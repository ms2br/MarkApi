using MarkAPI.Bussines.Dtos.RedisDtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.ExternalContext.Interfaces
{
    public interface IRedisContext
    {
        public IDatabase Database { get; set; }
        Task GetDatabaseAsync(RedisOption redisOption, int dbNumber = 0);
    }
}
